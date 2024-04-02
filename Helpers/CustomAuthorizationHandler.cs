using System.Net;
using System.Security.Claims;
using E_Commerce_Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Api.helpers
{

    public class CustomAuthorizationHandler : AuthorizationHandler<UserAuthorizationRequirement>
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserAddressRepository _userAddressRepository;

        private readonly IUserPaymentRepository _userPaymentRepository;

        private readonly IShoppingSessionRepository _shoppingSessionRepository;
        private readonly ICartItemRepository _cartItemRepository;

        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IOrderItemRepository _orderItemRepository;

        private readonly IPaymentDetailsRepository _paymentDetailsRepository;

        public CustomAuthorizationHandler(
        DbContext context,

        IUserRepository userRepository,

         IUserAddressRepository userAddressRepository,

         IUserPaymentRepository userPaymentRepository,

         IShoppingSessionRepository userShoppingSessionRepository,
         ICartItemRepository cartItemRepository,

         IOrderDetailRepository orderDetailRepository,

         IOrderItemRepository orderItemRepository,

         IPaymentDetailsRepository paymentDetailsRepository

        )
        {
            _userRepository = userRepository;
            _orderDetailRepository = orderDetailRepository;
            _orderItemRepository = orderItemRepository;
            _cartItemRepository = cartItemRepository;
            _userAddressRepository = userAddressRepository;
            _userPaymentRepository = userPaymentRepository;
            _shoppingSessionRepository = userShoppingSessionRepository;
            _paymentDetailsRepository = paymentDetailsRepository;

        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizationRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var path = context.Resource as string;
            var pathSegments = path?.Split('/');
            var resource = pathSegments?.Length > 3 ? pathSegments[3] : null;


            var routeValues = context.Resource as Dictionary<string, object>;
            if (routeValues == null)
            {
                context.Fail();
                return;
            }

            switch (resource)
            {
                case "CartItem":
                    if (!routeValues.ContainsKey("cartItemId") || !_cartItemRepository.IsCartOwner(int.Parse(routeValues["cartItemId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "OrderDetail":
                    if (!routeValues.ContainsKey("orderDetailId") || !_orderDetailRepository.IsOrderDetailOwner(int.Parse(routeValues["orderDetailId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "OrderItem":
                    if (!routeValues.ContainsKey("orderItemId") || !_orderItemRepository.IsOrderItemOwner(int.Parse(routeValues["orderItemId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "PasswordResetToken":
                    if (!routeValues.ContainsKey("passwordResetTokenId") || !_paymentDetailsRepository.IsPaymentDetailOwner(int.Parse(routeValues["passwordResetTokenId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "PaymentDetails":
                    if (!routeValues.ContainsKey("paymentDetailsId") || !_paymentDetailsRepository.IsPaymentDetailOwner(int.Parse(routeValues["paymentDetailsId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "ShoppingSession":
                    if (!routeValues.ContainsKey("shoppingSessionId") || !_shoppingSessionRepository.IsShoppingSessionOwner(int.Parse(routeValues["shoppingSessionId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "User":
                    if (!routeValues.ContainsKey("userId") || !_userRepository.IsUserOwner(int.Parse(routeValues["userId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "UserAddress":
                    if (!routeValues.ContainsKey("userAddressId") || !_userAddressRepository.IsUserAddressOwner(int.Parse(routeValues["userAddressId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                case "UserPayment":
                    if (!routeValues.ContainsKey("userPaymentId") || !_userPaymentRepository.IsUserPaymentOwner(int.Parse(routeValues["userPaymentId"]?.ToString()), int.Parse(userId)))
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    break;
                default:
                    break;
            }
        }

    }


    public class UserAuthorizationRequirement : IAuthorizationRequirement

    {
    }
}




