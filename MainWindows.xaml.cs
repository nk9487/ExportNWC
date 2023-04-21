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
using Microsoft.Win32;
using System.Windows.Forms;
using System.Collections;
using System.Security.Policy;

namespace ExportNWC
{
    /// <summary>
    /// Interaction logic for MainWindows.xaml
    /// </summary>
    public partial class MainWindows : Window
    {
        //Feilds for the UI
        public UIDocument UIdoc { get; }
        public Document Doc { get; }

        private String FILE_PATH;
        //public ICollection<View3D> All_VIEWS;
        private FilteredElementCollector All_VIEWS;
        private ICollection<Element> VIEW;
        private ICollection<Element> USER_SELECTED_3DVIEW;
    

        private List<Dictionary<ElementId, String>> viewsName;




        public MainWindows(UIDocument uiDoc)
        {

            UIdoc = uiDoc;
            Doc = uiDoc.Document;
            InitializeComponent();
            Title = "ExportNWC";

        
        }

        //Get a list of selected views from user and export all 
        //Use selected 3d view name as nwc file name
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
            
            

            nwcOptions.Parameters = NavisworksParameters.All;
            //Doc.Export(FILE_PATH, @"test.nwc", nwcOptions);
            // Doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions);
            
            //
            foreach(var s in ViewList.SelectedItems)
            {
                foreach (Element view in VIEW)
                {
                    if(s.ToString() == view.Name.ToString())
                    {
                        nwcOptions.ViewId = view.Id;//view ID to be exported
                        Doc.Export(FILE_PATH, view.Name, nwcOptions);

                    }


                }
            }


        }

        /*private void GetSelectedView() 
        {
            //Compare selected list view with Element list
            //if name of elements is same as selected view list add to a new collection called USER_SELECTED_3DVIEW
            foreach(Element view in VIEW)
            {
                if (VIEW.Contains(ViewList.SelectedItems[]){
                    USER_SELECTED_3DVIEW.Add(view);
                }
            }
            
            Console.WriteLine("This are count of selected views" ,ViewList.SelectedItems.Count);
            foreach(Element view in ViewList.SelectedItems)
            {
                
            }
        
        
        }*/



        //Function to grab the directory to save the file.
        private void SelectDirectory(object sender, RoutedEventArgs e)
        {


            // Create FolderBrowserDialog
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Show FolderBrowserDialog
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Get selected directory path
                FILE_PATH = folderBrowserDialog.SelectedPath;
                File_Path.Text = FILE_PATH;

                // Do something with the directory path
            }

        }

        //create a collection that gets all 3d views

        private void LoadViews(object sender, RoutedEventArgs e)
        {

             All_VIEWS = new FilteredElementCollector(Doc).OfClass(typeof(View3D));

            /*The data in a Revit document consists primarily of a collection of elements.An element usually corresponds to a single component of a building or drawing,
            such as a wall, door, or dimension, but it can also be something more abstract,like a wall type or a view. Every element in a document has a unique ID, represented by the ElementId class.
            Elements collection below contain all the 3d views.
             */

            //Create 3d views from the collector by filtering the 3D views
            //Note you can not instantiate View3D
            VIEW = All_VIEWS.ToElements();
            viewsName = new List<Dictionary<ElementId, String>>();
            Dictionary<ElementId, String> dict1 = new Dictionary<ElementId, String>();
            foreach (Element view in VIEW)
            {
                dict1.Add( view.Id, view.Name);
              
            }

            viewsName.Add(dict1);
            // ViewList.ItemsSource = views;//
            //To Do : Create mapping of element id for future filtering and 
            ViewList.ItemsSource = dict1.Values;
        }



        //Get list of 3d view from the current document and list it under UI


    }
}
