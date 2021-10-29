using PK.Kata.Shared.Extensions;

namespace PK.Kata.Shared.Models
{
    public class SetRemainder
    {
        public int Sets { get; set; }

        public int Remainders { get; set; }

        public static SetRemainder DoSet(int number, int set)
        {
            var sets = number.SetOf(set);
            var remainders = number.RemainderOf(set);

            return new SetRemainder() { Sets = sets, Remainders = remainders };
        }
    }
}
