using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.AspNetCore.Spa.Vuejs.DevExtreme
{
    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase
    {
        static DataSourceLoadOptions() => StringToLowerDefault = true;
    }
}
