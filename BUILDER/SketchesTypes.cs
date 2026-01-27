using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchesType
{
    //TODO: XML DONE
    /// <summary>
    /// Класс для хранения строковых констант
    /// </summary>
    static class SketchesTypes
    {
        /// <summary>
        /// Строка для обозначения основного скетча
        /// </summary>
        public const  string MainString = "Main";
        /// <summary>
        /// Строка для обозначения направляющего скетча 
        /// для лезвия
        /// </summary>
        public const string EdgeDirectionString = "EdgeDirection";
        /// <summary>
        /// Строка для обозначения скетча заточки
        /// </summary>
        public const string EdgeString = "Edge";
        /// <summary>
        /// Строка для обозначения скетча для 
        /// отверстий
        /// </summary>
        public const string HolesString = "Holes";
        /// <summary>
        /// Строка для обозначения скетча заточки серрейтора
        /// </summary>
        public const string SerreitorString = "Serreitor";
    }
}
