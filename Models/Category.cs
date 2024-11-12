﻿using MachineTest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineTest.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}