using AutoMapper;
using MiniMarket_API.Application.DTOs.Requests;
using MiniMarket_API.Application.ViewModels;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            #region ViewModels
            CreateMap<ProductView, Product>().ReverseMap();
            CreateMap<ProductBasicView, Product>().ReverseMap();

            CreateMap<ProductImageView, ProductImage>().ReverseMap();

            CreateMap<ProductImageBasicView, ProductImage>().ReverseMap();

            CreateMap<CategoryView, ProductCategory>().ReverseMap();
            CreateMap<CategoryViewProducts, ProductCategory>().ReverseMap();

            CreateMap<OrderDetailsView, OrderDetails>().ReverseMap();

            CreateMap<SaleOrder, SaleOrderView>()
                .ForMember(d => d.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<SaleOrder, SaleOrderViewDetails>()
                .ForMember(d => d.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CompanyCodeView, CompanyCode>().ReverseMap();

            CreateMap<UserView, User>().ReverseMap();

            CreateMap<UserViewProfile, User>().ReverseMap();

            CreateMap<SellerView, User>().ReverseMap();

            CreateMap<DeliveryAddressView, DeliveryAddress>().ReverseMap();
            #endregion

            #region Request DTOs
            CreateMap<AddCategoryDto, ProductCategory>().ReverseMap();

            CreateMap<AddProductDto, Product>().ReverseMap();

            CreateMap<AddCompanyCodeDto, CompanyCode>().ReverseMap();

            CreateMap<CreateSellerDto, Seller>()
                .ForSourceMember(c => c.Password, opt => opt.DoNotValidate());

            CreateMap<CreateUserDto, Customer>()
                .ForSourceMember(c => c.Password, opt => opt.DoNotValidate());

            CreateMap<CreateUserDto, SuperAdmin>()
                .ForSourceMember(c => c.Password, opt => opt.DoNotValidate());

            CreateMap<UpdateUserDto, Customer>().ReverseMap();

            CreateMap<CreateOrderDto, SaleOrder>()
                .ForSourceMember(c => c.NewDetails, opt => opt.DoNotValidate());                    //Prevents mapping the incomplete details from the DTO

            CreateMap<CreateDetailDto, OrderDetails>();

            CreateMap<AddDeliveryAddressDto, DeliveryAddress>();
            #endregion

        }
    }
}
