using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    //TODO: XML DONE
    /// <summary>
    /// Тип серрейтора
    /// </summary>
    public enum SerreitorType
    {
        /// <summary>
        /// Чередующийся тип серрейтора
        /// </summary>
        AlternationSerreitor,

        /// <summary>
        /// Постоянный большой тип серрейтора
        /// </summary>
        ConstBigSerreitor,

        /// <summary>
        /// Постоянный маленький тип серрейтора
        /// </summary>
        ConstSmallSerreitor
    }
}
