using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestLibTracer
{
    public class TestArrays
    {
        [Test]
        public void AddingArrays_Creates_NewArray()
        {
            Int32[] first = Array.Empty<Int32>();
            Int32[] second = Array.Empty<Int32>();

            IEnumerable<Int32> third = first.Concat(second);

            Assert.That(ReferenceEquals(third, first), Is.False);
            Assert.That(ReferenceEquals(third, second), Is.False);
        }

        [Test]
        public void AddingArrays_Concatentates_Arrays()
        {
            Int32[] first = { 1, 2 };
            Int32[] second = { 3, 4 };


            IEnumerable<Int32> third = first.Concat(second);

            Int32[] expected = { 1, 2, 3, 4 };
            Assert.That(third, Is.EqualTo(expected));
        }
    }
}
