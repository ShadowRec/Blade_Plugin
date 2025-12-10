using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BladePlugin
{
    public class Builder
    {
        private KompasWrapper _wrapper;
        const double ORIGIN = 0;
       
        /// <summary>
        /// Функция, запускающая и проводящая процесс постройки
        /// </summary>
        /// <param name="parameters">Параметры для постройки</param>
        public void BuildBlade(Parameters parameters)
        {
            _wrapper = new KompasWrapper();
            StartCreating();
            if(!parameters.BladeType)
            {
                DrawMainSketch(parameters.NumericalParameters[ParameterType.BladeLength].Value,
                                          parameters.NumericalParameters[ParameterType.PeakLenght].Value,
                                          parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                                          parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                                          parameters.NumericalParameters[ParameterType.BindingLength].Value,
                                          parameters.BindingType,
                                          parameters.BladeExistance,
                                          parameters.BladeType
                               );
                DrawDirection(parameters.NumericalParameters[ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                    true, parameters.BladeExistance
                    );
                DrawEdgeForm(parameters.NumericalParameters[ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value);
                ExtrudeMainPart(parameters.NumericalParameters[ParameterType.BladeThickness].Value);
                CreateEdge();
                MakeHoles(parameters.NumericalParameters[ParameterType.BindingLength].Value,
                          parameters.NumericalParameters[ParameterType.BladeWidth].Value
                    );
            }
            else
            {
                DrawMainSketch(parameters.NumericalParameters[ParameterType.BladeLength].Value,
                                          parameters.NumericalParameters[ParameterType.PeakLenght].Value,
                                          parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                                          parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                                          parameters.NumericalParameters[ParameterType.BindingLength].Value,
                                          parameters.BindingType,
                                          parameters.BladeExistance,
                                          parameters.BladeType
                               );
                DrawDirection(parameters.NumericalParameters[ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                    parameters.BladeType,
                    true,
                    parameters.BladeExistance
                    );
                DrawEdgeForm(parameters.NumericalParameters[ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value);
                ExtrudeMainPart(parameters.NumericalParameters[ParameterType.BladeThickness].Value);
                CreateEdge();
                _wrapper.SetEdgeSketchNull();
                DrawDirection(parameters.NumericalParameters[ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                    parameters.BladeType,
                    false,
                    parameters.BladeExistance
                    );
                DrawEdgeForm(parameters.NumericalParameters[ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[ParameterType.BladeWidth].Value,
                    false
                    );
                CreateEdge();
            }
           

        }
        /// <summary>
        /// Запуск процесса создания
        /// </summary>
        private void StartCreating()
        {
            _wrapper.StartKompas();
            _wrapper.CreateFile();
        }
        /// <summary>
        /// Функция строющая основной скетч
        /// </summary>
        /// <param name="bladelength">Длина клинка</param>
        /// <param name="peaklength">Длина острия</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="binlength">Длина крепления</param>
        /// <param name="bintype">Тип крепления</param>
        /// <param name="existance">TRUE- острие существует, FALSE- отсутствует</param>
        /// <param name="bladetype">TRUE - тип клинка "двусторонний", FALSE - тип клинка "односторонний"</param>
        private void DrawMainSketch(double bladelength,double peaklength,double edgewidth ,double bladewidth, double binlength
            , BindingType bintype,bool existance, bool bladetype)
        {

            _wrapper.ChooseSketch("Main");
            if (existance)
            {
                if (!bladetype)
                {
                    _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength - peaklength);
                    _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, bladelength);
                    _wrapper.CreateArc(ORIGIN, bladelength - peaklength, edgewidth, bladelength * 0.95, bladewidth, bladelength);
                }
                else
                {
                    _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength - peaklength);
                    _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, bladelength - peaklength);

                    _wrapper.DrawLine(ORIGIN, bladelength - peaklength, bladewidth/2, bladelength);
                    _wrapper.DrawLine(bladewidth, bladelength - peaklength, bladewidth/2, bladelength);
                }
            }
            else
            {
                _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength);
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, bladelength);
                _wrapper.DrawLine(ORIGIN,bladelength,bladewidth,bladelength);
            }

            if (bintype == BindingType.None)
            {
                _wrapper.DrawLine(ORIGIN, ORIGIN, bladewidth, ORIGIN);
            }

            if (bintype == BindingType.Insert)
            {
                _wrapper.DrawLine(ORIGIN, ORIGIN, bladewidth * 0.4, -binlength * 0.1);
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth * 0.6, -binlength * 0.1);

                _wrapper.DrawLine(bladewidth * 0.4, -binlength * 0.1, bladewidth * 0.5, -binlength);
                _wrapper.DrawLine(bladewidth * 0.6, -binlength * 0.1, bladewidth * 0.6, -binlength);

                _wrapper.DrawLine(bladewidth * 0.5, -binlength, bladewidth * 0.6, -binlength);
            }

            if (bintype == BindingType.Through)
            {
                _wrapper.DrawLine(ORIGIN, ORIGIN, bladewidth * 0.4, -binlength * 0.1);
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth * 0.6, -binlength * 0.1);

                _wrapper.DrawLine(bladewidth * 0.4, -binlength * 0.1, bladewidth * 0.55, -binlength);
                _wrapper.DrawLine(bladewidth * 0.6, -binlength * 0.1, bladewidth * 0.6, -binlength);

                _wrapper.DrawLine(bladewidth * 0.55, -binlength, bladewidth * 0.6, -binlength);
            }

            if (bintype == BindingType.ForOverlays)
            {
                _wrapper.DrawLine(ORIGIN, ORIGIN, bladewidth, -binlength);
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, -binlength);
                _wrapper.DrawLine(bladewidth, -binlength, bladewidth, -binlength);
            }
            _wrapper.EndSkethEdit();
        }

        /// <summary>
        /// Функция строющая траекторию для лезвия
        /// </summary>
        /// <param name="bladelength">Длина клинка</param>
        /// <param name="peaklength">Длина острия</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="bladetype">TRUE - тип клинка "двусторонний", FALSE - тип клинка "односторонний"</param>
        /// <param name="mainedge">TRUE - Основное лезвия, FALSE- Второстепенное лезвие</param>
        /// <param name="existance">TRUE- острие существует, FALSE- отсутствует</param>
        private void DrawDirection(double bladelength, double peaklength, 
            double edgewidth, double bladewidth,bool bladetype,bool mainedge=false,bool existance=true)
        {
            if (existance)
            {
                if (!bladetype)
                {
                    _wrapper.ChooseSketch("EdgeDirection");
                    _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength - peaklength);
                    _wrapper.CreateArc(ORIGIN, bladelength - peaklength, edgewidth, bladelength * 0.95, bladewidth, bladelength);
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainedge)
                    {
                        _wrapper.ChooseSketch("EdgeDirection");
                        _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength - peaklength);
                        _wrapper.DrawLine(ORIGIN, bladelength - peaklength, bladewidth / 2, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch("EdgeDirection");
                        _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, bladelength - peaklength);
                        _wrapper.DrawLine(bladewidth, bladelength - peaklength, bladewidth / 2, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
            else
            {
                if (!bladetype)
                {
                    _wrapper.ChooseSketch("EdgeDirection");
                    _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength - peaklength);
                    
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainedge)
                    {
                        _wrapper.ChooseSketch("EdgeDirection");
                        _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch("EdgeDirection");
                        _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
           
          
        }

        /// <summary>
        /// Функция строющая скетч формы лезвия
        /// </summary>
        /// <param name="thickness">Толщина клинка</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="mainedge">TRUE - Основное лезвия, FALSE- Второстепенное лезвие</param>
        private void DrawEdgeForm(double thickness, double edgewidth,double bladewidth,bool mainedge = true)
        {
            if (mainedge)
            {
                _wrapper.ChooseSketch("Edge");
                _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, -thickness / 2);
                _wrapper.DrawLine(ORIGIN, ORIGIN, ORIGIN, thickness / 2);

                _wrapper.DrawLine(ORIGIN, thickness / 2, edgewidth, thickness / 2);
                _wrapper.DrawLine(ORIGIN, -thickness / 2, edgewidth, -thickness / 2);

                _wrapper.DrawLine(edgewidth, thickness / 2, ORIGIN, ORIGIN);
                _wrapper.DrawLine(edgewidth, -thickness / 2, ORIGIN, ORIGIN);
                _wrapper.EndSkethEdit();
            }
            else
            {
                _wrapper.ChooseSketch("Edge");
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, -thickness / 2);
                _wrapper.DrawLine(bladewidth, ORIGIN, bladewidth, thickness / 2);

                _wrapper.DrawLine(bladewidth, thickness / 2, bladewidth-edgewidth, thickness / 2);
                _wrapper.DrawLine(bladewidth, -thickness / 2, bladewidth-edgewidth, -thickness / 2);

                _wrapper.DrawLine(bladewidth - edgewidth, thickness / 2, bladewidth, ORIGIN);
                _wrapper.DrawLine(bladewidth - edgewidth, -thickness / 2, bladewidth, ORIGIN);
                _wrapper.EndSkethEdit();
            }
            
        }

        /// <summary>
        /// Окончания построения текущего скетча
        /// </summary>
        private void EndSketchEdit()
        {
            _wrapper.EndSkethEdit();
        }
        /// <summary>
        /// Выдавить основной скетч
        /// </summary>
        /// <param name="thick">Толщина клинка</param>
        private void ExtrudeMainPart(double thick)
        {
            _wrapper.ExtrudeMainBase(thick);
        }
        /// <summary>
        /// Создание лезвия
        /// </summary>
        private void CreateEdge()
        {
            _wrapper.CutByTrajectory();
        }
        
        /// <summary>
        /// Создание отверстий
        /// </summary>
        /// <param name="binlen">Длина крепления</param>
        /// <param name="width">Ширина</param>
        private void MakeHoles(double binlen,double width)
        {
            _wrapper.ChooseSketch("Holes");
            _wrapper.CreateCircle(width /2, -binlen * 0.2,(width*0.2)/2);
            _wrapper.CreateCircle(width / 2, -binlen * 0.8, (width * 0.2) / 2);
            EndSketchEdit();
            _wrapper.Cut();
        }
    }
}
