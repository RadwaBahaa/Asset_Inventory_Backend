﻿using DTOs.Validation.Assets;

namespace DTOs.DTOs.Assets
{
    public class AddOrUpdateAssetDTO
    {
        public string AssetName { get; set; }
        public int CategoryID { get; set; }
        [PriceValidation] 
        public float Price { get; set; }
        public string Description { get; set; }
        public int SerialNumber { get; set; }
        public byte[] Picture { get; set; }   
    }
}