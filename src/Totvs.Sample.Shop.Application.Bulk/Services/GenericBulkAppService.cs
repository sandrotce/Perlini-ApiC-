using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Notifications;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Dto.BulkResponse;

public delegate Task < (int httpStatus, dynamic businessObj) > UpsertCommand<SomeDto> (SomeDto dto);

namespace Totvs.Sample.Shop.Application.Bulk.Services
{
    public delegate SomeDto DtoConversor<SomeDto, StandardMessageDto> (StandardMessageDto standardMessageDto);
    public class GenericBulkAppService<StandardMessageDto, SomeDto> : ApplicationService, IGenericBulkAppService<StandardMessageDto, SomeDto>
    {
        private readonly INotificationHandler notificationHandler;
        public GenericBulkAppService (INotificationHandler notificationHandler) : base (notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public async Task <(int httpStatus, List<BulkResponseItemDto> bulkResponseList)> UpsertBulk(
            List<StandardMessageDto> businessObjList,
            string endpointDomain, DtoConversor<SomeDto, StandardMessageDto> dtoConvertFunction,
            UpsertCommand<SomeDto> upsertSomething)
        {
            if (businessObjList == null)
            {
                notificationHandler.DefaultBuilder
                    .AsSpecification()
                    .WithMessage (Domain.Constants.LocalizationSourceName, Domain.GlobalizationKey.InvalidDTOListStructureGenericError)
                    .Raise ();

                return (400, null);
            }

            if (businessObjList.Count == 0)
            {
                notificationHandler.DefaultBuilder
                    .AsSpecification()
                    .WithMessage (Domain.Constants.LocalizationSourceName, Domain.GlobalizationKey.InvalidListBulkItems)
                    .Raise ();

                return (400, null);
            }

            var (httpStatus, bulkResponseList) = await RunThroughBusinessObjList(
                businessObjList, endpointDomain, dtoConvertFunction, upsertSomething);

            return (httpStatus, bulkResponseList);
        }

        private async Task < (int httpStatus, List<BulkResponseItemDto> bulkResponseList) > RunThroughBusinessObjList (
            List<StandardMessageDto> businessObjList,
            string endpointDomain, DtoConversor<SomeDto, StandardMessageDto> dtoConvertFunction,
            UpsertCommand<SomeDto> upsertSomething
        )
        {
            List<BulkResponseItemDto> bulkResponseList = new List<BulkResponseItemDto> ();
            int generalHttpStatus = 0;

            foreach (StandardMessageDto standardMessageDto in businessObjList)
            {
                if (Notification.HasNotification ())
                    break;

                SomeDto dto = dtoConvertFunction (standardMessageDto);
                BulkResponseItemDto bulkResponseItem = new BulkResponseItemDto ();
                var responseItem = await upsertSomething (dto);

                if (standardMessageDto.Equals (businessObjList.First ()))
                    generalHttpStatus = responseItem.httpStatus;

                if (Notification.HasNotification ())
                {
                    WriteErrorResponse (bulkResponseList, bulkResponseItem, responseItem);
                    generalHttpStatus = SetGeneneralHttpStatus (generalHttpStatus, responseItem);
                    break;
                }
                else
                {                    
                    WriteSuccessResponse (endpointDomain, bulkResponseList, bulkResponseItem, responseItem);
                    generalHttpStatus = SetGeneneralHttpStatus (generalHttpStatus, responseItem);
                }
            }
            return (generalHttpStatus, bulkResponseList);
        }

        private static int SetGeneneralHttpStatus (int generalHttpStatus, (int httpStatus, dynamic businessObj) responseItem)
        {
            generalHttpStatus = (generalHttpStatus == responseItem.httpStatus && generalHttpStatus != 207) ? generalHttpStatus : 207;
            return generalHttpStatus;
        }

        private static void WriteSuccessResponse (string endpointDomain, List<BulkResponseItemDto> bulkResponseList, BulkResponseItemDto bulkResponseItem, (int httpStatus, dynamic businessObj) responseItem)
        {
            bulkResponseItem.status = responseItem.httpStatus;
            bulkResponseItem.href = "/" + endpointDomain + "/" + responseItem.businessObj.Id;
            bulkResponseList.Add (bulkResponseItem);
        }

        private void WriteErrorResponse (List<BulkResponseItemDto> bulkResponseList, BulkResponseItemDto bulkResponseItem, (int httpStatus, dynamic businessObj) responseItem)
        {
            var errorNotification = Notification.GetAll ().First (); //TODO: Tratar cenários com mais de uma notificação?
            bulkResponseItem.status = responseItem.httpStatus == 0 ? 500 : responseItem.httpStatus;
            bulkResponseItem.code = errorNotification.Code;
            bulkResponseItem.message = errorNotification.Message;
            bulkResponseItem.detailedMessage = errorNotification.DetailedMessage;
            Notification.Delete (errorNotification); //É preciso deletar a notificação para que a mesma não seja capturada no filtro, alterando a resposta que foi criada
            bulkResponseList.Add (bulkResponseItem);
        }
    }
}