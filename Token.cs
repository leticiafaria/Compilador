using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador
{
    class Token
    {
        public string Chave { get; set; }
        public string Valor { get; set; }
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Token(string chave, string valor, int linha, int coluna)
        {
            this.Chave = chave;
            this.Valor = valor;
            this.Linha = linha;
            this.Coluna = coluna;
        }

        public Token(string chave, string valor)
        {
            this.Chave = chave;
            this.Valor = valor;
        }

        public string ImprimeToken()
        {
            return "<" + this.Chave + ", \"" + this.Valor + "\">";
        }

        public string ImprimeTokenComLinhaEColuna()
        {
            return "<" + this.Chave + ", \"" + this.Valor + "\">" + "  Linha: " + this.Linha + "  Coluna: " + this.Coluna;
        }
    }
}
