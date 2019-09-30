using System;
using System.Collections.Generic;
using System.Text;

namespace Compilador
{
    class ChaveToken
    {
        public static readonly string Class = "KW_CLASS";
        public static readonly string End = "KW_END";
        public static readonly string Void = "KW_VOID";
        public static readonly string Main = "KW_MAIN";
        public static readonly string Def = "KW_DEF";
        public static readonly string Defstatic = "KW_DEFSTATIC";
        public static readonly string Return = "KW_RETURN";
        public static readonly string String = "KW_STRING";
        public static readonly string Integer = "KW_INTEGER";
        public static readonly string Bool = "KW_BOOL";
        public static readonly string Double = "KW_DOUBLE";
        public static readonly string If = "KW_IF";
        public static readonly string Else = "KW_ELSE";
        public static readonly string While = "KW_WHILE";
        public static readonly string Write = "KW_WRITE";
        public static readonly string True = "KW_TRUE";
        public static readonly string False = "KW_FALSE";

        public static readonly string Or = "OP_OR";
        public static readonly string And = "OP_AND";
        public static readonly string Maior = "OP_MAIOR";
        public static readonly string Menor = "OP_MENOR";
        public static readonly string MaiorIgual = "OP_MAIOR_IGUAL";
        public static readonly string MenorIgual = "OP_MENOR_IGUAL";
        public static readonly string Atribuicao = "OP_ATRIBUICAO";
        public static readonly string Igual = "OP_IGUAL";
        public static readonly string Diferente = "OP_DIFERENTE";
        public static readonly string Soma = "OP_SOMA";
        public static readonly string Subtracao = "OP_SUB";
        public static readonly string Divisao = "OP_DIV";
        public static readonly string Multiplicacao = "OP_MULTI";
        public static readonly string Negacao = "OP_NEGACAO";

        public static readonly string AbreParenteses = "SMB_A_PARENTESES";
        public static readonly string FechaParenteses = "SMB_F_PARENTESES";
        public static readonly string Aspas = "SMB_ASPAS";
        public static readonly string PontoVirgula = "SMB_PONTO_VIRGULA";
        public static readonly string DoisPontos = "SMB_DOIS_PONTOS";
        public static readonly string Hashtag = "SMB_HASHTAG";

        public static readonly string EndOfFile = "EOF";
        public static readonly string Identificador = "ID";

    }
}
