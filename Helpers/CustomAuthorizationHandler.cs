using System.Net;
using System.Security.Claims;
using E_Commerce_Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomAuthorizationHandler> _logger;


        public CustomAuthorizationHandler(

        IUserRepository userRepository,

         IUserAddressRepository userAddressRepository,

         IUserPaymentRepository userPaymentRepository,

         IShoppingSessionRepository userShoppingSessionRepository,
         ICartItemRepository cartItemRepository,

         IOrderDetailRepository orderDetailRepository,

         IOrderItemRepository orderItemRepository,

         IPaymentDetailsRepository paymentDetailsRepository,
         IHttpContextAccessor httpContextAccessor,
         ILogger<CustomAuthorizationHandler> logger


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
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizationRequirement requirement)
        {
            var role = context.User.FindFirstValue(ClaimTypes.Role);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(role);

            _logger.LogInformation(role);

            if (role == "Admin" || role == "Manage") {
                context.Succeed(requirement);
                return;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resource = _httpContextAccessor.HttpContext.GetRouteData().Values["controller"].ToString();
            var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;
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
                case "PasswordReset":
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
                    if (!routeValues.ContainsKey("userId") || routeValues["userId"]?.ToString() != userId)
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
                    Console.WriteLine();
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




