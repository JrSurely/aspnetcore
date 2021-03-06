﻿using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozlite.Data.Migrations;
using Mozlite.Extensions.Security;
using Mozlite.Mvc.Routing;

namespace Mozlite.Mvc
{
    /// <summary>
    /// 服务扩展类。
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加MVC服务。
        /// </summary>
        /// <param name="services">当前服务集合。</param>
        /// <returns>返回MVC构建实例。</returns>
        public static IMvcBuilder AddMozliteMvc(this IServiceCollection services)
        {
            return services.AddMvc()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Clear();
                    options.AreaViewLocationFormats.Clear();
                    //网站试图路径：custom目录为后台修改网页代码后另存为的文件，有助于恢复原有文件
                    options.ViewLocationFormats.Add("/custom/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    options.ViewLocationFormats.Add("/custom/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                    options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                    //区域试图路径：Extensions下面的路径，可以将每个区域改为扩展，这样会降低程序集的耦合度
                    var assemblyName =
                        Assembly.GetEntryAssembly().GetName().Name; //网站的程序集名称，约定扩展程序集名称必须为“网站程序集名称.Extensions.当前扩展区域名称”
                    options.AreaViewLocationFormats.Add("/custom/Extensions/" + assemblyName +
                                                        ".Extensions.{2}/Views/{1}/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/custom/Extensions/" + assemblyName +
                                                        ".Extensions.{2}/Views/Shared/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/custom/Extensions/{2}/Views/{1}/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/custom/Extensions/{2}/Views/Shared/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/custom/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/Extensions/" + assemblyName +
                                                        ".Extensions.{2}/Views/{1}/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/Extensions/" + assemblyName +
                                                        ".Extensions.{2}/Views/Shared/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/Extensions/{2}/Views/{1}/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/Extensions/{2}/Views/Shared/{0}" +
                                                        RazorViewEngine.ViewExtension);
                    options.AreaViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                });
        }

        /// <summary>
        /// 添加默认Mvc。
        /// </summary>
        /// <param name="app">APP构建实例对象。</param>
        /// <param name="configuration">配置实例对象。</param>
        /// <returns>APP构建实例对象。</returns>
        public static IApplicationBuilder UseMozliteMvc(this IApplicationBuilder app, IConfiguration configuration)
        {
            //配置程序集
            var services = app.ApplicationServices.GetService<IEnumerable<IApplicationConfigurer>>();
            foreach (var service in services)
            {
                service.Configure(app, configuration);
            }
            //数据库迁移
            app.UseMigrations();
            //用户日志操作记录
            app.UseUserActivity();
            //MVC
            app.UseMvc(builder =>
                builder
                    .MapLowerCaseRoute("dashboard-area-default", RouteSettings.Dashboard + "/{area}/{controller:regex(^admin.*)=Admin}/{action=Index}/{id?}")
                    .MapLowerCaseRoute("dashboard-default", RouteSettings.Dashboard + "/{controller:regex(^admin.*)=Admin}/{action=Index}/{id?}")
                    .MapLowerCaseRoute("area-default", "{area:exists}/{controller}/{action=Index}/{id?}")
                    .MapLowerCaseRoute("default", "{controller=Home}/{action=Index}/{id?}"));
            return app;
        }
    }
}