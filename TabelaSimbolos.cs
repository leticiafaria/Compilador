using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compilador
{
    class TabelaSimbolos
    {
        public Dictionary<string, Token> TS { get; set; }
        private Token PalavraReservada;

        public TabelaSimbolos()
        {
            TS = new Dictionary<string, Token>();
            // Monta os tokens das palavras reservadas e insere na tabela de símbolos
            PalavraReservada = new Token(ChaveToken.Class, "class");
            // Repare que existe uma chave para o token e uma chave para a tabela. A chave da tabela é a mesma coisa que o seu valor, assim fica mais fácil de pesquisar
            // Enquanto a chave do Token é referente ao que é printado no console
            TS.Add(ChaveDictionary.Class, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.End, "end");
            TS.Add(ChaveDictionary.End, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Def, "def");
            TS.Add(ChaveDictionary.Def, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Void, "void");
            TS.Add(ChaveDictionary.Void, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Main, "Main");
            TS.Add(ChaveDictionary.Main, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Defstatic, "defstatic");
            TS.Add(ChaveDictionary.Defstatic, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Return, "return");
            TS.Add(ChaveDictionary.Return, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.String, "String");
            TS.Add(ChaveDictionary.String, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Integer, "integer");
            TS.Add(ChaveDictionary.Integer, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Bool, "bool");
            TS.Add(ChaveDictionary.Bool, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Double, "double");
            TS.Add(ChaveDictionary.Double, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.If, "if");
            TS.Add(ChaveDictionary.If, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Else, "else");
            TS.Add(ChaveDictionary.Else, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.While, "while");
            TS.Add(ChaveDictionary.While, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Write, "write");
            TS.Add(ChaveDictionary.Write, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Or, "or");
            TS.Add(ChaveDictionary.Or, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.And, "and");
            TS.Add(ChaveDictionary.And, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.True, "true");
            TS.Add(ChaveDictionary.True, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.False, "false");
            TS.Add(ChaveDictionary.False, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Maior, ">");
            TS.Add(ChaveDictionary.Maior, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Menor, "<");
            TS.Add(ChaveDictionary.Menor, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.MaiorIgual, ">=");
            TS.Add(ChaveDictionary.MaiorIgual, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.MenorIgual, "<=");
            TS.Add(ChaveDictionary.MenorIgual, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Atribuicao, "=");
            TS.Add(ChaveDictionary.Atribuicao, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Igual, "==");
            TS.Add(ChaveDictionary.Igual, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Diferente, "!=");
            TS.Add(ChaveDictionary.Diferente, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Soma, "+");
            TS.Add(ChaveDictionary.Soma, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Divisao, "/");
            TS.Add(ChaveDictionary.Divisao, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Multiplicacao, "*");
            TS.Add(ChaveDictionary.Multiplicacao, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Negacao, "!");
            TS.Add(ChaveDictionary.Negacao, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.AbreParenteses, "(");
            TS.Add(ChaveDictionary.AbreParenteses, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.FechaParenteses, ")");
            TS.Add(ChaveDictionary.FechaParenteses, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Aspas, "\"");
            TS.Add(ChaveDictionary.Aspas, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.PontoVirgula, ";");
            TS.Add(ChaveDictionary.PontoVirgula, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Virgula, ",");
            TS.Add(ChaveDictionary.Virgula, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.DoisPontos, ":");
            TS.Add(ChaveDictionary.DoisPontos, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Hashtag, "#");
            TS.Add(ChaveDictionary.Hashtag, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Print, "print");
            TS.Add(ChaveDictionary.Print, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.EndOfFile, "EOF");
            TS.Add(ChaveDictionary.EndOfFile, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.Ponto, ".");
            TS.Add(ChaveDictionary.Ponto, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.AbreCochetes, "[");
            TS.Add(ChaveDictionary.AbreCochetes, PalavraReservada);

            PalavraReservada = new Token(ChaveToken.FechaCochetes, "]");
            TS.Add(ChaveDictionary.FechaCochetes, PalavraReservada);
        }

        public void ImprimeTabelaSimbolos()
        {
            Console.WriteLine("\n\n\n-------- Tabela de Símbolos --------\n");
            // Percorre toda a tabela de símbolos e printa o seu conteúdo
            foreach (string key in TS.Keys)
            {
                Console.WriteLine(TS[key].ImprimeToken());
            }
        }

        public void ImprimeToken()
        {
            Console.WriteLine("\n\n\n-------- Tabela de Símbolos --------\n");
            // Percorre toda a tabela de símbolos e printa o seu conteúdo
            foreach (string key in TS.Keys)
            {
                Console.WriteLine(TS[key].ImprimeTokenComLinhaEColuna());
            }
        }

        public Token RetornaToken(string lexema)
        {
            // Como a chave é igual ao lexema, se a chave exsistir, irá retornar o valor
            try
            {
                return TS[lexema];
            }
            catch (KeyNotFoundException)
            {
                //CadastraNovoTokenTabelaSimbolos(lexema, linha, coluna);
                return null;
            }
        }

        public void RemoverTS(string lexema)
        {
            foreach (string key in TS.Keys)
            {
                if(key.Equals(lexema))
                {
                    TS.Remove(TS[key].Chave);
                }
            }   
        }

        public Token CadastraNegativo(string lexema, int linha, int coluna)
        {
            Token Token;
            Token = new Token(ChaveToken.Negativo, lexema, linha, coluna);
            
            this.TS.Add(lexema, Token);
            return Token;
        }

        public Token CadastraSubtracao(string lexema, int linha, int coluna)
        {
            Token Token;
            Token = new Token(ChaveToken.Subtracao, lexema, linha, coluna);
            
            this.TS.Add(lexema, Token);
            return Token;
        }

        private bool IsInteger(string lexema)
        {
            int valor;
            bool isNumber = int.TryParse(lexema, out valor);
            return isNumber;
        }

        private bool IsDouble(string lexema)
        {
            double val;
            bool isDouble = double.TryParse(lexema, out val);
            return isDouble;
        }

        private bool IsString(string lexema)
        {
            return lexema.Contains('"');
        }

        public Token CadastraNovoTokenTabelaSimbolos(string lexema, int linha, int coluna)
        {
            Token Token;

            if (this.IsInteger(lexema))
            {
                Token = new Token(ChaveToken.ValorInteiro, lexema, linha, coluna);
            }

            else if(this.IsDouble(lexema))
            {
                Token = new Token(ChaveToken.ValorReal, lexema, linha, coluna);
            }
            
            else if(this.IsString(lexema))
            {
                Token = new Token(ChaveToken.ConstString, lexema, linha, coluna);
            }

            else
            {
                Token = new Token(ChaveToken.Identificador, lexema, linha, coluna);
            }

            this.TS.Add(lexema, Token);
            return Token;
        }
    }
}
