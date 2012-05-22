using System;

using NUnit.Framework;
using SharpTestsEx;
using FluentCodeMetrics.Core;
using FluentCodeMetrics.Core.TypeSets;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    public class CeTests
    {
        [Test]
        public void Ce_ForSingleArgCtorIgnoringCommonTypes()
        {
            var ce = Ce.For<Samples.SingleArgCtor>()
                .Ignoring<System.Runtime.TargetedPatchingOptOutAttribute>()
                .Ignoring<System.Security.SecuritySafeCriticalAttribute>()
                .Ignoring<System.Runtime.ConstrainedExecution.ReliabilityContractAttribute>()
                .Ignoring<System.Runtime.CompilerServices.CompilerGeneratedAttribute>()
                .Ignoring<System.Object>()
                .Ignoring<System.Int32>()
                .Ignoring<System.String>()
                .Ignoring<System.Boolean>()
                .Ignoring<System.Type>();

            ce.Value.Should().Be(1);
            ce.References.Should().Have.SameValuesAs(
                new[] {typeof (Samples.Fee)}
                );
        }

        [Test]
        public void Ce_ForSingleArgCtorIgnoringTypeSet()
        {
            var ce = Ce.For<Samples.SingleArgCtor>()
                .Ignoring(TypeSet.With(
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    typeof(object),
                    typeof(int),
                    typeof(string),
                    typeof(bool),
                    typeof(Type)
                ));

            ce.Value.Should().Be(1);
            ce.References.Should().Have.SameValuesAs(
                new[] { typeof(Samples.Fee) }
                );
        }

        [Test]
        public void Ce_ForSingleArgCtorIgnoringOtherAssembliesTypes()
        {
            var ce = Ce.For<Samples.SingleArgCtor>()
                .Ignoring(typeof (Samples.SingleArgCtor).Assembly.Not());
                
            ce.Value.Should().Be(1);
            ce.References.Should().Have.SameValuesAs(
                new[] { typeof(Samples.Fee) }
                );
        }

        [Test]
        public void All_VoidMethodUsingFee()
        {
            ReferencesInspector.For(typeof (Samples.VoidMethodUsingFee)).All()
                .Should().Contain(typeof (Samples.Fee));
        }

        
    }
}