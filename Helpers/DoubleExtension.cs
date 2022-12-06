using System;

namespace DensityOfWaterAlcoholSolution.Helpers
{
    public static class DoubleExtension
    {
        /// <summary>
        /// Вернёт дробный остаток от дробного числа до ближайшего большего целого
        /// </summary>
        /// <example>Дали 0,75 => вернёт 0,25</example>
        /// <param name="n">дробное число</param>
        /// <returns></returns>
        public static double GetReminderPriorNextSolidNumber(this float n)
        {
            double reminder;
            if (n >= 0)
            {
                double solidNumber = Math.Ceiling(n);
                reminder = solidNumber - n;
            }
            else
            {
                double solidNumber = Math.Floor(n);
                reminder = Math.Abs(solidNumber - n);
            }
            return reminder;
        }

        /// <summary>
        /// Вернёт дробный остаток от дробного числа до ближайшего большего целого
        /// </summary>
        /// <example>Дали 0,75 => вернёт 0,25</example>
        /// <param name="d">дробное число</param>
        /// <returns></returns>
        public static double GetReminderPriorNextSolidNumber(this double d) 
        {
            double reminder;
            if (d >= 0)
            {
                double solidNumber = Math.Ceiling(d);
                reminder = solidNumber - d;
            }
            else
            {
                double solidNumber = Math.Floor(d);
                reminder = Math.Abs(solidNumber - d);
            }
            return reminder;
        }

        /// <summary>
        /// Вернёт остаток дробного числа
        /// </summary>
        /// <example>Дали 10,201 => вернёт 0,201</example>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double GetReminder(this float n) 
        {
            double reminder;
            if (n >= 0)
            {
                double solidNumber = Math.Ceiling(n);
                reminder = n - solidNumber;
            }
            else
            {
                double solidNumber = Math.Floor(n);
                reminder = Math.Abs(solidNumber - n);
            }
            return reminder;
        }

        /// <summary>
        /// Вернёт остаток дробного числа
        /// </summary>
        /// <example>Дали 10,201 => вернёт 0,201</example>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double GetReminder(this double n)
        {
            double reminder;
            if (n >= 0)
            {
                double solidNumber = Math.Ceiling(n);
                reminder = n - solidNumber;
            }
            else
            {
                double solidNumber = Math.Floor(n);
                reminder = Math.Abs(solidNumber - n);
            }
            return reminder;
        }
    }
}
