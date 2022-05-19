using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DensityOfWaterAlcoholSolution.BusinessLogic
{
    /// <summary>
    /// Нахождение плотности раствора
    /// </summary>
    internal class DensityCalc
    {
        //Verifications verificate = new Verifications();

        public DensityCalc()
        {
          //  if (!verificate.TempAndEthanolTextBoxesAreNotNULL())
           //     MessageBox.Show("Для вычисления плотности все поля должны быть заполнены", "ВНИМАНИЕ!");
        }

        /// <summary>
        /// Процент содержания этанола при 20C (По объёму)
        /// </summary>
        float ethanolContainment { get; set; }
        /// <summary>
        /// Температура раствора
        /// </summary>
        float solutionTemperature { get; set; }

        /// <summary>
        /// Метод, вычисляющий плотность раствора
        /// </summary>
        public void SolutionDensity()
        {

        }
    }
}
