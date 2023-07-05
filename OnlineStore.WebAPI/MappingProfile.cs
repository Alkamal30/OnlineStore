using AutoMapper;

namespace OnlineStore.WebAPI;

public class MappingProfile : Profile {

	public MappingProfile() {
		CreateMap<OnlineStore.WebAPI.Models.UserModel, OnlineStore.Core.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserPostModel, OnlineStore.Core.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserRequests, OnlineStore.Core.Models.UserRequests>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserAuthorizationModel, OnlineStore.Core.Models.UserAuthorizationModel>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserRegistrationModel, OnlineStore.Core.Models.UserRegistrationModel>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserAuthorizationResponseModel, OnlineStore.Core.Models.UserAuthorizationResponseModel>().ReverseMap();

		CreateMap<OnlineStore.WebAPI.Models.ProductModel, OnlineStore.Core.Models.Product>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.ProductPostModel, OnlineStore.Core.Models.Product>().ReverseMap();

		CreateMap<OnlineStore.Core.Models.Order, OnlineStore.WebAPI.Models.OrderModel>().ReverseMap();

		CreateMap<OnlineStore.WebAPI.Models.OrderPostModel, OnlineStore.Core.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderCheckoutModel, OnlineStore.Core.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderItemCheckoutModel, OnlineStore.Core.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderItemModel, OnlineStore.Core.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserOrderModel, OnlineStore.Core.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserPostOrderModel, OnlineStore.Core.Models.User>().ReverseMap();


		CreateMap<OnlineStore.Core.Models.User, OnlineStore.Infrastructure.Models.User>().ReverseMap();
		CreateMap<OnlineStore.Core.Models.Product, OnlineStore.Infrastructure.Models.Product>().ReverseMap();
		CreateMap<OnlineStore.Core.Models.Order, OnlineStore.Infrastructure.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.Core.Models.OrderItem, OnlineStore.Infrastructure.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.Core.Models.UserRequests, OnlineStore.Infrastructure.Models.UserRequests>().ReverseMap();
	}
}
