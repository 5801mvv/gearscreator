using System.ComponentModel;

namespace GearsCreator.Enumerations
{
    /// <summary>
    /// Параметры модели.
    /// </summary>
    public enum Parameter
    {
        [Description("1. Внешний диаметр")]
        FirstExternalDiameter,

        [Description("2. Внутренний диаметр")]
        FirstInnerDiameter,

        [Description("3. Толщина")]
        FirstThickness,

        [Description("4. Количество отверстий")]
        FirstHolesCount,
        
        [Description("5. Сгладить зубцы")]
        SmoothProng,

        [Description("6. Внешний диаметр")]
        SecondExternalDiameter,

        [Description("7. Внутренний диаметр")]
        SecondInnerDiameter,

        [Description("8. Толщина")]
        SecondThickness,

        [Description("9. Количество отверстий")]
        SecondHolesCount,

        [Description("10. Сгладить зубцы")]
        SmoothProng2,
    }
}