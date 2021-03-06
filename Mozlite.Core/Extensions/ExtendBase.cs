﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Mozlite.Extensions
{
    /// <summary>
    /// 扩展基类。
    /// </summary>
    public abstract class ExtendBase
    {
        private IDictionary<string, string> _extendProperties = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 扩展方法。
        /// </summary>
        [JsonIgnore]
        public string ExtendProperties
        {
            get => JsonConvert.SerializeObject(_extendProperties);
            set => _extendProperties = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
        }

        /// <summary>
        /// 索引访问和设置扩展属性。
        /// </summary>
        /// <param name="name">索引值。</param>
        /// <returns>返回当前扩展属性值。</returns>
        [NotMapped]
        [JsonIgnore]
        public string this[string name]
        {
            get
            {
                if (!name.StartsWith("ex:"))
                    name = "ex:" + name;
                string value;
                _extendProperties.TryGetValue(name, out value);
                return value;
            }
            set
            {
                if (!name.StartsWith("ex:"))
                    name = "ex:" + name;
                _extendProperties[name] = value;
            }
        }

        /// <summary>
        /// 扩展属性列表。
        /// </summary>
        [JsonIgnore]
        public IEnumerable<string> ExtendKeys => _extendProperties.Keys;
    }
}