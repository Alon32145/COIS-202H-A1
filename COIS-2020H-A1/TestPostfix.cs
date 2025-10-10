using Assignment1.Expressions;  
using System;

namespace COIS_2020H_A1
{
    internal class TestPostfix
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Postfix Expression Converter and Evaluator ===");
            Console.WriteLine("Supported operators: + - * / ^ ( )\n");

            // This is where the user should be asked to put the expression
            Console.Write("Enter an infix expression (or press Enter to quit): ");
            string input = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(input))
            {
                try
                {
                    var expr = new PostFixExpression(input);

                    Console.WriteLine($"Postfix form: {expr}");

                    // results should display here
                    var result = expr.Evaluate();
                    Console.WriteLine($"Result: {result}\n");
                }
                catch (Exception ex)
                {
                    // Catching any syntax or runtime errors must happen over here
                    Console.WriteLine($"Error: {ex.Message}\n");
                }

                // Prompting again for another expression
                Console.Write("Enter another infix expression (or press Enter to quit): ");
                input = Console.ReadLine();
            }

            Console.WriteLine("\nTesting completed. Program ended successfully.");
        }
    }
}
