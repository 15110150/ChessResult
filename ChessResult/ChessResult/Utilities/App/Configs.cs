using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ECOM.Web.Utilities.App
{
    public static class Configs
    {
        public static DateTime DefaultStartDate = DateTime.Now.AddDays(-1);

        #region Dev Control
        public static Action<DevExpress.Web.Mvc.TextBoxSettings> TextBoxSettingsMethod
        {
            get
            {
                return settings =>
                {                                       
                    settings.ShowModelErrors = true;
                    settings.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                    settings.Width = Unit.Pixel(ECOM.Web.Utilities.App.CssUtils.TextBoxWidth);
                    settings.Height = Unit.Pixel(ECOM.Web.Utilities.App.CssUtils.TextBoxHeight);
                };
            }
        }
        public static Action<DevExpress.Web.Mvc.TextBoxSettings> PasswordSettingsMethod
        {
            get
            {
                return settings =>
                {
                    settings.Properties.Password = true;
                    settings.ShowModelErrors = true;
                    settings.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                    settings.Width = Unit.Pixel(ECOM.Web.Utilities.App.CssUtils.TextBoxWidth);
                    settings.Height = Unit.Pixel(ECOM.Web.Utilities.App.CssUtils.TextBoxHeight);
                };
            }
        }


        public static Action<DevExpress.Web.Mvc.TextBoxSettings> Daikin_TextBoxSettingsMethod
        {
            get
            {
                return settings =>
                {
                    settings.ShowModelErrors = true;
                    settings.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                    settings.Width = Unit.Percentage(100);
                    settings.Properties.ValidationSettings.RequiredField.IsRequired = true;
                };
            }
        }
        public static Action<DevExpress.Web.Mvc.TextBoxSettings> Daikin_PasswordSettingsMethod
        {
            get
            {
                return settings =>
                {
                    settings.Properties.Password = true;
                    settings.ShowModelErrors = true;
                    settings.Properties.ValidationSettings.ErrorDisplayMode = DevExpress.Web.ErrorDisplayMode.ImageWithTooltip;
                    settings.Width = Unit.Percentage(100);
                    settings.Properties.ValidationSettings.RequiredField.IsRequired = true;
                };
            }
        }

        #endregion


    }
}