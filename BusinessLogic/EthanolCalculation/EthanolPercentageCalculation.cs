﻿using DensityOfWaterAlcoholSolution.Helpers;
using System;
using System.Data;
using System.IO;
using System.Windows;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.EthanolCalculation
{
    /// <summary>
    /// Класс методов вычислений процента содержания этанола
    /// </summary>
    /// <remarks>Методы варьируются в зависимости от типа представленных данных (целое/дробное)</remarks>
    public class EthanolPercentageCalculation
    {
        #region Поля
        private DataSet ds = new DataSet();
        private string dirFile = Directory.GetCurrentDirectory();
        #endregion

        /// <summary>
        /// Загрузка данных из xml-файла
        /// </summary>
        private void LoadDataBase()
        {
            try
            {
                this.ds.ReadXml($@"{dirFile}\DensityData\data2.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка при загрузки базы данных");
            }
        }

        #region Методы вычисления процента содержания этанола

        /// <summary>
        /// Метод 1: просто пробуем найти табличное значение содержания этанола
        /// </summary>
        /// <remarks>Плотность раствора - табличное значение</remarks>
        /// <param name="temperature">температура раствора</param>
        /// <param name="density">плотность раствора</param>
        /// <returns>Процент содержания этанола</returns>
        public double CalculateTableNumberOfEthanol(int temperature, double density)
        {
            LoadDataBase();
            string strDensity = density.ToString();
            // Если нам даны табличные значения, тогда ищем по таблице
            for (int t = 0; t < ds.Tables[0].Rows.Count; t++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[t][0].ToString());
                if (dsTemperature == temperature)
                {
                    for (int G = 0; G <= 100; G++)
                    {
                        if (ds.Tables[0].Rows[t][G + 1].ToString().Equals(strDensity))
                            return G;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Метод 2: находим этанол, если плотность раствора не табличное значение
        /// </summary>
        /// <param name="temperature">температура раствора</param>
        /// <param name="density">плотность раствора</param>
        /// <returns>Процент содержания этанола</returns>
        public double CalculateEthanol(int temperature, double density)
        {
            LoadDataBase();
            string strDensity = density.ToString();

            for (int t = 0; t < ds.Tables[0].Rows.Count; t++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[t][0].ToString());
                if (dsTemperature == temperature)
                {
                    for (int G = 0; G <= 100; G++)
                    {
                        if (ds.Tables[0].Rows[t][G].ToString().DoubleParseAdvanced() >= density &&
                            ds.Tables[0].Rows[t][G + 1].ToString().DoubleParseAdvanced() <= density)
                        {
                            string STR_nearestDensityWithEthanolAsGiven = ds.Tables[0].Rows[t][G].ToString();
                            string STR_nearestDensityWithEthanol1PercentMore = ds.Tables[0].Rows[t][G + 1].ToString();
                            double densityEthanolAsGiven = STR_nearestDensityWithEthanolAsGiven.DoubleParseAdvanced();
                            double densityEthanolPLUSne = STR_nearestDensityWithEthanol1PercentMore.DoubleParseAdvanced();
                            double densityDelta = densityEthanolAsGiven - densityEthanolPLUSne;

                            double ethanolContainment = G - 1 + Math.Round(1 - ((density - densityEthanolPLUSne) / (densityDelta)), 4);

                            return ethanolContainment;
                        }
                    }
                }
            }
            MessageBox.Show("Не удалось найти процент содержания этанола. \nВероятно, раствор с такими характеристиками просто не существует.", "Внимание!");
            return 0;
        }

        /// <summary>
        /// Метод 3: находим этанол, если плотность не табличное значение и температура не табличное значение
        /// </summary>
        /// <param name="temperature">температура раствора</param>
        /// <param name="density">плотность раствора</param>
        /// <returns>Процент содержания этанола</returns>
        public double CalculateEthanol(double temperature, double density)
        {
            LoadDataBase();
            string strDensity = density.ToString();
            for (int t = 0; t < ds.Tables[0].Rows.Count; t++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[t][0].ToString());
                if (dsTemperature == Math.Truncate(temperature))
                {
                    for (int G = 0; G <= 100; G++)
                    {
                        if ((ds.Tables[0].Rows[t][G]).ToString().DoubleParseAdvanced() >= density &
                            (ds.Tables[0].Rows[t][G + 1]).ToString().DoubleParseAdvanced() <= density)
                        {
                            string STR_nearestDensityWithTempAsGiven = ds.Tables[0].Rows[t][G + 1].ToString();
                            string STR_nearestDensityWithTemp1GradeMore = ds.Tables[0].Rows[t + 1][G + 1].ToString();
                            string STR_nearestDensityWithEthanolMENOSone = ds.Tables[0].Rows[t][G].ToString();
                            string STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone = ds.Tables[0].Rows[t + 1][G].ToString();
                            if (STR_nearestDensityWithTempAsGiven != "-" &&
                                STR_nearestDensityWithTemp1GradeMore != "-" &&
                                STR_nearestDensityWithEthanolMENOSone != "-" &&
                                STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone != "-"
                                )
                            {
                                double densityTempAsGiven = STR_nearestDensityWithTempAsGiven.DoubleParseAdvanced();
                                double densityTempPLUSOne = STR_nearestDensityWithTemp1GradeMore.DoubleParseAdvanced();
                                double densityDelta_Temperature = densityTempAsGiven - densityTempPLUSOne;
                                double temperatureReminder = temperature.GetReminderPriorNextSolidNumber();

                                double solutionDensity_A = (densityDelta_Temperature * temperatureReminder) + densityTempPLUSOne;

                                double densityEtanolMENOSOne = STR_nearestDensityWithEthanolMENOSone.DoubleParseAdvanced();
                                double densityEthanolMENOSOneTemperaturePlusOne = STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone.DoubleParseAdvanced();
                                double solutionDensity_B = (temperatureReminder * (densityEtanolMENOSOne - densityEthanolMENOSOneTemperaturePlusOne)) + densityEthanolMENOSOneTemperaturePlusOne;

                                double densityDelta = solutionDensity_B - solutionDensity_A;

                                double ethanolPercentage = (G - 1) + (1 - ((density - solutionDensity_A) / densityDelta));

                                return Math.Round(ethanolPercentage, 4);
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Не удалось найти процент содержания этанола. \nВероятно, раствор с такими характеристиками просто не существует.", "Внимание!");
            return 0;
        }

        #endregion

    }
}
