using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharp.AspNetCore.Spa.Vuejs.StartupExtensions
{
    public static class JsonSerializerOptionsExtensions
    {
        public static void SetupFSharp(this JsonSerializerOptions options)
        {
            /* JsonUnionEncoding.ExternalTag |
               JsonUnionEncoding.UnwrapFieldlessTags |
               JsonUnionEncoding.UnwrapOption; */
            const JsonUnionEncoding flags = (JsonUnionEncoding) 1538;
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonFSharpConverter(flags));
        }
    }
}
