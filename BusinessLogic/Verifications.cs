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

        #region Нахождение плотности раствора

        #region Проверка корректности входных данных

        /// <summary>
        /// Данные "Температуры" и "Содержание этанола" пригодны для работы
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        public bool DensityInputNumbersAreCorrect()
        {
            if (!DensTempAndEthanolTextBoxesAreNotNULL())
                return false;
            if(!DensTemperatureIsWithinLimit())
                return false;
            if (!DensEthanolContainmentIsWithinLimit())
                return false;

            return true;
        }

        /// <summary>
        /// Температура раствора внутри допутимого диапазона
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        private bool DensTemperatureIsWithinLimit()
        {
            float.TryParse(window.LeftTempTB.Text, out float temperature);
            bool temperatureIsLowerThanMax = temperature < 50.0;
            bool temperatureIsHigherThanMin = temperature > -60.0;
            if (temperatureIsLowerThanMax && temperatureIsHigherThanMin)
                return true;
            MessageBox.Show("Неверные данные! Температура раствора должна быть от -60 до 50 градусов!", "Внимание!");
            return false;
        }

        /// <summary>
        /// Содержание этанола внутри допустимого диапазона
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        private bool DensEthanolContainmentIsWithinLimit()
        {
            double ethanol = Convert.ToDouble(window.EthanolContainmentTB.Text);
            bool ethanolIsLowerThanMax = ethanol <= 100.0;
            bool ethanolIsHigherThanMin = ethanol >= 0.0;
            if (ethanolIsLowerThanMax && ethanolIsHigherThanMin)
                return true;
            MessageBox.Show("Содержание этанола в растворе не должно превышать 100.0 и быть ниже, чем 0.00!", "Ошибка ввода даных");
            return false;
        }

        /// <summary>
        /// "Содержание этанола" и "Температура" имеют значение
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        private bool DensTempAndEthanolTextBoxesAreNotNULL()
        {
            bool densityTemperatureNotNull = window.LeftTempTB.Text.Length != 0;
            bool ethanolContainmentNotNull = window.EthanolContainmentTB.Text.Length != 0;
            if (densityTemperatureNotNull && ethanolContainmentNotNull)
                return true;
            MessageBox.Show("Для вычисления плотности все поля должны быть заполнены", "ВНИМАНИЕ!");
            return false;
        }
        
        #endregion

        #region Определение дробных и целых чисел во входных данных

        /// <summary>
        /// В полях "Содержание этанола" и "Температура" указаны целые числа
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool IntEthanolContainment_IntTemperature()
        {
            int pointIndexInEthanol = window.EthanolContainmentTB.Text.IndexOf(",");
            int pointIndexInTemperature = window.LeftTempTB.Text.IndexOf(",");
            bool ethanolContainmentIsInteger = (pointIndexInEthanol == -1);
            bool temperatureIsInteger = (pointIndexInTemperature == -1);
            if (ethanolContainmentIsInteger && temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - целое число, в поле "Температура" - дробное
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool IntEthanolContainment_FloatTemperature()
        {
            int pointIndexInEthanol = window.EthanolContainmentTB.Text.IndexOf(",");
            int pointIndexInTemperature = window.LeftTempTB.Text.IndexOf(",");
            bool ethanolContainmentIsInteger = (pointIndexInEthanol == -1);
            bool temperatureIsInteger = (pointIndexInTemperature != -1);
            if (ethanolContainmentIsInteger && temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - дробное число, в поле температура - целое
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool FloatEthanolContainment_IntTemperature()
        {
            int pointIndexInEthanol = window.EthanolContainmentTB.Text.IndexOf(",");
            int pointIndexInTemperature = window.LeftTempTB.Text.IndexOf(",");
            bool ethanolContainmentIsInteger = (pointIndexInEthanol != -1);
            bool temperatureIsInteger = (pointIndexInTemperature == -1);
            if (ethanolContainmentIsInteger && temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В полях "Содержание этанола" и "Температура" указаны дробные числа
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool FloatEthanolContainment_FloatTemperature()
        {
            int pointIndexInEthanol = window.EthanolContainmentTB.Text.IndexOf(",");
            int pointIndexInTemperature = window.LeftTempTB.Text.IndexOf(",");
            bool ethanolContainmentIsInteger = (pointIndexInEthanol != -1);
            bool temperatureIsInteger = (pointIndexInTemperature != -1);
            if (ethanolContainmentIsInteger && temperatureIsInteger)
                return true;
            return false;
        }

        #endregion

        #endregion



        ////TODO ↓! 
        #region Нахождение процента содержания этанола


        #endregion
    }
}
