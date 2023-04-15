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
      
        UIDocument uidoc;
        Application app;
        Document doc;
        ExternalCommandData CommandData;
        private  string message;
        ElementSet elements;


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

    
            try { 

                MainWindows window = new MainWindows(uidoc);
                window.ShowDialog();
                //start export process 
               // window.Export_Button
                //then close window
                //window.Close();
                return Result.Succeeded;

                
            }

            catch (System.Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }
            //return Autodesk.Revit.UI.Result.Succeeded;
        }

        /*public void ExportNWC()
        {
            uiapp = CommandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            NavisworksExportOptions nwcOptions = new NavisworksExportOptions();
            nwcOptions.ConvertElementProperties = true;
            nwcOptions.ConvertLinkedCADFormats = true;
            nwcOptions.Coordinates = NavisworksCoordinates.Internal;//Desfualt is shared
            nwcOptions.DivideFileIntoLevels = true;
            nwcOptions.ExportElementIds = true;
            nwcOptions.ExportLinks = true;
            nwcOptions.ExportParts = true;
            nwcOptions.ExportRoomAsAttribute = true;
            nwcOptions.ExportRoomGeometry = true;
            nwcOptions.ExportScope = NavisworksExportScope.Model; // Check 
            nwcOptions.ExportUrls = true;
            nwcOptions.FacetingFactor = 1.0;
            nwcOptions.FindMissingMaterials = true;
            nwcOptions.ViewId = doc.ActiveView.Id;
            
            nwcOptions.Parameters = NavisworksParameters.All;

            try
            {
                //Selecting current view
                Selection select = uidoc.Selection;
                if (doc.ActiveView.Document == null)
                {
                    // If no elements selected.
                    TaskDialog.Show("Revit", "Currenyt view is null");
                }
                else
                {
                    doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions); ;
                    TaskDialog.Show("Revit", "Export Completed");

                }

            }

            catch (System.Exception e)
            {
                message = e.Message;    
            }
        }*/

    }
}

