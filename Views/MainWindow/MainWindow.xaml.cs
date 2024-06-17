using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.EventHandles;
using ParamScannerAddIn.Utils;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Linq;

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
            Closed += MainWindow_Closed;

            InitializeComponent();
            _mIsolateElementsHandler = mIsolateElementsHandler;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Isolate_In_View_Click(object sender, RoutedEventArgs e)
        {
            string paramName = txt_Parameter_name.Text;
            string paramValue = txt_Parameter_Value.Text;

            if (string.IsNullOrEmpty(txt_Parameter_name.Text))
            {
                MessageBox.Show("Parameter Name cannot be left empty");
                return;
            }

            GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
            List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, paramName, paramValue);

            if (elementsFound.Count == 0)
            { return; }

            _mIsolateElementsHandler.Uidoc = _uiDoc;

            _mIsolateElementsHandler.Raise(elementsFound.Select(elementItem => elementItem.Id).ToList());

            MessageBox.Show("Item Isolated");
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {

            string paramName = txt_Parameter_name.Text;
            string paramValue = txt_Parameter_Value.Text;

            if (string.IsNullOrEmpty(txt_Parameter_name.Text))
            {
                MessageBox.Show("Parameter Name cannot be left empty");
                return;
            }

            GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
            List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, paramName, paramValue);

            if (elementsFound.Count == 0)
            { return; }

            foreach (Element matchingElement in elementsFound)
            {
                _uiDoc.Selection.SetElementIds(elementsFound.Select(foundElement => foundElement.Id).ToList());
            }

            MessageBox.Show("Itens Selected");
        }
    } 
    #endregion
}
