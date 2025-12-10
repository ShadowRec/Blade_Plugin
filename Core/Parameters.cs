using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BladePlugin
{
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
            bladelength.MaxValue= 1200;

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
            

            //Занесения значений параметров в словарь с соотвествующими ключами
            NumericalParameters = new Dictionary<ParameterType,NumericalParameter>()
            {
                [ParameterType.BladeLength] = bladelength ,
                [ParameterType.BladeWidth] = bladewidth,
                [ParameterType.BladeThickness] = bladethick,
                [ParameterType.EdgeWidth] = edgewidth,
                [ParameterType.PeakLenght] = peaklength,
                [ParameterType.BindingLength] = binlength,
            };

        }
        public bool BladeExistance { get; set; } // False - Острие у клинка нету, True - Острие у клинка есть
        public bool BladeType {  get; set; } // False - односторонний, True - Двусторонний
        public BindingType BindingType { get; set; }
        public Dictionary<ParameterType, NumericalParameter> NumericalParameters { get; set; }

        /// <summary>
        /// Выставляет максимальное и минимальное(Если такое задано) значения для параметра
        /// </summary>
        /// <param name="independ"> Параметр на основе которого будет вычислятся макс и мин значения</param>
        /// <param name="depend"> Параметр к которому будет примернятся максимально и минимальное значение</param>
        /// <param name="maxratio"> Соотношение велечин для максимального значения</param>
        /// <param name="minratio">Соотношение велечин для минимального значения</param>
        public void SetDependenses(NumericalParameter independ,NumericalParameter depend, double maxratio, double minratio=0)
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
    }
}
