using System;

namespace Course01Lib {
    public class Student {
        
        private static readonly Student Instance = new Student();
        private static string FirstName { get;} = "Дмитрий";
        private static string LastName { get; } = "Самарцев";

        private Student() { }

        public static Student GetInstance()
        {
            return Instance;
        }
        
        public void Description()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine($"Студент {LastName} {FirstName}");
            Console.WriteLine("====================================================");
        }
    }
}