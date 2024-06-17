using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParamScannerAddIn.Commands;
using ParamScannerAddIn.EventHandles;
using ParamScannerAddIn.Models;
using ParamScannerAddIn.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ParamScannerAddIn.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ParameterModel _parameterModel;
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly IsolateElementsHandler _mIsolateElementsHandler;

        public ICommand IsolateInViewCommand { get; }
        public ICommand SelectElementsByParameterCommand { get; }

        public MainWindowViewModel(Document doc, UIDocument uiDoc, IsolateElementsHandler isolateElementsHandler)
        {
            _parameterModel = new ParameterModel();
            _doc = doc;
            _uiDoc = uiDoc;
            _mIsolateElementsHandler = isolateElementsHandler;

            IsolateInViewCommand = new RelayCommand(IsolateInView);
            SelectElementsByParameterCommand = new RelayCommand(SelectElementsByParameter);
        }

        public string ParameterName
        {
            get => _parameterModel.ParameterName;
            set
            {
                if (_parameterModel.ParameterName != value)
                {
                    _parameterModel.ParameterName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ParameterValue
        {
            get => _parameterModel.ParameterValue;
            set
            {
                if (_parameterModel.ParameterValue != value)
                {
                    _parameterModel.ParameterValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void IsolateInView(object parameter)
        {
            if (string.IsNullOrEmpty(ParameterName))
            {
                ShowMessageNotification("Parameter Name cannot be left empty");
                return;
            }

            try
            {
                GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
                List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, ParameterName, ParameterValue);

                if (elementsFound.Count == 0)
                {
                    ShowMessageNotification("No elements found");
                    return;
                }

                _mIsolateElementsHandler.Uidoc = _uiDoc;
                _mIsolateElementsHandler.Raise(elementsFound.Select(elementItem => elementItem.Id).ToList());

                ShowMessageNotification("Item Isolated");

            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        private void SelectElementsByParameter(object parameter)
        {
            if (string.IsNullOrEmpty(ParameterName))
            {
                ShowMessageNotification("Parameter Name cannot be left empty");
                return;
            }

            try
            {
                GetParameterByNameAndValue parameterFinder = new GetParameterByNameAndValue();
                List<Element> elementsFound = parameterFinder.FindParameterByNameAndValue(_doc, ParameterName, ParameterValue);

                if (elementsFound.Count == 0)
                {
                    ShowMessageNotification("No elements found");
                    return;
                }

                _uiDoc.Selection.SetElementIds(elementsFound.Select(foundElement => foundElement.Id).ToList());

                ShowMessageNotification("Items Selected");
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }

        private void ShowMessageNotification(string message)
        {
            TaskDialog.Show("Notification", message);
        }
    }
}
