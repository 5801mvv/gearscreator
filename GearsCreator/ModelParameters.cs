using System.Drawing;
using GearsCreator.Enumerations;
using System.Collections.Generic;

namespace GearsCreator
{
    /// <summary>
    /// Содержит параметры модели.
    /// </summary>
    public class ModelParameters
    {
        /// <summary>
        /// Словарь параметров.
        /// </summary>
        public Dictionary<Parameter, ParameterData> Parameters { get; private set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public ModelParameters()
        {
            Initialize();
        }

        /// <summary>
        /// Инициализирует переменные.
        /// </summary>
        private void Initialize()
        {
            Parameters = new Dictionary<Parameter, ParameterData>
                {
                    {
                        Parameter.FirstExternalDiameter, new ParameterData(Parameter.FirstExternalDiameter.ToString(), 20, new PointF(18, 50))
                    },
                    {
                        Parameter.FirstInnerDiameter, new ParameterData(Parameter.FirstInnerDiameter.ToString(), 8, new PointF(6, 50))
                    },
                    {
                        Parameter.FirstThickness, new ParameterData(Parameter.FirstThickness.ToString(), 2, new PointF(2, 50))
                    },
                    {
                        Parameter.FirstHolesCount, new ParameterData(Parameter.FirstHolesCount.ToString(), 4, new PointF(4, 8))
                    },                    
                    {
                        Parameter.SmoothProng, new ParameterData(Parameter.SmoothProng.ToString(), new PointF(0, 1))
                    },  
                    {
                        Parameter.SecondExternalDiameter, new ParameterData(Parameter.SecondExternalDiameter.ToString(), 13, new PointF(13, 50))
                    },
                    {
                        Parameter.SecondInnerDiameter, new ParameterData(Parameter.SecondInnerDiameter.ToString(), 5, new PointF(5, 50))
                    },
                    {
                        Parameter.SecondThickness, new ParameterData(Parameter.SecondThickness.ToString(), 2, new PointF(2, 50))
                    },
                    {
                        Parameter.SecondHolesCount, new ParameterData(Parameter.SecondHolesCount.ToString(), 4, new PointF(4, 8))
                    },    
                    {
                        Parameter.SmoothProng2, new ParameterData(Parameter.SmoothProng2.ToString(), new PointF(0, 1))
                    },                
                };
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <param name="parameters">Словарь параметров для проверки.</param>
        /// <returns>Список ошибок.</returns>
        public List<string> CheckData(Dictionary<Parameter, ParameterData> parameters)
        {
            var errorList = new List<string>();

            foreach (KeyValuePair<Parameter, ParameterData> parameter in parameters)
            {
                switch (parameter.Key)
                {
                    case Parameter.FirstExternalDiameter:
                        {
                            SetMaxValue(Parameter.FirstHolesCount, parameter.Value.Value);
                            SetMaxValue(Parameter.SecondExternalDiameter, parameter.Value.Value - 5);
                            SetMaxValue(Parameter.FirstInnerDiameter, parameter.Value.Value - 10);
                            SetMinValue(Parameter.FirstInnerDiameter, parameter.Value.Value / 3);
                            //if ((int) Parameter.FirstInnerDiameter < 6)
                            //{
                            //    SetMinValue(Parameter.FirstInnerDiameter, 6);
                            //    SetMaxValue(Parameter.FirstInnerDiameter, 8);
                            //}
                            //if ((int)Parameter.SecondExternalDiameter < 13)
                            //{
                            //    SetMinValue(Parameter.SecondExternalDiameter, 13);
                            //    SetMaxValue(Parameter.SecondExternalDiameter, 13);
                            //}
                            SetMinValue(Parameter.FirstHolesCount, 4);
                            SetMaxValue(Parameter.FirstHolesCount, 8);
                        }
                        break;

                    case Parameter.SecondExternalDiameter:
                        {
                            SetMaxValue(Parameter.SecondHolesCount, parameter.Value.Value);
                            SetMaxValue(Parameter.SecondInnerDiameter, parameter.Value.Value - 6);
                            SetMinValue(Parameter.SecondInnerDiameter, parameter.Value.Value / 3);
                            SetMinValue(Parameter.SecondHolesCount, 4);
                            SetMaxValue(Parameter.SecondHolesCount, 8);
                        }
                        break;
                }

                var value = parameter.Value.Value;
                var validValue = GetValidValue(parameter.Key);

                if (validValue == null) continue;

                if (!(value >= validValue.RangeValue.X && value <= validValue.RangeValue.Y))
                {
                    if (validValue.RangeValue.X <= validValue.RangeValue.Y)
                    {
                        errorList.Add("Значение параметра '" + parameter.Value.Description +
                                      "', должно лежать в диапазоне от " + validValue.RangeValue.X + " до " +
                                      validValue.RangeValue.Y + ".\n");
                    }
                }
            }

            return errorList;
        }

        /// <summary>
        /// Возвращает допустимые значения.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns>Допустимое значение.</returns>
        private ParameterData GetValidValue(Parameter parameter)
        {
            if (Parameters.ContainsKey(parameter))
            {
                return Parameters[parameter];
            }

            return null;
        }

        /// <summary>
        /// Задает новое максимальное значение параметра.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <param name="maxValue">Новое значение.</param>
        private void SetMaxValue(Parameter parameter, float maxValue)
        {
            if (Parameters.ContainsKey(parameter))
            {
                var currentParameter = Parameters[parameter];
                Parameters[parameter] = new ParameterData(currentParameter.Name,
                                                          new PointF(currentParameter.RangeValue.X, maxValue));
            }
        }

        /// <summary>
        /// Задает новое минимальное значение параметра.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <param name="minValue">Новое значение.</param>
        private void SetMinValue(Parameter parameter, float minValue)
        {
            if (Parameters.ContainsKey(parameter))
            {
                var currentParameter = Parameters[parameter];
                Parameters[parameter] = new ParameterData(currentParameter.Name,
                                                          new PointF(minValue, currentParameter.RangeValue.Y));
            }
        }

        ///// <summary>
        ///// Задает новый диапазон значений параметра.
        ///// </summary>
        ///// <param name="parameter">Параметр.</param>
        ///// <param name="minValue">Минимальное значение.</param>
        ///// <param name="maxValue">Максимальное значение.</param>
        //private void SetRange(Parameter parameter, float minValue, float maxValue)
        //{
        //    if (Parameters.ContainsKey(parameter))
        //    {
        //        var currentParameter = Parameters[parameter];
        //        Parameters[parameter] = new ParameterData(currentParameter.Name,
        //                                                  new PointF(minValue, maxValue));
        //    }
        //}
    }
}