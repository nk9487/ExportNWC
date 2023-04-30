using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportNWC
{
    internal class ExportColor : NavisworksExportOptions
    {
        /*public bool KeepViewColor { get; set; }

        public void NavisworksExportOptionsWithColor()
        {
            KeepViewColor = false;
        }

        public override void OnExportBegin(ExportBeginEventArgs args)
        {
            base.OnExportBegin(args);

            if (KeepViewColor)
            {
                Document doc = args.Document;
                View view = doc.ActiveView;

                if (view != null && view.CanBePrinted)
                {
                    RenderNodeSettings renderNodeSettings = new RenderNodeSettings();
                    renderNodeSettings.SetNodeOverridesForVisibility(RenderNode.OverrideFlags.Materials, true);

                    GraphicsStyle graphicsStyle = view.GetGraphicsStyle(GraphicsStyleType.Projection);
                    AppearanceAssetElement appearanceAssetElement = doc.GetElement(graphicsStyle.GraphicsStyleCategory.AppearanceAssetId) as AppearanceAssetElement;

                    if (appearanceAssetElement != null)
                    {
                        renderNodeSettings.SetAppearanceOverride(0, appearanceAssetElement.GetRenderingAsset(), true);
                    }

                    args.Exporter.IncludeRenderNodes(new RenderNode(view, renderNodeSettings));
                }
            }
        }*/

    }
}
