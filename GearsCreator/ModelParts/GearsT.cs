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
    /// Зубья.
    /// </summary>
    public class GearsT : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var fThickness = parameters[Parameter.FirstThickness].Value;
            var fExternalRadius = parameters[Parameter.FirstExternalDiameter].Value;
            var fTCount = (int) (fExternalRadius < 8 ? 8 : fExternalRadius);

            var sThickness = parameters[Parameter.SecondThickness].Value;
            var sInnerRadius = parameters[Parameter.SecondInnerDiameter].Value;
            var sExternalRadius = parameters[Parameter.SecondExternalDiameter].Value;
            var sTCount = (int)(sExternalRadius < 8 ? 8 : sExternalRadius);

            var smoothProng = (int)parameters[Parameter.SmoothProng].Value;
            var smoothProng2 = (int)parameters[Parameter.SmoothProng2].Value;
            var part = (ksPart)document3D.GetPart((short)Part_Type.pNew_Part);
            if (part != null)
            {
                var sketchProperty = new KompasSketch
                    {
                        Shape = ShapeType.Line,
                        Plane = PlaneType.PlaneYOZ,
                        CopiesCount = fTCount,
                        ReverseValue = fThickness,
                        Operation = OperationType.CutExtrusion,
                        DirectionType = Direction_Type.dtReverse,
                        OperationColor = Color.Gainsboro
                    };

                switch (smoothProng)

                {
                    case 0:
                    sketchProperty.PointsList.Add(new PointF(3.5f, fExternalRadius));
                    sketchProperty.PointsList.Add(new PointF(3.1f, fExternalRadius - 0.66f));
                    sketchProperty.PointsList.Add(new PointF(2, fExternalRadius - 0.8f));
                    sketchProperty.PointsList.Add(new PointF(1.5f, fExternalRadius - 1.4f));
                    sketchProperty.PointsList.Add(new PointF(1, fExternalRadius - 1.8f));
                    sketchProperty.PointsList.Add(new PointF(0, fExternalRadius - 2));
                    sketchProperty.PointsList.Add(new PointF(-1, fExternalRadius - 1.8f));
                    sketchProperty.PointsList.Add(new PointF(-1.5f, fExternalRadius - 1.4f));
                    sketchProperty.PointsList.Add(new PointF(-2, fExternalRadius - 0.8f));
                    sketchProperty.PointsList.Add(new PointF(-3.1f, fExternalRadius - 0.66f));
                    sketchProperty.PointsList.Add(new PointF(-3.5f, fExternalRadius));
                        break;
                    case 1:        
                    sketchProperty.PointsList.Add(new PointF(2, fExternalRadius));
                    sketchProperty.PointsList.Add(new PointF(1, fExternalRadius - 2));
                    sketchProperty.PointsList.Add(new PointF(-1, fExternalRadius - 2));
                    sketchProperty.PointsList.Add(new PointF(-2, fExternalRadius));
                    break;
                }
                sketchProperty.SketchName = "Зубья 1";
                sketchProperty.CreateNewSketch(part);

                var operation = sketchProperty.OperationsDictionary.Values.Last();
                sketchProperty.CircularCopy(part, null, new List<ksEntity> {operation});
                sketchProperty.PointsList.Clear();
                
                switch (smoothProng)
                {
                    case 0:
                    sketchProperty.PointsList.Add(new PointF(-3, fExternalRadius + 1));
                    sketchProperty.PointsList.Add(new PointF(3, fExternalRadius + 1));
                    sketchProperty.PointsList.Add(new PointF(3, fExternalRadius + 0.2f));
                    sketchProperty.PointsList.Add(new PointF(2.1f, fExternalRadius - 0.3f));
                    sketchProperty.PointsList.Add(new PointF(1.6f, fExternalRadius - 1.2f));
                    sketchProperty.PointsList.Add(new PointF(1, fExternalRadius - 1.7f));
                    sketchProperty.PointsList.Add(new PointF(0, fExternalRadius - 1.9f));
                    sketchProperty.PointsList.Add(new PointF(-1, fExternalRadius - 1.7f));
                    sketchProperty.PointsList.Add(new PointF(-1.6f, fExternalRadius - 1.2f));
                    sketchProperty.PointsList.Add(new PointF(-2.1f, fExternalRadius - 0.3f));
                    sketchProperty.PointsList.Add(new PointF(-3, fExternalRadius + 0.2f));
                        break;
                    case 1:
                    sketchProperty.PointsList.Add(new PointF(1.85f, fExternalRadius + 1));
                    sketchProperty.PointsList.Add(new PointF(1.85f, fExternalRadius));
                    sketchProperty.PointsList.Add(new PointF(1, fExternalRadius - 1.7f));
                    sketchProperty.PointsList.Add(new PointF(-1, fExternalRadius - 1.7f));
                    sketchProperty.PointsList.Add(new PointF(-1.85f, fExternalRadius));
                    sketchProperty.PointsList.Add(new PointF(-1.85f, fExternalRadius + 1));
                    break;
                }
                sketchProperty.CopiesCount = sTCount + 1;
                sketchProperty.NormalValue = sThickness;
                sketchProperty.DirectionType = Direction_Type.dtNormal;
                sketchProperty.Operation = OperationType.BaseExtrusion;
                sketchProperty.SketchName = "Зубья 2";
                sketchProperty.CreateNewSketch(part);

                operation = sketchProperty.OperationsDictionary.Values.Last();
                sketchProperty.CircularCopy(part, -1, -(fExternalRadius + sExternalRadius + 0.2f),
                                            -sInnerRadius, new List<ksEntity> { operation });
            }
        }
    }
}