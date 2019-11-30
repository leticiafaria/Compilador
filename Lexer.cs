using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compilador
{
    class Lexer
    {
        int linha = 1;

        public int coluna = 0, ponteiro = 0;

        public string lexema;

        public char[] Conteudo;

        int estado = 0;

        public TabelaSimbolos TabelaSimbolos = new TabelaSimbolos();

        Token token;

        List<Token> ListaTokens = new List<Token>();

        public char[] novoVetor(int tamanho, char[] conteudoArquivo)
        {
            char[] c = new char[tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                c[i] = conteudoArquivo[ponteiro];
                ponteiro++;
            }
            return c;
        }

        public Token GerarLexema(char[] conteudoArquivo, int posicao)
        {
            // veridicar linha
            int tamanhoVetor = conteudoArquivo.Length;
            Conteudo = conteudoArquivo;
            ponteiro = posicao;

            lexema = "";
            estado = 0;

            while (true)
            {
                //Console.Write("\nPonteiro: " +ponteiro);
                //Console.Write("\nEstado: " +estado);
                switch (estado)
                {
                    case 0:
                        if (ponteiro >= tamanhoVetor)
                        {
                            lexema += "EOF";
                            ponteiro++;
                            return GetToken();
                        }

                        else if (conteudoArquivo[ponteiro].Equals('\n'))
                        {
                            tamanhoVetor = conteudoArquivo.Length - ponteiro;

                            conteudoArquivo = novoVetor(tamanhoVetor, conteudoArquivo);
                            Conteudo = conteudoArquivo;

                            linha++;
                            ponteiro = 0;
                            estado = 0;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(' ')) { estado = 0; }

                        else if (Char.IsLetter(conteudoArquivo[ponteiro]))
                        {

                            lexema += conteudoArquivo[ponteiro];
                            estado = 1;
                        }

                        else if (Char.IsDigit(conteudoArquivo[ponteiro]))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 3;
                            coluna = ponteiro;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('.'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 34;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('#'))
                        {
                            estado = 9;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('"'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 10;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('('))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 13;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(')'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 15;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(':'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 16;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(';'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 17;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(','))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 18;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('<'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 19;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('>'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 22;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('='))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 25;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('+'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 29;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('-'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 30;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('*'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 31;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('/'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 33;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('['))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 35;
                        }

                        else if (conteudoArquivo[ponteiro].Equals(']'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 36;
                        }

                        else if (conteudoArquivo[ponteiro].Equals('!'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            estado = 37;
                        }

                        break;

                    case 1: // forma os id's

                        if (ponteiro < conteudoArquivo.Length &&
                            this.IsId(conteudoArquivo[ponteiro]))
                        {
                            lexema += conteudoArquivo[ponteiro];
                        }

                        else
                        {
                            return GetToken();
                        }
                        break;

                    case 3: // forma valores numericos do tipo inteiro

                        if (ponteiro < tamanhoVetor && Char.IsDigit(conteudoArquivo[ponteiro]))
                        {
                            lexema += conteudoArquivo[ponteiro];
                        }

                        else if (ponteiro < tamanhoVetor && conteudoArquivo[ponteiro].Equals('.'))
                        {

                            estado = 5;
                            lexema += conteudoArquivo[ponteiro];
                        }

                        else
                        {
                            if (!Char.IsDigit(conteudoArquivo[ponteiro-1]))
                            {
                                this.SinalizaErroLexico("Esperado um Numero");
                                Environment.Exit(1);
                            }

                            return GetToken();
                        }
                        break;

                    case 5: // forma valores numericos do tipo flutuante
                        if (ponteiro < tamanhoVetor && Char.IsDigit(conteudoArquivo[ponteiro]))
                        {
                            lexema += conteudoArquivo[ponteiro];
                        }
                        else
                        {
                            if (!Char.IsDigit(conteudoArquivo[ponteiro-1]))
                            {
                                this.SinalizaErroLexico("Esperado um Numero");
                                Environment.Exit(1);
                            }

                            return GetToken();
                        }
                        break;

                    case 9:
                        if (conteudoArquivo[ponteiro].Equals('\n'))
                        {
                            return GerarLexema(conteudoArquivo, ponteiro);
                        }
                        break;

                    case 10:
                        if (!conteudoArquivo[ponteiro].Equals('"') && !conteudoArquivo[ponteiro].Equals('\n'))
                        {
                            lexema += conteudoArquivo[ponteiro];
                        }

                        else
                        {
                            if (conteudoArquivo[ponteiro].Equals('"'))
                            {
                                lexema += conteudoArquivo[ponteiro];
                                ponteiro++;
                                return GetToken();
                            }

                            this.SinalizaErroLexico("Esperado Aspas");
                            Environment.Exit(1);
                        }

                        break;

                    case 13:
                        return GetToken();

                    case 15:
                        return GetToken();

                    case 16:
                        return GetToken();

                    case 17:
                        return GetToken();

                    case 18: // Retorna Virgula
                        return GetToken();

                    case 19: // Retorna Menor ou Menor Igual
                        if (conteudoArquivo[ponteiro].Equals('='))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            ponteiro++;
                            return GetToken();
                        }
                        return GetToken();

                    case 22: // Retorna Maior ou Maior Igual
                        if (conteudoArquivo[ponteiro].Equals('='))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            ponteiro++;
                            return GetToken();
                        }
                        return GetToken();

                    case 25: // Retorna Igual ou Atribuicao
                        if (conteudoArquivo[ponteiro].Equals('='))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            ponteiro++;
                            return GetToken();
                        }
                        return GetToken();

                    case 29:
                        return GetToken();

                    case 30:
                        if(IsSubtracao())
                        {
                            token = TabelaSimbolos.RetornaToken(lexema);
                            if (token != null)
                            {
                                token.Chave = ChaveToken.Subtracao;
                                token.Linha = linha;
                                token.Coluna = ponteiro;
                                ListaTokens.Add(token);
                                return token;
                                          
                            }  
                            token = TabelaSimbolos.CadastraSubtracao(lexema, linha, ponteiro);  
                            ListaTokens.Add(token);                   
                            return token; // Retorna Subtracao
                        }
                        // Retorna Negativo
                        token = TabelaSimbolos.RetornaToken(lexema);
                        if (token == null)
                        {
                            token = TabelaSimbolos.CadastraNegativo(lexema, linha, ponteiro);
                            ListaTokens.Add(token);
                            return token;
                        }
                        token.Chave = ChaveToken.Negativo;
                        token.Linha = linha;
                        token.Coluna = ponteiro;
                        ListaTokens.Add(token);
                        return token;


                    case 31:
                        return GetToken();

                    case 33:
                        return GetToken();

                    case 34:
                        return GetToken();

                    case 35:
                        return GetToken();

                    case 36:
                        return GetToken();

                    case 37:
                        if(conteudoArquivo[ponteiro].Equals('='))
                        {
                            lexema += conteudoArquivo[ponteiro];
                            ponteiro++;
                            return GetToken();
                        }
                        return GetToken();
                }
                ponteiro++;
            }
        }

        public void SinalizaErroLexico(string mensagem)
        {
            Console.Write("\n[Erro Lexico] na linha " + linha + " e coluna " + ponteiro + ", ");
            Console.WriteLine(mensagem + "\n");
        }

        public void ImprimirListaTokens()
        {
            foreach (Token T in ListaTokens)
            {
                Console.WriteLine(T.ImprimeTokenComLinhaEColuna());
            }
        }

        public void ImprimirTabelaSimbolo()
        {
            TabelaSimbolos.ImprimeTabelaSimbolos();
        }

        public Token GetToken()
        {
            token = TabelaSimbolos.RetornaToken(lexema);

            if (token == null)
            {
                token = TabelaSimbolos.CadastraNovoTokenTabelaSimbolos(lexema, linha, ponteiro);
                ListaTokens.Add(token);
                return token;
            }

            token.Linha = linha;
            token.Coluna = ponteiro;
            ListaTokens.Add(token);
            return token;
        }

        private bool IsInteger()
        {
            int valor;
            bool isNumber = int.TryParse(lexema, out valor);
            return isNumber;
        }

        private bool IsSubtracao()
        {   
            token = ListaTokens[ListaTokens.Count-1];
            
            if(!token.Chave.Equals(ChaveToken.ValorInteiro) && !token.Chave.Equals(ChaveToken.ValorReal))
            {
                return false;
            }
            return true;
        }

        private bool IsId(char conteudoArquivo){
            return (!conteudoArquivo.Equals(' ') &&
                            !conteudoArquivo.Equals('-') &&
                            !conteudoArquivo.Equals('+') &&
                            !conteudoArquivo.Equals('=') &&
                            !conteudoArquivo.Equals('>') &&
                            !conteudoArquivo.Equals('<') &&
                            !conteudoArquivo.Equals(':') &&
                            !conteudoArquivo.Equals('(') &&
                            !conteudoArquivo.Equals(')') &&
                            !conteudoArquivo.Equals(';') &&
                            !conteudoArquivo.Equals('.') &&
                            !conteudoArquivo.Equals(',') &&
                            !conteudoArquivo.Equals('@') &&
                            !conteudoArquivo.Equals('\n') &&
                            !conteudoArquivo.Equals('!') &&
                            !conteudoArquivo.Equals('[') &&
                            !conteudoArquivo.Equals(']') &&
                            !conteudoArquivo.Equals('_'));
        }
    }
}
