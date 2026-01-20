using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    //TODO: XML done
    /// <summary>
    /// Тип крепления
    /// </summary>
    public enum BindingType
    {
        /// <summary>
        /// Всадное крепление
        /// </summary>
        Insert, // 
        /// <summary>
        /// Сквозное крепление
        /// </summary>
        Through,
        /// <summary>
        ///Накладное крепления
        /// </summary>
        ForOverlays,
        /// <summary>
        /// Крепление отсутствует 
        /// </summary>
        None
    }
}
