using Kompas6API5;
using GearsCreator.Interfaces;
using GearsCreator.ModelParts;
using GearsCreator.Enumerations;
using System.Collections.Generic;

namespace GearsCreator
{
    /// <summary>
    /// Содержит методы для построения детали (модели).
    /// </summary>
    public class ModelBuilder
    {
        /// <summary>
        /// Строит модель.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Build(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var modelParts = new List<IModelPart>
                {
                    new ModelBody(),
                    new GearsT(),
                    new GearHoles()
                };


            HideAllGeom(document3D);
            SetViewProjection(document3D, 7);
            foreach (IModelPart modelPart in modelParts)
            {
                modelPart.Create(document3D, parameters);
            }
        }

        /// <summary>
        /// Скрывает все оси и геометрические обозначения.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        public void HideAllGeom(ksDocument3D document3D)
        {
            if (document3D == null) return;

            document3D.hideAllAuxiliaryGeom = true;
            document3D.hideAllSketches = false;
        }

        /// <summary>
        /// Задает ориентацию.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="index">Индекс.</param>
        public void SetViewProjection(ksDocument3D document3D, int index)
        {
            if (document3D == null) return;

            var projectionCollection = document3D.GetViewProjectionCollection() as ksViewProjectionCollection;

            if (projectionCollection == null) return;
            var projection = projectionCollection.Next() as ksViewProjection;

            while (projection != null)
            {
                if (projection.index == index)
                {
                    projection.SetCurrent();
                    break;
                }

                projection = projectionCollection.Next();
            }
        }
    }
}