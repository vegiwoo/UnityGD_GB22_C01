using System;

namespace Task08Lib 
{
    public static class Student {
        private const string firstName = "Дмитрий";
        private const string lastName = "Самарцев";

        public static void Description()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================================");
            Console.WriteLine($"Студент {lastName} {firstName}");
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}