using Autodesk.Revit.DB;
using ParamScannerAddIn.EventHandles;
using ParamScannerAddIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ParamScannerAddIn.Views.ElementsFoundNotification
{
    public partial class ElementsFoundNotification : Window
    {
        private readonly IsolateElementsHandler _mIsolateElementsHandler;

        #region Constructor
        public ElementsFoundNotification(List<Element> elementsFoundList)
        {
            InitializeComponent();
            ElementsFoundNotificationViewModel viewModel = new ElementsFoundNotificationViewModel(elementsFoundList);
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
}
