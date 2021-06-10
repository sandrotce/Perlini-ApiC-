using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Single.Interfaces;
using Totvs.Sample.Shop.Application.Services.Interfaces;

namespace Totvs.Sample.Shop.Application.Single.Services
{
    public abstract class SingleAppService<Dto> : ApplicationService, IBaseSingleInterface<Dto>
    {
        protected readonly string dataType;
        private readonly IGenericAppService<Dto> genericAppService;
        public SingleAppService(
            INotificationHandler notificationHandler,
            IGenericAppService<Dto> genericAppService,
            string dataType) : base(notificationHandler)
        {
            this.genericAppService = genericAppService;
            this.dataType = dataType;
        }

        public abstract Task<(int httpStatus, dynamic businessObj)> Upsert(Dto dto);
    }
}