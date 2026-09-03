namespace DevLib.FsmSystem.Editor
{
    public static class CodeFormat
    {
        public static readonly string EnumFormat = 
@"
namespace {0}
{{
    public enum {1}
    {{
        {2}
    }}
}}
";
    }
}