
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Core;
using System.Threading.Tasks;

namespace Core
{
    //XML
    /// <summary>
    /// Класс для исключений 
    /// </summary>
    public class ParameterException:Exception
    {
        /// <summary>
        /// Конструктор класса ParameterException
        /// с инициализацией только типа исключения
        /// </summary>
        /// <param name="exceptionType">Тип исключения</param>
        public ParameterException(ExceptionType exceptionType)
        {
            _exceptionType = exceptionType;
        }
        /// <summary>
        /// Конструктор класса ParameterException
        /// с использованием обоих полей класса
        /// </summary>
        /// <param name="exceptionType">Тип исключения</param>
        /// <param name="paramType">Тип параметра</param>
        public ParameterException(ExceptionType exceptionType,
            ParameterType paramType)
        {
            _exceptionType = exceptionType;
            _parameterType = paramType;
        }
        /// <summary>
        /// Тип параметра
        /// </summary>
        private ParameterType _parameterType;
        /// <summary>
        /// Тип исключения
        /// </summary>
        private ExceptionType _exceptionType;
        /// <summary>
        /// Свойство для поля "Тип параметров"
        /// </summary>
        public ExceptionType ExceptionType
        {
            get { return _exceptionType; }
        }
        /// <summary>
        /// Свойство для поля "Тип параметров"
        /// </summary>
        public ParameterType ParameterType 
        { 
            get { return _parameterType; }
        }
    }
}
