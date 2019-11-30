using System;
using System.Collections.Generic;

namespace Compilador
{
    class ValidaTokens
    {
        private int contador;

        public int GetContador(){
            return this.contador;
        }

        public string CriarId(char[] vetor, int posicao)
        {
            string lexema = vetor[posicao]+"";

            while( posicao < vetor.Length && Char.IsLetterOrDigit(vetor[posicao])  && !vetor[posicao].Equals(' '))
            {
                Console.Write("\nLexema = "+lexema);
                lexema += vetor[posicao];
                posicao++;
                Console.Write(posicao);   
            }

            this.contador = posicao;

            return lexema;
        }

        public bool VerificaCharEspecial(char caracter)
        {
            if(caracter.Equals(' ') || caracter.Equals('\n') || caracter.Equals('\t') )
            {
                return true;
            }
            return false;
        }


        /*  
            Metodos de leitura para a montagem do lexema
            para formar uma string, " ... "
        */
        public string MontaString(char[] vetor, int ponteiro){

            string lexema = vetor[ponteiro].ToString();
            ponteiro++;

            while(ponteiro < vetor.Length && !vetor[ponteiro].Equals('\n')){
                lexema += vetor[ponteiro];
                ponteiro++; 
            }

            this.contador = ponteiro-1;
            return lexema;
        }

        // verifica se na ultima posicao formada pelo lexema possui um ' " '
        public bool VerificaString(string lexema)
        {
            if(lexema.EndsWith("\"")) 
            {
                return true;
            }
            return false;
        }

        /* 
            Ele não faz nada, apenas vai contando a qtde de caracteres 
            nos comentarios para sabermos melhor a qtde de linha e coluna
         */
        public int VerificaComentario(char[] vetor, int ponteiro){
            while(!vetor[ponteiro].Equals('\n')){
                ponteiro++;
            }
            return ponteiro-2;
        }
        
    }
}