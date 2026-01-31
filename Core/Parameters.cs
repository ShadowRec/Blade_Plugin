using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Класс, хранящий все параметры клинка
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Конструктор класса Parameter.
        /// Содержит инициализацию численных параметров клинка
        /// </summary>
        public Parameters()
        {
            //Инициализация параметра длины клинка
            NumericalParameter bladelength = new NumericalParameter();
            bladelength.SetMinAndMax(80, 1200);
            bladelength.Value = 300;

            //Инициализация параметра ширины клинка
            NumericalParameter bladewidth = new NumericalParameter();
            bladewidth.SetMinAndMax(9, 60);
            bladewidth.Value = 40;
            //Инициализация параметра толщины клинка
            NumericalParameter bladethick = new NumericalParameter();
            bladethick.SetMinAndMax(1, 3);
            bladethick.Value = 2;
            NumericalParameter serreitorNumber = new NumericalParameter();
            serreitorNumber.SetMinAndMax(8, 32);
            serreitorNumber.Value = 8;    
            //Инициализация параметра ширины лезвия
            NumericalParameter edgewidth = new NumericalParameter();
            //Инициализация параметра длины острия
            NumericalParameter peaklength = new NumericalParameter();
            //Инициализация параметра длины крепления
            NumericalParameter binlength = new NumericalParameter();
            //Инициализация параметра длины серрейтора
            NumericalParameter serrlength = new NumericalParameter();
            BladeExistence = true;
            BladeType = false;
            BindingType = BindingType.ForOverlays;
            SerreitorType = SerreitorType.AlternationSerreitor;
            SerreitorExistence = true;

            //Занесения значений параметров в словарь 
            //с соответствующими ключами
            NumericalParameters = new Dictionary<
                ParameterType, NumericalParameter>()
            {
                [ParameterType.BladeLength] = bladelength,
                [ParameterType.BladeWidth] = bladewidth,
                [ParameterType.BladeThickness] = bladethick,
                [ParameterType.EdgeWidth] = edgewidth,
                [ParameterType.PeakLenght] = peaklength,
                [ParameterType.BindingLength] = binlength,
                [ParameterType.SerreitorLength] = serrlength,
                [ParameterType.SerreitorNumber] = serreitorNumber,
                
            };
            foreach (var param in _parametersRatios)
            {
                SetDependencies(param.Key.Item1, param.Key.Item2);
            }
            edgewidth.Value = 14;
            peaklength.Value = 46;
            binlength.Value = 300;
            serrlength.Value = 90;
        }

        /// <summary>
        /// Словарь соотношений между параметрами.
        /// </summary>
        private Dictionary<(ParameterType, ParameterType), (double, double)>
            _parametersRatios = new Dictionary<(ParameterType, ParameterType),
                (double, double)>()
            {
                [(ParameterType.BladeLength, ParameterType.PeakLenght)]
                    = (1.0 / 6.0, 1.0 / 10),
                [(ParameterType.BladeLength, ParameterType.BindingLength)]
                    = (1, 0),
                [(ParameterType.BladeWidth, ParameterType.EdgeWidth)] =
                    (3.0 / 6.0, 1.0 / 6.0),
                [(ParameterType.BladeLength,
                    ParameterType.SerreitorLength)] =
                    (5.0 / 10.0, 3 / 10.0),
            };


        /// <summary>
        /// Словарь соотношений для типов креплений.
        /// </summary>
        private Dictionary<BindingType, (double, double)> _bindingRatios =
            new Dictionary<BindingType, (double, double)>()
            {
                [BindingType.ForOverlays] = (1, 0),
                [BindingType.Insert] = (3.0 / 4.0, 0),
                [BindingType.Through] = (1, 0),
                [BindingType.None] = (1, 0)
            };

        //TODO: XML
        public Dictionary<(ParameterType, 
            ParameterType), (double, double)> ParametersRatios
        {
            get
            {
                return _parametersRatios;
            }
        }

        //TODO: XML
        public Dictionary<BindingType, (double, double)> BindingRatios
        {
            get 
            {
                return _bindingRatios;
            }
        }
        /// <summary>
        /// False - Острие у клинка нету, 
        /// True - Острие у клинка есть
        /// </summary>
        public bool BladeExistence { get; set; }

        /// <summary>
        /// False - односторонний, 
        /// True - Двусторонний
        /// </summary>
        public bool BladeType { get; set; }

        /// <summary>
        /// Тип крепления
        /// </summary>
        public BindingType BindingType { get; set; }

        /// <summary>
        /// Тип серрейтора
        /// </summary>
        public SerreitorType SerreitorType { get; set; }

        /// <summary>
        /// False - Серрейтора у клинка нету, 
        /// True -  Серрейтор у клинка есть
        /// </summary>
        public bool SerreitorExistence { get; set; }

        /// <summary>
        /// Перечень численных параметров
        /// </summary>
        public Dictionary<ParameterType, NumericalParameter> 
            NumericalParameters { get; set; }

        /// <summary>
        /// Выставляет максимальное и минимальное(Если такое задано) 
        /// значения для параметра
        /// </summary>
        /// <param name="independType">Параметр на основе которого будет 
        /// вычисляться макс и мин значения</param>
        /// <param name="dependType">Параметр к которому будет применяться 
        /// максимально и минимальное значение</param>
        public void SetDependencies(ParameterType independType,
            ParameterType dependType)
        {
            var maxratio = _parametersRatios[(independType, 
                dependType)].Item1;
            var minratio = _parametersRatios[(independType, 
                dependType)].Item2;
            var independ = NumericalParameters[independType];
            var depend = NumericalParameters[dependType];

            if (maxratio > 0 && minratio >= 0)
            {
                var tmpMinRatioCoefficient =
                    minratio != 0
                    ? minratio
                    : maxratio * 0.1;

                depend.SetMinAndMax(
                    independ.Value * tmpMinRatioCoefficient,
                    independ.Value * maxratio);
            }
            else
            {
                throw new ParameterException(
                    ExceptionType.RatioNegativeException);
            }
        }
        /// <summary>
        /// Установка соотношения для крепления
        /// </summary>
        /// <param name="binType">Тип крепления</param>
        public void SetBindingRatios(
            BindingType binType)
        {
            ParametersRatios[(ParameterType.BladeLength,
                    ParameterType.BindingLength)] = _bindingRatios[binType];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="independParamType">Параметр на основе
        /// которого будет 
        /// вычисляться макс и мин значения</param>
        /// <param name="dependParamType">Параметр к которому 
        /// будет применяться 
        /// максимально и минимальное значение</param>
        /// <param name="ratios"> соотношение</param>
        public void SetRatios(ParameterType independParamType,
            ParameterType dependParamType,
           (double maxratio, double minratio) ratios)
        {
            _parametersRatios[(independParamType,dependParamType)]=
                ratios;
        }
    }
}