﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Mozlite.Mvc.Themes.Menus
{
    /// <summary>
    /// 菜单。
    /// </summary>
    public class MenuItem : IEnumerable<MenuItem>
    {
        internal MenuItem()
        {
        }

        /// <summary>
        /// 初始化类<see cref="MenuItem"/>。
        /// </summary>
        /// <param name="name">唯一名称，需要保证同一级下的名称唯一。</param>
        /// <param name="parent">父级菜单。</param>
        public MenuItem(string name, MenuItem parent = null)
        {
            name = Check.NotEmpty(name, nameof(name)).ToLower();
            Parent = parent ?? new MenuItem();
            if (parent?.Name == null)
                Name = name;
            else
                Name = $"{parent.Name}.{name}";
            Level = Parent.Level + 1;
        }

        /// <summary>
        /// 菜单项在父级下的唯一名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 层级。
        /// </summary>
        public int Level { get; private set; } = -1;

        /// <summary>
        /// 排序。
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 设置标题。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="priority">优先级。</param>
        /// <returns>返回当前菜单。</returns>
        public MenuItem Titled(string title, int priority = 0)
        {
            Title = title;
            Priority = priority;
            return this;
        }

        /// <summary>
        /// 链接地址。
        /// </summary>
        public string LinkUrl { get; private set; }

        /// <summary>
        /// 设置链接地址。
        /// </summary>
        /// <param name="linkUrl">链接地址。</param>
        /// <returns>返回当前菜单。</returns>
        public MenuItem Hrefed(string linkUrl)
        {
            LinkUrl = linkUrl;
            return this;
        }

        /// <summary>
        /// 图标名称，一般为awesome标签fa-后面的部分。
        /// </summary>
        public string IconName { get; private set; }

        /// <summary>
        /// 设置图标样式。
        /// </summary>
        /// <param name="icon">图标样式。</param>
        /// <returns>返回当前菜单。</returns>
        public MenuItem Iconed(string icon)
        {
            if (icon.StartsWith("fa-"))
                icon += " fa";
            IconName = icon;
            return this;
        }


        private readonly IDictionary<string, MenuItem> _children = new Dictionary<string, MenuItem>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.Generic.IEnumerator`1"/>。
        /// </returns>
        public IEnumerator<MenuItem> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器。
        /// </summary>
        /// <returns>
        /// 可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator"/> 对象。
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 父级菜单项。
        /// </summary>
        public MenuItem Parent { get; private set; }

        /// <summary>
        /// 添加子菜单。
        /// </summary>
        /// <param name="name">唯一名称。</param>
        /// <param name="action">菜单实例化代理方法。</param>
        /// <returns>返回当前项目实例。</returns>
        public MenuItem AddMenu(string name, Action<MenuItem> action)
        {
            var menu = new MenuItem(name, this);
            action(menu);
            _children.Add(menu.Name, menu);
            return this;
        }

        /// <summary>
        /// 添加子菜单。
        /// </summary>
        /// <param name="action">菜单实例化代理方法。</param>
        /// <returns>返回当前项目实例。</returns>
        public MenuItem AddMenu(Action<MenuItem> action)
        {
            var menu = new MenuItem();
            menu.Parent = this;
            menu.Level = Level + 1;
            action(menu);
            if (menu.Name == null)
                throw new Exception("菜单唯一名称不能为空！");
            if (Name != null)
                menu.Name = $"{Name}.{menu.Name}";
            _children.Add(menu.Name, menu);
            return this;
        }

        internal void Merge(MenuItem item)
        {
            Title = Title ?? item.Title;
            IconName = IconName ?? item.IconName;
            Priority = Math.Max(Priority, item.Priority);
            Level = Math.Max(Level, item.Level);
            if (Parent?.Name == null)
                Parent = item.Parent;
            LinkUrl = LinkUrl ?? item.LinkUrl;

            foreach (var it in item)
            {
                MenuItem i;
                if (_children.TryGetValue(it.Name, out i))
                    i.Merge(it);
                else
                {
                    it.Parent = this;
                    _children.Add(it.Name, it);
                }
            }
        }
    }
}