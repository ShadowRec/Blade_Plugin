using Core;
using Kompas6API5;
using Kompas6Constants3D;
using KompasAPI7;
using SketchesType;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;

namespace KompasBuilder
{
    /// <summary>
    /// Класс, выполняющий функции связи с Kompas-3D через API.
    /// </summary>
    internal class KompasWrapper
    {
        /// <summary>
        /// Поле, хранящее объект компаса.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Поле, хранящее объект документа.
        /// </summary>
        private ksDocument3D _document;

        /// <summary>
        /// Поле, хранящее объект детали.
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Поле, хранящее основной скетч.
        /// </summary>
        private ksEntity _mainSketch;

        /// <summary>
        /// Поле, хранящее траектория лезвия.
        /// </summary>
        private ksEntity _edgeDircectionSketch;

        /// <summary>
        /// Поле, хранящее скетч, формирующий лезвие.
        /// </summary>
        private ksEntity _edgeSketch;

        /// <summary>
        /// Поле, хранящее скетч, формирующий заточку серрейтора.
        /// </summary>
        private ksEntity _serreitorSketch;

        /// <summary>
        /// Поле, хранящее скетч отверстий.
        /// </summary>
        private ksEntity _holesSketch;

        /// <summary>
        /// Поле, хранящее редактор текущего выбранного скетча.
        /// </summary>
        private ksSketchDefinition _sketchDefinition;

        /// <summary>
        /// Поле, хранящее редактируемый скетч.
        /// </summary>
        private ksDocument2D _sketchEdit;

        /// <summary>
        /// TRUE - Открыт скетч для редактирования,
        /// FALSE - Редактирование не занято.
        /// </summary>
        private bool _editStatus = false;

        /// <summary>
        /// Отступ для дополнительных
        /// плоскостей
        /// </summary>
        private double _offset;
        private ksEntity _customPlane;
        
