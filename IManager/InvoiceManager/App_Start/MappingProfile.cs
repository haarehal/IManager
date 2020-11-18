using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using InvoiceManager.Dtos;
using InvoiceManager.Models;

namespace InvoiceManager.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Invoice, InvoiceDto>();
            Mapper.CreateMap<InvoiceDto, Invoice>().ForMember(i => i.Id, opt => opt.Ignore());

            Mapper.CreateMap<Item, ItemDto>();
            Mapper.CreateMap<ItemDto, Item>().ForMember(i => i.Id, opt => opt.Ignore());
        }
    }
}