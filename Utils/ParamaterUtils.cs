using Autodesk.Revit.DB;

namespace ParamScannerAddIn.Utils
{
    public static class ParamaterUtils
    {
        /// <summary>
        /// Will Handle all Types of Value from the return of Element Parameter Value
        /// </summary>
        /// <param name="param"></param>
        /// <param name="parameterValue"></param>
        /// <param name="element"></param>
        /// <returns>Bool to pass insert the Element or not into the Elements Found to Select</returns>
        public static bool ContainsParameterValue(Parameter param, string parameterValue, Element element)
        {
            if (string.IsNullOrEmpty(parameterValue)) {
                return true;
            }

            #region Will Handle all Types of Value from the Parameter Value
            switch (param.StorageType)
            {
                case StorageType.String:
                    if (param.AsString() == parameterValue)
                        return true;
                    break;

                case StorageType.Integer:
                    int.TryParse(parameterValue, out int intValue);

                    if (param.AsInteger() == intValue)
                        return true;
                    break;

                case StorageType.Double:
                    double.TryParse(parameterValue, out double doubleValue);

                    if (param.AsDouble() == doubleValue)
                        return true;

                    break;

                case StorageType.ElementId:
                    ElementId paramElementId = param.AsElementId();
                    if (paramElementId != ElementId.InvalidElementId)
                    {
                        if (element != null && element.Name.Contains(parameterValue))
                            return true;
                    }
                    break;
                default:
                    return false;
            }
            #endregion

            return false;
        }
    }
}
