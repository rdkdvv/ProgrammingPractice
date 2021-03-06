﻿using System;
using System.Diagnostics;

namespace DA.List
{
    public class LinkedList<T>
    {
        /// <summary>
        /// Handle a value and a link to the next element in list of type T.
        /// </summary>
        public class Node<T>
        {
            /// <summary>
            /// Value of the current node.
            /// </summary>
            internal T Value { get; set; }

            /// <summary>
            /// Link to next node.
            /// </summary>
            internal Node<T> Next { get; set; }

            public Node (T value, Node<T> next)
            {
                Value = value;
                Next = next;
            }

            public Node (T value)
            {
                Value = value;
                Next = null;
            }

            public Node () : this (default (T)) { }
        }


        public LinkedList ()
        {
            Head = null;
        }

        #region Properties

        private Node<T> Head { get; set; }

        /// <summary>
        /// Total number of elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Check if the list is empty of not.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Return first element.
        /// </summary>
        public Node<T> FirstElement { get { return Head; } }

        /// <summary>
        /// Return last element.
        /// </summary>
        public Node<T> LastElement { get { return GetLast (); } }

        public T this[int index]
        {
            get { return GetNodeAt (index).Value; }
            set { GetNodeAt (index).Value = value; }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Get the last node of a list.
        /// </summary>
        private Node<T> GetLast ()
        {
            if (Head == null)
                return null;

            Node<T> temp = Head;
            while (temp.Next != null)
                temp = temp.Next;
            return temp;
        }

        /// <summary>
        /// Return element from index position.
        /// </summary>
        private Node<T> GetNodeAt (int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException ();

            if (Head == null)
                return null;

            Node<T> current = Head;
            int count = 0;

            while (current != null)
            {
                if (count == index)
                    return current;
                current = current.Next;
                ++count;
            }

            return null;
        }

        /// <summary>
        /// Return a first node that has the particular value.
        /// </summary>
        private Node<T> GetNode (T value)
        {
            if (Head.Value.Equals (value))
                return Head;

            Node<T> temp = Head;
            while (!temp.Value.Equals (value) && temp != null)
            {
                temp = temp.Next;
            }

            return temp;
        }

        #endregion

        #region Add Methods

        /// <summary>
        /// Insert value at front of list.
        /// </summary>
        public void AddFront (T value)
        {
            Node<T> node = new Node<T> (value);

            if (Head == null)
            {
                Head = node;
            }
            else
            {
                node.Next = Head;
                Head = node;
            }

            ++Count;
        }

        /// <summary>
        /// Add value to end of list.
        /// </summary>
        public void AddLast (T value)
        {
            Node<T> node = new Node<T> (value);
            if (Head == null)
            {
                Head = node;
            }
            else
            {
                GetLast ().Next = node;
            }

            ++Count;
        }

        /// <summary>
        /// Insert an element after index element.
        /// </summary>
        public void AddAfter (T value, int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException ();

            Node<T> node = new Node<T> (value);
            Node<T> temp = GetNodeAt (index);

            node.Next = temp.Next;
            temp.Next = node;

            ++Count;
        }

        #endregion

        #region Remove Methods

        /// <summary>
        /// Remove value from the list.
        /// </summary>
        public bool Remove (T value)
        {
            Node<T> previous = null;
            Node<T> current = Head;

            if (current != null && current.Value.Equals (value))
            {
                Head = Head.Next;
                --Count;
                return true;
            }

            while (current.Next != null && !current.Value.Equals (value))
            {
                previous = current;
                current = current.Next;
            }

            if (current == null)
            {
                return false;
            }

            previous.Next = current.Next;
            --Count;
            return true;
        }

        /// <summary>
        /// Remove element in the list at the index position.
        /// </summary>
        public bool Remove (int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException ();

            Node<T> temp = GetNodeAt (index - 1);

            if (temp == null)
                return false;

            temp.Next = temp.Next.Next;
            --Count;
            return true;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Reverse the list.
        /// </summary>
        public void Reverse ()
        {
            Node<T> previous = null;
            Node<T> current = Head;
            Node<T> next;

            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            Head = previous;
        }

        /// <summary>
        /// Reverse list via recursion
        /// </summary>
        public void ReverseRecursively ()
        {
            Head = ReverseRecursively (Head, null);
        }

        /// <summary>
        /// Revers recursively implementation
        /// </summary>
        private Node<T> ReverseRecursively (Node<T> nextNode, Node<T> previousNode)
        {
            if (nextNode == null)
            {
                return null;
            }

            if (nextNode.Next == null)
            {
                nextNode.Next = previousNode;
                return nextNode;
            }

            Node<T> returnNode = ReverseRecursively (nextNode.Next, nextNode);
            nextNode.Next = previousNode;
            return returnNode;
        }

        /// <summary>
        /// If list is cycled return true, otherwise return false.
        /// <para>Time Complexity - BigO(n)</para>
        /// </summary>
        public bool IsCycled ()
        {
            Node<T> slow = Head;
            Node<T> fast = Head.Next;

            while (true)
            {
                if (fast == null || fast.Next == null)
                {
                    return false;
                }
                else if (fast == slow || fast.Next == slow)
                {
                    return true;
                }
                else
                {
                    slow = slow.Next;
                    fast = fast.Next.Next;
                }
            }
        }

        /// <summary>
        /// Detect if there is a loop in the linked list.
        /// <para>Time Complexity - O(n)</para>
        /// </summary>
        /// 
        /// <param name="list"></param>
        /// 
        /// <returns>
        /// Returns true if loop is detected, otherwise return false.
        /// </returns>
        public static bool LoopDetect (LinkedList<T> list)
        {
            if (list == null)
            {
                return false;
            }

            Node<T> slowPointer;
            Node<T> fastPointer;

            slowPointer = fastPointer = list.Head;
            while (fastPointer.Next != null && fastPointer.Next.Next != null)
            {
                slowPointer = slowPointer.Next;
                fastPointer = fastPointer.Next.Next;
                if (slowPointer == fastPointer)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Delete loop from list if so exist.
        /// </summary>
        public static void RemoveLoop (LinkedList<T> list)
        {
            Node<T> loopPoint = LoopPointDetect (list);
            if (list == null) return;

            Node<T> firstPointer = list.Head;
            if (loopPoint == list.Head)
            {
                while (firstPointer.Next != list.Head)
                {
                    firstPointer = firstPointer.Next;
                }
                firstPointer.Next = null;
                return;
            }

            Node<T> secondPointer = loopPoint;
            while (firstPointer.Next != secondPointer.Next)
            {
                firstPointer = firstPointer.Next;
                secondPointer = secondPointer.Next;
            }
            secondPointer.Next = null;
        }

        /// <summary>
        /// Find point of loop.
        /// </summary>
        /// 
        /// <returns>
        /// Returns reference to looped value.
        /// </returns>
        private static Node<T> LoopPointDetect (LinkedList<T> list)
        {
            Node<T> slowPointer;
            Node<T> fastPointer;

            slowPointer = fastPointer = list.Head;
            while (fastPointer.Next != null && fastPointer.Next.Next != null)
            {
                slowPointer = slowPointer.Next;
                fastPointer = fastPointer.Next.Next;
                if (slowPointer == fastPointer)
                {
                    return slowPointer;
                }
            }
            return null;
        }

        /// <summary>
        /// Find intersection point in given two lists.
        /// </summary>
        /// 
        /// <returns>
        /// Returns intersection value or null.
        /// </returns>
        public static T FindIntersactionValue (LinkedList<T> first, LinkedList<T> second)
        {
            Node<T> firstHead = first.Head;
            Node<T> secondHead = second.Head;

            int firstCount = 0;
            int secondCount = 0;

            Node<T> tempFirstHead = firstHead;
            Node<T> tempSecondHead = secondHead;

            while (tempFirstHead != null)
            {
                ++firstCount;
                tempFirstHead = tempFirstHead.Next;
            }

            while (tempSecondHead != null)
            {
                ++secondCount;
                tempSecondHead = tempSecondHead.Next;
            }

            int difference;

            if (firstCount < secondCount)
            {
                Node<T> temp = firstHead;
                firstHead = secondHead;
                secondHead = temp;
                difference = secondCount - firstCount;
            }
            else
            {
                difference = firstCount - secondCount;
            }

            for (; difference > 0; difference--)
            {
                firstHead = firstHead.Next;
            }

            while (firstHead != secondHead)
            {
                firstHead = firstHead.Next;
                secondHead = secondHead.Next;
            }

            return firstHead.Value;
        }

        #endregion
    }
}
