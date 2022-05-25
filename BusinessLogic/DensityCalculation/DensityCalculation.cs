using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation
{
    /// <summary>
    /// Класс методов вычислений плотности раствора
    /// </summary>
    /// <remarks>Методы варьируются в зависимости от типа представленных данных (целое/дробное)</remarks>
    internal class DensityCalculation
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

        #region Методы вычисления плотности

        /// <summary>
        /// Вычисление плотности, если оба числа целые
        /// </summary>
        /// <param name="temperature">Температура</param>
        /// <param name="ethanolCont">Содержание этанола</param>
        /// <returns>Плотность раствора</returns>
        public double CalculateDensity(int temperature, int ethanolCont)
        {
            LoadDataBase();
            // Если нам даны табличные значения, тогда ищем по таблице
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();
                if (dsTemperature == temperature &
                    density.IndexOf("-") == -1)
                    return double.Parse(density);
            }
            MessageBox.Show("Значений нет!");
            return 0;
        }

        /// <summary>
        /// Вычисление плотности, температура - целое, этанол - дробное
        /// </summary>
        /// <param name="temperature">Температура</param>
        /// <param name="ethanolCont">Содержание этанола</param>
        /// <returns>Плотность раствора</returns>
        public double CalculateDensity(int temperature, double ethanolCont)
        {
            LoadDataBase();
            for (int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 1].ToString();
                if(dsTemperature == temperature &&
                    density.IndexOf('-') == -1)
                {
                    // Дали этанол 21,73 => нашли плотность по этанолу 22
                    string STR_nearestDensityWithEthanolAsGiven = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 1].ToString();
                    // Дали этанол 21,73 => нашли плотность по этанолу 21
                    string STR_nearestDensityWithEthanolMENOSone = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont)].ToString();
                    //Убеждаемся, что выбранные числа содержания этанола существуют
                    if((STR_nearestDensityWithEthanolAsGiven.IndexOf("-") == -1) && (STR_nearestDensityWithEthanolMENOSone.IndexOf("-") == -1))
                    {
                        //После проверки парсим значения
                        double densityEthanolAsGiven = double.Parse(STR_nearestDensityWithEthanolAsGiven);
                        double densityEtanolMENOSOne = double.Parse(STR_nearestDensityWithEthanolMENOSone);
                        double densityDelta = densityEtanolMENOSOne - densityEthanolAsGiven;
                        // Находим целую часть от данной доли этанола в растворе
                        double ethanolReminder = Math.Ceiling(ethanolCont) - ethanolCont;

                        double solutionDensity = Math.Round((densityDelta * ethanolReminder) + densityEthanolAsGiven, 5);
                        return solutionDensity;
                    }
                }
            }
            MessageBox.Show("Значений нет!");

            return 0;
        }

        /// <summary>
        /// Вычисление плотности, температура - дробное, этанол - целое
        /// </summary>
        /// <param name="temperature">Температура</param>
        /// <param name="ethanolCont">Содержание этанола</param>
        /// <returns>Плотность раствора</returns>
        public double CalculateDensity(double temperature, int ethanolCont)
        {
            LoadDataBase();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();

                if( dsTemperature == Math.Truncate(temperature) && //ищем таблицу, первый узел которой(temperature) будет совпадать с целой частью числа температуры
                    density.IndexOf("-") == -1) // Убеждаемся, что существует значение плотности
                {
                    // (Ввели 41,2124 => нашли плотность по 41) c указанным процентом этанола
                    string STR_nearestDensityWithTempAsGiven = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();
                    // (Ввели 41,2124 => нашли плотность по 41) с указанным процентом этанола по температуре на 1 больше
                    string STR_nearestDensityWithTemp1GradeMore = ds.Tables[0].Rows[i + 1][ethanolCont + 1].ToString();
                    //Убеждаемся, что выбранные числа содержания этанола существуют
                    if ((STR_nearestDensityWithTempAsGiven.IndexOf("-") == -1) && (STR_nearestDensityWithTemp1GradeMore.IndexOf("-") == -1))
                    {
                        // Считаю по своей формуле
                        double densityTempAsGiven = double.Parse(STR_nearestDensityWithTempAsGiven);
                        double densityTempPLUSOne = double.Parse(STR_nearestDensityWithTemp1GradeMore);
                        double densityDelta = densityTempAsGiven - densityTempPLUSOne;
                        double temperatureReminder = Math.Ceiling(temperature) - temperature;

                        double solutionDensity = Math.Round((densityDelta * temperatureReminder) + densityTempPLUSOne, 5);
                        return solutionDensity;
                    }
                }
            }
            MessageBox.Show("Значений нет!");
            return 0;
        }

        /// <summary>
        /// Вычисление плотности, температура и этанол дробные
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="ethanolCont"></param>
        /// <returns></returns>
        public double CalculateDensity(double temperature, double ethanolCont)
        {
            LoadDataBase();
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont)+ 1].ToString();

                if( dsTemperature == Math.Truncate(temperature) &&
                    density.IndexOf("-") == -1)
                {
                    string STR_nearestDensityWithTempAsGiven = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 1].ToString();
                    string STR_nearestDensityWithTemp1GradeMore = ds.Tables[0].Rows[i + 1][Convert.ToInt32(ethanolCont) + 1].ToString();
                    string STR_nearestDensityWithEthanolMENOSone = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont)].ToString();
                    string STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone = ds.Tables[0].Rows[i + 1][Convert.ToInt32(ethanolCont)].ToString();
                    if( STR_nearestDensityWithTempAsGiven != "-" &&
                        STR_nearestDensityWithTemp1GradeMore != "-" &&
                        STR_nearestDensityWithEthanolMENOSone != "-" &&
                        STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone != "-"
                        )
                    {
                        double densityTempAsGiven = double.Parse(STR_nearestDensityWithTempAsGiven);
                        double densityTempPLUSOne = double.Parse(STR_nearestDensityWithTemp1GradeMore);
                        double densityDelta_Temperature = densityTempAsGiven - densityTempPLUSOne;
                        double temperatureReminder = Math.Ceiling(temperature) - temperature;

                        double solutionDensity_A = (densityDelta_Temperature * temperatureReminder) + densityTempPLUSOne;
                        
                        double densityEtanolMENOSOne = double.Parse(STR_nearestDensityWithEthanolMENOSone);
                        double densityEthanolMENOSOneTemperaturePlusOne = double.Parse(STR_nearestDensityWithEthanolMENOSoneTemperaturePLUSone);
                        double ethanolReminder = Math.Ceiling(ethanolCont) - ethanolCont;

                        double solutionDensity_B = (temperatureReminder * (densityEtanolMENOSOne - densityEthanolMENOSOneTemperaturePlusOne)) + densityEthanolMENOSOneTemperaturePlusOne;


                        double solutionDensity = Math.Round((ethanolReminder * (solutionDensity_B - solutionDensity_A)) + solutionDensity_A, 5);
                        return solutionDensity;
                    }
                }
            }
            MessageBox.Show("Значений нет!");
            return 0;
        }
        #endregion
    }
}
