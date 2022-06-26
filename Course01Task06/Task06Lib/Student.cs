using System;

namespace Task05Lib 
{
    public static class Student {
    
        private static string firstName  = "Дмитрий";
        private static string lastName  = "Самарцев";
        
        public static void Description()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine($"Студент {lastName} {firstName}");
            Console.WriteLine("====================================================");
        }
    }
}