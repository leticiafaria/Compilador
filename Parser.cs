using System;

namespace Compilador
{
    class Parser
    {
        Lexer Lexer; Token Token; char[] letras;
        private int pont = 0, Erro = 0;

        public Parser(Lexer l, char[] listaTokens)
        {
            this.Lexer = l;
            this.letras = listaTokens;
            this.Token = this.Lexer.GerarLexema(this.letras, this.pont);
            this.pont = this.Lexer.ponteiro + 1;
            this.letras = this.Lexer.Conteudo;
        }

        public void Advance()
        {
            Console.Write("\n[DEBUG] token: " + this.Token.ImprimeTokenComLinhaEColuna());
            this.Token = this.Lexer.GerarLexema(this.letras, this.pont);
            this.pont = this.Lexer.ponteiro;
            this.letras = this.Lexer.Conteudo;
        }

        public bool Eat(string TokenConsumido)
        {
            if (!this.Token.Chave.Equals(TokenConsumido))
            {
                return false;
            }
            Advance();
            return true;
        }

        private void SinalizaErroSintatico(string message)
        {
            Console.Write("\n[Erro Sintatico] na linha " + Token.Linha + " e coluna " + Token.Coluna + ": ");
            Console.WriteLine(message + "\n");
        }

        public void Skip(string message)
        {
            SinalizaErroSintatico(message);
            Advance();
        }

