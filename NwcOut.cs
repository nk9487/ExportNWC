using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace ExportNWC
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class NwcOut : IExternalCommand
    {
        /// <inheritdoc/>
        /// 
      
        //UIDocument uidoc;
        //Application app;
        //Document doc;
        //ExternalCommandData CommandData;
        //private  string message;
        //ElementSet elements;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            //Document doc = uidoc.Document;

    
            try { 

                MainWindows window = new MainWindows(uidoc);

                window.ShowDialog();

                // Show task completed dialog box
 
                
                return Result.Succeeded;
            }

            catch (System.Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }
            //return Autodesk.Revit.UI.Result.Succeeded;
        }

       

    }
}

