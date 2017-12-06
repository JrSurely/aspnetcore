﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Mozlite.Data;
using Mozlite.Extensions.Security.Models;
using Mozlite.Extensions.Security.Permissions;

namespace Mozlite.Extensions.Security.Services
{
    /// <summary>
    /// 权限管理类型。
    /// </summary>
    public class PermissionManager : PermissionManager<UserRole>
    {
        /// <summary>
        /// 初始化类<see cref="PermissionManager"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口实例。</param>
        /// <param name="pirs">数据库操作接口。</param>
        /// <param name="httpContextAccessor">当前HTTP上下文访问器。</param>
        /// <param name="cache">缓存接口。</param>
        public PermissionManager(IRepository<Permission> repository, IRepository<PermissionInRole> pirs, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
            : base(repository, pirs, httpContextAccessor, cache)
        {
        }
    }
}