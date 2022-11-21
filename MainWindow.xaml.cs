using System.Windows;
using DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation;
using DensityOfWaterAlcoholSolution.BusinessLogic.EthanolCalculation;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace DensityOfWaterAlcoholSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Regex signFilter = new Regex(@"\-");
        private readonly Regex digitalFilter = new Regex(@"\d");
        private readonly Regex pointFilter = new Regex(@"\,");
        private readonly Regex pointFilter2 = new Regex(@"\.");

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Кнопка вычисления плотности раствора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SolutionDensityBtn(object sender, RoutedEventArgs e)
        {
            FindDensity calculateDensity = new();
            calculateDensity.SetDataForDensityCalculation(LeftTempTB.Text, EthanolContainmentTB.Text);
            calculateDensity.SolutionDensity();
            SolutinDenResultTB.Text = calculateDensity.calculatedSolutionDensity.ToString();
            return;
        }

        /// <summary>
        /// Кнопка вычисления процента содержания этанола в растворе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EthanolContainmentBtn(object sender, RoutedEventArgs e)
        {
            FindEthanol calculateEthanol = new();
            calculateEthanol.SetDataForEthanolCalculation(SolutionTemperatureTB.Text, SolutionDensityTB.Text);
            calculateEthanol.SolutionEthanolContainment();
            SolutinEthanolResultTB.Text = calculateEthanol.calculatedEthanolContainment.ToString();
            return;
        }
        
        /// <summary>
        /// Проверка значений, вводимых в TextBox.
        /// </summary>
        /// <remarks>Разрешены только цифры, минус, запятая</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            if (signFilter.IsMatch(e.Text)    ||
                pointFilter.IsMatch(e.Text)   ||
                pointFilter2.IsMatch(e.Text)  ||
                digitalFilter.IsMatch(e.Text))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
