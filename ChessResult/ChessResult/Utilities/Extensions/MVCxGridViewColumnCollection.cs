using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using DevExpress.Utils;

namespace DevExpress.Web.Mvc
{
    public class MVCxGridViewColumnCollection<TModel>
    {
        #region Ctors

        public MVCxGridViewColumnCollection(MVCxGridViewColumnCollection columns)
        {
            ColumnCollection = columns;
        }

        #endregion

        #region Properties

        public MVCxGridViewColumnCollection ColumnCollection { get; private set; }

        #endregion

        #region Methods

        public MVCxGridViewColumnCollection<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression, Action<MVCxGridViewColumn> setColumn)
        {
            setColumn(GetNewColumn(expression));
            return this;
        }

        public DevExpress.Web.Mvc.MVCxGridViewColumnCollection<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            GetNewColumn(expression);

            return this;
        }

        public MVCxGridViewColumnCollection<TModel> Add(Action<MVCxGridViewColumn> setColumn)
        {
            var gridViewColumn = ColumnCollection.Add();
            setColumn(gridViewColumn);
            return this;
        }

        public DevExpress.Web.Mvc.MVCxGridViewColumnCollection<TModel> AddBand(string bandName, Action<MVCxGridViewBandColumn<TModel>> setBand)
        {
            var band = ColumnCollection.AddBand(bandName);
            band.HeaderStyle.Wrap = DefaultBoolean.True;
            setBand(new MVCxGridViewBandColumn<TModel>(band));
            return this;
        }

        public MVCxGridViewColumn GetNewColumn<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            var gridViewColumn = ColumnCollection.Add();
            gridViewColumn.Caption = metaData.DisplayName;
            gridViewColumn.FieldName = metaData.PropertyName;
            gridViewColumn.HeaderStyle.Wrap = DefaultBoolean.True;
            return gridViewColumn;
        }

        #endregion
    }
}