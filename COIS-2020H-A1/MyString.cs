using System;

namespace COIS_2020H_A1
{
    public class MyString : ICloneable, IComparable
    {
        private class Node
        {
            public char item;
            public Node next;

            public Node(char item, Node next = null)
            {
                this.item = item;
                this.next = next;
            }
        }

        private Node front; // Reference to the first (header) node
        private int length; // Number of characters

        // Initialize an instance of MyString based on the given character array A (4 marks)
        public MyString(char[] A)
        {
            front = null;
            length = 0;
            Node tail = null;
            foreach (char c in A)
            {
                Node newNode = new Node(c);
                if (front == null)
                {
                    front = newNode;
                    tail = newNode;
                }
                else
                {
                    tail.next = newNode;
                    tail = newNode;
                }
                length++;
            }
        }

        // Create and return a clone of the current instance (5 marks)
        public object Clone()
        {
            if (front == null)
                return new MyString(new char[0]);

            char[] chars = new char[length];
            Node current = front;
            int i = 0;
            while (current != null)
            {
                chars[i++] = current.item;
                current = current.next;
            }
            return new MyString(chars);
        }

        // Compare the current instance of MyString with obj and return a -1, 0 or +1  
        // if the current string comes before, at or after obj in alphabetical order (5 marks)
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (!(obj is MyString other))
                throw new ArgumentException("Object is not a MyString");

            Node a = this.front;
            Node b = other.front;
            while (a != null && b != null)
            {
                if (a.item < b.item) return -1;
                if (a.item > b.item) return 1;
                a = a.next;
                b = b.next;
            }
            if (a == null && b == null) return 0;
            if (a == null) return -1;
            return 1;
        }

        // Return the index of the first occurrence of c; otherwise return -1 (2 marks)
        public int IndexOf(char c)
        {
            Node current = front;
            int index = 0;
            while (current != null)
            {
                if (current.item == c)
                    return index;
                current = current.next;
                index++;
            }
            return -1;
        }

        // Remove all occurrences of c (4 marks)
        public void Remove(char c)
        {
            // Remove from the front
            while (front != null && front.item == c)
            {
                front = front.next;
                length--;
            }
            // Remove from the rest
            Node current = front;
            while (current != null && current.next != null)
            {
                if (current.next.item == c)
                {
                    current.next = current.next.next;
                    length--;
                }
                else
                {
                    current = current.next;
                }
            }
        }

        // Return true if obj is both of type MyString and the same as the current instance;  
        // otherwise return false (2 marks)
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MyString))
                return false;
            return this.CompareTo(obj) == 0;
        }

        // Print the current instance of MyString (2 marks)
        public void Print()
        {
            Node current = front;
            while (current != null)
            {
                Console.Write(current.item);
                current = current.next;
            }
            Console.WriteLine();
        }
    }
}
