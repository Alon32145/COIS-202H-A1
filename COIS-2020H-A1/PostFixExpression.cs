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


        private int GetPrecedence(char op)
        {
            switch (op)
            {
                case '^':
                    return 3;
                case '*':
                case '/':
                    return 2;
                case '+':
                case '-':
                    return 1;
                default:
                    return 0;
            }
        }

        private bool IsRightAssociative(char op)
        {
            return op == '^';
        }

        // Convert: transforms infix list L into postfix using operator stack
        private void Convert()
        {
            Stack<char> operatorStack = new Stack<char>();
            List<object> postfixList = new List<object>();

            foreach (object item in L)
            {
                if (item is int)
                {
                    postfixList.Add(item);
                }
                else if (item is char)
                {
                    char c = (char)item;

                    if (c == '(')
                    {
                        operatorStack.Push(c);
                    }
                    else if (c == ')')
                    {
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
                        if (!foundLeftParen)
                        {
                            throw new ArgumentException("Unbalanced parentheses: missing left parenthesis.");
                        }
                    }
                    else
                    {
                        while (operatorStack.Count > 0)
                        {
                            char top = operatorStack.Peek();
                            if (top == '(')
                            {
                                break;
                            }
                            int topPrecedence = GetPrecedence(top);
                            int currentPrecedence = GetPrecedence(c);

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
                        operatorStack.Push(c);
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                char top = operatorStack.Pop();
                if (top == '(')
                {
                    throw new ArgumentException("Unbalanced parentheses: missing right parenthesis.");
                }
                postfixList.Add(top);
            }

            L = postfixList;
        }

        // Evaluate: computes the result of the postfix expression, question mark means nullable
        public float? Evaluate()
        {

            Stack<float> operandStack = new Stack<float>();

            foreach (object item in L)
            {
                if (item is int)
                {
                    operandStack.Push((int)item);
                }
                else if (item is char)
                {
                    char op = (char)item;

                    if (operandStack.Count < 2)
                    {
                        throw new ArgumentException("Syntax error: too few operands for operator.");
                    }

                    float operand2 = operandStack.Pop();
                    float operand1 = operandStack.Pop();
                    float result;

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
                            if (operand2 == 0)
                            {
                                throw new ArgumentException("Runtime error: division by zero.");
                            }
                            result = operand1 / operand2;
                            break;
                        case '^':
                            result = (float)Math.Pow(operand1, operand2);
                            break;
                        default:
                            throw new ArgumentException($"Unknown operator: {op}");
                    }

                    operandStack.Push(result);
                }
            }

            if (operandStack.Count == 0)
            {
                throw new ArgumentException("Syntax error: too many operators, not enough operands.");
            }
            else if (operandStack.Count > 1)
            {
                throw new ArgumentException("Syntax error: too many operands, not enough operators.");
            }

            return operandStack.Pop();
        }

        // ToString: returns the postfix expression as a string
        public override string ToString()
        {
            
            return string.Join(" ", L);
        }
    }
}
