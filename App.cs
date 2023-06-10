using Autodesk.Revit.UI;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace ExportNWC
{

    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //assembly name

            String assName = Assembly.GetExecutingAssembly().Location;

            //String iconPath = System.IO.Path.GetDirectoryName(assName);

            //tab name
            String tabName = "ExportPro";
            application.CreateRibbonTab(tabName);

            //create panel
            RibbonPanel panelAddin = application.CreateRibbonPanel(tabName, "Export");


            // create push button
            PushButtonData nwcExportButton = new PushButtonData("nwcExportButton", "NWC", assName, "ExportNWC.NwcOut");

            BitmapImage nwcButton = new BitmapImage(new Uri("pack://application:,,,/ExportNWC;component/Resource/img/nwc_export_button.png"));

            nwcExportButton.LargeImage = nwcButton;
            panelAddin.AddItem(nwcExportButton);

            //Adding tool tip
            nwcExportButton.ToolTip = "Export nwc";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Failed;
        }
    }

}





