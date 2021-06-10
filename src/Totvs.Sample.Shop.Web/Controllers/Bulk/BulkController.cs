using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Tnf.Application.Services;
using Tnf.AspNetCore.Mvc.Response;
using Totvs.Sample.Shop.Application.Bulk.Interfaces;
using Totvs.Sample.Shop.Dto.BulkResponse;

namespace Totvs.Sample.Shop.Web.Controllers.Bulk
{
    public abstract class BulkController<StandardMessageDto> : TnfController
    {
        private readonly IBaseBulkInterface<StandardMessageDto> appService;
        public BulkController(IBaseBulkInterface<StandardMessageDto> appService)
        {
            this.appService = appService;
        }
    }
}