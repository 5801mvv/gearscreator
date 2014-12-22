using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using GearsCreator.Interfaces;
using GearsCreator.Enumerations;
using System.Collections.Generic;

namespace GearsCreator.ModelParts
{
    /// <summary>
    /// Тело шестерней.
    /// </summary>
    public class ModelBody : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var fThickness = parameters[Parameter.FirstThickness].Value;
            var fInnerRadius = parameters[Parameter.FirstInnerDiameter].Value;
            var fExternalRadius = parameters[Parameter.FirstExternalDiameter].Value;
            
            var sThickness = parameters[Parameter.SecondThickness].Value;
            var sInnerRadius = parameters[Parameter.SecondInnerDiameter].Value;
            var sExternalRadius = parameters[Parameter.SecondExternalDiameter].Value;

            var part = (ksPart)document3D.GetPart((short)Part_Type.pNew_Part);
            if (part != null)
            {
                var sketchProperty = new KompasSketch
                {
                    Shape = ShapeType.Circle,
                    Plane = PlaneType.PlaneYOZ,
                    Radius = fExternalRadius,
                    NormalValue = fThickness,
                    Operation = OperationType.BaseExtrusion,
                    DirectionType = Direction_Type.dtNormal,
                    OperationColor = Color.Gainsboro
                };

                sketchProperty.PointsList.Add(new PointF(0, 0));

                sketchProperty.SketchName = "Тело 1";
                sketchProperty.CreateNewSketch(part);

                sketchProperty.PointsList.Clear();
                sketchProperty.PointsList.Add(new PointF(0, fExternalRadius + sExternalRadius + 0.2f));

                sketchProperty.Radius = sExternalRadius + 0.4f;
                sketchProperty.NormalValue = sThickness;
                sketchProperty.SketchName = "Тело 2";
                sketchProperty.CreateNewSketch(part);

                // Центральные отверстия.
                sketchProperty.PointsList.Clear();
                sketchProperty.PointsList.Add(new PointF(0, 0));

                sketchProperty.Radius = fInnerRadius;
                sketchProperty.ReverseValue = fThickness;
                sketchProperty.Operation = OperationType.CutExtrusion;
                sketchProperty.DirectionType = Direction_Type.dtReverse;
                sketchProperty.SketchName = "Центральное отверстие 1";
                sketchProperty.CreateNewSketch(part);

                sketchProperty.PointsList.Clear();
                sketchProperty.PointsList.Add(new PointF(0, fExternalRadius + sExternalRadius + 0.2f));

                sketchProperty.Radius = sInnerRadius;
                sketchProperty.ReverseValue = sThickness;
                sketchProperty.SketchName = "Центральное отверстие 2";
                sketchProperty.CreateNewSketch(part);
            }
        }
    }
}