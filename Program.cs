using System;
using System.IO;

namespace Compilador
{
    class Program
    {
        // *** Não se esqueça de ajustar o caminho do arquivo antes de executar ***
        private static readonly string caminho = @"D:\projetos\c#\Compilador\Compilador\Compilador\text.txt";
        static void Main(string[] args)
        {
            string texto = LerArquivo(caminho);

            // Converte a string com todo o conteúdo do arquivo em um array de Char
            char[] letras = texto.ToCharArray();

            TabelaSimbolos TSimbolos = new TabelaSimbolos();

            TSimbolos.ImprimeTabelaSimbolos();

        }

        #region Métodos estáticos
        // Lê todo o arquivo e retorna como string
        public static string LerArquivo(string caminho)
        {
            return File.ReadAllText(caminho);
        }

        #endregion
    }
}
