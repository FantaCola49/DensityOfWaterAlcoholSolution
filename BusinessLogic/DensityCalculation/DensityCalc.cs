using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation
{
    /// <summary>
    /// Нахождение плотности раствора
    /// </summary>
    internal partial class DensityCalc
    {
        #region Поля
        readonly Verifications verificate = new Verifications();
        readonly MainWindow window = new MainWindow();

        /// <summary>
        /// Полученный результат вычислений
        /// </summary>
        public float calculatedSolutionDensity { get; set; }
        #endregion


        #region Методы
        /// <summary>
        /// Метод, вычисляющий плотность раствора
        /// </summary>
        public void SolutionDensity() ////TODO
        {
            //Вызов проверки данных
            //Вызов функции, определяющего, какой метод вычислений использовать (Так как значение всегда будет [1;4], используем byte methodNumber)
            //switch-конструкция, определяющая на основе methodNumber, какой метод вычислений использовать
                //Вызов соответствующего метода вычислений внутри switch-конструкции
                //Парсинг возвращаемого значения
                //Присваиваем полученное значение полю класса
        }
        #endregion
    }
}
