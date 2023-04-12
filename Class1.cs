using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ExportNWC
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]


    //Test @ Class1 adsa 

    public class Class1 : IExternalCommand
    {
        /// <inheritdoc/>
        //MainWindow test = new MainWindow();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;


            MainWindow window = new MainWindow();
            TaskDialog.Show("WELCOME11", "DEV0");
            return Result.Succeeded;

        }
    }
}