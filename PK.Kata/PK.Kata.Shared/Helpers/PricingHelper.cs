using PK.Kata.Shared.Extensions;
using PK.Kata.Shared.Models;

namespace PK.Kata.Shared.Helpers
{
    /// <summary>
    /// Pricing Helper class
    /// </summary>
    public class PricingHelper
    {
        /// <summary>
        /// calculates set pricing promotion
        /// </summary>
        /// <param name="amount">amount tendered</param>
        /// <param name="unitPrice">unit price</param>
        /// <param name="set">promotion offer set amount</param>
        /// <param name="setPrice">promotion set price</param>
        /// <returns>price calculated</returns>
        public static decimal CalculateSetPricePromotion(int amount, int unitPrice, int set, int setPrice)
        {
            var setRemainders = SetRemainder.DoSet(amount, set);
            var price = (setRemainders.Sets * setPrice) + (setRemainders.Remainders * unitPrice);

            return price;
        }

        /// <summary>
        /// calculates actual pricing promotion 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="promotion"></param>
        /// <param name="tenderAmount"></param>
        /// <returns></returns>
        public static decimal CalculateActualPrice(Product product, Promotion promotion, int tenderAmount)
        {
            decimal thePrice = 0;

            if (promotion.Code == "3 for 40")
            {
                var sets = tenderAmount.SetOf(promotion.SetAmount);
                var remainder = tenderAmount.RemainderOf(promotion.SetAmount);
                thePrice = (sets * promotion.SetPrice) + (remainder * product.UnitPrice);

            }
            else // must be percentage discount
            {
                var sets = tenderAmount.SetOf(promotion.SetAmount);
                var remainder = tenderAmount.RemainderOf(promotion.SetAmount);
                var aPairPrice = (product.UnitPrice * promotion.SetAmount);
                var discountPerPair = (aPairPrice * promotion.PercentOff) / 100;
                var aPairCosts = aPairPrice - discountPerPair;
                thePrice = (aPairCosts * sets) + (remainder * product.UnitPrice);

            }
            
            return thePrice;
        }

        /// <summary>
        /// calculates percentage off promotion
        /// </summary>
        /// <param name="amount">quantity amount to be tendered</param>
        /// <param name="unitPrice">unit price</param>
        /// <param name="set">set of items to be discounted</param>
        /// <param name="percentOff">amount percentage off</param>
        /// <returns>price to be paid</returns>
        public decimal CalculatePecentageOff(int amount, int unitPrice, int set, int percentOff)
        { 
            var sets = amount.SetOf(amount);
            var remainder =amount.RemainderOf(set);
           
            var aPairPrice = (unitPrice * set);
            var discountPerPair = (aPairPrice * percentOff) / 100;
            var aPairCosts = aPairPrice - discountPerPair;
            var thePrice = (aPairCosts * sets) + (remainder * unitPrice);

            return thePrice;
        }
    }
}
