using DensityOfWaterAlcoholSolution.Helpers;
using System;
using System.Windows;

namespace DensityOfWaterAlcoholSolution.BusinessLogic
{
    /// <summary>
    /// Класс служебных проверок
    /// </summary>
    public class Verifications : MainWindow
    {

        #region Нахождение плотности раствора

        /// <summary>
        /// Температура раствора
        /// </summary>
        /// <remarks>Используется при нахождении плотности раствора (левый текстбокс)</remarks>
        private string solutionTemperature { get; set; }
        /// <summary>
        /// Содержание етанола
        /// </summary>
        /// <remarks>Используется при нахождении плотности раствора (левый текстбокс)</remarks>
        private string ethanolContainment { get; set; }

        public void SetDataForDensityCalc(string SolutionTemperature, string EthanolContainment)
        {
            this.solutionTemperature = SolutionTemperature;
            this.ethanolContainment = EthanolContainment;
        }

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
            if (!DensTemperatureIsWithinLimit())
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
            try
            {
                double temperature = solutionTemperature.DoubleParseAdvanced();
                bool temperatureIsLowerThanMax = temperature <= 50.0;
                bool temperatureIsHigherThanMin = temperature >= -60.0;
                if (temperatureIsLowerThanMax && temperatureIsHigherThanMin)
                    return true;
                MessageBox.Show("Неверные данные! Температура раствора должна быть от -60 до 50 градусов!", "Внимание!");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось распознать значения температуры для вычисления плотности.");
                return false;
            }
        }

        /// <summary>
        /// Содержание этанола внутри допустимого диапазона
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        private bool DensEthanolContainmentIsWithinLimit()
        {
            try
            {
                double ethanol = ethanolContainment.DoubleParseAdvanced();
                bool ethanolIsLowerThanMax = ethanol <= 100.0;
                bool ethanolIsHigherThanMin = ethanol >= 0.0;
                if (ethanolIsLowerThanMax && ethanolIsHigherThanMin)
                    return true;
                MessageBox.Show("Содержание этанола в растворе не должно превышать 100.0 и быть ниже, чем 0.00!", "Ошибка ввода даных");
                return false;
            }
            catch
            {
                MessageBox.Show("Не удалось распознать значения температуры для вычисления плотности.");
                return false;
            }
        }

