﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models
{
    public class PagedResult<T> where T : class
    {
        public PagedResult()
        {
            Data = new List<T>();
        }
        public List<T> Data { get; set; }
        public int TotalItems { get; set; } = 0;
    }
}
