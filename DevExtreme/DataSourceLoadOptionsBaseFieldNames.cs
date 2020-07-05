using DevExtreme.AspNet.Data;

namespace CSharp.AspNetCore.Spa.Vuejs.DevExtreme
{
    internal static class DataSourceLoadOptionsBaseFieldNames
    {
        public const string RequireTotalCountKey = nameof(DataSourceLoadOptionsBase.RequireTotalCount);
        public const string RequireGroupCountKey = nameof(DataSourceLoadOptionsBase.RequireGroupCount);
        public const string IsCountQueryKey = nameof(DataSourceLoadOptionsBase.IsCountQuery);
        public const string SkipKey = nameof(DataSourceLoadOptionsBase.Skip);
        public const string TakeKey = nameof(DataSourceLoadOptionsBase.Take);
        public const string SortKey = nameof(DataSourceLoadOptionsBase.Sort);
        public const string GroupKey = nameof(DataSourceLoadOptionsBase.Group);
        public const string FilterKey = nameof(DataSourceLoadOptionsBase.Filter);
        public const string TotalSummaryKey = nameof(DataSourceLoadOptionsBase.TotalSummary);
        public const string GroupSummaryKey = nameof(DataSourceLoadOptionsBase.GroupSummary);
        public const string SelectKey = nameof(DataSourceLoadOptionsBase.Select);
    }
}