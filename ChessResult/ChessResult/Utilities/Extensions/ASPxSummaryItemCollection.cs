using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using DevExpress.Data;

namespace DevExpress.Web.ASPxGridView
{
    public class ASPxSummaryItemCollection<TModel>
    {        
        public ASPxSummaryItemCollection(ASPxSummaryItemCollection columns)
        {
            SummaryCollection = columns;
        }
      
        #region Properties

        public ASPxSummaryItemCollection SummaryCollection { get; private set; }

        #endregion

        #region Methods

        public ASPxSummaryItemCollection<TModel> Add<TValue>(SummaryItemType summaryItemType, Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            SummaryCollection.Add(summaryItemType, metaData.PropertyName);
            return this;
        }

        public ASPxSummaryItemCollection<TModel> Add<TValue>(SummaryItemType summaryItemType, Expression<Func<TModel, TValue>> expression, Action<ASPxSummaryItem> setColumn)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            setColumn(SummaryCollection.Add(summaryItemType, metaData.PropertyName));
            return this;
        }

        #endregion


    }
}