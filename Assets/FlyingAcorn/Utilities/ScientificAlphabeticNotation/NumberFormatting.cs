using System;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
using UnityEngine;

namespace FlyingAcorn.Utilities.ScientificAlphabeticNotation
{
    public static class NumberFormatting
    {
        public static string ConvertToScientificAlphabeticFormat(BigDouble number, int decimals = 0,
            bool precisionMode = false)
        {
            var scalingPower = precisionMode ? 3 : 0;

            var precision = number >= BigDouble.Pow10(3 + scalingPower)
                ? number.Exponent % 3 + scalingPower
                : number.Exponent % (3 + scalingPower);

            var modifiedMantissa =
                Math.Round(number.Mantissa, decimals + (int) precision) * Math.Pow(10, precision);
            var alphabeticExponent = "";

            if (number >= BigDouble.Pow10(3 + scalingPower) && number < BigDouble.Pow10(6 + scalingPower))
                alphabeticExponent = "K";

            if (number >= BigDouble.Pow10(6 + scalingPower) && number < BigDouble.Pow10(9 + scalingPower))
                alphabeticExponent = "M";

            if (number >= BigDouble.Pow10(9 + scalingPower) && number < BigDouble.Pow10(12 + scalingPower))
                alphabeticExponent = "B";

            if (number >= BigDouble.Pow10(12 + scalingPower))
            {
                FindAlphabeticExponent(number.Exponent - scalingPower, out alphabeticExponent);
            }

            return modifiedMantissa + alphabeticExponent;
        }

        private static void FindAlphabeticExponent(long exponent, out string alphabeticExponent)
        {
            var reductedExponent = Mathf.CeilToInt((exponent - 11) / 3f);

            alphabeticExponent = DecimalToAlphabeticSystem(reductedExponent);
        }

        public static string ConvertToScientificAlphabeticFormat(string number, int decimals = 0,
            bool precisionMode = false)
        {
            var bigDouble = ConvertStringToBigDouble(number);

            return ConvertToScientificAlphabeticFormat(bigDouble, decimals, precisionMode);
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