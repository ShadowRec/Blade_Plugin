using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    //TODO: XML done
    /// <summary>
    /// Численные параметры клинка
    /// </summary>
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
            //TODO: validation done
            get { return _minValue; }
            set {
                if (value > 0)
                {
                    if (MinValue != 0 && MaxValue != 0)
                    {
                        if (value < MaxValue)
                        {
                            _minValue = value;
                        }
                        else
                        {
                            throw new ParameterException(
                                ExceptionType.MinGreaterMaxException
                                );
                        }
                    }
                    else
                    {
                        _minValue = value;
                    }
                }
                else
                {
                    throw new ParameterException(
                        ExceptionType.MinValueNegativeException
                        );
                }
            }
        }
        /// <summary>
        /// Свойство, возвращающее _maxValue
        /// </summary>
        public double MaxValue
        {
            //TODO: validation done

            get { return _maxValue; }
            set
            {
                if(value > 0)
                {
                    if (MinValue != 0 && MaxValue != 0)
                    {
                        if (value > MinValue)
                        {
                            _maxValue = value;
                        }
                        else
                        {
                            throw new ParameterException(
                                ExceptionType.MaxLesserrMinException
                                );
                        }
                    }
                    else
                    {
                        _maxValue = value;
                    }
                }
                else
                {
                    throw new ParameterException(
                        ExceptionType.MaxValueNegativeException
                        );
                }
            }
        }

        /// <summary>
        /// Свойство, возвращающее _value
        /// </summary>
        public double Value
        {
            get 
            {
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
        /// <exception cref="ArgumentException">Исключение, вызванное тем,
        /// что вводное значение не прошло валидацию</exception>
        private void Validate(double value)
        {
            //TODO: refactor done
            if (value < MinValue)
            {
                throw new ParameterException(
                    ExceptionType.TooSmallException);
            }
            if (value > MaxValue) 
            {
                throw new ParameterException(
                    ExceptionType.TooBigException);
            }
        } 
    }
}
