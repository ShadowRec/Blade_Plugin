using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Тип исключения
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// Исключение пустого значения
        /// </summary>
        NullException,

        /// <summary>
        /// Исключение некорректного значения
        /// </summary>
        InvalidException,

        /// <summary>
        /// Исключение значение ниже допустимого
        /// диапазона
        /// </summary>
        TooSmallException,

        /// <summary>
        /// Исключение значение выше допустимого
        /// диапазона
        /// </summary>
        TooBigException,

        /// <summary>
        /// Исключение максимального значение параметра
        /// ниже нуля
        /// </summary>
        MaxValueNegativeException,

        /// <summary>
        /// Исключение минимального значение параметра
        /// ниже нуля
        /// </summary>
        MinValueNegativeException,

        /// <summary>
        /// Исключение минимального значение параметра
        /// больше максимального
        /// </summary>
        MinGreaterMaxException,

        /// <summary>
        /// Исключения соотношения ниже нуля
        /// </summary>
        RatioNegativeException,

        /// <summary>
        /// Исключение ошибки открытия компаса
        /// </summary>
        KompasOpenErrorException,

        /// <summary>
        /// Исключение ошибки постройки детали 
        /// </summary>
        PartBuildingErrorException

    }
}
