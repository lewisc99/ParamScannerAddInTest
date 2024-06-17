using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.EventHandles;
using System.Windows;

namespace ParamScannerAddIn.Views.MainWindow
{
    #region Main Window View Scanner
    public partial class MainWindow : Window
    {
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly IsolateElementsHandler _mIsolateElementsHandler;

        public MainWindow(UIApplication uiApp, IsolateElementsHandler mIsolateElementsHandler)
        {
            _uiDoc = uiApp.ActiveUIDocument;
            _doc = _uiDoc.Document;
            //Closed += MainWindow_Closed;

            InitializeComponent();
            _mIsolateElementsHandler = mIsolateElementsHandler;
        }

    } 
    #endregion
}
