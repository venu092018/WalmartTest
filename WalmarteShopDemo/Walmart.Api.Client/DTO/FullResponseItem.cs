using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Api.Client.DTO
{
    public class FullResponseItem
    {
        public int ItemId { get; set; }
        public int ParentItemId { get; set; }
        public string Name { get; set; }
        public decimal Msrp { get; set; }
        public decimal SalePrice { get; set; }
        public string Upc { get; set; }
        public string CategoryPath { get; set; }
        public string CategoryNode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string BrandName { get; set; }
        public string ThumbnailImage { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
        public decimal StandardShipRate { get; set; }
        public bool Marketplace { get; set; }
        public string ModelNumber { get; set; }
        public string ProductUrl { get; set; }
        public decimal CustomerRating { get; set; }
        public string CustomerRatingImage { get; set; }
        public string NumReviews { get; set; }
    }
}
