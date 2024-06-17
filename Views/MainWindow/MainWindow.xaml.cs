using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.EventHandles;
using ParamScannerAddIn.Utils;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Linq;
using ParamScannerAddIn.ViewModels;

namespace ParamScannerAddIn.Views.MainWindow
{
    #region Main Window View Scanner
    public partial class MainWindow : Window
    {
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly IsolateElementsHandler _mIsolateElementsHandler;

        public MainWindow(UIApplication uiApp, IsolateElementsHandler isolateElementsHandler)
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(uiApp.ActiveUIDocument.Document, uiApp.ActiveUIDocument, isolateElementsHandler);
            DataContext = viewModel;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Close();
        }
    } 
    #endregion
}
