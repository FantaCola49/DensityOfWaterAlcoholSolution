using System.Windows;
using DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation;

namespace DensityOfWaterAlcoholSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            FindDensity calculateDensity = new FindDensity();
            calculateDensity.SolutionDensity();
            SolutinDenResultTB.Text = calculateDensity.calculatedSolutionDensity.ToString();
            return;
        }
    }
}
