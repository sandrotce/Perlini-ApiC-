using Totvs.Sample.Shop.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Totvs.Sample.Shop.Web.Tests.ProductByTestBase
{
    public class FakeCrudDbContext : CrudDbContext
    {
        public FakeCrudDbContext(DbContextOptions<CrudDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
