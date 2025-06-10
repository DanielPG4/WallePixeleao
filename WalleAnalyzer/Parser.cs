using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WalleClass;


namespace WalleAnalyzer
{
    class Parser
    {
        Dictionary<string, List<Type>> parametersType;
        Dictionary<string, string> admissibleParameters;
        public List<Error> errors;
        List<Token> tokenList;

        int tokenAct = 0;

        public Parser(List<Token> tokenList)
        {
            this.tokenList = tokenList;
            errors = new List<Error>();
            admissibleParameters = new Dictionary<string, string>();
            parametersType = new Dictionary<string, List<Type>>();
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
            Register(); // Asegurar que los parámetros estén registrados

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
                            // El error ya fue agregado en ValidateParameters
                        }
                        // Aquí podrías construir el nodo del AST correspondiente
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

        void AddError(string text)
        {
            errors.Add(new Error(tokenList[tokenAct].location, text));
        }

        List<Expression> ParseParameters(string name)//revisar
        {
            List<Expression> parameters = new List<Expression>();

            // Verificar paréntesis de apertura
            if (!ConsumeToken() || tokenList[tokenAct].Value != "(")
            {
                AddError($"Se esperaba '(' después de {name}");
                return parameters;
            }

            // Parsear parámetros
            while (true)
            {
                if (!ConsumeToken())
                {
                    AddError("Fin inesperado del código");
                    return parameters;
                }

                // Fin de los parámetros
                if (tokenList[tokenAct].Value == ")")
                    break;

                // Parsear expresión según el tipo de token
                switch (tokenList[tokenAct].Type)
                {
                    case TokenType.Number:
                        parameters.Add(new Number(tokenList[tokenAct].Value));
                        break;
                    case TokenType.Variable:
                        parameters.Add(new Variable(tokenList[tokenAct].Value));
                        break;
                    default:
                        AddError($"Parámetro inválido: {tokenList[tokenAct].Value}");
                        break;
                }

                // Verificar coma o paréntesis de cierre
                if (!ConsumeToken())
                {
                    AddError("Fin inesperado del código");
                    return parameters;
                }

                if (tokenList[tokenAct].Value == ")")
                    break;

                if (tokenList[tokenAct].Value != ",")
                {
                    AddError($"Se esperaba ',' o ')', se encontró {tokenList[tokenAct].Value}");
                    return parameters;
                }
            }

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





            parametersType["int,int"] = new List<Type> { tInt, tInt };
            parametersType["string"] = new List<Type> { tString };
            parametersType["int"] = new List<Type> { tInt };
            parametersType["int,int,int"] = new List<Type> { tInt, tInt, tInt };
            parametersType["int,int,int,int,int"] = new List<Type> { tInt, tInt, tInt, tInt, tInt };
            parametersType["string,int,int,int,int"] = new List<Type> { tString, tInt, tInt, tInt, tInt };
            parametersType["string,int,int"] = new List<Type> { tString, tInt, tInt };
        }

        /*public void SpawnReader()
        {
            if(ConsumeToken() && TokenList[tokenAct].Value == "(")
            {
                if (ConsumeToken() && TokenList[tokenAct].Type == TokenType.Number)
                {
                    if (ConsumeToken() && TokenList[tokenAct].Value == ",")
                    {
                        if (ConsumeToken() && TokenList[tokenAct].Type == TokenType.Number)
                        {
                            if (ConsumeToken() && TokenList[tokenAct].Value == ")")
                            {
                                Console.WriteLine("Correcto");
                            }
                            else Console.WriteLine("Se esperaba ) y se encontro " + TokenList[tokenAct].Value);
                        }
                        else Console.WriteLine("Se esperaba un numero y se encontro " + TokenList[tokenAct].Value);
                    }
                    else Console.WriteLine("Se esperaba una , y se encontro " + TokenList[tokenAct].Value);
                }
                else Console.WriteLine("Se esperaba un numeroy se encontro " + TokenList[tokenAct].Value);
            }
            else Console.WriteLine("Se esperaba un ( y se encontro " + TokenList[tokenAct].Value);
        }*/
    }

}

