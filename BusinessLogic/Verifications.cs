using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DensityOfWaterAlcoholSolution;

namespace DensityOfWaterAlcoholSolution.BusinessLogic
{
    /// <summary>
    /// Класс служебных проверок
    /// </summary>
    public partial class Verifications
    {        
        MainWindow window = new MainWindow();
        
        /// <summary>
        /// "Содержание этанола" и "Температура" имеют значение
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool TempAndEthanolTextBoxesAreNotNULL()
        {
            bool densityTemperatureNotNull = window.LeftTempTB.Text.Length != 0;
            bool ethanolContainmentNotNull = window.EthanolContainmentTB.Text.Length != 0;
            if (densityTemperatureNotNull && ethanolContainmentNotNull)
                return true;
            return false;
        }
        /// <summary>
        /// В полях "Содержание этанола" и "Температура" указаны целые числа
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool IntEthanolContainment_IntTemperature()
        {
            bool ethanolContainmentIsInteger = int.TryParse(window.EthanolContainmentTB.Text, out int ethanol);
            bool temperatureIsInteger = int.TryParse(window.LeftTempTB.Text, out int temperature);
            if(ethanolContainmentIsInteger && temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - целое число, в поле "Температура" - дробное
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool IntEthanolContainment_FloatTemperature()
        {
            bool ethanolContainmentIsInteger = int.TryParse(window.EthanolContainmentTB.Text, out int ethanol);
            bool temperatureIsFloat = float.TryParse(window.LeftTempTB.Text, out float temperature);
            if (ethanolContainmentIsInteger && temperatureIsFloat)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - дробное число, в поле температура - целое
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool FloatEthanolContainment_IntTemperature()
        {
            bool ethanolContainmentIsFloat = float.TryParse((window.EthanolContainmentTB.Text), out float ethanol);
            bool temperatureIsInteger = int.TryParse((window.LeftTempTB.Text), out int temperature);
            if (ethanolContainmentIsFloat && temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В полях "Содержание этанола" и "Температура" указаны дробные числа
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool FloatEthanolContainment_FloatTemperature()
        {
            bool ethanolContainmentIsFloat = float.TryParse(window.EthanolContainmentTB.Text, out float ethanol);
            bool temperatureIsFloat = float.TryParse(window.LeftTempTB.Text, out float temperature);
            if (ethanolContainmentIsFloat && temperatureIsFloat)
                return true;
            return false ;
        }

        /// <summary>
        /// Температура раствора внутри допутимого диапазона
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool TemperatureIsWithinLimit()
        {
            float.TryParse(window.LeftTempTB.Text, out float temperature);
            bool temperatureIsLowerThanMax = temperature < 50;
            bool temperatureIsHigherThanMin = temperature > -60;
            if (temperatureIsLowerThanMax && temperatureIsHigherThanMin)
                return true;
            return false;
        }
    }
}
