using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;

namespace Compiler
{
    class Program
    {
        public static CommonTokenStream tokens;

        static void Main()
        {
            try
            {
                // В качестве входного потока символов устанавливаем консольный ввод или содержимое файла
                // AntlrInputStream input = new AntlrInputStream(Console.In);
                AntlrFileStream input = new AntlrFileStream("Source.txt");
                // Настраиваем лексер на этот поток
                CompilerLexer lexer = new CompilerLexer(input);
                // Создаем поток токенов на основе лексера
                tokens = new CommonTokenStream(lexer);
                // Создаем парсер
                CompilerParser parser = new CompilerParser(tokens);
                IParseTree tree = parser.compiler();
                // Запускаем первое правило грамматики
                MyVisitor visitor = new MyVisitor();
                // Запускаем обход дерева
                visitor.Visit(tree);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
