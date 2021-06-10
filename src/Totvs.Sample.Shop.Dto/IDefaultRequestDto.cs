using System;
using Tnf.Dto;

namespace Totvs.Sample.Shop.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
