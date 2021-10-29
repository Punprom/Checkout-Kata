using PK.Kata.Shared.Models;

namespace PK.Kata.Shared.Helpers
{
    public class PricingHelper
    {
        public static decimal CalculateSetPricePromotion(int amount,int unitPrice, int set, int setPrice)
        {
            var setRemainders = SetRemainder.DoSet(amount, set);
            var price = (setRemainders.Sets * setPrice) + (setRemainders.Remainders * unitPrice);

            return price;
        }
    }
}
