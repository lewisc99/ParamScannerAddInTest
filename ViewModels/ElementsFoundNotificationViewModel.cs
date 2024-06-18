using Autodesk.Revit.DB;
using ParamScannerAddIn.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using ParamScannerAddIn.Commands;
using System;
using ParamScannerAddIn.Utils;

namespace ParamScannerAddIn.ViewModels
{
    public class ElementsFoundNotificationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ElementParamModel> _elementsList;
        private string _elementsCountText;

        public ObservableCollection<ElementParamModel> ElementsList
        {
            get => _elementsList;
            set
            {
                _elementsList = value;
                OnPropertyChanged();
            }
        }

        public string ElementsCountText
        {
            get => _elementsCountText;
            set
            {
                _elementsCountText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ListOfElementsFoundCommand { get; }

        public ElementsFoundNotificationViewModel(List<Element> elementsFoundList)
        {
            _elementsList = new ObservableCollection<ElementParamModel>();
            ListOfElementsFoundCommand = new RelayCommand(param => ListOfElementsFound(elementsFoundList));

            ListOfElementsFound(elementsFoundList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ListOfElementsFound(List<Element> elementsFoundList)
        {
            try
            {
                foreach (Element matchingElement in elementsFoundList)
                {
                    ElementParamModel element = new ElementParamModel
                    {
                        ElementId = matchingElement.Id.ToString(),
                        ElementName = matchingElement.Name
                    };

                    _elementsList.Add(element);
                }

                ElementsCountText = $"Parameters with Value Found: {_elementsList.Count}";
            }
            catch (Exception exception)
            {
                ExceptionHandler.HandleException(exception);
            }
        }
    }
}
