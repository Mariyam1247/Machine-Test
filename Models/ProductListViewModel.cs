﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineTest.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}