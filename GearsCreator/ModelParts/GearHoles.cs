using System.Linq;
using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using GearsCreator.Interfaces;
using GearsCreator.Enumerations;
using System.Collections.Generic;

namespace GearsCreator.ModelParts
{
    /// <summary>
    /// Отверстия.
    /// </summary>
    public class GearHoles : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var fThickness = parameters[Parameter.FirstThickness].Value;
            var fHolesCount = (int)parameters[Parameter.FirstHolesCount].Value;
            var fInnerRadius = parameters[Parameter.FirstInnerDiameter].Value;
            var fExternalRadius = parameters[Parameter.FirstExternalDiameter].Value;

            var sThickness = parameters[Parameter.SecondThickness].Value;
            var sInnerRadius = parameters[Parameter.SecondInnerDiameter].Value;
            var sHolesCount = (int)parameters[Parameter.SecondHolesCount].Value;
            var sExternalRadius = parameters[Parameter.SecondExternalDiameter].Value;

            var part = (ksPart)document3D.GetPart((short)Part_Type.pNew_Part);
            if (part != null)
            {
                var sketchProperty = new KompasSketch
                {
                    Shape = ShapeType.Circle,
                    Plane = PlaneType.PlaneYOZ,
                    Radius = (fExternalRadius-fInnerRadius)/5,
                    CopiesCount = fHolesCount,
                    ReverseValue = fThickness,
                    Operation = OperationType.CutExtrusion,
                    DirectionType = Direction_Type.dtReverse,
                    OperationColor = Color.Gainsboro
                };

                sketchProperty.PointsList.Add(new PointF(0, fInnerRadius + (fExternalRadius - fInnerRadius)/2 - 0.5f));

                sketchProperty.SketchName = "Отверстия 1";
                sketchProperty.CreateNewSketch(part);

                var operation = sketchProperty.OperationsDictionary.Values.Last();
                sketchProperty.CircularCopy(part, null, new List<ksEntity> { operation });

                sketchProperty.PointsList.Clear();
                sketchProperty.PointsList.Add(new PointF(0, fExternalRadius + (sExternalRadius - sInnerRadius)/2));
                sketchProperty.Radius = (sExternalRadius - sInnerRadius)/4.6f;

                sketchProperty.CopiesCount = sHolesCount;
                sketchProperty.ReverseValue = sThickness;
                sketchProperty.SketchName = "Отверстия 2";
                sketchProperty.CreateNewSketch(part);

                operation = sketchProperty.OperationsDictionary.Values.Last();
                sketchProperty.CircularCopy(part, -1, -(fExternalRadius + sExternalRadius + 0.2f),
                                            -sInnerRadius, new List<ksEntity> { operation });
            }
        }
    }
}