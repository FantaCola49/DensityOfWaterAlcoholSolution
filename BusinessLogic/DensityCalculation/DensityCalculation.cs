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
        private int stroke;


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
        public float CalculateDensity(int temperature, int ethanolCont)
        {
            LoadDataBase();
            // Если нам даны табличные значения, тогда ищем по таблице
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) // ds пустой
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();
                if (dsTemperature == temperature &
                    density.IndexOf("-") == -1)
                    return float.Parse(density);
            }
            return 0;
        }


        /// Немыслимая погрешность в 0,005!! 
        /// TODO: ОПТИМИЗИРОВАТЬ!!! (float => double, перепроверить выведенную накануне формулу (коэффициент дельты этанола - коэфф. DensityEthanolPlusOne))

        /// <summary>
        /// Вычисление плотности, температура - целое, этанол - дробное
        /// </summary>
        /// <param name="temperature">Температура</param>
        /// <param name="ethanolCont">Содержание этанола</param>
        /// <returns>Плотность раствора</returns>
        public float CalculateDensity(int temperature, float ethanolCont)
        {
            LoadDataBase();
            for (int i = 0; i< ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 1].ToString();
                if(dsTemperature == temperature ||
                    density.IndexOf('-') == -1)
                {
                    // Дали этанол 21,73 => нашли плотность по этанолу 21
                    string STR_nearestDensityWithEthanolAsGiven = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 1].ToString();
                    // Дали этанол 21,73 => нашли плотность по этанолу 22
                    string STR_nearestDensityWithEthanolPLUSone = ds.Tables[0].Rows[i][Convert.ToInt32(ethanolCont) + 2].ToString();
                    //Убеждаемся, что выбранные числа содержания этанола существуют
                    if((STR_nearestDensityWithEthanolAsGiven.IndexOf("-") == -1) && (STR_nearestDensityWithEthanolPLUSone.IndexOf("-") == -1))
                    {
                        //После проверки парсим значения
                        float densityEthanolAsGiven = float.Parse(STR_nearestDensityWithEthanolAsGiven);
                        float densityEtanolPLUSOne = float.Parse(STR_nearestDensityWithEthanolPLUSone);
                        float densityDelta = densityEthanolAsGiven - densityEtanolPLUSOne;
                        // Находим целую часть от данной доли этанола в растворе
                        int solidEthanolPLUSone = Convert.ToInt32(ethanolCont) + 1;
                        float ethanolDelta = (float)(solidEthanolPLUSone - ethanolCont);
                        float solutionDensity = (float)Math.Abs(Math.Round((ethanolDelta * densityDelta + densityEtanolPLUSOne), 5));
                        return solutionDensity;
                    }
                }
            }

            return 0;
        }

        /// Немыслимая погрешность в 0,005!! 
        /// TODO: ОПТИМИЗИРОВАТЬ!!! (float => double, перепроверить выведенную накануне формулу (коэффициент дельты этанола - коэфф. DensityEthanolPlusOne))
        /// Можно проверить по формуле Стечкина


        /// <summary>
        /// Вычисление плотности, температура - дробное, этанол - целое
        /// </summary>
        /// <param name="temperature">Температура</param>
        /// <param name="ethanolCont">Содержание этанола</param>
        /// <returns>Плотность раствора</returns>
        public float CalculateDensity(float temperature, int ethanolCont)
        {
            LoadDataBase();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int dsTemperature = int.Parse(ds.Tables[0].Rows[i][0].ToString());
                string density = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();
                if (dsTemperature == Convert.ToInt32(temperature) || //ищем таблицу, первый узел которой(temperature) будет совпадать с целой частью числа температуры
                    density.IndexOf("-") == -1) // Убеждаемся, что существует значение плотности
                {
                    // (Ввели 41,2124 => нашли плотность по 41) c указанным процентом этанола
                    string STR_nearestDensityWithTempAsGiven = ds.Tables[0].Rows[i][ethanolCont + 1].ToString();
                    // (Ввели 41,2124 => нашли плотность по 42) с указанным процентом этанола
                    string STR_nearestDensityWithTemp1GradeMore = ds.Tables[0].Rows[i + 1][ethanolCont + 1].ToString();
                    //Убеждаемся, что выбранные числа содержания этанола существуют
                    if ((STR_nearestDensityWithTempAsGiven.IndexOf("-") == -1) && (STR_nearestDensityWithTemp1GradeMore.IndexOf("-") == -1))
                    {
                        float densityTempAsGiven = float.Parse(STR_nearestDensityWithTempAsGiven);
                        float densityTempPLUSOne = float.Parse(STR_nearestDensityWithTemp1GradeMore);
                        float densityDelta = densityTempAsGiven - densityTempPLUSOne;
                        double nearestIntTemperature = (Convert.ToInt32(temperature) + 1);
                        float temperatureDelta = (float)(nearestIntTemperature - temperature);
                        //Упаковочный Ад, я знаю. Ну а как ты хотел? Всё во имя точности!!
                        float solutionDensity = (float)Math.Abs(Math.Round(temperatureDelta * densityDelta + densityTempPLUSOne, 5));
                        return solutionDensity;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Вычисление плотности, температура и этанол дробные
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="ethanolCont"></param>
        /// <returns></returns>
        public float CalculateDensity(float temperature, float ethanolCont)
        {
            return 0;
        }
        #endregion


    }
}
