using Totvs.Sample.Shop.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Totvs.Sample.Shop.Infra.SqLite.Context
{
    public class SqliteCrudDbContext : CrudDbContext
    {

        public SqliteCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}