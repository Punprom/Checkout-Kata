using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PK.Kata.Shared.Extensions;
using PK.Kata.Shared.Helpers;
using PK.Kata.Shared.Models;
using Xunit;

namespace PK.Kata.Tests
{
    public class PricingTests
    {
        private List<Promotion> _promotions;
        private List<Product> _products;

        public PricingTests()
        {
            _promotions = InitPromotionsData();
            _products = InitProductsData();
        }
        
        
        [Fact]
        public void Test_3_for_40_promotion_against_sku_B()
        {
            var actualSku = "B";
            var amount = 5;

            var actualProduct = _products.FirstOrDefault(p => p.Sku == actualSku);
            Assert.NotNull(actualProduct);

            var promotion = _promotions.FirstOrDefault(pm => pm.Code == actualProduct.Promotion);
            Assert.NotNull(promotion);

            var sets = amount.SetOf(promotion.SetAmount);
            var remainder = amount.RemainderOf(promotion.SetAmount);
            var actualPrice = (sets * promotion.SetPrice) + (remainder * actualProduct.UnitPrice);
            var expectedPrice =
                PricingHelper.CalculateSetPricePromotion(amount, actualProduct.UnitPrice, promotion.SetAmount, promotion.SetPrice);
            
            Assert.Equal(actualPrice, expectedPrice);
        }


        


        private List<Product> InitProductsData()
        {
            var list = new List<Product>();
            list.AddRange(new Product[]
            {
                new Product() { Sku = "A", UnitPrice = 10 },
                new Product() { Sku = "B", UnitPrice = 15, Promotion = "3 for 40"},
                new Product() { Sku = "C", UnitPrice = 40 },
                new Product() { Sku = "D", UnitPrice = 55, Promotion = "25% off for every 2 purchased together" }
            });

            return list;
        }


        private List<Promotion> InitPromotionsData()
        {
            var list = new List<Promotion>();
            list.AddRange(new Promotion[]
            {
                new Promotion() {Code = "3 for 40", SetAmount = 3, SetPrice = 40, IsSet = true, PercentOff = 0},
                new Promotion() {Code = "25% off for every 2 purchased together", SetAmount = 2, SetPrice = 0, 
                    IsSet = false, PercentOff = 25 }
            });

            return list;
        }
    }
}
