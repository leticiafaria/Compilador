using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador
{
    class ChaveDictionary
    {
        public static readonly string Class = "class";
        public static readonly string End = "end";
        public static readonly string Void = "void";
        public static readonly string Main = "main";
        public static readonly string Def = "def";
        public static readonly string Defstatic = "defstatic";
        public static readonly string Return = "return";
        public static readonly string String = "String";
        public static readonly string Integer = "integer";
        public static readonly string Bool = "bool";
        public static readonly string Double = "double";
        public static readonly string If = "if";
        public static readonly string Else = "else";
        public static readonly string While = "while";
        public static readonly string Write = "write";
        public static readonly string True = "true";
        public static readonly string False = "false";

        public static readonly string Or = "or";
        public static readonly string And = "and";
        public static readonly string Maior = ">";
        public static readonly string Menor = "<";
        public static readonly string MaiorIgual = ">=";
        public static readonly string MenorIgual = "<=";
        public static readonly string Atribuicao = "=";
        public static readonly string Igual = "==";
        public static readonly string Diferente = "!=";
        public static readonly string Soma = "+";
        public static readonly string Subtracao = "-";
        public static readonly string Divisao = "/";
        public static readonly string Multiplicacao = "*";
        public static readonly string Negacao = "!";

        public static readonly string AbreParenteses = "(";
        public static readonly string FechaParenteses = ")";
        public static readonly string Aspas = "\"";
        public static readonly string PontoVirgula = ";";
        public static readonly string DoisPontos = ":";
        public static readonly string Hashtag = "#";

        public static readonly string EndOfFile = "EOF";
    }
}
