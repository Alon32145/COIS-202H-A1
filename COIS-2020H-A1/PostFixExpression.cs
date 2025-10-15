// Asssignment 1 COIS 2020H
// Group Members:
// Alon Raigorodetsky ID: 0827093    Email: alonraigorodetsky@trentu.ca
// Edidiong Jairus    ID: 0866074    Email: Edidiongjairus@trentu.ca




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
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')' || c == '^') //if c is an operator or parenthesis 
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
                        numberAccumulator = ""; // reset the accumulator for the next number
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


        // Returns the precedence level of an operator.
        // Higher numbers indicate higher precedence.
        private int GetPrecedence(char op)
        {
            switch (op)
            {
                case '^':
                    return 3; // Highest precedence (exponentiation)
                case '*':
                case '/':
                    return 2; // Medium precedence (multiplication/division)
                case '+':
                case '-':
                    return 1; // Lowest precedence (addition/subtraction)
                default:
                    return 0; // No precedence
            }
        }
       
        // Helper method: Checks if an operator is right-associative.
        // Only exponentiation (^) is right-associative; all others are left-associative. 
        private bool IsRightAssociative(char op)
        {
            return op == '^'; // Only exponentiation is right-associative
        }

        // Convert: transforms infix list L into postfix using operator stack and rules of precedence.
        private void Convert()
        {
            // Stack to temporarily hold operators during conversion
            Stack<char> operatorStack = new Stack<char>();

            // List to build the postfix expression
            List<object> postfixList = new List<object>();

            // Process each item in the infix expression
            foreach (object item in L)
            {
                // If item is a number (operand), add directly to output
                if (item is int)
                {
                    postfixList.Add(item);
                }
                // If item is a character (operator or parenthesis)
                else if (item is char)
                {
                    char c = (char)item;

                    if (c == '(')
                    {
                        // Left parenthesis: push onto stack
                        operatorStack.Push(c);
                    }
                    else if (c == ')')
                    {
                        // Right parenthesis: pop operators until matching left parenthesis
                        bool foundLeftParen = false;

                        while (operatorStack.Count > 0)
                        {
                            char top = operatorStack.Pop();

                            if (top == '(')
                            {
                                foundLeftParen = true;
                                break;
                            }

                            postfixList.Add(top);
                        }

                        // Check for balanced parentheses
                        if (!foundLeftParen)
                        {
                            throw new ArgumentException("Unbalanced parentheses: missing left parenthesis.");
                        }
                    }
                    else
                    {
                        // Operator: pop operators based on precedence and associativity
                        while (operatorStack.Count > 0)
                        {
                            char top = operatorStack.Peek();

                            // Don't pop left parenthesis
                            if (top == '(')
                            {
                                break;
                            }

                            int topPrecedence = GetPrecedence(top);
                            int currentPrecedence = GetPrecedence(c);

                            // Pop if: top has higher precedence, OR
                            // top has equal precedence and current is left-associative
                            if (topPrecedence > currentPrecedence ||
                                (topPrecedence == currentPrecedence && !IsRightAssociative(c)))
                            {
                                postfixList.Add(operatorStack.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }

                        // Push current operator onto stack
                        operatorStack.Push(c);
                    }
                }
            }

            // Pop remaining operators from stack to output
            while (operatorStack.Count > 0)
            {
                char top = operatorStack.Pop();

                // Check for unbalanced left parentheses
                if (top == '(')
                {
                    throw new ArgumentException("Unbalanced parentheses: missing right parenthesis.");
                }

                postfixList.Add(top);
            }

            // Replace L with the postfix expression
            L = postfixList;
        }

        // Evaluate: computes the result of the postfix expression, question mark means nullable
        public float? Evaluate()
        {
            // Stack to hold operands during evaluation
            Stack<float> operandStack = new Stack<float>();

            // Process each item in the postfix expression
            foreach (object item in L)
            {
                if (item is int)
                {
                    // Operand: push onto stack
                    operandStack.Push((int)item);
                }
                else if (item is char)
                {
                    char op = (char)item;

                    // Check if we have enough operands for the operator
                    if (operandStack.Count < 2)
                    {
                        throw new ArgumentException("Syntax error: too few operands for operator.");
                    }

                    // Pop two operands (note: order matters for - and /)
                    float operand2 = operandStack.Pop(); // Second operand (right)
                    float operand1 = operandStack.Pop(); // First operand (left)

                    float result;

                    // Perform the operation
                    switch (op)
                    {
                        case '+':
                            result = operand1 + operand2;
                            break;
                        case '-':
                            result = operand1 - operand2;
                            break;
                        case '*':
                            result = operand1 * operand2;
                            break;
                        case '/':
                            // Runtime check for division by zero
                            if (operand2 == 0)
                            {
                                throw new ArgumentException("Runtime error: division by zero.");
                            }
                            result = operand1 / operand2;
                            break;
                        case '^':
                            // Exponentiation (power)
                            result = (float)Math.Pow(operand1, operand2);
                            break;
                        default:
                            throw new ArgumentException($"Unknown operator: {op}");
                    }

                    // Push result back onto stack
                    operandStack.Push(result);
                }
            }

            // Final syntax check: should have exactly one value left
            if (operandStack.Count == 0)
            {
                throw new ArgumentException("Syntax error: too many operators, not enough operands.");
            }
            else if (operandStack.Count > 1)
            {
                throw new ArgumentException("Syntax error: too many operands, not enough operators.");
            }

            // Return the final result
            return operandStack.Pop();
        }

        // ToString: returns the postfix expression as a string
        public override string ToString()
        {
            
            return string.Join(" ", L);
        }
    }
}
