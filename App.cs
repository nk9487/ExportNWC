using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ExportNWC
{

    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //assembly name

            String assName = Assembly.GetExecutingAssembly().Location;

            string iconPath = System.IO.Path.GetDirectoryName(assName);

            //tab name
            String tabName = "ExportNWC";
            application.CreateRibbonTab(tabName);

            //create panel
            RibbonPanel panelAddin = application.CreateRibbonPanel(tabName, "NWC");


            // create push button
            PushButtonData button1 = new PushButtonData("Btn1", "Export", assName, "ExportNWC.NwcOut");

            button1.LargeImage = new BitmapImage(new Uri(iconPath + @"\img\free.png"));




            panelAddin.AddItem(button1);


            //Adding tool tip
            button1.ToolTip = "Export nwc";



            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Failed;
        }
    }

}





