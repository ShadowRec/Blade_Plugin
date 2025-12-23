using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CORE
{
    public class NumericalParameter
    {
        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        private double _minValue;
        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        private double _maxValue;
        /// <summary>
        /// Значение параметра
        /// </summary>
        private double _value;
        /// <summary>
        /// Свойство, возвращающее _minValue
        /// </summary>
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// Свойство, возвращающее _maxValue
        /// </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        /// <summary>
        /// Свойство, возвращающее _value
        /// </summary>
        public double Value
        {
            get {
                return _value; 
            }
            set 
            {
                Validate(value);
                _value = value; 
            }
        }

        /// <summary>
        /// Функция валидации значения параметра 
        /// </summary>
        /// <param name="value">Валидируемое значение</param>
        /// <exception cref="ArgumentException">Исключение, вызванное тем, что вводное значение не прошло валидацию</exception>
        private void Validate(double value)
        {
            if (value < MinValue)
            {
                throw new ArgumentException("Value_small");
            }
            if (value > MaxValue) 
            {
                throw new ArgumentException("Value_TooBig");
            }
        } 
    }
}
