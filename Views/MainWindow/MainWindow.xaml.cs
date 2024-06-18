using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.EventHandles;
using System;
using System.Windows;
using ParamScannerAddIn.ViewModels;

namespace ParamScannerAddIn.Views.MainWindow
{
    #region Main Window View Scanner
    public partial class MainWindow : Window
    {
        #region Properties
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly IsolateElementsHandler _mIsolateElementsHandler;
        #endregion

        #region Constructor
        public MainWindow(UIApplication uiApp, IsolateElementsHandler isolateElementsHandler)
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(uiApp.ActiveUIDocument.Document, uiApp.ActiveUIDocument, isolateElementsHandler);
            DataContext = viewModel;
            Closed += MainWindow_Closed;
        }
        #endregion

        #region Events
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
    #endregion
}
