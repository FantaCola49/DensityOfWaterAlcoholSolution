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
        /// <returns>Номер метода [1;4]</returns>
        public byte DensityCalculationMethodNumber()
        {
            byte methodNumber = 0;

            if (IntEthanolContainment_IntTemperature())
                methodNumber = 1;
            if(IntEthanolContainment_FloatTemperature())
                methodNumber = 2;
            if(FloatEthanolContainment_IntTemperature())
                methodNumber = 3;
            if (FloatEthanolContainment_FloatTemperature())
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
