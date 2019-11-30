using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador
{
    class Estados
    {
        private int posicao;

        Lexer Lexer = new Lexer();

        ValidaTokens Valida = new ValidaTokens();

        public void teste(char[] c, int pont){
            posicao = pont;

            while(Char.IsLetterOrDigit(c[posicao]) && c != '"')
            {
                Console.Write(c[posicao]);
                posicao++;
            }
        }

        public void getPosicao(){
            return this.posicao;
        }

        public void verificarEstados(char[] vetor, int estado, int linha)
        {

            switch (estado)
            {
                case 0:
                    Lexer.lexema = "";
                    break;

                case 1: // forma os id's
                    while (Lexer.ponteiro < vetor.Length && Char.IsLetterOrDigit(vetor[Lexer.ponteiro]) && !vetor[Lexer.ponteiro].Equals(' ') && !vetor[Lexer.ponteiro].Equals(':'))
                    {
                        Lexer.lexema += vetor[Lexer.ponteiro];
                        Lexer.ponteiro++;
                    }

                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    Lexer.ponteiro--;
                    estado = 0;

                    break;

                case 3: // forma valores numericos do tipo inteiro
                    if (Char.IsDigit(vetor[Lexer.ponteiro]))
                    {
                        Lexer.lexema += vetor[Lexer.ponteiro];
                    }
                    else if (vetor[Lexer.ponteiro].Equals('.'))
                    {
                        estado = 5;
                        Lexer.lexema += vetor[Lexer.ponteiro];
                    }
                    else
                    {
                        Lexer.getLexema(Lexer.lexema);
                    }
                    break;

                case 5: // forma valores numericos do tipo flutuante
                    if (Char.IsDigit(vetor[Lexer.ponteiro]) || vetor[Lexer.ponteiro].Equals('.'))
                    {
                        Lexer.lexema += vetor[Lexer.ponteiro];
                    }
                    else
                    {
                        Lexer.getLexema(Lexer.lexema);
                    }
                    break;

                case 9:
                    Valida.VerificaComentario(vetor, Lexer.ponteiro);
                    Lexer.ponteiro = Valida.GetContador();
                    break;

                case 10:
                    Lexer.lexema = Valida.MontaString(vetor, Lexer.ponteiro);

                    if (Valida.VerificaString(Lexer.lexema))
                    {
                        Lexer.ponteiro = Valida.GetContador();
                        Lexer.getLexema(Lexer.lexema);
                    }

                    else
                    {
                        Lexer.ponteiro = Valida.GetContador();
                        Lexer.SinalizaErroLexico("Espera-se um \" na linha: " + linha + " coluna: " + Lexer.ponteiro);
                    }
                    break;

                case 13:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 15:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 16:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 17:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 18:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 19:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.ponteiro++;

                    if (vetor[Lexer.ponteiro].Equals('='))
                    {
                        Lexer.lexema += vetor[Lexer.ponteiro];
                        Lexer.getLexema(Lexer.lexema);
                        Lexer.lexema = "";
                    }

                    else
                    {
                        Lexer.getLexema(Lexer.lexema);
                        Lexer.lexema = "";
                    }
                    break;

                case 25:
                    if (vetor[Lexer.ponteiro].Equals('='))
                    {
                        Lexer.lexema = "=";
                        Lexer.getLexema(Lexer.lexema);
                        estado = 0;
                        Lexer.lexema = "";
                    }
                    else
                    {
                        string mensagem = "Caractere invalido [ {0} ] na linha" + linha + "e coluna " + Lexer.ponteiro;
                        Lexer.SinalizaErroLexico(mensagem);
                    }
                    break;

                case 29:
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 30:
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 31:
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                case 33:
                    Lexer.lexema += vetor[Lexer.ponteiro];
                    Lexer.getLexema(Lexer.lexema);
                    Lexer.lexema = "";
                    break;

                default:
                    Lexer.lexema = "";
                    break;
            }
        }
    }
}