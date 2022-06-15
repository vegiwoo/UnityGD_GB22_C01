using System;

namespace Task04Lib 
{
    public class Student {
        
        private static readonly Student Instance = new Student();
        private static string firstName  = "Дмитрий";
        private static string lastName  = "Самарцев";

        private Student() { }

        public static Student GetInstance()
        {
            return Instance;
        }
        
        public void Description()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine($"Студент {lastName} {firstName}");
            Console.WriteLine("====================================================");
        }
    }
}