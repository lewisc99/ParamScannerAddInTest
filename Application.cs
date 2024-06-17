using Autodesk.Revit.UI;

namespace ParamScannerAddIn
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            throw new System.NotImplementedException();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            throw new System.NotImplementedException();
        }
    }
}
