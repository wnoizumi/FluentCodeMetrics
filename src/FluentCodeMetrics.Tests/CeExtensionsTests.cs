using System;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using SharpTestsEx;
using FluentCodeMetrics.Core;
using Samples;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class CeExtensionsTests
    {
        private readonly List<Type> common = new List<Type>()
        {
            typeof (System.Runtime.TargetedPatchingOptOutAttribute),
            typeof (System.Security.SecuritySafeCriticalAttribute),
            typeof (System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
            typeof (object),
            typeof (string),
            typeof (bool),
            typeof (int),
            typeof (Type)
        };

        [Test]
        public void GetReferencedTypes_EmptyClass()
        {
            typeof(EmptyClass).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common
                );
        }

        [Test]
        public void GetReferencedTypes_SingleField()
        {
            typeof(SingleField).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_SingleProperty()
        {
            typeof(SingleProperty).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime),
                    typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_SingleNonAutoProperty()
        {
            typeof(SingleNonAutoProperty).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_FeeMethod()
        {
            typeof(FeeMethod).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_SingleArgVoidMethod()
        {
            typeof(SingleArgVoidMethod).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_SingleArgCtor()
        {
            typeof(SingleArgCtor).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_ExceptionRaiser()
        {
            typeof(ExceptionRaiser).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Exception) 
                    })
                 );
        }

        [Test]
        public void GetReferencedTypes_Attributes()
        {
            typeof(Attributes).GetReferencedTypes()
                .Should().Have.SameValuesAs(
                    common.Union(new []
                    {
                    typeof(SerializableAttribute),  // Class Attribute
                    typeof(NonSerializedAttribute), // Field Attribute
                    typeof(FooAttribute),           // MethodAttribute
                    typeof(FooAttribute2)           // Parameter Attribute
                    })
                 );
        }
    }
    // ReSharper restore InconsistentNaming
}