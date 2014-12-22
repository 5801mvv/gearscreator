using System.ComponentModel;

namespace GearsCreator.Enumerations
{
    /// <summary>
    /// ��������� ������.
    /// </summary>
    public enum Parameter
    {
        [Description("1. ������� �������")]
        FirstExternalDiameter,

        [Description("2. ���������� �������")]
        FirstInnerDiameter,

        [Description("3. �������")]
        FirstThickness,

        [Description("4. ���������� ���������")]
        FirstHolesCount,
        
        [Description("5. �������� �����")]
        SmoothProng,

        [Description("6. ������� �������")]
        SecondExternalDiameter,

        [Description("7. ���������� �������")]
        SecondInnerDiameter,

        [Description("8. �������")]
        SecondThickness,

        [Description("9. ���������� ���������")]
        SecondHolesCount,

        [Description("10. �������� �����")]
        SmoothProng2,
    }
}