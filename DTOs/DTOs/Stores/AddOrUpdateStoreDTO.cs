﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.Stores
{
    public class AddOrUpdateStoreDTO
    {
        public string? StoreName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public AddOrUpdateStoreAssetsDTO StoreAssets { get; set; }
    }
}
