using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SketchesType;

namespace KompasBuilder
{
    /// <summary>
    /// Класс, отвечающий за постройку объекта 
    /// при помощи API Kompas-3D V23
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Поле, хранящее в себе объект KompasWrapper
        /// </summary>
        private KompasWrapper _wrapper;

        /// <summary>
        /// Функция, запускающая и проводящая процесс постройки
        /// </summary>
        /// <param name="parameters">Параметры для постройки</param>
        public void BuildBlade(Parameters parameters)
        {
            _wrapper = new KompasWrapper();
            StartCreating();

            if (!parameters.BladeType)
            {
                DrawMainSketch(
                    parameters.NumericalParameters[
                        ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BindingLength].Value,
                    parameters.BindingType,
                    parameters.BladeExistence,
                    parameters.BladeType
                );

                DrawDirection(
                    parameters.NumericalParameters[
                        ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
                    parameters.BladeType,
                    parameters.BladeExistence
                );

                DrawEdgeForm(
                    parameters.NumericalParameters[
                        ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value
                );

                ExtrudeMainPart(
                    parameters.NumericalParameters[
                        ParameterType.BladeThickness].Value
                );

                CreateEdge();

                if (parameters.BindingType == BindingType.ForOverlays)
                {
                    MakeHoles(
                        parameters.NumericalParameters[
                            ParameterType.BindingLength].Value,
                        parameters.NumericalParameters[
                            ParameterType.BladeWidth].Value
                    );
                }
            }
            else
            {
                DrawMainSketch(
                    parameters.NumericalParameters[
                        ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BindingLength].Value,
                    parameters.BindingType,
                    parameters.BladeExistence,
                    parameters.BladeType
                );

                DrawDirection(
                    parameters.NumericalParameters[
                        ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
                    parameters.BladeType,
                    true,
                    parameters.BladeExistence
                );

                DrawEdgeForm(
                    parameters.NumericalParameters[
                        ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value
                );

                ExtrudeMainPart(
                    parameters.NumericalParameters[
                        ParameterType.BladeThickness].Value
                );

                CreateEdge();
                _wrapper.SetEdgeSketchNull();

                DrawDirection(
                    parameters.NumericalParameters[
                        ParameterType.BladeLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.PeakLenght].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
                    parameters.BladeType,
                    false,
                    parameters.BladeExistence
                );

                DrawEdgeForm(
                    parameters.NumericalParameters[
                        ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters[
                        ParameterType.EdgeWidth].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value,
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
        /// Функция построения основного скетча
        /// </summary>
        /// <param name="bladelength">Длина клинка</param>
        /// <param name="peaklength">Длина острия</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="binlength">Длина крепления</param>
        /// <param name="bintype">Тип крепления</param>
        /// <param name="existance">TRUE- острие существует, 
        /// FALSE- отсутствует</param>
        /// <param name="bladetype">TRUE - тип клинка "двусторонний",
        /// FALSE - тип клинка "односторонний"</param>
        /// //TODO: RSDN
        private void DrawMainSketch(double bladelength, double peaklength,
            double edgewidth, double bladewidth, double binlength,
            BindingType bintype, bool existance, bool bladetype)
        {
            const double bladeShortage = 0.95;
            const double bindingLeftIntendUp = 0.4;
            const double bindingRightIntendDown = 0.6;
            const double bindingLeftIntendDownFirst = 0.5;
            const double bindingLeftIntendDownSecond = 0.55;
            const double bindingLenghtShortage = 0.1;

            _wrapper.ChooseSketch(SketchesTypes.MainString);

            if (existance)
            {
                if (!bladetype)
                {
                    _wrapper.DrawLine(0, 0, 0, bladelength - peaklength);
                    _wrapper.DrawLine(bladewidth, 0, bladewidth, bladelength);
                    _wrapper.CreateArc(0, bladelength - peaklength, edgewidth,
                        bladelength * bladeShortage, bladewidth, bladelength);
                }
                else
                {
                    _wrapper.DrawLine(0, 0, 0, bladelength - peaklength);
                    _wrapper.DrawLine(bladewidth, 0, bladewidth,
                        bladelength - peaklength);

                    _wrapper.DrawLine(0, bladelength - peaklength,
                        bladewidth / 2, bladelength);
                    _wrapper.DrawLine(bladewidth, bladelength - peaklength,
                        bladewidth / 2, bladelength);
                }
            }
            else
            {
                _wrapper.DrawLine(0, 0, 0, bladelength);
                _wrapper.DrawLine(bladewidth, 0, bladewidth, bladelength);
                _wrapper.DrawLine(0, bladelength, bladewidth, bladelength);
            }

            if (bintype == BindingType.None)
            {
                _wrapper.DrawLine(0, 0, bladewidth, 0);
            }

            if (bintype == BindingType.Insert)
            {
                _wrapper.DrawLine(0, 0, bladewidth * bindingLeftIntendUp,
                    -binlength * bindingLenghtShortage);
                _wrapper.DrawLine(bladewidth, 0,
                    bladewidth * bindingRightIntendDown,
                    -binlength * bindingLenghtShortage);

                _wrapper.DrawLine(bladewidth * bindingLeftIntendUp,
                    -binlength * bindingLenghtShortage,
                    bladewidth * 0.5, -binlength);
                _wrapper.DrawLine(bladewidth * bindingRightIntendDown,
                    -binlength * bindingLenghtShortage,
                    bladewidth * bindingRightIntendDown, -binlength);

                _wrapper.DrawLine(bladewidth * bindingLeftIntendDownFirst,
                    -binlength,
                    bladewidth * bindingRightIntendDown, -binlength);
            }

            if (bintype == BindingType.Through)
            {
                _wrapper.DrawLine(0, 0, bladewidth * bindingLeftIntendUp,
                    -binlength * bindingLenghtShortage);

                _wrapper.DrawLine(bladewidth, 0,
                    bladewidth * bindingRightIntendDown,
                    -binlength * bindingLenghtShortage);

                double check = bladewidth * bindingRightIntendDown;
                _wrapper.DrawLine(bladewidth * bindingLeftIntendUp,
                    -binlength * bindingLenghtShortage,
                    bladewidth * bindingLeftIntendDownSecond, -binlength);
                _wrapper.DrawLine(bladewidth * bindingRightIntendDown,
                    -binlength * bindingLenghtShortage,
                    bladewidth * bindingRightIntendDown, -binlength);

                check = bladewidth * bindingRightIntendDown;
                _wrapper.DrawLine(bladewidth * bindingLeftIntendDownSecond,
                    -binlength,
                    bladewidth * bindingRightIntendDown, -binlength);
            }

            if (bintype == BindingType.ForOverlays)
            {
                _wrapper.DrawLine(0, 0, 0, -binlength);
                _wrapper.DrawLine(bladewidth, 0, bladewidth, -binlength);
                _wrapper.DrawLine(0, -binlength, bladewidth, -binlength);
            }

            _wrapper.EndSkethEdit();
        }

        /// <summary>
        /// Функция построения траектории лезвия
        /// </summary>
        /// <param name="bladelength">Длина клинка</param>
        /// <param name="peaklength">Длина острия</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="bladetype">TRUE - тип клинка "двусторонний",
        /// FALSE - тип клинка "односторонний"</param>
        /// <param name="mainedge">TRUE - Основное лезвия,
        /// FALSE- Второстепенное лезвие</param>
        /// <param name="existance">TRUE- острие существует, 
        /// FALSE- отсутствует</param>
        /// //TODO: RSDN
        private void DrawDirection(double bladelength, double peaklength,
            double edgewidth, double bladewidth, bool bladetype,
            bool mainedge = false, bool existance = true)
        {
            const double lengthShortage = 0.95;
            if (existance)
            {
                if (!bladetype)
                {
                    _wrapper.ChooseSketch(
                        SketchesTypes.EdgeDirectionString);
                    _wrapper.DrawLine(0, 0, 0, bladelength - peaklength);
                    _wrapper.CreateArc(0, bladelength - peaklength,
                        edgewidth,
                        bladelength * lengthShortage,
                        bladewidth, bladelength);
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainedge)
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(0, 0, 0, bladelength - peaklength);
                        _wrapper.DrawLine(0, bladelength - peaklength,
                            bladewidth / 2, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(bladewidth, 0, bladewidth,
                            bladelength - peaklength);
                        _wrapper.DrawLine(bladewidth,
                            bladelength - peaklength,
                            bladewidth / 2, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
            else
            {
                if (!bladetype)
                {
                    _wrapper.ChooseSketch(
                        SketchesTypes.EdgeDirectionString);
                    _wrapper.DrawLine(0, 0, 0, bladelength - peaklength);
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainedge)
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(0, 0, 0, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(bladewidth, 0,
                            bladewidth, bladelength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
        }

        /// <summary>
        /// Функция построения скетча формы лезвия
        /// </summary>
        /// <param name="thickness">Толщина клинка</param>
        /// <param name="edgewidth">Ширина острия</param>
        /// <param name="bladewidth">Ширина клинка</param>
        /// <param name="mainedge">TRUE - Основное лезвия,
        /// FALSE- Второстепенное лезвие</param>
        private void DrawEdgeForm(double thickness, double edgewidth,
            double bladewidth, bool mainedge = true)
        {
            if (mainedge)
            {
                _wrapper.ChooseSketch(SketchesTypes.EdgeString);
                _wrapper.DrawLine(0, 0, 0, -thickness / 2);
                _wrapper.DrawLine(0, 0, 0, thickness / 2);

                _wrapper.DrawLine(0, thickness / 2,
                    edgewidth, thickness / 2);
                _wrapper.DrawLine(0, -thickness / 2,
                    edgewidth, -thickness / 2);

                _wrapper.DrawLine(edgewidth, thickness / 2, 0, 0);
                _wrapper.DrawLine(edgewidth, -thickness / 2, 0, 0);
                _wrapper.EndSkethEdit();
            }
            else
            {
                _wrapper.ChooseSketch(SketchesTypes.EdgeString);
                _wrapper.DrawLine(bladewidth, 0,
                    bladewidth, -thickness / 2);
                _wrapper.DrawLine(bladewidth, 0,
                    bladewidth, thickness / 2);

                _wrapper.DrawLine(bladewidth, thickness / 2,
                    bladewidth - edgewidth, thickness / 2);
                _wrapper.DrawLine(bladewidth, -thickness / 2,
                    bladewidth - edgewidth, -thickness / 2);

                _wrapper.DrawLine(bladewidth - edgewidth, thickness / 2,
                    bladewidth, 0);
                _wrapper.DrawLine(bladewidth - edgewidth, -thickness / 2,
                    bladewidth, 0);
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
        /// //TODO: RSDN
        private void MakeHoles(double binlen, double width)
        {
            const double upIntend = 0.2;
            const double downIntend = 0.8;
            const double widthShortage = 0.2;
            _wrapper.ChooseSketch(SketchesTypes.HolesString);
            _wrapper.CreateCircle(width / 2, -binlen * upIntend,
                (width * widthShortage) / 2);
            _wrapper.CreateCircle(width / 2, -binlen * downIntend,
                (width * widthShortage) / 2);
            EndSketchEdit();
            _wrapper.Cut();
        }
    }
}