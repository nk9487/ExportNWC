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
using Color = Autodesk.Revit.DB.Color;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        private string FILE_PATH;
        private bool CONVERT_ELEMENT_PROPERTIES = false;
        private bool CONVERT_LINK_CAD_FORMATS = false;
        
        private bool EXPORT_ELEMENT_ID = false;
        private bool DIVIDE_FILE_TO_LEVEL = false;
        private bool EXPORT_LINK = false;
        private bool EXPORT_PART = false;
        private bool EXPORT_ROOM_ATTRIBUTE = false;
        private bool EXPORT_ROOM_GEOMETRY = false;
        
        private bool EXPORT_URL = false;
        private bool FIND_MISSING_MATERIAL = false;
   
        private bool DEFAULT_EXPORT_SETTING = false;

        private NavisworksExportOptions nwcOptions;
        private NavisworksCoordinates COORDINATE = NavisworksCoordinates.Shared;//SHARED OR INTERANL
        private NavisworksExportScope EXPORT_SCOPE = NavisworksExportScope.View ;//MODEL/VIEW;

        //public ICollection<View3D> All_VIEWS;
        private FilteredElementCollector All_VIEWS;
        private ICollection<Element> VIEW;
        private ICollection<Element> USER_SELECTED_3DVIEW;
        private List<Dictionary<ElementId, String>> viewsName;

        //File read and save
        private List<int> PROJECT_NUMBER = new List<int>() ;
        private ProjectSetting INDIVIDUAL_PROJECT_SETTING = new ProjectSetting() ;  
        private List<ProjectSetting>PROJECT_LIST = new List<ProjectSetting>(); //STORES ALL PROJECT WITH THEIR ID PATH COORDINATE




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
            // Create a Material object with the desired color
            

            nwcOptions = new NavisworksExportOptions();
            nwcOptions.ConvertElementProperties = CONVERT_ELEMENT_PROPERTIES;
            nwcOptions.ConvertLinkedCADFormats = CONVERT_LINK_CAD_FORMATS;
            nwcOptions.Coordinates = COORDINATE;//Desfualt is shared
            nwcOptions.DivideFileIntoLevels = DIVIDE_FILE_TO_LEVEL;
            nwcOptions.ExportElementIds = EXPORT_ELEMENT_ID;
            nwcOptions.ExportLinks = EXPORT_LINK;
            nwcOptions.ExportParts = EXPORT_PART;
            nwcOptions.ExportRoomAsAttribute = EXPORT_ROOM_ATTRIBUTE;
            nwcOptions.ExportRoomGeometry = EXPORT_ROOM_GEOMETRY;
            nwcOptions.ExportScope = EXPORT_SCOPE; // Check 
            nwcOptions.ExportUrls = EXPORT_URL;     
            nwcOptions.FindMissingMaterials = FIND_MISSING_MATERIAL;

            //options below are not exposed to the user 
            nwcOptions.FacetingFactor = 1.0;
            nwcOptions.Parameters = NavisworksParameters.All;//ALL ELEEMNTS

            //Doc.Export(FILE_PATH, @"test.nwc", nwcOptions);
            // Doc.Export(@"C:\Users\namun\OneDrive\Desktop\NWC", @"test.nwc", nwcOptions);
            
            //check for selected
            
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



        //Read and write a text file 
        //Create n x 3 matrix that stores project number, path and 

        private void Select_Project_Click(object sender, RoutedEventArgs e)
        {
            //List<List<string>> ProjectList = new List<List<string>>();

            // Open and read the JSON file

            string path = @"C:\Users\namun\OneDrive\Desktop\NWC\ExportSetting.json";

            string jsonContent = File.ReadAllText(path);



            PROJECT_LIST = JsonConvert.DeserializeObject<List<ProjectSetting>>(jsonContent);



            foreach (ProjectSetting item in PROJECT_LIST)
            {
                int id = item.Id;
                string paths = item.Path;
                string coordinate = item.Coordinate;

                PROJECT_NUMBER.Add(id);


            }



            ProjectList.ItemsSource = PROJECT_NUMBER;

            foreach (ProjectSetting s in PROJECT_LIST)
            {
                if (s.Id == ProjectList.SelectedIndex)
                {
                    INDIVIDUAL_PROJECT_SETTING.Id = s.Id;
                    INDIVIDUAL_PROJECT_SETTING.Path = s.Path;
                    INDIVIDUAL_PROJECT_SETTING.Coordinate = s.Coordinate;
                    FILE_PATH = "@C:" + INDIVIDUAL_PROJECT_SETTING.Path;
                    Console.WriteLine(FILE_PATH);
                    if (INDIVIDUAL_PROJECT_SETTING.Coordinate == "Internal")
                    {
                        COORDINATE = NavisworksCoordinates.Internal;
                    }
                    if (INDIVIDUAL_PROJECT_SETTING.Coordinate == "Shared")
                    {
                        COORDINATE = NavisworksCoordinates.Shared;
                    }
                }

            }

            //




            //change path and coordinate setting for selected item


        }

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



        private void Convert_Element_Properties_Checked(object sender, RoutedEventArgs e)
        {
            CONVERT_ELEMENT_PROPERTIES = true;
        }

        private void Convert_Linked_CAD_Formats_Checked(object sender, RoutedEventArgs e)
        {
            CONVERT_LINK_CAD_FORMATS = true;
        }

        private void Export_Coordinate_Checked(object sender, RoutedEventArgs e)
        {
            COORDINATE = NavisworksCoordinates.Internal;
        }

        private void Export_ElementID_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_ELEMENT_ID = true;
        }

        private void Divide_To_Level_Checked(object sender, RoutedEventArgs e)
        {
            DIVIDE_FILE_TO_LEVEL = true;
        }

        private void Export_Link_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_LINK = true;
        }

        private void Export_Parts_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_PART = true;
        }

        private void Export_Room_Attribute_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_ROOM_ATTRIBUTE = true;
        }

        private void Export_Room_Geometery_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_ROOM_GEOMETRY = true;
        }

        private void Export_Scope_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_SCOPE = NavisworksExportScope.Model;
        }

        private void Export_URl_Checked(object sender, RoutedEventArgs e)
        {
            EXPORT_URL = true;
        }

        private void Find_Missing_Material_Checked(object sender, RoutedEventArgs e)
        {
            FIND_MISSING_MATERIAL = true;
        }

        private void Convert_Linked_CAD_Formats_Unchecked(object sender, RoutedEventArgs e)
        {
            CONVERT_LINK_CAD_FORMATS = true;

        }

        private void Default_Export_Setting_Checked(object sender, RoutedEventArgs e)
        {
            if(Default_Export_Setting.IsChecked == true)
            {
                Convert_Element_Properties.IsEnabled = false;
                Convert_Linked_CAD_Formats.IsEnabled = false;
                Export_Coordinate.IsEnabled = false;
                Export_ElementID.IsEnabled = false;
                Divide_To_Level.IsEnabled = false;
                Export_Link.IsEnabled = false;
                Export_Parts.IsEnabled = false;
                Export_Room_Attribute.IsEnabled = false;
                Export_Room_Geometery.IsEnabled = false;
                Export_Scope.IsEnabled = false;
                Export_URl.IsEnabled = false;
                Find_Missing_Material.IsEnabled = false;
                Default_Setting();

            }

        }

        private void Default_Setting()
        {
            CONVERT_ELEMENT_PROPERTIES = true;
            CONVERT_ELEMENT_PROPERTIES = true;
            CONVERT_LINK_CAD_FORMATS = true;
            EXPORT_ELEMENT_ID =true;
            DIVIDE_FILE_TO_LEVEL = true;
            EXPORT_LINK = true;
            EXPORT_PART = true;
            EXPORT_ROOM_ATTRIBUTE = true;
            EXPORT_ROOM_GEOMETRY = true;
            EXPORT_URL = true;
            FIND_MISSING_MATERIAL = true;
            DEFAULT_EXPORT_SETTING = true;
            COORDINATE = NavisworksCoordinates.Internal;//SHARED OR INTERANL
            EXPORT_SCOPE = NavisworksExportScope.Model;//MODEL/VIEW;
;
        }

        private void Default_Export_Setting_Unchecked(object sender, RoutedEventArgs e)
        {
        if (Default_Export_Setting.IsChecked == false)
            Convert_Element_Properties.IsEnabled = true;
            Convert_Linked_CAD_Formats.IsEnabled = true;
            Export_Coordinate.IsEnabled = true;
            Export_ElementID.IsEnabled = true;
            Divide_To_Level.IsEnabled = true;
            Export_Link.IsEnabled = true;
            Export_Parts.IsEnabled = true;
            Export_Room_Attribute.IsEnabled = true;
            Export_Room_Geometery.IsEnabled = true;
            Export_Scope.IsEnabled = true;
            Export_URl.IsEnabled = true;
            Find_Missing_Material.IsEnabled = true;
        }


    }




    // Define a class to represent the data in your JSON file
    public class ProjectSetting
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Coordinate { get; set; }

    }
}
