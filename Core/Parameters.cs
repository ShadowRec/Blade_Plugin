using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    //TODO: XML done
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
            bladelength.MinValue = 30;
            bladelength.MaxValue = 1200;

            //Инициализация параметра ширины клинка
            NumericalParameter bladewidth = new NumericalParameter();
            bladewidth.MinValue = 9;
            bladewidth.MaxValue = 60;

            //Инициализация параметра толщины клинка
            NumericalParameter bladethick = new NumericalParameter();
            bladethick.MinValue = 1;
            bladethick.MaxValue = 3;

            //Инициализация параметра ширины лезвия
            NumericalParameter edgewidth = new NumericalParameter();

            //Инициализация параметра длины острия
            NumericalParameter peaklength = new NumericalParameter();

            //Инициализация параметра длины крепления
            NumericalParameter binlength = new NumericalParameter();

            //Инициализация параметра длины серрейтора
            NumericalParameter serrlength = new NumericalParameter();

            //Инициализация параметра глубины серрейтора
            NumericalParameter serrdepth =  new NumericalParameter();

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
                [ParameterType.SerreitorDepth] = serrdepth
            };
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

        public bool SerreitorExistance { get; set; }

        /// <summary>
        /// Перечень численных параметров
        /// </summary>
        public Dictionary<ParameterType, NumericalParameter>
            NumericalParameters
        { get; set; }

        /// <summary>
        /// Выставляет максимальное и минимальное(Если такое задано) 
        /// значения для параметра
        /// </summary>
        /// <param name="independ">Параметр на основе которого будет 
        /// вычисляться макс и мин значения</param>
        /// <param name="depend">Параметр к которому будет применяться 
        /// максимально и минимальное значение</param>
        /// <param name="maxratio">Соотношение величин для 
        /// максимального значения</param>
        /// <param name="minratio">Соотношение величин для 
        /// минимального значения</param>
        public void SetDependencies(NumericalParameter independ,
            NumericalParameter depend, double maxratio,
            double minratio = 0)
        {
            if (maxratio > 0 && minratio >= 0)
            {
                depend.MaxValue = independ.Value * maxratio;
                if (minratio != 0)
                {
                    depend.MinValue = independ.Value * minratio;
                }
                else
                {
                    depend.MinValue = depend.MaxValue * 0.1;
                }
            }
            else
            {
                //TODO: refactor DONE
                throw new ParameterException(ExceptionType.RatioNegativeException);
            }
        }
    }
}