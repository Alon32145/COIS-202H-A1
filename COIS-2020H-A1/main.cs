using System;
using Assignment1.Expressions;

class main
{
    static void Main()
    {

        // Display welcome message and instructions
        Console.WriteLine("=== Postfix Expression Converter and Evaluator ===");
        Console.WriteLine("Supported operators: + - * / ^ ( )");
        Console.WriteLine("Enter an infix expression:");

        // Read user input
        string s = Console.ReadLine();

        try
        {
            // Create PostFixExpression instance (converts to postfix automatically)
            var expr = new PostFixExpression(s);

            // Show the postfix representation
            Console.WriteLine("\nPostfix Expression: " + expr.ToString());

            // Evaluate the postfix expression
            var result = expr.Evaluate();

            // Show us the result
            if (result.HasValue)
            {
                Console.WriteLine("Result: " + result.Value);
            }
            else
            {
                Console.WriteLine("Evaluation failed (syntax or runtime error).");
            }
        }
        catch (ArgumentException ex)
        {
            // Catch and show any errors from Analyzer, Convert, or Evaluate
            Console.WriteLine("\nError: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Catch and take notes of any unexpected errors
            Console.WriteLine("\nUnexpected error: " + ex.Message);
        }

        // Wait for the user input before closing
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}

