using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace ParamScannerAddIn.Utils
{
    public class GetParameterByNameAndValue
    {
        public List<Element> FindParameterByNameAndValue(Document doc, string parameterName, string parameterValue)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            IList<Element> allElements = collector.WhereElementIsNotElementType().ToElements();

            List<Element> matchingElements = new List<Element>();

            foreach (Element element in allElements)
            {
                foreach (Parameter param in element.Parameters)
                {
                    if (param.Definition != null && param.Definition.Name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (param.HasValue)
                        {
                            if (param.AsString() == parameterValue)
                            {
                                matchingElements.Add(element);
                                break;
                            }
                        }
                        else
                        {
                            TaskDialog.Show("Parameter Found", $"Parameter '{parameterName}' found in Element ID: {element.Id} but has no value.");
                        }
                    }
                }

                // Check type parameters
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
                                if (param.HasValue)
                                {
                                    if (param.AsString() == parameterValue)
                                    {
                                        matchingElements.Add(element);
                                        break;
                                    }
                                }
                                else
                                {
                                    TaskDialog.Show("Parameter Found", $"Parameter '{parameterName}' found in Type Element ID: {typeElement.Id} but has no value.");
                                }
                            }
                        }
                    }
                }
            }

            foreach (Element matchingElement in matchingElements)
            {
                TaskDialog.Show("Matching Element", $"Element ID: {matchingElement.Id}, Name: {matchingElement.Name}");
            }

            return matchingElements;
        }
    }

}
