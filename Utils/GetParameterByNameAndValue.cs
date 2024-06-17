using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace ParamScannerAddIn.Utils
{
    public class GetParameterByNameAndValue
    {
        public List<Element> FindParameterByNameAndValue(Document doc, string parameterName, string parameterValue = null)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> allElements = collector.WhereElementIsNotElementType().ToElements();

            List<Element> matchingElements = new List<Element>();

            foreach (Element element in allElements)
            {
                bool parameterMatched = false;

                foreach (Parameter param in element.Parameters)
                {
                    if (param.Definition != null && param.Definition.Name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(parameterValue) || param.AsString() == parameterValue)
                        {
                            matchingElements.Add(element);
                            parameterMatched = true;
                            break;
                        }
                    }
                }

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
                                    if (string.IsNullOrEmpty(parameterValue) || param.AsString() == parameterValue)
                                    {
                                        matchingElements.Add(element);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return matchingElements;
        }
    }
}
