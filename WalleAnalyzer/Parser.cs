using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WalleClass;


namespace WalleAnalyzer
{ 
    class Parser
    {
        List<string> function;
        List<string> instruction;
        Dictionary<string, List<Type>> parametersType;
        Dictionary<string, string> admissibleParameters;
        public List<Error> errors;
        List<Token> tokenList;
        List<Node> program;

        int tokenAct = 0;

        public Parser(List<Token> tokenList)
        {
            this.tokenList = tokenList;
            errors = new List<Error>();
            admissibleParameters = new Dictionary<string, string>();
            parametersType = new Dictionary<string, List<Type>>();
            function = new List<string>();
            instruction = new List<string>();
            program = new List<Node>();
        }
        /*public ParserIntruction()
        {
            if (tokenList[tokenAct].Type == TokenType.In)
        }*/
        Expression ParseExpression(int parentPrecedence = 0)
        {
            if (!IsInRange()) return null;

            Token current = tokenList[tokenAct];
            Expression left;

            // Parse operand inicial (número o variable)
            if (current.Type == TokenType.Number)
            {
                left = new Literal(current.location, int.Parse(current.Value));
                ConsumeToken();
            }
            else if (current.Type == TokenType.Variable)
            {
                left = new Variable(current.location, current.Value);
                ConsumeToken();
            }
            else
            {
                AddError($"Se esperaba un número o una variable, se encontró: {current.Value}");
                return null;
            }

            // Analizar operadores con precedencia
            while (IsInRange() && GetTokenType() == TokenType.Operator)
            {
                string op = GetTokenValue();
                int precedence = GetPrecedence(op);

                if (precedence < parentPrecedence) break;

                ConsumeToken(); // Consumir el operador

                Expression right = ParseExpression(precedence + 1); // Recursivo con mayor precedencia

                if (right == null)
                {
                    AddError("Expresión incompleta después del operador " + op);
                    return null;
                }

                left = new BinaryOp(current.location, left, op, right);
            }

            return left;
        }

        int GetPrecedence(string op)
        {
            return op switch
            {
                "**" => 4,
                "*" or "/" or "%" => 3,
                "+" or "-" => 2,
                "==" or "!=" or "<" or ">" or "<=" or ">=" => 1,
                "&&" or "||" => 0,
                _ => -1 // operadores desconocidos
            };
        }

        public bool ConsumeToken()
        {
            if (tokenAct < tokenList.Count)
            {
                tokenAct++;
                return true;
            }
            return false;
        }


        public void Parseador()
        {
            
            tokenAct = 0;
            
            if (!IsInRange()) AddError("No hay codigo");

            Register();


            ParseIntruction("Spawn");

            while (tokenAct < tokenList.Count)
            {
                if (tokenList[tokenAct].Type == TokenType.KeyWord)
                {
                    string commandName = tokenList[tokenAct].Value;
                    List<Expression> parameters = ParseParameters(commandName);

                    if (admissibleParameters.ContainsKey(commandName))
                    {
                        if (!ValidateParameters(parameters, commandName))
                        {
                            
                        }
                        
                    }
                    else
                    {
                        AddError($"Comando desconocido: {commandName}");
                    }
                }
                else if (tokenList[tokenAct].Type == TokenType.EndLine)
                {
                    // Saltar líneas en blanco
                    ConsumeToken();
                }
                else
                {
                    AddError($"Token inesperado: {tokenList[tokenAct].Value}");
                    ConsumeToken();
                }
            }
        }

        

