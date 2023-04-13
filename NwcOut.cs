using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace ExportNWC
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class NwcOut : IExternalCommand
    {
        /// <inheritdoc/>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //nwc properties
            //document indicate an autodesk revit project
            try
            {
                UIApplication uiapp = commandData.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Application app = uiapp.Application;
                Document doc = uidoc.Document;



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
                //to Do: nwcOptions.IsValidObject check property
                nwcOptions.Parameters = NavisworksParameters.All;
                // nwcOptions.ViewId =view.Id;

                //Selecting current view
                Selection select = uidoc.Selection;


                // String assName = Assembly.GetExecutingAssembly().Location;
                //  string Path = System.IO.Path.GetDirectoryName();

                /*using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Export");

                    doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions);

                    tx.Commit();
                }*/
                if (doc.ActiveView == null)
                {
                    // If no elements selected.
                    TaskDialog.Show("Revit", "Currenyt view is null");
                }
                else
                {
                    doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions);
                    TaskDialog.Show("Revit", "Completed");
                }

            }

            catch (System.Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}

