using System;
using System.IO;

namespace Compilador
{
    class Program
    {
        // *** Não se esqueça de ajustar o caminho do arquivo antes de executar ***
        private static readonly string caminho = @"C:\Users\JF\Compilador\Testes_Gramatica_Pyscal\teste2.pys";
        static void Main(string[] args)
        {
            string texto = LerArquivo(caminho);

            texto = TransformarTab(texto);

            // Converte a string com todo o conteúdo do arquivo em um array de Char
            char[] letras = texto.ToCharArray();

            Lexer lexer = new Lexer();

            Console.WriteLine("\n-------- Lista de Tokens --------");

            Parser p = new Parser(lexer, letras);

            p.Programa();

            Console.WriteLine("\n\n-------- Fim da Compilação --------");
        }

        #region Métodos estáticos
        // Lê todo o arquivo e retorna como string
        public static string LerArquivo(string caminho)
        {
            return File.ReadAllText(caminho);
        }

        public static string TransformarTab(string texto)
        {
            int TabSize = 4;
            string TabSpace = new String(' ', TabSize);
            texto = texto.Replace("\t", TabSpace);
            return texto;
        }

        #endregion
    }
}
