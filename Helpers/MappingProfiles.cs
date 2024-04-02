using AutoMapper;
using E_Commerce_Api.Dto;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.helpers {
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<UserPayment, UserPaymentDto>().ReverseMap();
            CreateMap<UserAddress, UserAddressDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailsDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemsDto>().ReverseMap();
            CreateMap<ShoppingSession, ShoppingSessionDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<PaymentDetail, PaymentDetailsDto>().ReverseMap();
            CreateMap<OrderItem,CreateOrderItemsDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<PasswordResetToken, PasswordResetTokenDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();

        }
    }
}
