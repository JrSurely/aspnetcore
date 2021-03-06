﻿using System.Linq.Expressions;
using Mozlite.Extensions;

namespace Mozlite.Data.Query
{
    /// <summary>
    /// SQL语句生成接口。
    /// </summary>
    public interface IQuerySqlGenerator
    {
        /// <summary>
        /// 新建实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Create(IEntityType entityType);

        /// <summary>
        /// 更新实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Update(IEntityType entityType);

        /// <summary>
        /// 更新实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <param name="expression">条件表达式。</param>
        /// <param name="parameters">匿名对象。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Update(IEntityType entityType, Expression expression, object parameters);

        /// <summary>
        /// 更新实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <param name="expression">条件表达式。</param>
        /// <param name="parameters">匿名对象。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Update(IEntityType entityType, Expression expression, LambdaExpression parameters);

        /// <summary>
        /// 删除实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Delete(IEntityType entityType, Expression expression);

        /// <summary>
        /// 查询实例。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Fetch(IEntityType entityType, Expression expression);

        /// <summary>
        /// 判断是否存在。
        /// </summary>
        /// <param name="entityType">模型实例。</param>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回SQL构建实例。</returns>
        SqlIndentedStringBuilder Any(IEntityType entityType, Expression expression);

        /// <summary>
        /// 解析表达式。
        /// </summary>
        /// <param name="expression">表达式实例。</param>
        /// <returns>返回解析的表达式字符串。</returns>
        string Visit(Expression expression);

        /// <summary>
        /// 生成实体类型的SQL脚本。
        /// </summary>
        /// <param name="sql">SQL查询实例。</param>
        /// <returns>返回SQL脚本。</returns>
        SqlIndentedStringBuilder Query(IQuerySql sql);
    }
}