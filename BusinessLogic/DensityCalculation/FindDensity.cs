using System;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation
{
    /// <summary>
    /// Нахождение плотности раствора
    /// </summary>
    internal partial class FindDensity
    {
        #region Поля
        readonly MainWindow window = new MainWindow();
        readonly MethodUsageVerifications verificate = new MethodUsageVerifications();
        readonly DensityCalculation denCalculate = new DensityCalculation();

        /// <summary>
        /// Полученный результат вычислений
        /// </summary>
        public float calculatedSolutionDensity { get; set; }
        #endregion


        #region Методы
        /// <summary>
        /// Метод, вычисляющий плотность раствора
        /// </summary>
        public void SolutionDensity()
        {
            //Вызов проверки данных
            if (!verificate.DensityInputNumbersAreCorrect())
                return;
            //Вызов функции, определяющего, какой метод вычислений использовать (Так как значение всегда будет [1;4], используем byte methodNumber)
            byte methodNumber = verificate.DensityCalculationMethodNumber();
            //switch-конструкция, определяющая на основе methodNumber, какой метод вычислений использовать
            switch(methodNumber)
            {
                case 0:
                    throw new Exception("Ошибка в определении входных чисел для вычисления плотности раствора. Осторожно! Принимаются только целые значения" +
                        "и дробные числа в русской культуре");
                    //calculatedSolutionDensity = 0; ← как варик для всех неподходящих цифр буду возвращать 0 и всё
                    break;
                case 1:
                    int temperature1 = Convert.ToInt32(window.LeftTempTB.Text);
                    int ethanolCont1 = Convert.ToInt32(window.EthanolContainmentTB.Text);
                    calculatedSolutionDensity = denCalculate.CalculateDensity(temperature1, ethanolCont1);
                    break;
                case 2:
                    int temperature2 = Convert.ToInt32(window.LeftTempTB.Text);
                    float ethanolCont2 = float.Parse(window.EthanolContainmentTB.Text);
                    calculatedSolutionDensity = denCalculate.CalculateDensity(temperature2, ethanolCont2);
                    break;
                case 3:
                    float temperature3 = Convert.ToInt32(window.LeftTempTB.Text);
                    int ethanolCont3 = Convert.ToInt32(window.EthanolContainmentTB.Text);
                    calculatedSolutionDensity = denCalculate.CalculateDensity(temperature3, ethanolCont3);
                    break;
                case 4:
                    float temperature4 = Convert.ToInt32(window.LeftTempTB.Text);
                    float ethanolCont4 = Convert.ToInt32(window.EthanolContainmentTB.Text);
                    calculatedSolutionDensity = denCalculate.CalculateDensity(temperature4, ethanolCont4);
                    break;
            }
        }
        #endregion
    }
}
