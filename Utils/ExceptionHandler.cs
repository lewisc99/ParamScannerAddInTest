using System;
using System.Windows;

namespace ParamScannerAddIn.Utils
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception ex)
        {
            MessageBox.Show($"An error occurred:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
