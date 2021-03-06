using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Mozlite.Extensions.Security.Permissions
{
    /// <summary>
    /// 权限验证特性。
    /// </summary>
    public class PermissionAuthorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 初始化类<see cref="PermissionAuthorizeAttribute"/>。
        /// </summary>
        /// <param name="permission">当前权限名称。</param>
        public PermissionAuthorizeAttribute(string permission) : base(typeof(PermissionAuthorizeAttributeImpl))
        {
            Arguments = new object[] { new OperationAuthorizationRequirement { Name = permission } };
        }

        /// <summary>
        /// 初始化类<see cref="PermissionAuthorizeAttribute"/>。
        /// </summary>
        public PermissionAuthorizeAttribute() : base(typeof(PermissionAuthorizeAttributeImpl))
        {
        }

        private class PermissionAuthorizeAttributeImpl : Attribute, IAsyncAuthorizationFilter
        {
            private readonly ILogger _logger;
            private readonly IAuthorizationService _authorizationService;
            private readonly OperationAuthorizationRequirement _requirement;

            public PermissionAuthorizeAttributeImpl(ILogger<PermissionAuthorizeAttribute> logger, IAuthorizationService authorizationService, OperationAuthorizationRequirement requirement)
            {
                _logger = logger;
                _authorizationService = authorizationService;
                _requirement = requirement;
            }

            public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User,
                     context.ActionDescriptor, _requirement);
                if (!result.Succeeded)
                    context.Result = new ChallengeResult();
            }
        }
    }
}