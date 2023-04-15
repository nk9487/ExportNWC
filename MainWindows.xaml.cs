using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.DB;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.UI.Selection;

namespace ExportNWC
{
    /// <summary>
    /// Interaction logic for MainWindows.xaml
    /// </summary>
    public partial class MainWindows : Window
    {
        //Feilds
        public UIDocument UIdoc { get;  }
        public Document Doc { get; }




        public MainWindows(UIDocument uiDoc) 
        {

            UIdoc = uiDoc;
            Doc = uiDoc.Document;
            InitializeComponent();
            Title = "ExportNWC";
        }


        private void ExportNWC(object sender, RoutedEventArgs e)
        {
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
            nwcOptions.ViewId = Doc.ActiveView.Id;

            nwcOptions.Parameters = NavisworksParameters.All;
            Doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions);


           /* using (Transaction t = new Transaction(Doc,"Export"))
            {

                t.Start();
                Selection select = UIdoc.Selection;
                
                //TaskDialog.Show("Revit", "Export Completed");
                t.Commit();
            }*/

        }
    }
}
