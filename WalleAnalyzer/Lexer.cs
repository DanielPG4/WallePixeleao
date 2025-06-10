using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WalleAnalyzer
{
    class Lexer
    {
        public Dictionary<string, string> symbol = new Dictionary<string, string>();
        public Dictionary<string, string> Operator = new Dictionary<string, string>();
        public Dictionary<string, string> KeyWord = new Dictionary<string, string>();
        public List<string> InitOperator = new List<string>();
        public List<Token> Tokenizzer(string code)
        {
            List<Token> tokens = new List<Token>();
            string key = "";
            int row = 0;

            for (int i = 0; i < code.Length; i++)
            {
                key = "";

                if (code[i].ToString() == "\n")
                {
                    key = code[i].ToString();

                    tokens.Add(VerifyValidate(key, row, i));
                    row++;
                    continue;
                }
                else if (MatchSymbol(code[i].ToString()))
                {
                    key = code[i].ToString();
                    tokens.Add(VerifyValidate(key, row, i));
                    continue;
                }
                else if (MatchOperator(code[i].ToString()) || MatchInitOperator(code[i].ToString()))
                {
                    key = code[i].ToString();
                    if (InitOperator.Contains(key))
                    {
                        if (i + 1 < code.Length && MatchInitOperator(code[i + 1].ToString()))
                        {
                            key += code[i + 1].ToString();
                            i++;
                        }
                    }
                    tokens.Add(VerifyValidate(key, row, i));
                    continue;
                }
                else
                {
                    bool lector1 = false;

                    for (; i < code.Length && MatchNumber(key + code[i].ToString()); i++)
                    {
                        lector1 = true;
                        key += code[i].ToString();
                    }
                    if (lector1)
                    {
                        lector1 = false;
                        i--;
                    }
                    if (key != "")
                    {
                        tokens.Add(VerifyValidate(key, row, i));
                        continue;
                    }
                }
                bool lector = false;
                for (; i < code.Length && MatchWord(key + code[i].ToString()); i++)
                {
                    lector = true;
                    key += code[i].ToString();
                }
                if (lector)
                {
                    lector = false;
                    i--;
                }
                if (key != "")
                {
                    tokens.Add(VerifyValidate(key, row, i));
                    continue;
                }
            }

            return tokens;
        }

        public Lexer()
        {
            //Llenar
            AddOperator("+", TokenValues.Add);
            AddOperator("-", TokenValues.Sub);
            AddOperator("*", TokenValues.Mul);
            AddOperator("/", TokenValues.Div);
            AddOperator("**", TokenValues.Pot);
            AddOperator("%", TokenValues.Module);
            AddOperator("<-", TokenValues.Assign);

            AddOperator("&&", TokenValues.And);
            AddOperator("||", TokenValues.Or);
            AddOperator("==", TokenValues.Equals);
            AddOperator("!=", TokenValues.Different);
            AddOperator("<", TokenValues.LessThan);
            AddOperator(">", TokenValues.GreaterThan);
            AddOperator("<=", TokenValues.LessEqual);
            AddOperator(">=", TokenValues.GreaterEqual);


            AddSymbol(",", TokenValues.ValueSeparator);
            AddSymbol("\n", TokenValues.EndLine); 
            AddSymbol("(", TokenValues.OpenBracket);
            AddSymbol(")", TokenValues.ClosedBracket);
            AddSymbol("[", TokenValues.OpenSquareBraces);
            AddSymbol("]", TokenValues.ClosedSquareBraces);

            AddInitialOperator("*");
            AddInitialOperator("&");
            AddInitialOperator("|");
            AddInitialOperator("=");
            AddInitialOperator("!");
            AddInitialOperator("<");
            AddInitialOperator(">");
            AddInitialOperator("-");


            AddKeyWord("Spawn", TokenValues.Spawn);
            AddKeyWord("Color", TokenValues.Color);
            AddKeyWord("Size", TokenValues.Size);
            AddKeyWord("DrawLine", TokenValues.DrawLine);
            AddKeyWord("DrawCircle", TokenValues.DrawCircle);
            AddKeyWord("DrawRectangle", TokenValues.DrawRectangle);
            AddKeyWord("Fill", TokenValues.Fill);
            AddKeyWord("GetActualX", TokenValues.GetActualX);
            AddKeyWord("GetActualY", TokenValues.GetActualY);
            AddKeyWord("GetCanvasSize", TokenValues.GetCanvasSize);
            AddKeyWord("GetColorCount", TokenValues.GetColorCount);
            AddKeyWord("IsBrushColor", TokenValues.IsBrushColor);
            AddKeyWord("IsBrushSize", TokenValues.IsBrushSize);
            AddKeyWord("IsCanvasColor", TokenValues.IsCanvasColor);
            AddKeyWord("IsColor", TokenValues.IsColor);
            AddKeyWord("GoTo", TokenValues.GoTo);


        }

        public void AddInitialOperator(string op)
        {
            this.InitOperator.Add(op);
        }
        public void AddSymbol(string symbol, string type)
        {
            this.symbol[symbol] = type;
        }

        public void AddOperator(string oper, string type)
        {
            Operator[oper] = type;
        }

        public void AddKeyWord(string key, string type)
        {
            KeyWord[key] = type;
        }
        public bool MatchKeyWord(string letter)
        {
            return KeyWord.ContainsKey(letter);

        }
        public bool MatchSymbol(string letter)
        {
            return symbol.ContainsKey(letter);
            
        }
        public bool MatchOperator(string letter)
        {
            return Operator.ContainsKey(letter);
        }
        public bool MatchInitOperator(string letter)
        {
            return InitOperator.Contains(letter);
        }
        public bool MatchNumber(string letter)
        {
            return Regex.IsMatch(letter, @"^-?\d+$");
        }
        public bool MatchWord(string letter)
        {
            return (Regex.IsMatch(letter, @"^[a-zA-Z][a-zA-Z0-9_]*$"));
        }

        public Token VerifyValidate(string text, int row,int col)
        {
            if(MatchKeyWord(text))
            {
                Console.WriteLine("{0} es una palabra clave", text);
                return new Token((row,col), TokenType.KeyWord, text);
            }
            else if (MatchSymbol(text))
            {
                Console.WriteLine("{0} es un simbolo", text);
                return new Token((row, col), TokenType.Symbol, text);
            }
            else if (MatchOperator(text))
            {
                Console.WriteLine("{0} es un operador", text);
                return new Token((row, col), TokenType.Operator, text);
            }
            else if (Regex.IsMatch(text, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            {
                Console.WriteLine("{0} es una variable valida", text);
                return new Token((row, col), TokenType.Variable, text);
            }
            else if (Regex.IsMatch(text, @"^-?\d+$"))
            {
                Console.WriteLine("{0} es un numero ", text);
                return new Token((row, col), TokenType.Number, text);
            }
            else
            {
                Console.WriteLine("{0} sintax error", text);
                return new Token((row, col), TokenType.Error, text);

            }
        }

    }

}
