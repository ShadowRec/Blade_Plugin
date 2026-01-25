using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    //TODO: XML done
    /// <summary>
    /// Типы параметров
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// Длина клинка
        /// </summary>
        BladeLength, 
        /// <summary>
        /// Длина крепления
        /// </summary>
        BindingLength, 
        /// <summary>
        /// Ширина клинка
        /// </summary>
        BladeWidth, 
        /// <summary>
        /// Толщина клинка
        /// </summary>
        BladeThickness, 
        /// <summary>
        /// Ширина лезвия
        /// </summary>
        EdgeWidth, 
        /// <summary>
        /// Длина острия
        /// </summary>
        PeakLenght,
        /// <summary>
        /// Тип крепления
        /// </summary>
        BindingType,
        /// <summary>
        /// Тип клинка
        /// </summary>
        BladeType,
        /// <summary>
        /// Тип серрейтора
        /// </summary>
        SerreitorType,
        /// <summary>
        /// Глубина серрейтора
        /// </summary>
        SerreitorDepth,
        /// <summary>
        /// Длина серрейтора
        /// </summary>
        SerreitorLength
    }
}
