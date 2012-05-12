using System;

using NUnit.Framework;
using SharpTestsEx;
using FluentCodeMetrics.Core;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    public class CeTests
    {
        [Test]
        public void Ce_ForSingleArgCtorIgnoringObject()
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
    }
}