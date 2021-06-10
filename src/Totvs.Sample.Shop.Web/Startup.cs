using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Totvs.Sample.Shop.Application;
using Totvs.Sample.Shop.Application.Single;
using Totvs.Sample.Shop.Application.Bulk;
using Totvs.Sample.Shop.Domain;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Dto;
using Totvs.Sample.Shop.Infra;
using Totvs.Sample.Shop.Infra.SqLite;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using Tnf.Configuration;
using System.Transactions;

namespace Totvs.Sample.Shop.Web
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddApplicationServiceDependency()
                .AddSingleApplicationServiceDependency()
                .AddBulkApplicationServiceDependency();

            if (DatabaseConfiguration.DatabaseType == DatabaseType.Sqlite)
                services.AddSqLiteDependency();
            else
                throw new NotSupportedException("No database configuration found");

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "TOTVS Shop API", Version = "v1" });

                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Totvs.Sample.Shop.Web.xml"));
                });

            services.AddTnfAspNetCore();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseTnfAspNetCore(options =>
            {
                // Adiciona as configurações de localização da aplicação
                options.UseDomainLocalization();

                // Configuração global de como irá funcionar o Get utilizando o repositorio do Tnf
                // O exemplo abaixo registra esse comportamento através de uma convenção:
                // toda classe que implementar essas interfaces irão ter essa configuração definida
                // quando for consultado um método que receba a interface IRequestDto do Tnf
                options.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity =>
                        entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });

                // Configura a connection string da aplicação
                options.DefaultNameOrConnectionString = DatabaseConfiguration.ConnectionString;

                // ---------- Configurações de Unit of Work a nível de aplicação

                // Altera o default isolation level para Unspecified (SqlLite não trabalha com isolationLevel)
                //options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.Unspecified;

                // Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
                // comitadas ou desfeitas em caso de erro
                options.UnitOfWorkOptions().IsTransactional = true;

                // IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

                // Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
                options.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

                // Timeout que será aplicado (se este valor for informado) para toda nova transação criada
                // Não é indicado informar este valor pois irá afetar toda a aplicação.
                //options.UnitOfWorkOptions().Timeout = TimeSpan.FromSeconds(5);

                // ----------
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "TOTVS SHOP API v1");
            });

            app.UseMvcWithDefaultRoute();
            app.UseResponseCompression();
            app.ApplicationServices.MigrateDatabase();
        }
    }
}
