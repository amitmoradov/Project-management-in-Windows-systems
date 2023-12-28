using System;
//namespace Targil0;
partial class Program7061

{
    static void Main(string[] args)
    {
        Welcome7061();
        Welcome3114();
        Console.ReadKey();
    }

    static partial void Welcome3114();
    private static void Welcome7061()
    {
        Console.WriteLine("Enter your name: ");
        string username = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", username);
    }
}
