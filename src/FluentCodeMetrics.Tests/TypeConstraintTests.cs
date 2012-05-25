using System;
using Samples;
using SharpTestsEx;
using NUnit.Framework;

using FluentCodeMetrics.Core.TypeConstraints;
using FluentCodeMetrics.Core;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    public class TypeConstraintTests
    {
        [Test]
        [TestCase(typeof(int), true)]
        [TestCase(typeof(string), false)]
        public void EqualsToInt32(Type type, bool expected)
        {
            TypeConstraint
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

        [Test]
        [TestCase(typeof(int), false)]
        [TestCase(typeof(string), false)]
        [TestCase(typeof(DateTime), true)]
        public void NotEqualsToInt32AndNotEqualsToString(Type type, bool expected)
        {
            typeof(int).Not()
                .And(typeof(string).Not())
                .Check(type)
                .Should().Be(expected);

        }

        [Test]
        [TestCase(typeof(ClassDependsOnASubClass), typeof(ClassDependsOnASubClass.SubClass))]
        public void NotNestedTypes(Type declaringType, Type nestedType)
        {
            declaringType.NestedTypes()
                .Check(nestedType)
                .Should()
                .Be(true);
        }
    }
}
