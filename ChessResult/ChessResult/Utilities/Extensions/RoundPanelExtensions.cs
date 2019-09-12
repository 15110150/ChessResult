using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Web.Mvc
{
    public static class RoundPanelExtensions
    {
        public static RoundPanelExtension RoundPanelCustom(this ExtensionsFactory extensionsFactory, string name, string headerText, Action<RoundPanelSettings> action)
        {
            RoundPanelSettings settings = new RoundPanelSettings();
            settings.View = DevExpress.Web.View.GroupBox;
            settings.Name = name;
            settings.HeaderText = headerText;
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            settings.ShowHeader = true;

            settings.CornerRadius = System.Web.UI.WebControls.Unit.Pixel(0);

            //Collapsing Options
            //settings.View = View.Standard;
            settings.AllowCollapsingByHeaderClick = true;
            settings.ShowCollapseButton = true;
           
            
            settings.EnableAnimation = true;


            action(settings);
            return new RoundPanelExtension(settings);
        }
    }
}