        /// <summary>
        /// Свойство для поля отступа
        /// </summary>
        public double Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset= value;
            }
        }

        private void CreateCustomPlane(double offset,ksEntity basePlane)
        {
            _customPlane = _part.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition planehDefinition = _customPlane.GetDefinition();

            planehDefinition.SetPlane(basePlane);
            planehDefinition.direction = false;
            planehDefinition.offset= offset;
            _customPlane.Create();
        }
        /// <summary>
        /// Запуск Kompas-3D V23.
        /// </summary>
        /// <exception cref="Exception">
        /// Ошибка запуска Kompas-3D V23</exception>
        public void StartKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }
                
                if (_kompas != null) return;

                var kompasType = Type.GetTypeFromProgID(
                    "KOMPAS.Application.5");
                _kompas = (KompasObject)Activator.CreateInstance(
                    kompasType);
                StartKompas();

                if (_kompas == null)
                {
                    throw new ParameterException(
                        ExceptionType.KompasOpenErrorException);
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartKompas();
            }
        }

        /// <summary>
        /// Создание документа внутри Kompas-3D V23.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Ошибка запуска документа</exception>
        public void CreateFile()
        {
            try
            {
                _document = (ksDocument3D)_kompas.Document3D();
                _document.Create();
                _document = (ksDocument3D)_kompas.ActiveDocument3D();
                _part = (ksPart)_document.GetPart(
                    (short)Part_Type.pTop_Part);
            }
            catch
            {
                throw new ParameterException(
                    ExceptionType.PartBuildingErrorException);
            }
        }
        
        /// <summary>
        /// Функция создание скетча.
        /// </summary>
        /// <param name="plane">Плоскость построения</param>
        /// <param name="sketch">Ссылка на поле скетча</param>
        public void CreateSketch(ksEntity plane, ref ksEntity sketch)
        {
            
            sketch = (ksEntity)_part.NewEntity(
                (short)Obj3dType.o3d_sketch);
            _sketchDefinition = (ksSketchDefinition)sketch
                .GetDefinition();
            _sketchDefinition.SetPlane(plane);
            sketch.Create();
        }

        /// <summary>
        /// Выбор текущего скетча.
        /// </summary>
        /// <param name="target">Целевой скетч</param>
        public void ChooseSketch(string target)
        {
            if (_editStatus)
            {
                _sketchDefinition.EndEdit();
                _editStatus = false;
            }

            switch (target)
            {
                case SketchesTypes.MainString:
                    if (_mainSketch == null)
                    {
                        CreateSketch(
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY),
                            ref _mainSketch);
                    }
                    else
                    {
                        _sketchDefinition = (ksSketchDefinition)
                            _mainSketch.GetDefinition();
                        _sketchDefinition.SetPlane((ksEntity)
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY));
                    }
                    _editStatus = true;
                    break;

                case SketchesTypes.EdgeDirectionString:
                    if (_edgeDircectionSketch == null)
                    {
                        CreateSketch(
                           _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY),
                            ref _edgeDircectionSketch);
                    }
                    else
                    {
                        _sketchDefinition = (ksSketchDefinition)
                            _edgeDircectionSketch.GetDefinition();
                        _sketchDefinition.SetPlane((ksEntity)
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY));
                    }
                    _editStatus = true;
                    break;

                case SketchesTypes.EdgeString:
                    if (_edgeSketch == null)
                    {
                        CreateSketch(
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOZ),
                            ref _edgeSketch);
                    }
                    else
                    {
                        _sketchDefinition = (ksSketchDefinition)
                            _edgeSketch.GetDefinition();
                        _sketchDefinition.SetPlane((ksEntity)
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOZ));
                    }
                    _editStatus = true;
                    break;

                case SketchesTypes.HolesString:
                    if (_holesSketch == null)
                    {
                        CreateSketch(
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY),
                            ref _holesSketch);
                    }
                    else
                    {
                        _sketchDefinition = (ksSketchDefinition)
                            _holesSketch.GetDefinition();
                        _sketchDefinition.SetPlane((ksEntity)
                            _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY));
                    }
                    break;           
                case SketchesTypes.SerreitorString:
                    if (_serreitorSketch == null)
                    {
                        CreateCustomPlane(Offset, 
                             _part.GetDefaultEntity((short)
                            Obj3dType.o3d_planeXOY));
                        CreateSketch(
                            _customPlane,
                            ref _serreitorSketch);
                    }
                    else
                    {
                        _sketchDefinition = (ksSketchDefinition)
                            _serreitorSketch.GetDefinition();
                        _sketchDefinition.SetPlane(_customPlane);
                    }
                    _editStatus = true;
                    break;

                default:
                    break;
            }

            _sketchEdit = (ksDocument2D)_sketchDefinition
                .BeginEdit();
        }

        /// <summary>
        /// Закончить редактирования скетча.
        /// </summary>
        public void EndSkethEdit()
        {
            _sketchDefinition.EndEdit();
            _editStatus = false;
        }

        /// <summary>
        /// Функция постройки отрезка на скетче.
        /// </summary>
        /// <param name="x1">X координата первой точки отрезка</param>
        /// <param name="y1">Y координата первой точки отрезка</param>
        /// <param name="x2">X координата второй точки отрезка</param>
        /// <param name="y2">Y координата второй точки отрезка</param>
        /// <returns>Идентификатор отрезка</returns>
        public long DrawLine(double x1, double y1,
            double x2, double y2)
        {
            return _sketchEdit.ksLineSeg(x1, y1, x2, y2, 1);
        }

        /// <summary>
        /// Выдавливание основного скетча.
        /// </summary>
        /// <param name="thick">Толщина выдавливания</param>
        public void ExtrudeMainBase(double thick)
        {
            ksEntity entity = _part.NewEntity(
                (short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition definition =
                entity.GetDefinition();
            definition.directionType =
                (short)Direction_Type.dtBoth;
            definition.SetSideParam(true,
                (short)End_Type.etBlind, thick / 2);
            definition.SetSideParam(false,
                (short)End_Type.etBlind, thick / 2);
            definition.SetSketch(_mainSketch);
            entity.Create();
        }

        /// <summary>
        /// Выдавливание лезвия.
        /// </summary>
        public void ExtrudeEdge()
        {
            ksEntity entity = _part.NewEntity(
                (short)Obj3dType.o3d_baseExtrusion);
            ksBaseExtrusionDefinition definition =
                entity.GetDefinition();
            definition.directionType =
                (short)Direction_Type.dtBoth;
            definition.SetDepthObject(
                true, _edgeDircectionSketch);
            definition.SetSketch(_edgeSketch);
            entity.Create();
        }

        public void ExtrudeSerreitor()
        {
            ksEntity entity = _part.NewEntity(
                           (short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition definition =
                entity.GetDefinition();
            definition.directionType =
                (short)Direction_Type.dtReverse;
            definition.SetSketch(_serreitorSketch);
            definition.SetSideParam(false, (short)End_Type.etBlind,
                5, 70, false);
            entity.Create();
        }
        /// <summary>
        /// Создание окружности.
        /// </summary>
        /// <param name="x">X координата центра окружности</param>
        /// <param name="y">Y координата центра окружности</param>
        /// <param name="radius">Радиус окружности</param>
        /// <returns>Идентификатор окружности</returns>
        public long CreateCircle(double x, double y,
            double radius)
        {
            return _sketchEdit.ksCircle(x, y, radius, 1);
        }

        /// <summary>
        /// Создание дуги по трем точкам.
        /// </summary>
        /// <param name="x1">X координата первой точки отрезка</param>
        /// <param name="y1">Y координата первой точки отрезка</param>
        /// <param name="x2">X координата второй точки отрезка</param>
        /// <param name="y2">Y координата второй точки отрезка</param>
        /// <param name="x3">X координата третьей точки отрезка</param>
        /// <param name="y3">Y координата третьей точки отрезка</param>
        /// <returns>Идентификатор дуги</returns>
        public long CreateArc(double x1, double y1,
            double x2, double y2, double x3, double y3)
        {
            return _sketchEdit.ksArcBy3Points(
                x1, y1, x2, y2, x3, y3, 1);
        }

        /// <summary>
        /// Вырез лезвия.
        /// </summary>
        public void CutByTrajectory()
        {
            ksEntity cut = _part.NewEntity(
                (short)Obj3dType.o3d_cutEvolution);
            ksCutEvolutionDefinition definition =
                cut.GetDefinition();
            definition.cut = true;
            definition.sketchShiftType = 1;
            definition.SetSketch(_edgeSketch);

            ksEntityCollection entityCollection =
                definition.PathPartArray();
            entityCollection.Clear();
            entityCollection.Add(_edgeDircectionSketch);
            cut.Create();
        }

        /// <summary>
        /// Вырезание выдавливанием.
        /// </summary>
        public void Cut()
        {
            ksEntity entity = _part.NewEntity(
                (short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition definition =
                entity.GetDefinition();
            definition.directionType =
                (short)Direction_Type.dtBoth;
            definition.SetSideParam(true,
                (short)End_Type.etThroughAll);
            definition.SetSketch(_holesSketch);
            entity.Create();
        }

        /// <summary>
        /// Сброс текущего скетча.
        /// </summary>
        public void SetEdgeSketchNull()
        {
            _edgeDircectionSketch = null;
            _edgeSketch = null;
        }

        public void CreateArcBy2PointsAndCenter(double xc, double yc,
            double x1, double y1, double x2, double y2, double rad
            )
        {
            _sketchEdit.ksArcByPoint(xc, yc, rad, x1, y1, x2, y2, -1, 1);
        }
    }
}