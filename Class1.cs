using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
//using Autodesk.Revit.UI.IExternalCommand;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //Place the group
            Transaction trans = new Transaction(doc);
            trans.Start("Lab");
            doc.Create.PlaceGroup(point, group.GroupType);
            trans.Commit();*/
            TaskDialog.Show("WELCOME!!","DEV");

            MainWindow window = new MainWindow();
            TaskDialog.Show("WELCOME11", "DEV0");
            return Result.Succeeded;

        }
    }
}