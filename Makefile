PROJECT_NAME ?= Totvs.Sample.Shop

.PHONY: mgSqlite

mgSqlite:	
	cd ./src/Totvs.Sample.Shop.Web && dotnet ef migrations add $(mname) --context Totvs.Sample.Shop.Infra.Sqlite.Context.SqliteCrudDbContext  --project ../Totvs.Sample.Shop.Infra.Sqlite

dbUpdate:
	cd ./src/Totvs.Sample.Shop.Web && dotnet ef database update --context Totvs.Sample.Shop.Infra.Sqlite.Context.SqliteCrudDbContext  --project ../Totvs.Sample.Shop.Infra.Sqlite