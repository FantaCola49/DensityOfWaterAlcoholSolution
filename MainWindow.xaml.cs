using System.Windows;
using DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation;
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
            MethodUsageVerifications verificate = new MethodUsageVerifications();

            verificate.solutionTemperature = LeftTempTB.Text;
            verificate.ethanolContainment = EthanolContainmentTB.Text;

            if (!verificate.DensityInputNumbersAreCorrect())
                return;
            
            calculateDensity.SolutionDensity();
            SolutinDenResultTB.Text = calculateDensity.calculatedSolutionDensity.ToString();
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
            if (signFilter.IsMatch(e.Text))
                e.Handled = false;
            else if (digitalFilter.IsMatch(e.Text))
                e.Handled = false;
            else if (pointFilter.IsMatch(e.Text))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
