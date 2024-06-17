using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows.Controls;

namespace ParamScannerAddIn.Utils
{
    public static class ParamaterUtils
    {
        public static bool ContainsParameterValue(Parameter param, string parameterValue, Element element)
        {
            if (string.IsNullOrEmpty(parameterValue)) {
                return true;
            }

            switch (param.StorageType)
            {
                case StorageType.String:
                    if (param.AsString() == parameterValue) return true;
                 break;

                case StorageType.Integer:
                    int.TryParse(parameterValue, out int intValue);

                    if (param.AsInteger() == intValue) return true;
                break;

                case StorageType.Double:
                    double.TryParse(parameterValue, out double doubleValue);

                    if (param.AsDouble() == doubleValue) return true;

                    break;

                case StorageType.ElementId:
                    ElementId paramElementId = param.AsElementId();
                    if (paramElementId != ElementId.InvalidElementId)
                    {
                        if (element != null && element.Name.Contains(parameterValue)) return true;
                    }
                    break;
                default:
                    return false;
            }

            return false;
        }
    }
}
