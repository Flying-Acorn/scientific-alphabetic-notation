using System;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
using UnityEngine;

namespace FlyingAcorn.Utilities.ScientificAlphabeticNotation
{
    public static class NumberFormatting
    {
        public static string ConvertToScientificAlphabeticFormat(BigDouble number)
        {
            var modifiedMantissa = BigDouble.Floor(number.Mantissa * BigDouble.Pow10(number.Exponent % 3));
            var alphabeticExponent = "";

            if (number >= BigDouble.Pow10(3) && number < BigDouble.Pow10(6))
                alphabeticExponent = "K";

            if (number >= BigDouble.Pow10(6) && number < BigDouble.Pow10(9))
                alphabeticExponent = "M";

            if (number >= BigDouble.Pow10(9) && number < BigDouble.Pow10(12))
                alphabeticExponent = "B";

            if (number >= BigDouble.Pow10(12))
            {
                FindAlphabeticExponent(number.Exponent, out alphabeticExponent);
            }

            return modifiedMantissa + alphabeticExponent;
        }

        private static void FindAlphabeticExponent(long exponent, out string alphabeticExponent)
        {
            var reductedExponent = Mathf.CeilToInt((exponent - 11) / 3f);

            alphabeticExponent = DecimalToAlphabeticSystem(reductedExponent);
        }

        public static string ConvertToScientificAlphabeticFormat(string number)
        {
            var bigDouble = ConvertStringToBigDouble(number);

            return ConvertToScientificAlphabeticFormat(bigDouble);
        }

        public static BigDouble ConvertStringToBigDouble(string number)
        {
            if (number.Any(char.IsLetter))
            {
                return BigDouble.Zero;
            }

            var temp = BigDouble.Zero;

            for (var i = 0; i < number.Length; i++)
            {
                var chr = number[number.Length - 1 - i];
                var num = char.GetNumericValue(chr);
                temp += num * BigDouble.Pow10(i);
            }

            return temp;
        }

        private static string DecimalToAlphabeticSystem(long number)
        {
            var alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            var result = "";

            while (number > 0)
            {
                number--;

                var remainder = (int) number % 26;
                result = result.Insert(0, alpha[remainder].ToString());
                number /= 26;
            }

            return result;
        }
    }
}