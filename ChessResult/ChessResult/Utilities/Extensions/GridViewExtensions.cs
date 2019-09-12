using System;
using System.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Utils;

namespace DevExpress.Web.Mvc
{
    public static class GridViewExtensions
    {


        /// <summary>
        /// Generate  extension GridView from specify class config 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="extensionsFactory"></param>
        /// <param name="gridConfig"></param>
        /// <param name="viewContext"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static GridViewExtension GridViewCustom<TModel>(this ExtensionsFactory extensionsFactory, ECOM.Models.GridViewConfig gridConfig, ViewContext viewContext, Action<GridViewSettings<TModel>> action)
        {
            var gridViewSettings = GetGridViewSetting<TModel>(gridConfig);
            action(gridViewSettings);

            return new GridViewExtension(gridViewSettings, viewContext);
        }

        public static GridViewExtension GridViewCustom<TModel>(this ExtensionsFactory extensionsFactory, ECOM.Models.GridViewConfig gridConfig, Action<GridViewSettings<TModel>> action, bool IsGenerateColumn = true)
        {
            var gridViewSettings = GetGridViewSetting<TModel>(gridConfig, IsGenerateColumn);
            action(gridViewSettings);

            return new GridViewExtension(gridViewSettings);
        }

        public static GridViewExtension GridViewReport<TModel>(this ExtensionsFactory extensionsFactory, ECOM.Models.GridViewConfig gridConfig, Action<GridViewSettings<TModel>> action, bool IsExportData = true)
        {
            var gridViewSettings = GetGridViewReportSetting<TModel>(gridConfig, IsExportData);

            action(gridViewSettings);

            return new GridViewExtension(gridViewSettings);
        }

        public static GridViewSettings<TModel> GetGridViewReportSetting<TModel>(ECOM.Models.GridViewConfig gridConfig, bool IsExportData = true)
        {
            var gridViewSettings = new GridViewSettings<TModel>();
            gridViewSettings.Name = gridConfig.GridViewName;
            gridViewSettings.KeyFieldName = gridConfig.KeyFieldName;
            gridViewSettings.SettingsPager.PageSize = gridConfig.PageSize;
            gridViewSettings.CallbackRouteValues = gridConfig.CallbackRouteValues;
            gridViewSettings.CustomActionRouteValues = gridConfig.CustomActionRouteValues;

            gridViewSettings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.TopAndBottom;
            gridViewSettings.SettingsPager.FirstPageButton.Visible = true;
            gridViewSettings.SettingsPager.LastPageButton.Visible = true;
            gridViewSettings.SettingsPager.PageSizeItemSettings.Visible = true;          
            gridViewSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            gridViewSettings.Height = System.Web.UI.WebControls.Unit.Percentage(100);           
            gridViewSettings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;           
            gridViewSettings.CommandColumn.ShowClearFilterButton = true;
            gridViewSettings.Settings.ShowFilterRow = true;
            gridViewSettings.Settings.ShowFilterRowMenu = true;

            gridViewSettings.SettingsPager.PageSizeItemSettings.Items = new string[] { "10", "25", "50", "100" };

            gridViewSettings.SettingsBehavior.AllowFocusedRow = true; //Selected Row

            gridViewSettings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
          
            gridViewSettings.ControlStyle.CssClass = "grid-view-custom";

            //Menu Context Setting
            gridViewSettings.SettingsContextMenu.Enabled = true;
            gridViewSettings.SettingsBehavior.EnableCustomizationWindow = true;
            gridViewSettings.SettingsContextMenu.EnableRowMenu = DefaultBoolean.False;
            gridViewSettings.SettingsContextMenu.EnableFooterMenu = DefaultBoolean.False;
            gridViewSettings.SettingsContextMenu.EnableGroupPanelMenu = DefaultBoolean.False;
            gridViewSettings.SettingsPopup.CustomizationWindow.HorizontalAlign = PopupHorizontalAlign.LeftSides;
            gridViewSettings.SettingsPopup.CustomizationWindow.VerticalAlign = PopupVerticalAlign.Below;

            gridViewSettings.ContextMenuItemVisibility = (sender, e) =>
            {

                //    Custom menu context Item
                if (e.MenuType == GridViewContextMenuType.Columns)
                {
                    foreach (var item in e.Items)
                    {
                        GridViewContextMenuItem menuItemDelete = item as GridViewContextMenuItem;
                        if (menuItemDelete.Name == "GroupByColumn"
                            || menuItemDelete.Name == "ShowGroupPanel"
                            || menuItemDelete.Name == "ShowSearchPanel"
                            || menuItemDelete.Name == "ShowFilterRow"
                            || menuItemDelete.Name == "ShowFilterRowMenu"
                            || menuItemDelete.Name == "ShowFooter"
                            )
                        {
                            e.SetVisible(menuItemDelete, false);
                        }

                    }
                }
            };
            gridViewSettings.SettingsBehavior.EnableCustomizationWindow = true;
            gridViewSettings.CustomJSProperties = (sender, e) =>
            {
                string separator = ",";
                string fieldNames = string.Empty;
                MVCxGridView grid = (MVCxGridView)sender;

                foreach (DevExpress.Web.GridViewColumn column in grid.VisibleColumns)
                {
                    if (column is GridViewDataColumn)
                    {
                        GridViewDataColumn dataColumn = (GridViewDataColumn)column;
                        if (!string.IsNullOrEmpty(dataColumn.FieldName) && dataColumn.Visible)
                        {
                            fieldNames += dataColumn.FieldName + ",";
                        }
                    }
                }
                fieldNames = fieldNames.TrimEnd(separator.ToCharArray());
                e.Properties["cpFieldNames"] = fieldNames;
            };             

            //End menu context setting

            if (IsExportData == false)
            {
                gridViewSettings.Columns.Add(c =>
                {
                    c.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    c.Caption = ECOM.Resources.ECOMResource.Number;
                    c.Settings.AllowAutoFilter = DefaultBoolean.False;
                    c.Settings.AllowDragDrop = DefaultBoolean.False;
                    c.Settings.AllowSort = DefaultBoolean.False;
                    c.Width = 30;
                });

                gridViewSettings.CustomColumnDisplayText = (s, e) =>
                {
                    if (e.Column.Caption == ECOM.Resources.ECOMResource.Number)
                    {
                        e.DisplayText = (e.VisibleRowIndex + 1).ToString();
                    }
                };
            }
            foreach (var item in gridConfig.Columns.Where(x=>x.Enable == true))
            {
                MVCxGridViewColumn column = gridViewSettings.Columns.Add();
                column.FieldName = item.ColumnName;
                column.Caption = item.DisplayText;
                //column.Visible = item.Enable;
                column.Visible = true;
                if (item.ColumnType == "System.DateTime" || item.ColumnType == "System.Date" || item.ColumnType.Contains("System.DateTime"))
                {
                    if (!string.IsNullOrEmpty(item.ColumnFormat))
                    {
                        column.PropertiesEdit.DisplayFormatString = item.ColumnFormat;
                    }
                    else
                    { 
                        //Default is DateTime format
                        column.PropertiesEdit.DisplayFormatString = ECOM.Resources.ECOMResource.ResourceManager.GetString("DefaultDateFormat");
                    }
                    
                }
                if (item.ColumnType == "System.Decimal")
                {
                    column.PropertiesEdit.DisplayFormatString = "N2";
                   
                }
            }           
            return gridViewSettings;
        }



