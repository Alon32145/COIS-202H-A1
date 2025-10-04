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


            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i]; // set the value of c to the current character in the string

                if (char.IsDigit(c)) //if value of c is a digit add it to the numberAccumulator.
                {
                    // Accumulate digit for multi-digit numbers
                    numberAccumulator += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')') //if c is an operator or parenthesis 
                {
                    if (numberAccumulator.Length > 0) // If a number was being accumulated, add it to L
                    {
                        L.Add(int.Parse(numberAccumulator)); // convert the accumulated string to an integer and add it to L
                        numberAccumulator = ""; // reset the accumulator for the next number
                    }
                    L.Add(c);// Add operator to L
                }
                else if (char.IsWhiteSpace(c)) // detects whitespace
                {
                    if (numberAccumulator.Length > 0) // If a number was being accumulated, add it to L
                    {
                        L.Add(int.Parse(numberAccumulator));
                        numberAccumulator = "";
                    }
                    // Ignore whitespace otherwise
                }
                else
                {
                    throw new ArgumentException($"Invalid character '{c}' in expression.");
                }
            }
            if (numberAccumulator.Length > 0)// Add any remaining number at the end
            {
                L.Add(int.Parse(numberAccumulator));
            }

        }


        // Convert: transforms infix list L into postfix using operator stack
        private void Convert()
        {
            
        }

        // Evaluate: computes the result of the postfix expression, question mark means nullable
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
