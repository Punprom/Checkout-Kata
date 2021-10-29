using System;

namespace PK.Kata.Shared.Extensions
{
    public static class IntegerExtension
    {
        public static int SetOf(this int number, int set)
        {
            return Math.Abs(number / set);
        }

        public static int RemainderOf(this int number, int set)
        {
            return (number % set);
        }
    }
}
