using System;
using System.Collections.Generic;
using Members.LYG._PlayerScripts.TokenSystem;

namespace Members.LYG._PlayerScripts
{
        public class Lexer
        {
                public Lexer(string text)
                {
                        _text = text;
                }
                
                private string _text;
                private int _position = 0;

                private char Current => _position < _text.Length ? _text[_position] : '\0';

                private static readonly Dictionary<string, TokenType> Keywords = new()
                {
                        { "if", TokenType.If },
                        { "else", TokenType.Else },
                        { "int", TokenType.Integer },
                        { "double", TokenType.Double },
                        { "else if", TokenType.ElseIf },
                };

                public List<Token> Tokenize()
                {
                        var tokens = new List<Token>();

                        while (_position < _text.Length)
                        {
                                if (char.IsWhiteSpace(Current))
                                {
                                        _position++;
                                        continue;
                                }

                                if (char.IsDigit(Current))
                                {
                                        int start = _position;
                                        bool hasDot = false;

                                        while (_position < _text.Length && (char.IsDigit(Current) || Current == '.'))
                                        {
                                                if (Current == '.')
                                                {
                                                        if (hasDot)
                                                                throw new Exception(
                                                                        "Unexpected number format: '.' is written two times");
                                                        hasDot = true;
                                                }

                                                _position++;
                                        }

                                        string numberStr = _text.Substring(start, _position - start);
                                        if (hasDot)
                                                tokens.Add(new Token(TokenType.Double, numberStr));
                                        else
                                                tokens.Add(new Token(TokenType.Integer, numberStr));

                                        continue;
                                }

                                if (char.IsLetter(Current) || Current == '_')
                                {
                                        int start = _position;
                                        while (_position < _text.Length &&
                                               (char.IsLetterOrDigit(Current) || Current == '_'))
                                        {
                                                _position++;
                                        }

                                        string word = _text.Substring(start, _position - start);

                                        if (Keywords.TryGetValue(word, out TokenType keywordType))
                                        {
                                                tokens.Add(new Token(keywordType, word));
                                        }
                                        else
                                        {
                                                tokens.Add(new Token(TokenType.Identifier, word));
                                        }

                                        continue;
                                }

                                switch (Current)
                                {
                                        case '+': tokens.Add(new Token(TokenType.Plus, "+")); break;
                                        case '-': tokens.Add(new Token(TokenType.Minus, "-")); break;
                                        case '*': tokens.Add(new Token(TokenType.Multiply, "*")); break;
                                        case '/': tokens.Add(new Token(TokenType.Divide, "/")); break;
                                        case '=': tokens.Add(new Token(TokenType.Equal, "=")); break;
                                        case '<': tokens.Add(new Token(TokenType.LessThan, "<")); break;
                                        case '>': tokens.Add(new Token(TokenType.GreaterThan, ">")); break;
                                        case '(': tokens.Add(new Token(TokenType.LeftParenthesis, "(")); break;
                                        case ')': tokens.Add(new Token(TokenType.RightParenthesis, ")")); break;
                                        case ';': tokens.Add(new Token(TokenType.EndOfLine, ";")); break;
                                        default: throw new Exception("Unexpected token: " + Current);
                                }

                                _position++;
                        }

                        tokens.Add(new Token(TokenType.EndOfFile));
                        return tokens;
                }
        }
}