        // Programa → Classe EOF
        public void Programa()
        {
            Classe();
            if (!Eat(ChaveToken.EndOfFile))
            {
                this.SinalizaErroSintatico("Esperado \"EOF\"; encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
            }
        }

        // Classe → "class" ID ":" ListaFuncao Main "end" "."
        public void Classe()
        {
            if (Eat(ChaveToken.Class))
            {
                ID();

                if (!Eat(ChaveToken.DoisPontos))
                {
                    this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                ListaFuncao();
                Main();

                if (!Eat(ChaveToken.End))
                {
                    this.SinalizaErroSintatico(" Esperado \"end\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.Ponto))
                {
                    this.SinalizaErroSintatico(" Esperado \".\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

            }
            else
            {
                // --> Synch: FOLLOW(Classe)
                if (this.Token.Chave == ChaveToken.EndOfFile)
                {
                    this.SinalizaErroSintatico("Esperado \"class\", encontrado " + "\"" + Token.Valor + "\"");
                    return;
                }

                // --> Skip
                else
                {
                    this.Skip("Esperado \"class\", encontrado " + "\"" + Token.Valor + "\"");
                    if (this.Token.Chave != ChaveToken.EndOfFile) { Classe(); }
                }
            }
        }

        public void ID()
        {
            if (!Eat(ChaveToken.Identificador))
            {
                this.SinalizaErroSintatico(" Esperado \"ID\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
            }
        }

        // ListaFuncao → ListaFuncao’
        public void ListaFuncao() { ListaFuncaoLinha(); }

        // ListaFuncao’ → Funcao ListaFuncao’ | ε
        public void ListaFuncaoLinha()
        {
            if (Token.Chave.Equals(ChaveToken.Def))
            {
                Funcao();
                ListaFuncaoLinha();
            }

            // ListaFuncao’ → ε
            else if (Token.Chave.Equals(ChaveToken.Defstatic)) { return; }

            else
            {
                this.Skip("Esperado \"def\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { ListaFuncaoLinha(); }
            }
        }

        // Funcao →"def" TipoPrimitivo ID "(" ListaArg ")" ":" RegexDeclaraId ListaCmd Retorno "end" ";"
        public void Funcao()
        {
            if (Eat(ChaveToken.Def))
            {
                TipoPrimitivo();
                ID();

                if (!Eat(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"(\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                ListaArg();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.DoisPontos))
                {
                    this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                RegexDeclaraId();
                ListaCmd();
                Retorno();

                if (!Eat(ChaveToken.End))
                {
                    this.SinalizaErroSintatico(" Esperado \"end\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

            }
        }

        // TipoPrimitivo → "bool" | "integer" | "String" | "double" | "void"
        public void TipoPrimitivo()
        {
            if (!Eat(ChaveToken.Bool) && !Eat(ChaveToken.Integer) && !Eat(ChaveToken.String)
                && !Eat(ChaveToken.Double) && !Eat(ChaveToken.Void))
            {
                // --> Synch: FOLLOW(TipoPrimitivo)
                if (this.Token.Chave == ChaveToken.Identificador)
                {
                    this.SinalizaErroSintatico("Esperado \"bool, integer, String, double ou void\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    return;
                }

                // --> Skip
                else
                {
                    this.Skip("Esperado \"bool, integer, String, double ou void\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { TipoPrimitivo(); }
                }
            }
        }

        // Main →"defstatic" "void" "main" "(" "String" "[" "]" ID ")" ":" RegexDeclaraId ListaCmd "end" ";"
        public void Main()
        {
            if (Eat(ChaveToken.Defstatic))
            {

                if (!Eat(ChaveToken.Void))
                {
                    this.SinalizaErroSintatico(" Esperado \"void\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.Main))
                {
                    this.SinalizaErroSintatico(" Esperado \"main\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"(\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.String))
                {
                    this.SinalizaErroSintatico(" Esperado \"String\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.AbreCochetes))
                {
                    this.SinalizaErroSintatico(" Esperado \"[\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.FechaCochetes))
                {
                    this.SinalizaErroSintatico(" Esperado \"]\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                ID();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.DoisPontos))
                {
                    this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                RegexDeclaraId();
                ListaCmd();

                if (!Eat(ChaveToken.End))
                {
                    this.SinalizaErroSintatico(" Esperado \"end\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }
            else
            {
                // --> Synch: FOLLOW(Main)
                if (this.Token.Chave == ChaveToken.End)
                {
                    this.SinalizaErroSintatico("Esperado \"defstatic\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    return;
                }

                // --> Skip
                else
                {
                    this.Skip("Esperado \"defstatic\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { Main(); }
                }
            }
        }

        // RegexDeclaraId → DeclaraID RegexDeclaraId | ε
        public void RegexDeclaraId()
        {
            if (Token.Chave.Equals(ChaveToken.Void) || Token.Chave.Equals(ChaveToken.String) ||
                Token.Chave.Equals(ChaveToken.Bool) || Token.Chave.Equals(ChaveToken.Integer) ||
                Token.Chave.Equals(ChaveToken.Double))
            {
                DeclaraID();
                RegexDeclaraId();
            }

            // RegexDeclaraId → ε
            else if (Token.Chave.Equals(ChaveToken.Identificador) || Token.Chave.Equals(ChaveToken.End) ||
                Token.Chave.Equals(ChaveToken.Return) || Token.Chave.Equals(ChaveToken.If) ||
                Token.Chave.Equals(ChaveToken.While) || Token.Chave.Equals(ChaveToken.Write))
            {
                return;
            }

            else
            {
                this.Skip("Esperado \"void, String, bool, integer, double\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { RegexDeclaraId(); }
            }
        }

        // DeclaraID → TipoPrimitivo ID ";"
        public void DeclaraID()
        {
            if (this.Token.Chave == ChaveToken.Void || this.Token.Chave == ChaveToken.String ||
                this.Token.Chave == ChaveToken.Bool || this.Token.Chave == ChaveToken.Integer ||
                this.Token.Chave == ChaveToken.Double)
            {
                TipoPrimitivo();
                ID();
                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }
            else
            {
                // --> Synch: FOLLOW(DeclaraID)
                if (this.Token.Chave == ChaveToken.Void || this.Token.Chave == ChaveToken.String ||
                    this.Token.Chave == ChaveToken.Bool || this.Token.Chave == ChaveToken.Integer ||
                    this.Token.Chave == ChaveToken.Double || this.Token.Chave == ChaveToken.If ||
                    this.Token.Chave == ChaveToken.While || this.Token.Chave == ChaveToken.Identificador ||
                    this.Token.Chave == ChaveToken.Write || this.Token.Chave == ChaveToken.Return ||
                    this.Token.Chave == ChaveToken.End)
                {
                    this.SinalizaErroSintatico(" Esperado \"void, String, bool, integer, double\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    return;
                }

                else
                {
                    this.Skip(" Esperado \"void, String, bool, integer, double\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { DeclaraID(); }
                }
            }

        }

        // ListaCmd → ListaCmd’ 
        public void ListaCmd()
        {
            if (Token.Chave.Equals(ChaveToken.Identificador) || Token.Chave.Equals(ChaveToken.End) ||
                Token.Chave.Equals(ChaveToken.Return) || Token.Chave.Equals(ChaveToken.If) ||
                Token.Chave.Equals(ChaveToken.Else) || Token.Chave.Equals(ChaveToken.While) ||
                Token.Chave.Equals(ChaveToken.Write))
            {
                ListaCmdLinha();
            }
            else
            {
                this.Skip(" Esperado \"ID, end, return, if, else, while ou write\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { ListaCmd(); }
            }
        }

        // ListaCmd’ → Cmd ListaCmd’ | ε 
        public void ListaCmdLinha()
        {
            if (Token.Chave.Equals(ChaveToken.If) || Token.Chave.Equals(ChaveToken.While) ||
                Token.Chave.Equals(ChaveToken.Write) || Token.Chave.Equals(ChaveToken.Identificador))
            {
                Cmd();
                ListaCmdLinha();
            }

            // ListaCmd’ → ε
            else if (Token.Chave.Equals(ChaveToken.End) || Token.Chave.Equals(ChaveToken.Else)
                || Token.Chave.Equals(ChaveToken.Return))
            {
                return;
            }

            else
            {
                this.Skip(" Esperado \"if, while, write ou ID\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { ListaCmdLinha(); }
            }
        }

        // Cmd → CmdIF | CmdWhile | ID CmdAtribFunc | CmdWrite
        public void Cmd()
        {
            if (Token.Chave.Equals(ChaveToken.If)) { CmdIF(); }

            else if (Token.Chave.Equals(ChaveToken.While)) { CmdWhile(); }

            else if (Token.Chave.Equals(ChaveToken.Write)) { CmdWrite(); }

            else if (Token.Chave.Equals(ChaveToken.Identificador))
            {
                ID();
                CmdAtribFunc();
            }
            else
            {
                // --> Synch: FOLLOW(Cmd)
                if (Token.Chave.Equals(ChaveToken.Return) ||
                    Token.Chave.Equals(ChaveToken.End) ||
                    Token.Chave.Equals(ChaveToken.Else))
                {
                    this.SinalizaErroSintatico(" Esperado \"if | while | write | Id\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    return;
                }

                // --> Skip
                else
                {
                    this.Skip(" Esperado \"if | while | write | Id\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { Cmd(); }
                }
            }
        }

        // CmdIF → "if" "(" Expressao ")" ":" ListaCmd CmdIF’
        public void CmdIF()
        {
            if (Eat(ChaveToken.If))
            {
                if (!Eat(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"(\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
                Expressao();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.DoisPontos))
                {
                    this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                ListaCmd();
                CmdIFLinha();
            }
        }

        // Expressao → Exp1 Exp’
        public void Expressao()
        {
            Exp1();
            ExpLinha(); 
        }

        // Exp1 → Exp2 Exp1’
        public void Exp1()
        {
            Exp2();
            Exp1Linha();
            
        }

        // Exp2 → Exp3 Exp2’
        public void Exp2()
        {
            Exp3();
            Exp2Linha();
        }

        // Exp3 → Exp4 Exp3’
        public void Exp3()
        {
            Exp4();
            Exp3Linha();
        }

        // Exp4 → ID Exp4’ | ConstInteger | ConstDouble | ConstString 
        // | "true" | "false" | OpUnario Exp4 | "(" Expressao")"

        public void Exp4()
        {
            if (Eat(ChaveToken.Identificador)) { Exp4Linha(); }

            // OpUnario
            else if (Token.Chave.Equals(ChaveToken.Negativo) ||
                Token.Chave.Equals(ChaveToken.Negacao))
            {
                OpUnario();
                Exp4();
            }
            
            else if (Eat(ChaveToken.AbreParenteses))
            {
                Expressao();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    Erro++;
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                }
            }

            else if (!Eat(ChaveToken.ValorInteiro) &&
                !Eat(ChaveToken.ConstString) &&
                !Eat(ChaveToken.ValorReal) &&
                !Eat(ChaveToken.True) &&
                !Eat(ChaveToken.False))
            {
                // --> Synch: FOLLOW(Exp4)
                if (Token.Chave.Equals(ChaveToken.Multiplicacao) || Token.Chave.Equals(ChaveToken.Divisao) ||
                    Token.Chave.Equals(ChaveToken.Soma) || Token.Chave.Equals(ChaveToken.Subtracao) ||
                    Token.Chave.Equals(ChaveToken.Menor) || Token.Chave.Equals(ChaveToken.MenorIgual) ||
                    Token.Chave.Equals(ChaveToken.Maior) || Token.Chave.Equals(ChaveToken.MaiorIgual) ||
                    Token.Chave.Equals(ChaveToken.Igual) || Token.Chave.Equals(ChaveToken.Diferente) ||
                    Token.Chave.Equals(ChaveToken.Or) || Token.Chave.Equals(ChaveToken.And) ||
                    Token.Chave.Equals(ChaveToken.FechaParenteses) || Token.Chave.Equals(ChaveToken.PontoVirgula)
                    || Token.Chave.Equals(ChaveToken.Virgula))
                {
                    Erro++;
                    this.SinalizaErroSintatico(" Esperado \"ID, ConstInteger, ConstDouble, ConstString, true ,false , OpUnario, '('\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                    return;
                }

                else
                {
                    Erro++;
                    this.Skip("Esperado \"ID, ConstInteger, ConstDouble, ConstString, true ,false , OpUnario, '('\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { Exp4(); }
                }
            }
        }

        // Exp4’ → "(" RegexExp ")" | ε

        public void Exp4Linha()
        {
            if (Eat(ChaveToken.AbreParenteses))
            {
                RegexExp();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }
            
            // Exp4’ → ε
            else if(Token.Chave.Equals(ChaveToken.PontoVirgula) ||
                Token.Chave.Equals(ChaveToken.FechaParenteses) ||
                Token.Chave.Equals(ChaveToken.Virgula) ||
                Token.Chave.Equals(ChaveToken.Or) ||
                Token.Chave.Equals(ChaveToken.And) ||
                Token.Chave.Equals(ChaveToken.Menor) ||
                Token.Chave.Equals(ChaveToken.MenorIgual) ||
                Token.Chave.Equals(ChaveToken.Maior) ||
                Token.Chave.Equals(ChaveToken.MaiorIgual) ||
                Token.Chave.Equals(ChaveToken.Igual) ||
                Token.Chave.Equals(ChaveToken.Diferente) ||
                Token.Chave.Equals(ChaveToken.Soma) ||
                Token.Chave.Equals(ChaveToken.Subtracao) ||
                Token.Chave.Equals(ChaveToken.Multiplicacao) ||
                Token.Chave.Equals(ChaveToken.Divisao))
            {
                return;
            }

            else
            {
                Erro++;
                this.Skip("Esperado \"(, ε\", encontrado " + "\"" + Token.Valor + "\"");
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { Exp4(); }
            }
        }

        // RegexExp → Expressao RegexExp’ | ε
        public void RegexExp()
        {
            if (Token.Chave.Equals(ChaveToken.Identificador) ||
                Token.Chave.Equals(ChaveToken.AbreParenteses) ||
                Token.Chave.Equals(ChaveToken.ValorInteiro) ||
                Token.Chave.Equals(ChaveToken.ValorReal) ||
                Token.Chave.Equals(ChaveToken.ConstString) ||
                Token.Chave.Equals(ChaveToken.True) ||
                Token.Chave.Equals(ChaveToken.False) ||
                Token.Chave.Equals(ChaveToken.Negativo) ||
                Token.Chave.Equals(ChaveToken.Negacao))
            {
                Expressao();
                RegexExpLinha();
            }

            // RegexExp → ε
            else if (Token.Chave.Equals(ChaveToken.FechaParenteses)) { return; }

            else
            {
                Erro++;
                this.Skip(" Esperado \"Id, ConstInteger, ConstDouble, ConstString, true, false, -(Negacao), !, (\", encontrado " + "\"" + Token.Valor + "\"");
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { RegexExp(); }
            }
        }

        // RegexExp’ → , Expressao RegexExp’ | ε
        public void RegexExpLinha()
        {
            if (Eat(ChaveToken.Virgula))
            {
                Expressao();
                RegexExpLinha();
            }
        }

        // ListaArg → Arg ListaArg’
        public void ListaArg()
        {
            if (Token.Chave.Equals(ChaveToken.Void) || Token.Chave.Equals(ChaveToken.String) ||
                Token.Chave.Equals(ChaveToken.Bool) || Token.Chave.Equals(ChaveToken.Integer) ||
                Token.Chave.Equals(ChaveToken.Double))
            {
                Arg();
                ListaArgLinha();
            }
        }

        // Arg → TipoPrimitivo ID
        public void Arg()
        {
            TipoPrimitivo();
            ID();
        }

        // ListaArg’ → "," ListaArg | ε
        public void ListaArgLinha()
        {
            if (Eat(ChaveToken.Virgula)) { ListaArg(); }

            // ListaArg’ → ε
            else if (Token.Chave.Equals(ChaveToken.FechaParenteses)) { return; }

            else
            {
                Erro++;
                this.Skip(" Esperado \", ou )\", encontrado " + "\"" + Token.Valor + "\"");
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { ListaArgLinha(); }
            }
        }

        // CmdWhile → "while" "(" Expressao ")" ":" ListaCmd "end" ";"
        public void CmdWhile()
        {
            if (Eat(ChaveToken.While))
            {
                if (!Eat(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"(\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                Expressao();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.DoisPontos))
                {
                    this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                ListaCmd();

                if (!Eat(ChaveToken.End))
                {
                    this.SinalizaErroSintatico(" Esperado \"end\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }

            else
            {
                this.SinalizaErroSintatico(" Esperado \"while\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
            }
        }

        // CmdWrite → "write" "(" Expressao ")" ";"
        public void CmdWrite()
        {
            if (Eat(ChaveToken.Write))
            {
                if (!Eat(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"(\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                Expressao();

                if (!Eat(ChaveToken.FechaParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \")\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }

            else
            {
                this.SinalizaErroSintatico(" Esperado \"write\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
            }
        }

        // CmdAtribui → "=" Expressao ";"
        public void CmdAtribFunc()
        {
            if (Eat(ChaveToken.Atribuicao))
            {
                Expressao();

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    Erro++;
                    ContarErros();
                }
            }

            else
            {
                this.SinalizaErroSintatico(" Esperado \"=\", encontrado " + "\"" + Token.Valor + "\"");
                Erro++;
                ContarErros();
            }
        }

        // CmdIF’ → "end" ";" | "else" ":" ListaCmd "end" ";"
        public void CmdIFLinha()
        {
            if (Eat(ChaveToken.End))
            {

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    Erro++;
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                }
            }

            else
            {
                if (Eat(ChaveToken.Else))
                {
                    if (!Eat(ChaveToken.DoisPontos))
                    {
                        Erro++;
                        this.SinalizaErroSintatico(" Esperado \":\", encontrado " + "\"" + Token.Valor + "\"");
                        ContarErros();
                    }

                    ListaCmd();

                    if (!Eat(ChaveToken.End))
                    {
                        Erro++;
                        this.SinalizaErroSintatico(" Esperado \"end\", encontrado " + "\"" + Token.Valor + "\"");
                        ContarErros();
                    }

                    if (!Eat(ChaveToken.PontoVirgula))
                    {
                        Erro++;
                        this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                        ContarErros();
                    }
                }
            }
        }

        // Exp’ → "or" Exp1 Exp’ | "and" Exp1 Exp’ | ε
        public void ExpLinha()
        {
            if (Eat(ChaveToken.Or) || Eat(ChaveToken.And))
            {
                Exp1();
                ExpLinha();
            }

        }

        // Exp1’ → "<" Exp2 Exp1’ | "<=" Exp2 Exp1’ | ">" Exp2 Exp1’ |
        // ">=" Exp2 Exp1’ | "==" Exp2 Exp1’ | "!=" Exp2 Exp1’ | ε
        public void Exp1Linha()
        {
            if (Eat(ChaveToken.Menor) ||
                Eat(ChaveToken.MenorIgual) ||
                Eat(ChaveToken.Maior) ||
                Eat(ChaveToken.MaiorIgual) ||
                Eat(ChaveToken.Igual) ||
                Eat(ChaveToken.Diferente))
            {
                Exp2();
                ExpLinha();
            }
        }

        // Exp2’ → "+" Exp3 Exp2’ | "-" Exp3 Exp2’ | ε
        public void Exp2Linha()
        {
            if (Eat(ChaveToken.Soma) ||
                Eat(ChaveToken.Subtracao))
            {
                Exp3();
                Exp2Linha();
            }
        }

        // Exp3’ → "*" Exp4 Exp3’ | "/" Exp4 Exp3’ | ε
        public void Exp3Linha()
        {
            if (Eat(ChaveToken.Multiplicacao) ||
                Eat(ChaveToken.Divisao))
            {
                Exp4();
                Exp3Linha();
            }
        }

        // Retorno → "return" Expressao ";" | ε
        public void Retorno()
        {
            if (Eat(ChaveToken.Return))
            {
                Expressao();

                if (!Eat(ChaveToken.PontoVirgula))
                {
                    Erro++;
                    this.SinalizaErroSintatico(" Esperado \";\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                }
            }

            // Retorno → ε
            else if (Token.Chave.Equals(ChaveToken.End)) { return; }

            else
            {
                Erro++;
                this.Skip("Esperado \",\", encontrado " + "\"" + Token.Valor + "\"");
                ContarErros();
                if (this.Token.Chave != ChaveToken.EndOfFile) { Retorno(); }
            }

        }

        public void OpUnario()
        {
            if (!Eat(ChaveToken.Negativo) &&
                !Eat(ChaveToken.Negacao))
            {
                Erro++;
                // --> Synch: FOLLOW(OpUnario)
                if (Token.Chave.Equals(ChaveToken.Identificador) || Token.Chave.Equals(ChaveToken.ValorInteiro) ||
                    Token.Chave.Equals(ChaveToken.ValorReal) || Token.Chave.Equals(ChaveToken.ConstString) ||
                    Token.Chave.Equals(ChaveToken.True) || Token.Chave.Equals(ChaveToken.False) ||
                    Token.Chave.Equals(ChaveToken.AbreParenteses))
                {
                    this.SinalizaErroSintatico(" Esperado \"- (Negacao) ou !\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                    return;
                }

                else
                {
                    this.Skip(" Esperado \"- (Negacao) ou !\", encontrado " + "\"" + Token.Valor + "\"");
                    ContarErros();
                    if (this.Token.Chave != ChaveToken.EndOfFile) { OpUnario(); }
                }
            }
        }

        private void ContarErros()
        {
            if(this.Erro > 4)
            {
                Environment.Exit(1);
            }
        } 
    }
}