        public static GridViewSettings<TModel> GetGridViewSetting<TModel>(ECOM.Models.GridViewConfig gridConfig, bool IsGenerateColumn = true)
        {
            var gridViewSettings = new GridViewSettings<TModel>();
            gridViewSettings.Name = gridConfig.GridViewName;
            gridViewSettings.KeyFieldName = gridConfig.KeyFieldName;
            gridViewSettings.SettingsPager.PageSize = gridConfig.PageSize;
            gridViewSettings.CallbackRouteValues = gridConfig.CallbackRouteValues;
            gridViewSettings.CustomActionRouteValues = gridConfig.CustomActionRouteValues;

            gridViewSettings.SettingsPager.Position = System.Web.UI.WebControls.PagerPosition.TopAndBottom;
            gridViewSettings.SettingsPager.FirstPageButton.Visible = true;
            gridViewSettings.SettingsPager.LastPageButton.Visible = true;
            gridViewSettings.SettingsPager.PageSizeItemSettings.Visible = true;
            //gridViewSettings.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
            gridViewSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            gridViewSettings.Height = System.Web.UI.WebControls.Unit.Percentage(100);

            gridViewSettings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;

            gridViewSettings.CommandColumn.Visible = true;
            gridViewSettings.CommandColumn.ShowClearFilterButton = true;
            gridViewSettings.Settings.ShowFilterRow = true;
            gridViewSettings.Settings.ShowFilterRowMenu = true;

            gridViewSettings.SettingsPager.PageSizeItemSettings.Items = new string[] { "10", "25", "50", "100" };

            gridViewSettings.SettingsBehavior.AllowFocusedRow = true; //Selected Row

            gridViewSettings.Settings.HorizontalScrollBarMode = ScrollBarMode.Auto;
            //gridViewSettings.Settings.VerticalScrollBarMode = ScrollBarMode.Auto;

            gridViewSettings.ControlStyle.CssClass = "grid-view-custom";

            if (IsGenerateColumn)
            {
                foreach (var item in gridConfig.Columns)
                {
                    MVCxGridViewColumn cloumn = gridViewSettings.Columns.Add();
                    cloumn.FieldName = item.ColumnName;
                    cloumn.Caption = item.DisplayText;
                    cloumn.Visible = item.Enable;
                }
            }

            return gridViewSettings;
        }

        public static GridViewModel GetGridViewModel(ECOM.Models.GridViewConfig gridConfig)
        {
            GridViewModel gvModel = new GridViewModel();

            gvModel.KeyFieldName = gridConfig.KeyFieldName;
            gvModel.Pager.PageSize = gridConfig.PageSize;

            foreach (var item in gridConfig.Columns)
            {
                gvModel.Columns.Add(item.ColumnName);
            }

            return gvModel;

        }
    }
}