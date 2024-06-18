using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace ParamScannerAddIn.Utils
{
    public class GetParameterByNameAndValue
    {
        /// <summary>
        /// This method is responsible to get All Parameter, with/no Values
        /// </summary>
        /// <param name="doc">Current App</param>
        /// <param name="parameterName">Form Parameter Name</param>
        /// <param name="parameterValue">Form Parameter Value</param>
        /// <returns></returns>
        public List<Element> FindParameterByNameAndValue(Document doc, string parameterName, string parameterValue = null)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> allElements = collector.WhereElementIsNotElementType().ToElements();

            List<Element> matchingElements = new List<Element>();

            foreach (Element element in allElements)
            {
                bool parameterMatched = false;

                #region Verify instance parameters of the element
                foreach (Parameter param in element.Parameters)
                {
                    if (param.Definition != null && param.Definition.Name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (ParamaterUtils.ContainsParameterValue(param, parameterValue, element))
                        {
                            matchingElements.Add(element);
                            parameterMatched = true;
                            break;
                        }
                    }
                }
                #endregion

                #region Verify type parameters of the element
                if (!parameterMatched)
                {
                    ElementId typeId = element.GetTypeId();
                    if (typeId != ElementId.InvalidElementId)
                    {
                        Element typeElement = doc.GetElement(typeId);
                        if (typeElement != null)
                        {
                            foreach (Parameter param in typeElement.Parameters)
                            {
                                if (param.Definition != null && param.Definition.Name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (ParamaterUtils.ContainsParameterValue(param, parameterValue, element))
                                    {
                                        matchingElements.Add(element);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                } 
                #endregion
            }

            return matchingElements;
        }
    }
}
