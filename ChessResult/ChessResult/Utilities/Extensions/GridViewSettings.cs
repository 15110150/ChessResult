using DevExpress.Web.ASPxGridView;

namespace DevExpress.Web.Mvc
{
    public class GridViewSettings<TModel> : GridViewSettings
    {
        #region Methods

        public MVCxGridViewColumnCollection<TModel> GetColumns()
        {
            return new MVCxGridViewColumnCollection<TModel>(Columns);
        }

        public ASPxSummaryItemCollection<TModel> GetSummaryItems()
        {
            return new ASPxSummaryItemCollection<TModel>(TotalSummary);
        }

        #endregion
    }
}