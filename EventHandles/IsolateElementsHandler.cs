using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace ParamScannerAddIn.EventHandles
{
    public class IsolateElementsHandler : ExternalEventHandler<List<ElementId>>
    {
        public UIDocument Uidoc { get; set; }

        #region Execute
        /// <summary>
        /// Responsible to Execute A External Event that needs Transaction
        /// </summary>
        /// <param name="uiApp">To Get the uiDoc and Activated View</param>
        /// <param name="elementIds">Elements Id to be shown in the isolate View</param>
        public override void Execute(UIApplication uiApp, List<ElementId> elementIds)
        {
            Document doc = Uidoc.Document;
            View activeView = doc.ActiveView;

            using (Transaction t = new Transaction(doc, "Create and Isolate View For the Elements"))
            {
                t.Start();

                ElementId newViewId = activeView.Duplicate(ViewDuplicateOption.WithDetailing);

                View newView = doc.GetElement(newViewId) as View;

                if (newView != null)
                {
                    newView.IsolateElementsTemporary(elementIds);

                    t.Commit();

                    Uidoc.ActiveView = newView;
                }
                else
                {
                    t.RollBack();
                }
            }
        } 
        #endregion
    }

}
