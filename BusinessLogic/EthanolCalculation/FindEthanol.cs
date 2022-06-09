using System;
using DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.EthanolCalculation
{
    /// <summary>
    /// Нахождение процента содержания этанола
    /// </summary>
    internal class FindEthanol
    {
        #region Поля
        readonly MethodUsageVerifications verificate = new MethodUsageVerifications();
        readonly EthanolPercentageCalculation ethCalculate = new ();

        /// <summary>
        /// Температура раствора
        /// </summary>
        /// <remarks>Правый текстБокс</remarks>
        private string solutionTemperature { get; set; }

        /// <summary>
        /// Плотность раствора
        /// </summary>
        /// <remarks>Правый текстБокс</remarks>
        private string solutionDensity { get; set; }

        /// <summary>
        /// Полученная в результате вычислений плотность
        /// </summary>
        public double calculatedEthanolContainment { get; set; }
        #endregion

        #region Методы

        public void SetDataForEthanolCalculation(string SolutionTemperature, string SolutionDensity)
        {
            this.solutionDensity = SolutionDensity;
            this.solutionTemperature = SolutionTemperature;
        }

        /// <summary>
        /// Метод, вычисляющий процент содержания этанола в растворе
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void SolutionEthanolContainment()
        {
            verificate.SetDataForDensityCalc(solutionTemperature, solutionDensity);
            if (!verificate.DensityInputNumbersAreCorrect())
                return;
            byte methodNumber = verificate.EthanolContainCalculationMethodNumber();

            // Всегда будет приходить [0,1,3], где 0 - ошибка, 1 - 1й или 2й методы, 3 - 3й метод
            switch (methodNumber)
            {
                case 0:
                    throw new Exception("Ошибка в определении входных чисел для вычисления плотности раствора. Осторожно! Принимаются только целые значения" +
                        "и дробные числа в русской культуре");
                    break;
                case 1:
                    int temperature1 = Convert.ToInt32(solutionTemperature);
                    double density1 = solutionDensity.DoubleParseAdvanced();
                    calculatedEthanolContainment = ethCalculate.CalculateTableNumberOfEthanol(temperature1, density1);
                    if (calculatedEthanolContainment == 0)
                    {
                        calculatedEthanolContainment = ethCalculate.CalculateEthanol(temperature1, density1);
                    }
                    break;
                case 3:
                    double temperature3 = solutionTemperature.DoubleParseAdvanced();
                    double density3 = solutionDensity.DoubleParseAdvanced();
                    calculatedEthanolContainment = ethCalculate.CalculateEthanol(temperature3, density3);
                    break;
            }
        }
        #endregion
    }
}
