
## Parameter Scanner Add-In


### Description

This project aims to develop my knowledge and proficiency in developing Revit add-ins.

The main window has two inputs, "Parameter Name" and "Parameter Value," where you can search the model for all elements that match the parameter by name or by both name and value.

There is a button to isolate the matched elements in a new view or to select the elements.

I implemented a new feature that displays all elements with both the specified parameter name and value, including the ID and name of each element.

In this project, I followed best architectural practices using WPF with MVVM, ensuring proper separation of responsibilities.

Enjoy it :).

---

### Stack

1. .Net framework 4.8
2. WPF
3. `RevitAPI.dll` and `RevitAPIUI.dll` (2020)

### How to Run Cloning the Project, Without the Zip Files

1. **Clone/Download the Repository**:
   - Clone or download this repository and unzip the files.

2. **Open the Solution**:
   - Open the `.sln` file at the root of the repository with Microsoft Visual Studio.
   - Ensure the add-in manifest file is correctly configured to point to the `dll` in `<Assembly>`:

     ```xml
     <?xml version="1.0" encoding="utf-8" standalone="no"?>
     <RevitAddIns>
       <AddIn Type="Application">
         <Name>Revit Parameter Scanner</Name>
         <Assembly>%AppData%\Autodesk\Revit\Addins\2020\ParamScannerAddIn.dll</Assembly>
         <AddInId>e2f8c35d-0393-45c1-a6e3-4bd653dc98a1</AddInId>
         <FullClassName>ParamScannerAddIn.Startup</FullClassName>
         <VendorId>Lewis Carlos</VendorId>
         <VendorDescription>https://github.com/lewisc99</VendorDescription>
       </AddIn>
     </RevitAddIns>
     ```

3. **Re-link References**:
   - Re-link references to `RevitAPI.dll` and `RevitAPIUI.dll` and other required libraries that may be missing (the project doesn't have external libraries).
   - If you don't know where `RevitAPI.dll` and `RevitAPIUI.dll` are, check in `%AppData%\Autodesk\Revit\Addins\2020`.

4. **Build the Solution**:
   - Build the solution. Building the solution will automatically create and copy the add-in files to the folder for Revit 2020.

5. **Open Revit**:
   - Upon opening Revit 2020, there should be a tab called "Parameters" in Revit, with a button to launch the WPF add-in.

---

### Manual Installation (Alternative)

1. **Unzip the Add-In Files**:
   - Copy and paste the files into `%AppData%\Autodesk\Revit\Addins\2020`.

2. **Add-In Manifest**:
   - Ensure the add-in manifest file is correctly configured to point to the `dll` in `<Assembly>`:

     ```xml
     <?xml version="1.0" encoding="utf-8" standalone="no"?>
     <RevitAddIns>
       <AddIn Type="Application">
         <Name>Revit Parameter Scanner</Name>
         <Assembly>%AppData%\Autodesk\Revit\Addins\2020\ParamScannerAddIn.dll</Assembly>
         <AddInId>e2f8c35d-0393-45c1-a6e3-4bd653dc98a1</AddInId>
         <FullClassName>ParamScannerAddIn.Startup</FullClassName>
         <VendorId>Lewis Carlos</VendorId>
         <VendorDescription>https://github.com/lewisc99</VendorDescription>
       </AddIn>
     </RevitAddIns>
     ```

3. **Open Revit**:
   - Upon opening Revit 2020, there should be a tab called "Parameters" in Revit, with a button to launch the WPF add-in.
---

## LinkedIn

<h4 align="center">
   Created by <a href="https://www.linkedin.com/in/luiz-carlos-b50693173/" target="_blank"> Luiz Carlos </a>
</h4>
