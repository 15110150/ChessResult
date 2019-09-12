namespace DevExpress.Web.Mvc
{
    public class MVCxGridViewBandColumn<TModel> : MVCxGridViewBandColumn
    {
        #region Ctors

        public MVCxGridViewBandColumn(MVCxGridViewBandColumn band)
        {
            Band = band;
        }

        #endregion

        #region Properties

        public MVCxGridViewBandColumn Band { get; set; }

        #endregion

        #region Methods

        public MVCxGridViewColumnCollection<TModel> GetColumns()
        {
            return new MVCxGridViewColumnCollection<TModel>(Band.Columns);
        }

        #endregion
    }
}