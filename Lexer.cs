using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compilador
{
    class Lexer
    {
        // Lógica para encontrar linha e coluna dos lexemas
        // Obs: Importante! Todos os "/r" devem ser ignorados. 
        int linha = 1;
        int coluna = 1;


        public void CalculaLinhaColuna(char[] conteudoArquivo)
        {
            for (int i = 0; i < conteudoArquivo.Length; i++)
            {
                if (conteudoArquivo[i] == '\n')
                {
                    linha++;
                }

                // i corresponde a coluna, que seria o "cursor" que você utilizaria para ir progredindo no arquivo. Obs: Não se esqueça de ignorar os "\r".
                // Esse método é só um exemplo
            }
        }

        public void SinalizaErroLexico(string mensagem)
        {
            Console.WriteLine("[Erro Lexico]: " + mensagem + "\n");
        }

        public void getLexema()
        {
            // ... código para formar o lexema, validando os estados, etc

            // Após o lexema estar "pronto", segue lógica para verificar se existe na tabela de símbolos,

            string lexema = "teste";

            TabelaSimbolos TabelaSimbolos = new TabelaSimbolos();



            try
            {
                Token token = TabelaSimbolos.RetornaToken(lexema);
            }
            catch (KeyNotFoundException)
            {
                //Se ele deu erro, é porque não existe uma chave com aquele valor, portanto, o lexema não foi cadastrado
                TabelaSimbolos.CadastraNovoTokenTabelaSimbolos(lexema, linha, coluna);
            }
        }

    }
}
