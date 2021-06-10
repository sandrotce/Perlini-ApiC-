﻿using System;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Dto
{
    public class DefaultRequestDto : RequestDto, IDefaultRequestDto
    {
        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(Guid id, RequestDto request)
        {
            Id = id;
            Fields = request.Fields;
            Expand = request.Expand;
        }

        public DefaultRequestDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
