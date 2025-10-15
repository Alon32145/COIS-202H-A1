// Asssignment 1 COIS 2020H
// Group Members:
// Alon Raigorodetsky ID: 0827093    Email: alonraigorodetsky@trentu.ca
// Edidiong Jairus    ID: 0866074    Email: Edidiongjairus@trentu.ca



using Assignment1.Expressions;
using System;
using System.Collections.Generic;

namespace COIS_2020H_A1
{
    internal class main
    {
        static void Main(string[] args)
        {
            // List to store MyString instances in alphabetical order
            List<MyString> myStrings = new List<MyString>();
            bool running = true;

            // Main menu loop
            while (running)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Postfix Expression Converter and Evaluator");
                Console.WriteLine("2. MyString Operations");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option (1-3): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // (Unchanged: Postfix Expression code)
                        Console.WriteLine("\nSupported operators: + - * / ^ ( )\n");
                        Console.Write("Enter an infix expression (or press Enter to return to menu): ");
                        string input = Console.ReadLine();

                        while (!string.IsNullOrWhiteSpace(input))
                        {
                            try
                            {
                                var expr = new PostFixExpression(input);
                                Console.WriteLine($"Postfix form: {expr}");
                                var result = expr.Evaluate();
                                Console.WriteLine($"Result: {result}\n");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}\n");
                            }

                            Console.Write("Enter another infix expression (or press Enter to return to menu): ");
                            input = Console.ReadLine();
                        }
                        break;

                    case "2":
                        // === MyString Operations Menu ===
                        bool inMyStringMenu = true;
                        while (inMyStringMenu)
                        {
                            Console.WriteLine("\n=== MyString Operations ===");
                            Console.WriteLine("1. Create new MyString");
                            Console.WriteLine("2. Print all MyStrings");
                            Console.WriteLine("3. Clone a MyString");
                            Console.WriteLine("4. Compare two MyStrings");
                            Console.WriteLine("5. Find index of character in MyString");
                            Console.WriteLine("6. Remove character from MyString");
                            Console.WriteLine("7. Check equality of two MyStrings");
                            Console.WriteLine("8. Return to Main Menu");
                            Console.Write("Select an option (1-8): ");
                            string option = Console.ReadLine();

                            switch (option)
                            {
                                case "1":
                                    // Create and insert a new MyString in alphabetical order which is optional
                                    Console.Write("Enter a string: ");
                                    string str = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        var myStr = new MyString(str.ToCharArray());
                                        int insertIndex = myStrings.BinarySearch(myStr);
                                        if (insertIndex < 0) insertIndex = ~insertIndex;
                                        myStrings.Insert(insertIndex, myStr);
                                        Console.WriteLine($"MyString added at position {insertIndex}.");
                                    }
                                    break;

                                case "2":
                                    // Print all MyString instances
                                    if (myStrings.Count == 0)
                                    {
                                        Console.WriteLine("No MyString instances available.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Current MyStrings:");
                                        for (int i = 0; i < myStrings.Count; i++)
                                        {
                                            Console.Write($"{i}: ");
                                            myStrings[i].Print();
                                        }
                                    }
                                    break;

                                case "3":
                                    // Clone a MyString and insert in order
                                    if (myStrings.Count == 0)
                                    {
                                        Console.WriteLine("No MyString instances to clone.");
                                        break;
                                    }
                                    Console.Write("Enter the index of the MyString to clone: ");
                                    if (int.TryParse(Console.ReadLine(), out int cloneIdx) && cloneIdx >= 0 && cloneIdx < myStrings.Count)
                                    {
                                        var clone = (MyString)myStrings[cloneIdx].Clone();
                                        int insertIndex = myStrings.BinarySearch(clone);
                                        if (insertIndex < 0) insertIndex = ~insertIndex;
                                        myStrings.Insert(insertIndex, clone);
                                        Console.WriteLine($"Cloned MyString added at position {insertIndex}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid index.");
                                    }
                                    break;

                                case "4":
                                    // Compare two MyStrings
                                    if (myStrings.Count < 2)
                                    {
                                        Console.WriteLine("Need at least two MyStrings to compare.");
                                        break;
                                    }
                                    Console.Write("Enter the index of the first MyString: ");
                                    int idx1 = GetIndex(myStrings.Count);
                                    Console.Write("Enter the index of the second MyString: ");
                                    int idx2 = GetIndex(myStrings.Count);
                                    if (idx1 != -1 && idx2 != -1)
                                    {
                                        int cmp = myStrings[idx1].CompareTo(myStrings[idx2]);
                                        if (cmp < 0)
                                            Console.WriteLine("First MyString comes before the second.");
                                        else if (cmp > 0)
                                            Console.WriteLine("First MyString comes after the second.");
                                        else
                                            Console.WriteLine("The two MyStrings are equal.");
                                    }
                                    break;

                                case "5":
                                    // Find index of character in MyString
                                    if (myStrings.Count == 0)
                                    {
                                        Console.WriteLine("No MyString instances available.");
                                        break;
                                    }
                                    Console.Write("Enter the index of the MyString: ");
                                    int idx = GetIndex(myStrings.Count);
                                    if (idx != -1)
                                    {
                                        Console.Write("Enter the character to search for: ");
                                        string ch = Console.ReadLine();
                                        if (!string.IsNullOrEmpty(ch))
                                        {
                                            int foundIdx = myStrings[idx].IndexOf(ch[0]);
                                            if (foundIdx != -1)
                                                Console.WriteLine($"Character '{ch[0]}' found at index {foundIdx}.");
                                            else
                                                Console.WriteLine($"Character '{ch[0]}' not found.");
                                        }
                                    }
                                    break;

                                case "6":
                                    // Remove character from MyString
                                    if (myStrings.Count == 0)
                                    {
                                        Console.WriteLine("No MyString instances available.");
                                        break;
                                    }
                                    Console.Write("Enter the index of the MyString: ");
                                    int remIdx = GetIndex(myStrings.Count);
                                    if (remIdx != -1)
                                    {
                                        Console.Write("Enter the character to remove: ");
                                        string ch = Console.ReadLine();
                                        if (!string.IsNullOrEmpty(ch))
                                        {
                                            myStrings[remIdx].Remove(ch[0]);
                                            Console.WriteLine($"All occurrences of '{ch[0]}' removed.");
                                        }
                                    }
                                    break;

                                case "7":
                                    // Check equality of two MyStrings
                                    if (myStrings.Count < 2)
                                    {
                                        Console.WriteLine("Need at least two MyStrings to check equality.");
                                        break;
                                    }
                                    Console.Write("Enter the index of the first MyString: ");
                                    int eqIdx1 = GetIndex(myStrings.Count);
                                    Console.Write("Enter the index of the second MyString: ");
                                    int eqIdx2 = GetIndex(myStrings.Count);
                                    if (eqIdx1 != -1 && eqIdx2 != -1)
                                    {
                                        bool equal = myStrings[eqIdx1].Equals(myStrings[eqIdx2]);
                                        Console.WriteLine(equal ? "The two MyStrings are equal." : "The two MyStrings are not equal.");
                                    }
                                    break;

                                case "8":
                                    // Return to main menu
                                    inMyStringMenu = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please select 1-8.");
                                    break;
                            }
                        }
                        break;

                    case "3":
                        running = false;
                        Console.WriteLine("\nProgram ended successfully.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        break;
                }
            }
        }

      
        private static int GetIndex(int count) // check that a valid integer is inserted.
        {
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < count)
                return idx;
            Console.WriteLine("Invalid index.");
            return -1;
        }
    }
}
