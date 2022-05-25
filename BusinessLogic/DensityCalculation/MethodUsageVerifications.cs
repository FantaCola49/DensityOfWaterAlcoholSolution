using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DensityOfWaterAlcoholSolution.BusinessLogic.DensityCalculation
{
    /// <summary>
    /// Класс, определяющий, какие методы использовать для вычислений
    /// </summary>
    internal sealed class MethodUsageVerifications : Verifications
    {
        /// <summary>
        /// Определение метода для вычисления плотности раствора
        /// </summary>
        /// <remarks>temperature/ethanolCont |1 int/int | 2 int/float | 3 float/int | 4 float/float</remarks>
        /// <returns>Номер метода [1;4]</returns>
        public byte DensityCalculationMethodNumber()
        {
            byte methodNumber = 0;

            if (IntEthanolContainment_IntTemperature())
                methodNumber = 1;
            if(IntEthanolContainment_DoubleTemperature())
                methodNumber = 2;
            if(DoubleEthanolContainment_IntTemperature())
                methodNumber = 3;
            if (DoubleEthanolContainment_DoubleTemperature())
                methodNumber = 4;         
            return methodNumber;
        }

        /// <summary>
        /// Определение метода для вычисления процента содержания этанола
        /// </summary>
        /// <returns>Номер метода [1;4]</returns>
        public byte EthanolContainCalculationMethodNumber() ////TODO!!!!!!!!!!!!!!!
        {
            byte methodNumber = 0;
            
            return methodNumber;
        }
    }
}
