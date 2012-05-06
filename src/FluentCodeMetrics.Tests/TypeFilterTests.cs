using System;

using SharpTestsEx;
using NUnit.Framework;

using FluentCodeMetrics.Core.TypeFilters;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    public class TypeFilterTests
    {
        [Test]
        [TestCase(typeof(int), true)]
        [TestCase(typeof(string), false)]
        public void EqualsToInt32(Type type, bool expected)
        {
            TypeFilter
                .EqualsTo(typeof (int))
                .Check(type)
                .Should().Be(expected);
        }

        [Test]
        [TestCase(typeof(int), true)]
        [TestCase(typeof(string), true)]
        [TestCase(typeof(DateTime), false)]
        public void EqualsToInt32OrString(Type type, bool expected)
        {
            typeof(int)
                .Or(typeof(string))
                .Check(type)
                .Should().Be(expected);
        }


        [Test]
        [TestCase(typeof(int), false)]
        [TestCase(typeof(string), false)]
        [TestCase(typeof(DateTime), true)]
        public void Not_EqualsToInt32OrString(Type type, bool expected)
        {
            typeof(int)
                .Or(typeof(string))
                .Not()
                .Check(type)
                .Should().Be(expected);
               
        }
    }
}
