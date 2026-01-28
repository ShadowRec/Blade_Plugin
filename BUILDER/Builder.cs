using System;
using Core;
using Kompas6Constants;
using KompasAPI7;
using SketchesType;

namespace KompasBuilder
{
    /// <summary>
    /// Класс, отвечающий за постройку объекта при помощи API Kompas-3D V23.
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Поле, хранящее в себе объект KompasWrapper.
        /// </summary>
        private KompasWrapper _wrapper;

        /// <summary>
        /// Функция, запускающая и проводящая процесс постройки.
        /// </summary>
        /// <param name="parameters">
        /// Параметры для постройки.
        /// </param>
        public void BuildBlade(Parameters parameters)
        {
            
            _wrapper.CreateFile();

            if (!parameters.BladeType)
            {
                BuildSingleSidedBlade(parameters);
            }
            else
            {
                BuildDoubleSidedBlade(parameters);
            }
        }

        /// <summary>
        /// Построение одностороннего клинка.
        /// </summary>
        /// <param name="parameters">
        /// Параметры для постройки.
        /// </param>
        private void BuildSingleSidedBlade(Parameters parameters)
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

            if (parameters.BindingType == BindingType.ForOverlays)
            {
                MakeHoles(
                    parameters.NumericalParameters[
                        ParameterType.BindingLength].Value,
                    parameters.NumericalParameters[
                        ParameterType.BladeWidth].Value
                    );
            }

            if (parameters.SerreitorExistence)
            {
                DrawSerreitor(
                    parameters.NumericalParameters
                    [ParameterType.SerreitorLength].Value,
                    parameters.NumericalParameters
                    [ParameterType.BladeWidth].Value,
                    parameters.NumericalParameters
                    [ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters
                    [ParameterType.SerreitorNumber].Value,
                    parameters.SerreitorType
                    );
                _wrapper.ExtrudeSerreitor();
            }
        }

        /// <summary>
        /// Построение двустороннего клинка.
        /// </summary>
        /// <param name="parameters">
        /// Параметры для постройки.
        /// </param>
        private void BuildDoubleSidedBlade(Parameters parameters)
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

