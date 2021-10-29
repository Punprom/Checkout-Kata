using System.Collections.Generic;
using System.Linq;
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

            var actualPrice = PricingHelper.CalculateActualPrice(actualProduct, promotion, amount);
            var expectedPrice =
                PricingHelper.CalculateSetPricePromotion(amount, actualProduct.UnitPrice, promotion.SetAmount, promotion.SetPrice);

            Assert.Equal(actualPrice, expectedPrice);
        }

        [Fact]
        public void Test_25_percentage_discount_promotion_for_sku_D()
        {
            var actualSku = "D";
            var amount = 5;

            var actualProduct = _products.FirstOrDefault(p => p.Sku == actualSku);
            Assert.NotNull(actualProduct);

            var promotion = _promotions.FirstOrDefault(pm => pm.Code == actualProduct.Promotion);
            Assert.NotNull(promotion);

            var actualPrice = PricingHelper.CalculateActualPrice(actualProduct, promotion, amount);
            var expectedPrice = PricingHelper.CalculatePecentageOff(amount, actualProduct.UnitPrice,
                promotion.SetAmount, promotion.PercentOff);

            Assert.Equal(actualPrice, expectedPrice);

        }

        [Theory]
        [InlineData("A", 3)]
        [InlineData("C", 6)]
        [InlineData("D", 4)]
        [InlineData("B", 8)]
        public void Test_Products_against_promotions(string sku, int qty)
        {
            decimal actualPrice = 0;
            var actualProduct = _products.FirstOrDefault(p => p.Sku == sku);
            Assert.NotNull(actualProduct);

            // some Sku does not have promotion so skip this.
            if (!string.IsNullOrEmpty(actualProduct.Promotion))
            {
                var promotion = _promotions.FirstOrDefault(pm => pm.Code == actualProduct.Promotion);
                Assert.NotNull(promotion);

                decimal expectedPrice = 0;
                actualPrice = PricingHelper.CalculateActualPrice(actualProduct, promotion, qty);
                if (promotion.IsSet)
                    expectedPrice = PricingHelper.CalculateSetPricePromotion(qty, actualProduct.UnitPrice,
                        promotion.SetAmount, promotion.SetPrice);
                else
                    expectedPrice = PricingHelper.CalculatePecentageOff(qty, actualProduct.UnitPrice,
                        promotion.SetAmount, promotion.PercentOff);

                Assert.Equal(actualPrice, expectedPrice);
            }
            else
            {
                actualPrice = (actualProduct.UnitPrice * qty);
                Assert.True(actualPrice>0);
            }

           
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
