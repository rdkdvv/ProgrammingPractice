﻿using DA.List;
using NUnit.Framework;
using System;

namespace DA.UnitTests.DataStructures
{
    [TestFixture]
    public class SortedLinkedListTest
    {
        SortedLinkedList<int> integerList;

        [SetUp]
        public void Setup ()
        {
            integerList = new SortedLinkedList<int> ();
        }

        [Test]
        public void Insert_WhenCalled_InsertElementAtListInSortedOrder ()
        {
            integerList.Insert (4);
            integerList.Insert (3);
            integerList.Insert (2);
            integerList.Insert (1);
            integerList.Insert (41);

            Assert.AreEqual (5, integerList.Count);
            Assert.AreEqual (1, integerList[0]);
            Assert.AreEqual (2, integerList[1]);
            Assert.AreEqual (3, integerList[2]);
            Assert.AreEqual (4, integerList[3]);
            Assert.AreEqual (41, integerList[4]);
        }

        [Test]
        public void Remove_WhenCalled_RemoveElementFromList ()
        {
            integerList.Insert (4);
            integerList.Insert (3);

            Assert.AreEqual (2, integerList.Count);
            Assert.AreEqual (3, integerList[0]);
            Assert.AreEqual (4, integerList[1]);

            integerList.Remove (3);
            Assert.AreEqual (1, integerList.Count);
            Assert.AreEqual (4, integerList[0]);
        }

        [Test]
        public void IsExist_ElementIsFounded_ReturnTrue ()
        {
            integerList.Insert (4);
            integerList.Insert (3);
            integerList.Insert (2);

            Assert.IsTrue (integerList.IsExist(4));
            Assert.IsTrue (integerList.IsExist(3));
            Assert.IsTrue (integerList.IsExist(2));
        }

        [Test]
        public void IsExist_ElementIsNotFounded_ReturnFalse ()
        {
            integerList.Insert (4);
            integerList.Insert (3);
            integerList.Insert (2);

            Assert.IsFalse (integerList.IsExist (5));
        }

        [Test]
        public void RemoveFirst_ElementExists_DeleteElement ()
        {
            integerList.Insert (1);
            integerList.Insert (2);
            integerList.Insert (3);

            integerList.RemoveFirst ();
            Assert.AreEqual (2, integerList.Count);
            Assert.AreEqual (2, integerList[0]);
        }

        [Test]
        public void RemoveFirst_ListIsEmpty_ThrowInvalidOperationException ()
        {
            Assert.Throws<System.InvalidOperationException> (() => integerList.RemoveFirst ());
        }

        [Test]
        public void RemoveLast_ElementExists_DeleteElement ()
        {
            integerList.Insert (1);
            integerList.Insert (2);
            integerList.Insert (3);

            integerList.RemoveLast ();
            Assert.AreEqual (2, integerList.Count);
            Assert.AreEqual (2, integerList[1]);
        }

        [Test]
        public void RemoveLast_ListIsEmpty_ThrowInvalidOperationException ()
        {
            Assert.Throws<System.InvalidOperationException> (() => integerList.RemoveLast ());
        }
    }
}