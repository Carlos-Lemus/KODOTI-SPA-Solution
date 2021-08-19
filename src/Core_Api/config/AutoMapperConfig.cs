using AutoMapper;
using Model;
using Model.DTOs;
using Model.Indentity;
using Service.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Api.config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientDTO>();
            CreateMap<DataCollection<Client>, DataCollection<ClientDTO>>();
 
            CreateMap<Product, ProductDTO>();
            CreateMap<DataCollection<Product>, DataCollection<ProductDTO>>();

            CreateMap<Order, OrderDTO>();
            CreateMap<DataCollection<Order>, DataCollection<OrderDTO>>();

            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<DataCollection<Order>, DataCollection<OrderDTO>>();

            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(
                    dest => dest.FullName,
                    opts => opts.MapFrom(src => src.UserName )
                ).ForMember(
                    dest => dest.Roles,
                    opts => opts.MapFrom(src => src.UserRoles.Select(y => y.Role.Name).ToList())
                );
            CreateMap<DataCollection<ApplicationUser>, DataCollection<ApplicationUserDTO>>();

            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderDetailCreateDTO, OrderDetail>();


        }
    }
}
