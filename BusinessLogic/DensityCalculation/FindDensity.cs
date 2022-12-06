using System;
using System.Windows;
using DensityOfWaterAlcoholSolution.Helpers;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation
{
    /// <summary>
    /// Нахождение плотности раствора
    /// </summary>
    public class FindDensity
    {
        #region Поля
        readonly MethodUsageVerifications verificate = new MethodUsageVerifications();
        readonly DensityCalculation denCalculate = new DensityCalculation();

        /// <summary>
        /// Температура раствора (левый текстбокс)
        /// </summary>
        private string solutionTemperature { get; set; }
        
        /// <summary>
        /// Количество этанола (левый текстбокс)
        /// </summary>
        private string ethanolContainment { get; set; }

        /// <summary>
        /// Полученная в результате вычислений плотность
        /// </summary>
        public double calculatedSolutionDensity { get; set; }
        #endregion


        #region Методы

        public void SetDataForDensityCalculation(string SolutionTemperature, string EthanolContainment)
        {
            this.solutionTemperature = SolutionTemperature;
            this.ethanolContainment = EthanolContainment;
        }

        /// <summary>
        /// Метод, вычисляющий плотность раствора
        /// </summary>
        public void SolutionDensity()
        {
            verificate.SetDataForDensityCalc(solutionTemperature, ethanolContainment);
            if (!verificate.DensityInputNumbersAreCorrect())
                return;
            //Вызов функции, определяющего, какой метод вычислений использовать (Так как значение всегда будет [1;4], используем byte methodNumber)
            byte methodNumber = verificate.DensityCalculationMethodNumber();
            //switch-конструкция, определяющая на основе methodNumber, какой метод вычислений использовать
            try
            {
                switch (methodNumber)
                {
                    case 0:
                        throw new Exception("Ошибка в определении входных чисел для вычисления плотности раствора. Осторожно! Принимаются только целые значения" +
                            "и дробные числа в русской культуре");
                        break;
                    case 1:
                        int temperature1 = Convert.ToInt32(solutionTemperature.DoubleParseAdvanced());
                        int ethanolCont1 = Convert.ToInt32(ethanolContainment.DoubleParseAdvanced());
                        calculatedSolutionDensity = denCalculate.CalculateDensity(temperature1, ethanolCont1);
                        break;
                    case 2:
                        double temperature2 = solutionTemperature.DoubleParseAdvanced();
                        int ethanolCont2 = Convert.ToInt32(ethanolContainment);
                        calculatedSolutionDensity = denCalculate.CalculateDensity(temperature2, ethanolCont2);
                        break;
                    case 3:
                        int temperature3 = Convert.ToInt32(solutionTemperature.DoubleParseAdvanced());
                        double ethanolCont3 = ethanolContainment.DoubleParseAdvanced();
                        calculatedSolutionDensity = denCalculate.CalculateDensity(temperature3, ethanolCont3);
                        break;
                    case 4:
                        double temperature4 = solutionTemperature.DoubleParseAdvanced();
                        double ethanolCont4 = ethanolContainment.DoubleParseAdvanced();
                        calculatedSolutionDensity = denCalculate.CalculateDensity(temperature4, ethanolCont4);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестный формат данных");
            }
        }
        #endregion
    }
}
