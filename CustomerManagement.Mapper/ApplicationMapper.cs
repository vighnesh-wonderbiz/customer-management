using AutoMapper;
using CustomerManagement.DTO;
using CustomerManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<RoleDTO, UpdateRoleDTO>().ReverseMap();
            CreateMap<Role, UpdateRoleDTO>().ReverseMap();

            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<GenderDTO, UpdateGenderDTO>().ReverseMap();
            CreateMap<Gender, UpdateGenderDTO>().ReverseMap();
        }
    }
}
