using System;
using Assignment1.Expressions;

class main
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter an infix expression:");
        string input = Console.ReadLine();

        try
        {
            var expr = new PostFixExpression(input);
            Console.WriteLine("Postfix Expression: " + expr.ToString());
            var result = expr.Evaluate();
            if (result.HasValue)
                Console.WriteLine("Result: " + result.Value);
            else
                Console.WriteLine("Evaluation failed (syntax or runtime error).");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
