using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalmarteShopDemo.ViewModels
{
    public class ProductInfoFull
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ThumbnailImage { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
        public decimal SalePrice { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string CustomerRatingImage { get; set; }
        public string NumReviews { get; set; }

    }
}
