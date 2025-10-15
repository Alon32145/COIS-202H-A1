using System;

namespace COIS_2020H_A1
{
    // MyString class: Implements a string as a singly linked list of characters
    public class MyString : ICloneable, IComparable
    {
        // Node class: Represents a single character in the linked list
        private class Node
        {
            public char item;   // The character stored in this node
            public Node next;   // Reference to the next node in the list

            // Node constructor
            public Node(char item, Node next = null)
            {
                this.item = item;   // Set the character
                this.next = next;   // Set the next node (default is null)
            }
        }

        private Node front;   // Reference to the first node in the list
        private int length;   // Number of characters in the string

        // Constructor: Initializes MyString from a character array
        public MyString(char[] A)
        {
            front = null;     // Start with an empty list
            length = 0;       // No characters yet
            Node tail = null; // Used to keep track of the last node

            // Loop through each character in the array
            foreach (char c in A)
            {
                Node newNode = new Node(c); // Create a new node for the character
                if (front == null)
                {
                    // If the list is empty, set front and tail to the new node
                    front = newNode;
                    tail = newNode;
                }
                else
                {
                    // Otherwise, add the new node to the end and update tail
                    tail.next = newNode;
                    tail = newNode;
                }
                length++; // Increment the length for each character added
            }
        }

        // Clone: Creates and returns a copy of the current MyString
        public object Clone()
        {
            if (front == null)
                return new MyString(new char[0]); // Return an empty MyString if original is empty

            char[] chars = new char[length]; // Array to hold the characters
            Node current = front;            // Start at the front of the list
            int i = 0;
            while (current != null)
            {
                chars[i++] = current.item;   // Copy each character to the array
                current = current.next;      // Move to the next node
            }
            return new MyString(chars);      // Create a new MyString from the array
        }

        // CompareTo: Compares this MyString to another object
        public int CompareTo(object obj)
        {
            if (obj == null) return 1; // Any string is greater than null
            if (!(obj is MyString other))
                throw new ArgumentException("Object is not a MyString");

            Node a = this.front; // Pointer for this string
            Node b = other.front; // Pointer for the other string

            // Compare character by character
            while (a != null && b != null)
            {
                if (a.item < b.item) // This string comes before
                {
                    return -1;
                } 

                if (a.item > b.item)// This string comes after
                {
                    return 1;
                }  
                a = a.next; // Move to next character
                b = b.next;
            }
            
            if (a == null && b == null) // If both strings end at the same time, they are equal
            {
                return 0;
            }
           
            if (a == null)  // If this string is shorter, it comes before
            { 
                return -1;
            }
            // If the other string is shorter, this comes after
            return 1;
        }

        // IndexOf: Returns the index of the first occurrence of c, or -1 if not found
        public int IndexOf(char c)
        {
            Node current = front; // Start at the front
            int index = 0;        // Index counter
            while (current != null)
            {
                if (current.item == c)
                    return index; // Found the character, return its index
                current = current.next; // Move to next node
                index++;
            }
            return -1; // Character not found
        }

        // Remove: Removes all occurrences of character c from the string
        public void Remove(char c)
        {
            // Remove from the front as long as the front node matches c
            while (front != null && front.item == c)
            {
                front = front.next; // Move front to the next node
                length--;           // Decrement length
            }
            // Remove from the rest of the list
            Node current = front;
            while (current != null && current.next != null)
            {
                if (current.next.item == c)
                {
                    // Skip the node with the matching character
                    current.next = current.next.next;
                    length--;
                }
                else
                {
                    current = current.next; // Move to next node
                }
            }
        }

        // Equals: Returns true if obj is a MyString and is equal to this instance
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MyString)) // Not equal if obj is null or not a MyString
            { 
                return false;
            } 
            return this.CompareTo(obj) == 0; // Use CompareTo for equality
        }

        // Print: Outputs the string to the console
        public void Print()
        {
            Node current = front; // Start at the front
            while (current != null)
            {
                Console.Write(current.item); // Print each character
                current = current.next;      // Move to next node
            }
            Console.WriteLine(); // Newline at the end
        }
    }
}
