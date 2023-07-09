//using System.Reflection;
//using Carmax.Application.Common.Exceptions;
//using Carmax.Application.Common.Interfaces;
//using Carmax.Application.Common.Security;
//using Carmax.Domain.Enums;
//using MediatR;

//namespace Carmax.Application.Common.Behaviours;

//public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
//{
//    private readonly ICurrentUserService _currentUserService;
//    private readonly IUserService _userService;

//    public AuthorizationBehaviour(
//        ICurrentUserService currentUserService,
//        IUserService userService)
//    {
//        _currentUserService = currentUserService;
//        _userService = userService;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

//        if (authorizeAttributes.Any())
//        {
//            // Must be authenticated user
//            if (_currentUserService.UserId == null)
//            {
//                throw new UnauthorizedAccessException();
//            }

//            // Role-based authorization
//            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

//            if (authorizeAttributesWithRoles.Any())
//            {
//                var authorized = false;

//                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
//                {
//                    foreach (var role in roles)
//                    {
//                        var isInRole = await _userService.IsInRoleAsync(_currentUserService.UserId?.Value, UserRole.User);
//                        if (isInRole)
//                        {
//                            authorized = true;
//                            break;
//                        }
//                    }
//                }

//                // Must be a member of at least one role in roles
//                if (!authorized)
//                {
//                    throw new ForbiddenAccessException();
//                }
//            }

//            // Policy-based authorization
//            //var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
//            //if (authorizeAttributesWithPolicies.Any())
//            //{
//            //    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
//            //    {
//            //        var authorized = await _userService.AuthorizeAsync(_currentUserService.UserId, policy);

//            //        if (!authorized)
//            //        {
//            //            throw new ForbiddenAccessException();
//            //        }
//            //    }
//            //}
//        }

//        // User is authorized / authorization not required
//        return await next();
//    }
//}