        /// <summary>
        /// "Содержание этанола" и "Температура" имеют значение
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        private bool DensTempAndEthanolTextBoxesAreNotNULL()
        {
            bool densityTemperatureIsNull = string.IsNullOrEmpty(solutionTemperature) | string.IsNullOrWhiteSpace(solutionTemperature);
            bool ethanolContainmentIsNull = string.IsNullOrEmpty(ethanolContainment) | string.IsNullOrWhiteSpace(ethanolContainment);

            if (densityTemperatureIsNull | ethanolContainmentIsNull)
            {
                MessageBox.Show("Для вычисления плотности все поля должны быть заполнены", "ВНИМАНИЕ!");
                return false;
            }
            else return true;
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
            bool ethanolContainmentIsInteger = ethanolContainment.DoubleParseAdvanced().GetReminder() == 0;
            bool temperatureIsInteger = solutionTemperature.DoubleParseAdvanced().GetReminder() == 0;
            if (ethanolContainmentIsInteger & temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - целое число, в поле "Температура" - дробное
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool IntEthanolContainment_DoubleTemperature()
        {
            bool ethanolContainmentIsInteger = ethanolContainment.DoubleParseAdvanced().GetReminder() == 0;
            bool temperatureIsDouble = solutionTemperature.DoubleParseAdvanced().GetReminder() != 0;
            if (ethanolContainmentIsInteger & temperatureIsDouble)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Содержание этанола" - дробное число, в поле температура - целое
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool DoubleEthanolContainment_IntTemperature()
        {
            bool ethanolContainmentIsDouble = ethanolContainment.DoubleParseAdvanced().GetReminder() != 0;
            bool temperatureIsInteger = solutionTemperature.DoubleParseAdvanced().GetReminder() == 0;
            if (ethanolContainmentIsDouble & temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В полях "Содержание этанола" и "Температура" указаны дробные числа
        /// </summary>
        /// <remarks>Нахождение плотности раствора</remarks>
        /// <returns></returns>
        protected bool DoubleEthanolContainment_DoubleTemperature()
        {
            bool ethanolContainmentIsDouble = ethanolContainment.DoubleParseAdvanced().GetReminder() != 0;
            bool temperatureIsDouble = solutionTemperature.DoubleParseAdvanced().GetReminder() != 0;
            if (ethanolContainmentIsDouble & temperatureIsDouble)
                return true;
            return false;
        }

        #endregion

        #endregion


        #region Нахождение процента содержания этанола

        /// <summary>
        /// Температура раствора
        /// </summary>
        /// <remarks>Используется при нахождении содержания этанола (правый текстБокс)</remarks>
        private string temperatureOfSolution { get; set; }

        /// <summary>
        /// Плотность раствора
        /// </summary>
        /// <remarks>Используется при нахождении содержания этанола (правый текстБокс)</remarks>
        private string densityOfSolution { get; set; }

        public void SetDataForEthanolCalc(string SolutionTemperature, string SolutionDensity)
        {
            this.temperatureOfSolution = SolutionTemperature;
            this.densityOfSolution = SolutionDensity;
        }

        #region Проверка корректности входных данных

        /// <summary>
        /// Данные "Температуры" и "Плотность раствора" пригодны для работы
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        public bool EthanolInputNumbersAreCorrect()
        {
            if (!EthanolTempAndDensityTextBoxesAreNotNULL())
                return false;
            if (!EthanolTemperatureIsWithinLimit())
                return false;
            if (!EthanolSolutionDensityIsWithinLimit())
                return false;
            return true;
        }

        /// <summary>
        /// Температура раствора внутри допутимого диапазона
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        private bool EthanolTemperatureIsWithinLimit()
        {
            try
            {
                double temperature = temperatureOfSolution.DoubleParseAdvanced();
                bool temperatureIsLowerThanMax = temperature <= 50.0;
                bool temperatureIsHigherThanMin = temperature >= -60.0;
                if (temperatureIsLowerThanMax && temperatureIsHigherThanMin)
                    return true;
                MessageBox.Show("Неверные данные! Температура раствора должна быть от -60 до 50 градусов!", "Внимание!");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось распознать значения температуры для вычисления содержания этанола.");
                return false;
            }
        }

        /// <summary>
        /// "Плотность раствора" и "Температура" имеют значение
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        private bool EthanolTempAndDensityTextBoxesAreNotNULL()
        {
            bool etanolTemperatureIsNull = string.IsNullOrEmpty(temperatureOfSolution) || string.IsNullOrWhiteSpace(temperatureOfSolution);
            bool ethanolDensityIsNull = string.IsNullOrEmpty(densityOfSolution) || string.IsNullOrWhiteSpace(densityOfSolution);

            if (etanolTemperatureIsNull || ethanolDensityIsNull)
            {
                MessageBox.Show("Для вычисления плотности все поля должны быть заполнены", "ВНИМАНИЕ!");
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Плотность раствора внутри допутимого диапазона
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        private bool EthanolSolutionDensityIsWithinLimit()
        {
            try
            {
                double density = densityOfSolution.DoubleParseAdvanced();
                bool densityIsLowerThanMax = density < 1;
                bool densityIsHigherThanMin = density > 0;
                if (densityIsLowerThanMax && densityIsHigherThanMin)
                    return true;
                MessageBox.Show("Неверные данные! Плотность раствора должна быть от 0 до 1!", "Внимание!");
                return false;
            }
            catch
            {
                MessageBox.Show("Не удалось распознать значения плотности для вычисления содержания этанола.");
                return false;
            }

        }

        #endregion

        #region Определение дробных и целых чисел во входных данных

        /// <summary>
        /// В поле "Температура" указано целое число
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        protected bool TemperatureIsInteger()
        {
            bool temperatureIsInteger = temperatureOfSolution.DoubleParseAdvanced().GetReminder() == 0;
            if (temperatureIsInteger)
                return true;
            return false;
        }

        /// <summary>
        /// В поле "Температура" указано дробное число
        /// </summary>
        /// <remarks>Нахождение процента содержания этанола</remarks>
        /// <returns></returns>
        protected bool TemperatureIsFloat()
        {
            bool temperatureIsFloat = temperatureOfSolution.DoubleParseAdvanced().GetReminder() != 0;
            if (temperatureIsFloat)
                return true;
            return false;
        }

        #endregion

        #endregion
    }
}
