using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentCodeMetrics.Core;
using Samples.Ca;
using SharpTestsEx;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    public class CaTests
    {
        [Test]
        public void Ca_ForNotUsedType()
        {
            Ca ca = Ca.For<FooCalculator>();

            ca.Value.Should().Be(0);
            ca.References.Should().Be.Empty();
        }

        [Test]
        public void Ca_ForTypeThatIsReferencedByOtherTypes()
        {
            Ca ca = Ca.For<FooException>();

            ca.Value.Should().Be(2);
            ca.References.Should().Have.SameValuesAs(new Type[] { 
                typeof(TryToCatchFooException), typeof(FooCalculator)
            });
        }
    }
}
