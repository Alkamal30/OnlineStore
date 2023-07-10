using AutoMapper;

namespace OnlineStore.WebAPI;

public class MappingProfile : Profile {

	public MappingProfile() {
		CreateMap<OnlineStore.WebAPI.Models.UserModel, OnlineStore.Core.Abstractions.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserPostModel, OnlineStore.Core.Abstractions.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserRequests, OnlineStore.Core.Abstractions.Models.UserRequests>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserAuthorizationModel, OnlineStore.Core.Abstractions.Models.UserAuthorizationModel>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserRegistrationModel, OnlineStore.Core.Abstractions.Models.UserRegistrationModel>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserAuthorizationResponseModel, OnlineStore.Core.Abstractions.Models.UserAuthorizationResponseModel>().ReverseMap();

		CreateMap<OnlineStore.WebAPI.Models.ProductModel, OnlineStore.Core.Abstractions.Models.Product>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.ProductPostModel, OnlineStore.Core.Abstractions.Models.Product>().ReverseMap();

		CreateMap<OnlineStore.Core.Abstractions.Models.Order, OnlineStore.WebAPI.Models.OrderModel>().ReverseMap();

		CreateMap<OnlineStore.WebAPI.Models.OrderPostModel, OnlineStore.Core.Abstractions.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderCheckoutModel, OnlineStore.Core.Abstractions.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderItemCheckoutModel, OnlineStore.Core.Abstractions.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.OrderItemModel, OnlineStore.Core.Abstractions.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserOrderModel, OnlineStore.Core.Abstractions.Models.User>().ReverseMap();
		CreateMap<OnlineStore.WebAPI.Models.UserPostOrderModel, OnlineStore.Core.Abstractions.Models.User>().ReverseMap();


		CreateMap<OnlineStore.Core.Abstractions.Models.User, OnlineStore.Infrastructure.Models.User>().ReverseMap();
		CreateMap<OnlineStore.Core.Abstractions.Models.Product, OnlineStore.Infrastructure.Models.Product>().ReverseMap();
		CreateMap<OnlineStore.Core.Abstractions.Models.Order, OnlineStore.Infrastructure.Models.Order>().ReverseMap();
		CreateMap<OnlineStore.Core.Abstractions.Models.OrderItem, OnlineStore.Infrastructure.Models.OrderItem>().ReverseMap();
		CreateMap<OnlineStore.Core.Abstractions.Models.UserRequests, OnlineStore.Infrastructure.Models.UserRequests>().ReverseMap();
	}
}
