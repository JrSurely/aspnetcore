﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 后台服务管理接口。
    /// </summary>
    public interface ITaskManager : ISingletonService
    {
        /// <summary>
        /// 确保服务列表。
        /// </summary>
        /// <param name="services">当前程序包含的后台服务列表。</param>
        Task EnsuredTaskServicesAsync(IEnumerable<ITaskService> services);

        /// <summary>
        /// 加载所有后台服务。
        /// </summary>
        /// <returns>返回所有后台服务列表。</returns>
        Task<IEnumerable<TaskDescriptor>> LoadTasksAsync();

        /// <summary>
        /// 通过类型获取后台服务。
        /// </summary>
        /// <param name="type">任务<seealso cref="ITaskService"/>类型。</param>
        /// <returns>返回当前类型的服务对象。</returns>
        TaskDescriptor GeTask(Type type);

        /// <summary>
        /// 设置时间间隔。
        /// </summary>
        /// <param name="id">服务Id。</param>
        /// <param name="interval">时间间隔。</param>
        /// <returns>返回设置结果。</returns>
        bool SetInterval(int id, TaskInterval interval);

        /// <summary>
        /// 设置参数。
        /// </summary>
        /// <param name="id">当前服务Id。</param>
        /// <param name="argument">参数。</param>
        /// <returns>返回设置结果。</returns>
        Task<bool> SetArgumentAsync(int id, string argument);

        /// <summary>
        /// 设置执行时间。
        /// </summary>
        /// <param name="id">当前服务Id。</param>
        /// <param name="next">下一次执行时间。</param>
        /// <param name="last">上一次执行时间。</param>
        /// <returns>返回设置结果。</returns>
        Task<bool> SetExecuteDateAsync(int id, DateTime next, DateTime last);

        /// <summary>
        /// 保存错误日志。
        /// </summary>
        /// <param name="name">服务名称。</param>
        /// <param name="exception">错误实例。</param>
        void LogError(string name, Exception exception);

        /// <summary>
        /// 保存错误日志。
        /// </summary>
        /// <param name="id">服务ID。</param>
        /// <param name="name">名称。</param>
        /// <param name="exception">错误实例。</param>
        Task LogErrorAsync(int id, string name, Exception exception);
    }
}