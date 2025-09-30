using System;
using System.Collections.Generic;

namespace Assignment1.Expressions
{
    public class PostFixExpression
    {
        private List<object> L;

        // Constructor: creates a list of L objects calls Analyzer
        // and Convert to preform initialization of L.
        public PostFixExpression(string s)
        {
            Analyzer(s);
            Convert();
        }

        // Analyzer: parses input string into operands and operators
        private void Analyzer(string s)
        {
            // Initialize the list L to store operands and operators
            L = new List<object>();

            //Initialize a variable to accumulate digits for multi-digit numbers
            string numberAccumulator = "";


            
        }


        // Convert: transforms infix list L into postfix using operator stack
        private void Convert()
        {
            
        }

        // Evaluate: computes the result of the postfix expression
        public float? Evaluate()
        {
            
            return null;
        }

        // ToString: returns the postfix expression as a string
        public override string ToString()
        {
            
            return string.Join(" ", L);
        }
    }
}
