namespace Members.LYG._PlayerScripts.TokenSystem
{
        public class Token
        {
                private TokenType Type { get; }
                private string Value { get; } 

                public Token(TokenType type, string value = "")
                {
                        Type = type;
                        Value = value;
                }

                public override string ToString()=> $"Token.{Type}, Value: ({Value})";
        }
}