            // Построение основного лезвия.
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
                    ParameterType.EdgeWidth].Value / 2,
                parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value
                );

            ExtrudeMainPart(
                parameters.NumericalParameters[
                    ParameterType.BladeThickness].Value
                );

            CreateEdge();
            _wrapper.SetEdgeSketchNull();

            // Построение второстепенного лезвия.
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
                    ParameterType.EdgeWidth].Value / 2,
                parameters.NumericalParameters[
                    ParameterType.BladeWidth].Value,
                false
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

            if (parameters.SerreitorExistence)
            {
                DrawSerreitor(
                    parameters.NumericalParameters
                    [ParameterType.SerreitorLength].Value,
                    parameters.NumericalParameters
                    [ParameterType.BladeWidth].Value,
                    parameters.NumericalParameters
                    [ParameterType.BladeThickness].Value,
                    parameters.NumericalParameters
                    [ParameterType.SerreitorNumber].Value,
                    parameters.SerreitorType
                    );
                _wrapper.ExtrudeSerreitor();
            }
        }

        /// <summary>
        /// Запуск процесса создания.
        /// </summary>
        public void StartCreating()
        {
            _wrapper = new KompasWrapper();
            _wrapper.StartKompas();
        }

        /// <summary>
        /// Функция построения основного скетча.
        /// </summary>
        /// <param name="bladeLength">
        /// Длина клинка.
        /// </param>
        /// <param name="peakLength">
        /// Длина острия.
        /// </param>
        /// <param name="edgeWidth">
        /// Ширина острия.
        /// </param>
        /// <param name="bladeWidth">
        /// Ширина клинка.
        /// </param>
        /// <param name="binLength">
        /// Длина крепления.
        /// </param>
        /// <param name="binType">
        /// Тип крепления.
        /// </param>
        /// <param name="existance">
        /// TRUE - острие существует, FALSE - отсутствует.
        /// </param>
        /// <param name="bladeType">
        /// TRUE - тип клинка "двусторонний",
        /// FALSE - тип клинка "односторонний".
        /// </param>
        private void DrawMainSketch(
            double bladeLength,
            double peakLength,
            double edgeWidth,
            double bladeWidth,
            double binLength,
            BindingType binType,
            bool existance,
            bool bladeType)
        {
            const double BladeShortage = 0.95;
            const double BindingLeftIntendUp = 0.4;
            const double BindingRightIntendDown = 0.6;
            const double BindingLeftIntendDownFirst = 0.5;
            const double BindingLeftIntendDownSecond = 0.55;
            const double BindingLenghtShortage = 0.1;
            const double WidthShortage = 0.05;

            _wrapper.ChooseSketch(SketchesTypes.MainString);

            if (existance)
            {
                if (!bladeType)
                {
                    _wrapper.DrawLine(0, 0, 0, bladeLength - peakLength);
                    _wrapper.DrawLine(bladeWidth, 0, bladeWidth,
                        bladeLength);
                    _wrapper.CreateArc(0, bladeLength - peakLength,
                        edgeWidth, bladeLength * BladeShortage,
                        bladeWidth, bladeLength);
                }
                else
                {
                    _wrapper.DrawLine(0, 0, 0, bladeLength - peakLength);
                    _wrapper.DrawLine(bladeWidth, 0, bladeWidth,
                        bladeLength - peakLength);
                    _wrapper.DrawLine(0, bladeLength - peakLength,
                        bladeWidth / 2, bladeLength);
                    _wrapper.DrawLine(bladeWidth, bladeLength - peakLength,
                        bladeWidth / 2, bladeLength);
                }
            }
            else
            {
                _wrapper.DrawLine(0, 0, 0, bladeLength);
                _wrapper.DrawLine(bladeWidth, 0, bladeWidth, bladeLength);
                _wrapper.DrawLine(0, bladeLength, bladeWidth, bladeLength);
            }

            if (binType == BindingType.None)
            {
                _wrapper.DrawLine(0, 0, bladeWidth * WidthShortage, -25);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth - bladeWidth * WidthShortage, -25);
                _wrapper.DrawLine(bladeWidth * WidthShortage, -25,
                    bladeWidth - bladeWidth * WidthShortage, -25);
            }

            if (binType == BindingType.Insert)
            {
                _wrapper.DrawLine(0, 0, bladeWidth * BindingLeftIntendUp,
                    -binLength * BindingLenghtShortage);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth * BindingRightIntendDown,
                    -binLength * BindingLenghtShortage);
                _wrapper.DrawLine(bladeWidth * BindingLeftIntendUp,
                    -binLength * BindingLenghtShortage,
                    bladeWidth * 0.5, -binLength);
                _wrapper.DrawLine(bladeWidth * BindingRightIntendDown,
                    -binLength * BindingLenghtShortage,
                    bladeWidth * BindingRightIntendDown, -binLength);
                _wrapper.DrawLine(bladeWidth * BindingLeftIntendDownFirst,
                    -binLength,
                    bladeWidth * BindingRightIntendDown, -binLength);
            }

            if (binType == BindingType.Through)
            {
                _wrapper.DrawLine(0, 0, bladeWidth * BindingLeftIntendUp,
                    -binLength * BindingLenghtShortage);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth * BindingRightIntendDown,
                    -binLength * BindingLenghtShortage);
                _wrapper.DrawLine(bladeWidth * BindingLeftIntendUp,
                    -binLength * BindingLenghtShortage,
                    bladeWidth * BindingLeftIntendDownSecond, -binLength);
                _wrapper.DrawLine(bladeWidth * BindingRightIntendDown,
                    -binLength * BindingLenghtShortage,
                    bladeWidth * BindingRightIntendDown, -binLength);
                _wrapper.DrawLine(bladeWidth * BindingLeftIntendDownSecond,
                    -binLength,
                    bladeWidth * BindingRightIntendDown, -binLength);
            }

            if (binType == BindingType.ForOverlays)
            {
                _wrapper.DrawLine(0, 0, bladeWidth * WidthShortage,
                    -binLength);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth - bladeWidth * WidthShortage, -binLength);
                _wrapper.DrawLine(bladeWidth * WidthShortage, -binLength,
                    bladeWidth - bladeWidth * WidthShortage, -binLength);
            }

            _wrapper.EndSkethEdit();
        }

        /// <summary>
        /// Функция построения траектории лезвия.
        /// </summary>
        /// <param name="bladeLength">
        /// Длина клинка.
        /// </param>
        /// <param name="peakLength">
        /// Длина острия.
        /// </param>
        /// <param name="edgeWidth">
        /// Ширина острия.
        /// </param>
        /// <param name="bladeWidth">
        /// Ширина клинка.
        /// </param>
        /// <param name="bladeType">
        /// TRUE - тип клинка "двусторонний",
        /// FALSE - тип клинка "односторонний".
        /// </param>
        /// <param name="mainEdge">
        /// TRUE - Основное лезвия, FALSE - Второстепенное лезвие.
        /// </param>
        /// <param name="existance">
        /// TRUE - острие существует, FALSE - отсутствует.
        /// </param>
        private void DrawDirection(
            double bladeLength,
            double peakLength,
            double edgeWidth,
            double bladeWidth,
            bool bladeType,
            bool mainEdge = false,
            bool existance = true)
        {
            const double LengthShortage = 0.95;

            if (existance)
            {
                if (!bladeType)
                {
                    _wrapper.ChooseSketch(SketchesTypes.EdgeDirectionString);
                    _wrapper.DrawLine(0, 0, 0, bladeLength - peakLength);
                    _wrapper.CreateArc(0, bladeLength - peakLength,
                        edgeWidth, bladeLength * LengthShortage,
                        bladeWidth, bladeLength);
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainEdge)
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(0, 0, 0, bladeLength - peakLength);
                        _wrapper.DrawLine(0, bladeLength - peakLength,
                            bladeWidth / 2, bladeLength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(bladeWidth, 0, bladeWidth,
                            bladeLength - peakLength);
                        _wrapper.DrawLine(bladeWidth, bladeLength - peakLength,
                            bladeWidth / 2, bladeLength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
            else
            {
                if (!bladeType)
                {
                    _wrapper.ChooseSketch(SketchesTypes.EdgeDirectionString);
                    _wrapper.DrawLine(0, 0, 0, bladeLength);
                    _wrapper.EndSkethEdit();
                }
                else
                {
                    if (mainEdge)
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(0, 0, 0, bladeLength);
                        _wrapper.EndSkethEdit();
                    }
                    else
                    {
                        _wrapper.ChooseSketch(
                            SketchesTypes.EdgeDirectionString);
                        _wrapper.DrawLine(bladeWidth, 0, bladeWidth,
                            bladeLength);
                        _wrapper.EndSkethEdit();
                    }
                }
            }
        }

        /// <summary>
        /// Функция построения скетча формы лезвия.
        /// </summary>
        /// <param name="thickness">
        /// Толщина клинка.
        /// </param>
        /// <param name="edgeWidth">
        /// Ширина острия.
        /// </param>
        /// <param name="bladeWidth">
        /// Ширина клинка.
        /// </param>
        /// <param name="mainEdge">
        /// TRUE - Основное лезвия, FALSE - Второстепенное лезвие.
        /// </param>
        private void DrawEdgeForm(
            double thickness,
            double edgeWidth,
            double bladeWidth,
            bool mainEdge = true)
        {
            if (mainEdge)
            {
                _wrapper.ChooseSketch(SketchesTypes.EdgeString);
                _wrapper.DrawLine(0, 0, 0, -thickness / 2);
                _wrapper.DrawLine(0, 0, 0, thickness / 2);
                _wrapper.DrawLine(0, thickness / 2,
                    edgeWidth, thickness / 2);
                _wrapper.DrawLine(0, -thickness / 2,
                    edgeWidth, -thickness / 2);
                _wrapper.DrawLine(edgeWidth, thickness / 2, 0, 0);
                _wrapper.DrawLine(edgeWidth, -thickness / 2, 0, 0);
                _wrapper.EndSkethEdit();
            }
            else
            {
                _wrapper.ChooseSketch(SketchesTypes.EdgeString);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth, -thickness / 2);
                _wrapper.DrawLine(bladeWidth, 0,
                    bladeWidth, thickness / 2);
                _wrapper.DrawLine(bladeWidth, thickness / 2,
                    bladeWidth - edgeWidth, thickness / 2);
                _wrapper.DrawLine(bladeWidth, -thickness / 2,
                    bladeWidth - edgeWidth, -thickness / 2);
                _wrapper.DrawLine(bladeWidth - edgeWidth, thickness / 2,
                    bladeWidth, 0);
                _wrapper.DrawLine(bladeWidth - edgeWidth, -thickness / 2,
                    bladeWidth, 0);
                _wrapper.EndSkethEdit();
            }
        }

        /// <summary>
        /// Окончание построения текущего скетча.
        /// </summary>
        private void EndSketchEdit()
        {
            _wrapper.EndSkethEdit();
        }

        /// <summary>
        /// Выдавить основной скетч.
        /// </summary>
        /// <param name="thick">
        /// Толщина клинка.
        /// </param>
        private void ExtrudeMainPart(double thick)
        {
            _wrapper.ExtrudeMainBase(thick);
        }

        /// <summary>
        /// Создание лезвия.
        /// </summary>
        private void CreateEdge()
        {
            _wrapper.CutByTrajectory();
        }

        /// <summary>
        /// Создание отверстий.
        /// </summary>
        /// <param name="binLen">
        /// Длина крепления.
        /// </param>
        /// <param name="width">
        /// Ширина.
        /// </param>
        private void MakeHoles(double binLen, double width)
        {
            const double UpIntend = 0.2;
            const double DownIntend = 0.8;
            const double WidthShortage = 0.2;
            const double WidthShortageForCords = 0.95;

            _wrapper.ChooseSketch(SketchesTypes.HolesString);
            _wrapper.CreateCircle(
                (width * WidthShortageForCords) / 2,
                -binLen * UpIntend,
                (width * WidthShortage) / 2);
            _wrapper.CreateCircle(
                (width * WidthShortageForCords) / 2,
                -binLen * DownIntend,
                (width * WidthShortage) / 2);
            EndSketchEdit();
            _wrapper.Cut();
        }

        /// <summary>
        /// Функция рисования серрейтора (зубчатого лезвия).
        /// </summary>
        /// <param name="serreitorLength">
        /// Общая длина серрейтора вдоль оси.
        /// </param>
        /// <param name="bladeWidth">
        /// Ширина лезвия.
        /// </param>
        /// <param name="bladeThick">
        /// Толщина лезвия.
        /// </param>
        /// <param name="serrNumber">
        /// Количество зубцов на серрейторе.
        /// </param>
        /// <param name="serreitorType">
        /// Тип серрейтора из перечисления SerreitorType.
        /// </param>
        private void DrawSerreitor(
            double serreitorLength,
            double bladeWidth,
            double bladeThick,
            double serrNumber,
            SerreitorType serreitorType)
        {
            const double widthIntedUp = 1.1;
            const double cutHeight = 3;

            _wrapper.Offset = bladeThick;
            _wrapper.ChooseSketch(SketchesTypes.SerreitorString);
            double totalLengt = 0;
            double peakLen = serreitorLength / serrNumber;

            switch (serreitorType)
            {
                case SerreitorType.ConstBigSerreitor:
                    while (true)
                    {
                        if ((totalLengt + peakLen) < serreitorLength)
                        {
                            _wrapper.CreateArcBy2PointsAndCenter(
                                bladeWidth * widthIntedUp,
                                totalLengt + peakLen / 2,
                                bladeWidth * widthIntedUp,
                                totalLengt,
                                bladeWidth * widthIntedUp,
                                totalLengt + peakLen,
                                peakLen / 2);
                            totalLengt += peakLen;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;

                case SerreitorType.ConstSmallSerreitor:
                    while (true)
                    {
                        if ((totalLengt + peakLen / 2) < serreitorLength)
                        {
                            _wrapper.CreateArcBy2PointsAndCenter(
                                bladeWidth * widthIntedUp,
                                totalLengt + peakLen / 4,
                                bladeWidth * widthIntedUp,
                                totalLengt,
                                bladeWidth * widthIntedUp,
                                totalLengt + peakLen,
                                peakLen / 4);
                            totalLengt += peakLen / 2;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;

                case SerreitorType.AlternationSerreitor:
                    bool isLittlePeak = false;
                    while (true)
                    {
                        if (isLittlePeak)
                        {
                            if ((totalLengt + peakLen) < serreitorLength)
                            {
                                _wrapper.CreateArcBy2PointsAndCenter(
                                    bladeWidth * widthIntedUp,
                                    totalLengt + peakLen / 2,
                                    bladeWidth * widthIntedUp,
                                    totalLengt,
                                    bladeWidth * widthIntedUp,
                                    totalLengt + peakLen,
                                    peakLen / 2);
                                totalLengt += peakLen;
                                isLittlePeak = !isLittlePeak;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if ((totalLengt + peakLen / 2) < serreitorLength)
                            {
                                _wrapper.CreateArcBy2PointsAndCenter(
                                    bladeWidth * widthIntedUp,
                                    totalLengt + peakLen / 4,
                                    bladeWidth * widthIntedUp,
                                    totalLengt,
                                    bladeWidth * widthIntedUp,
                                    totalLengt + peakLen,
                                    peakLen / 4);
                                totalLengt += peakLen / 2;
                                isLittlePeak = !isLittlePeak;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    break;
            }

            _wrapper.DrawLine(bladeWidth * widthIntedUp, 0,
                bladeWidth * widthIntedUp + cutHeight, 0);
            _wrapper.DrawLine(bladeWidth * widthIntedUp + cutHeight, 0,
                bladeWidth * widthIntedUp + cutHeight, totalLengt);
            _wrapper.DrawLine(bladeWidth * widthIntedUp + cutHeight,
                totalLengt, bladeWidth * widthIntedUp, totalLengt);
            _wrapper.EndSkethEdit();
        }

        /// <summary>
        /// Функция закрытия враппера
        /// </summary>
        public void CLose()
        {
            _wrapper.Close();
        }
    }
}