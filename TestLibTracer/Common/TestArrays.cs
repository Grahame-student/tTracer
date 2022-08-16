using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestLibTracer.Common
{
    public class TestArrays
    {
        [Test]
        public void AddingArrays_Creates_NewArray()
        {
            int[] first = Array.Empty<int>();
            int[] second = Array.Empty<int>();

            IEnumerable<int> third = first.Concat(second);

            Assert.That(ReferenceEquals(third, first), Is.False);
            Assert.That(ReferenceEquals(third, second), Is.False);
        }

        [Test]
        public void AddingArrays_Concatentates_Arrays()
        {
            int[] first = { 1, 2 };
            int[] second = { 3, 4 };


            IEnumerable<int> third = first.Concat(second);

            int[] expected = { 1, 2, 3, 4 };
            Assert.That(third, Is.EqualTo(expected));
        }
    }
}
