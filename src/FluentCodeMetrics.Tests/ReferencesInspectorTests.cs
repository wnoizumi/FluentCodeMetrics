using System;
using System.Linq;
using System.Collections.Generic;
using FluentCodeMetrics.Core.TypeFilters;
using NUnit.Framework;
using SharpTestsEx;
using FluentCodeMetrics.Core;
using Samples;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class ReferencesInspectorTests
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
        public void All_EmptyClass()
        {
            ReferencesInspector.For(typeof(EmptyClass)).All()
                .Should().Have.SameValuesAs(
                    common
                );
        }

        [Test]
        public void All_SingleField()
        {
            ReferencesInspector.For(typeof(SingleField)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime) 
                    })
                 );
        }

        [Test]
        public void All_SingleProperty()
        {
            ReferencesInspector.For(typeof(SingleProperty)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime),
                    typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)
                    })
                 );
        }

        [Test]
        public void All_SingleNonAutoProperty()
        {
            ReferencesInspector.For(typeof(SingleNonAutoProperty)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime) 
                    })
                 );
        }

        [Test]
        public void All_FeeMethod()
        {
            ReferencesInspector.For(typeof(FeeMethod)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void All_SingleArgVoidMethod()
        {
            ReferencesInspector.For(typeof(SingleArgVoidMethod)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void All_SingleArgCtor()
        {
            ReferencesInspector.For(typeof(SingleArgCtor)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Fee) 
                    })
                 );
        }

        [Test]
        public void All_ExceptionRaiser()
        {
            ReferencesInspector.For(typeof(ExceptionRaiser)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(Exception) 
                    })
                 );
        }

        [Test]
        public void All_Attributes()
        {
            ReferencesInspector.For(typeof(Attributes)).All()
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

        [Test]
        public void All_StaticProperty()
        {
            ReferencesInspector.For(typeof(StaticMember)).All()
                .Should().Have.SameValuesAs(
                    common.Union(new[]
                    {
                    typeof(DateTime),
                    typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)
                    })
                 );
        }
    }
    // ReSharper restore InconsistentNaming
}