        List<Expression> ParseParameters(string name)
        {
            List<Expression> parameters = new List<Expression>();

            if (!ConsumeToken() || tokenList[tokenAct].Value != "(")
            {
                AddError($"Se esperaba '(' después de {name}");
                return null;
            }

            for (;IsInRange() && GetTokenValue() != ")" ; ConsumeToken())
            {
                Expression expression = ParseExpression();
                if (expression == null)
                {
                    return null;
                }

                if (!ConsumeToken()) return null;   

                if (GetTokenValue() != "," && GetTokenValue() != ")")
                {
                    AddError($"Se esperaba ',' o ')', se encontró {tokenList[tokenAct].Value}");
                    return null;
                }
                
            }
            if (!ValidateParameters(parameters, name)) return null;

            return parameters;
        }
        bool ValidateParameters(List<Expression> parameters, string name)
        {
            if (!admissibleParameters.ContainsKey(name))
            {
                AddError($"Comando desconocido: {name}");
                return false;
            }

            string expectedParams = admissibleParameters[name];

            // Comandos sin parámetros
            if (string.IsNullOrEmpty(expectedParams))
            {
                if (parameters.Count > 0)
                {
                    AddError($"{name} no acepta parámetros");
                    return false;
                }
                return true;
            }

            // Verificar cantidad de parámetros
            List<Type> correspondingTypes = parametersType[expectedParams];
            if (correspondingTypes.Count != parameters.Count)
            {
                AddError($"Número incorrecto de parámetros para {name}. Esperados: {correspondingTypes.Count}, Obtenidos: {parameters.Count}");
                return false;
            }

            // Verificar tipos de parámetros
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].ExprType != correspondingTypes[i])
                {
                    AddError($"Tipo incorrecto para parámetro {i + 1} en {name}. Esperado: {correspondingTypes[i].Name}, Obtenido: {parameters[i].ExprType.Name}");
                    return false;
                }
            }

            return true;
        }

        bool ParseIntruction(string expectedInstruction = "")
        {
            if(tokenAct != 0 && tokenList[tokenAct].Value == "Spawn")
            {
                AddError("Spawn solo se puede utilizar como primera instrucción");
            }
            List<Expression> parameters = new List<Expression>();
            Token inst = tokenList[tokenAct];
            if (instruction.Contains(GetTokenValue()))
            {
                ConsumeToken();
                parameters = ParseParameters(inst.Value);

                if (parameters == null) return false;

                
                if( !(expectedInstruction != "" && inst.Value==expectedInstruction))
                {
                    AddError($"Se esperaba {expectedInstruction}");
                    return false;
                }

                program.Add(Intruction.GetIntruction(inst.location, inst.Value, parameters));

                return true;
            }
            AddError("Se esperaba una instrucción");
            return false;
        }

        bool ParseAsign()
        {
            Token tok;
            Expression exp;
            if(GetTokenType() == TokenType.Variable)
            {
                tok = tokenList[tokenAct];
                if(ConsumeToken() && GetTokenValue() == "<-")
                {
                    exp = ParseExpression();
                    
                    
                    if(exp != null)
                    {
                        program.Add(new Asign(tok.location, tok.Value, exp));
                        return true;
                    }
                }
                
            }
            return false;
        }

        bool IsInRange(int i)
        {
            return i< tokenList.Count;
        }
        bool IsInRange()
        {
            return tokenAct < tokenList.Count;
        }
        string GetTokenValue()
        {

            return tokenList[tokenAct].Value;
        }
        TokenType GetTokenType()
        {

            return tokenList[tokenAct].Type;
        }
        void AddError(string text)
        {
            errors.Add(new Error(tokenList[tokenAct].location, text));
        }

        void Register()
        {
            Type tInt = typeof(int);
            Type tString = typeof(string);
            admissibleParameters["Spawn"] = "int,int";
            admissibleParameters["Color"] = "string";
            admissibleParameters["Size"] = "int";
            admissibleParameters["DrawLine"] = "int,int,int";
            admissibleParameters["DrawCircle"] = "int,int,int";
            admissibleParameters["DrawRectangle"] = "int,int,int,int,int";
            admissibleParameters["Fill"] = "";
            admissibleParameters["GetActualX"] = "";
            admissibleParameters["GetActualY"] = "";
            admissibleParameters["GetCavasSize"] = "";
            admissibleParameters["GetColorCount"] = "string,int,int,int,int";
            admissibleParameters["IsBrushColor"] = "string";
            admissibleParameters["IsBrushSize"] = "int";
            admissibleParameters["IsCanvasColor"] = "string,int,int";

            instruction.Add("Spawn");
            instruction.Add("Color");
            instruction.Add("Size");
            instruction.Add("DrawLine");
            instruction.Add("DrawCircle");
            instruction.Add("DrawRectangle");
            instruction.Add("Fill");

            function.Add("GetActualX");
            function.Add("GetActualY");
            function.Add("GetCavasSize");
            function.Add("GetColorCount");
            function.Add("IsBrushColor");
            function.Add("IsBrushSize");
            function.Add("IsCanvasColor");


            parametersType["int,int"] = new List<Type> { tInt, tInt };
            parametersType["string"] = new List<Type> { tString };
            parametersType["int"] = new List<Type> { tInt };
            parametersType["int,int,int"] = new List<Type> { tInt, tInt, tInt };
            parametersType["int,int,int,int,int"] = new List<Type> { tInt, tInt, tInt, tInt, tInt };
            parametersType["string,int,int,int,int"] = new List<Type> { tString, tInt, tInt, tInt, tInt };
            parametersType["string,int,int"] = new List<Type> { tString, tInt, tInt };
        }

        
    }

}

