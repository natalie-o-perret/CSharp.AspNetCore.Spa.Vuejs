namespace CSharp.AspNetCore.Spa.Vuejs.SqliteData
{
    public class SqliteRecord
    {
        public long Id { get; set; }
        public long Integer { get; set; }
        public double Real { get; set; }
        public string? NullableText { get; set; }
        public string? NonNullableText { get; set; }
    }
}
