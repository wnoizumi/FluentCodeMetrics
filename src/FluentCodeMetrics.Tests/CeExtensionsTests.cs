using NUnit.Framework;
using SharpTestsEx;
using FluentCodeMetrics.Core;
using Samples;
using System;

namespace FluentCodeMetrics.Tests
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    public class CeExtensionsTests
    {
        [Test]
        public void GetReferencedTypes_EmptyClass()
        {
            typeof(EmptyClass).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),    
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_SingleField()
        {
            typeof(SingleField).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(DateTime), // field type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_SingleProperty()
        {
            typeof(SingleProperty).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(DateTime), // field type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_SingleNonAutoProperty()
        {
            typeof(SingleNonAutoProperty).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(DateTime), // property type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_FeeMethod()
        {
            typeof(FeeMethod).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(Fee), // returning type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_SingleArgVoidMethod()
        {
            typeof(SingleArgVoidMethod).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(Fee), // argument type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_SingleArgCtor()
        {
            typeof(SingleArgCtor).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(Fee), // argument type
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_ExceptionRaiser()
        {
            typeof(ExceptionRaiser).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(ExceptionRaiser),  // Class Attribute
                    typeof(string),
                    typeof(bool),
                    typeof(int),
                    typeof(Type)
                );
        }

        [Test]
        public void GetReferencedTypes_Attributes()
        {
            typeof(Attributes).GetReferencedTypes()
                .Should().Have.SameSequenceAs(
                    typeof(object),
                    typeof(SerializableAttribute),  // Class Attribute
                    typeof(NonSerializedAttribute), // Field Attribute
                    typeof(FooAttribute),           // MethodAttribute
                    typeof(System.Runtime.TargetedPatchingOptOutAttribute),
                    typeof(System.Security.SecuritySafeCriticalAttribute),
                    typeof(System.Runtime.ConstrainedExecution.ReliabilityContractAttribute),
                    typeof(FooAttribute2),          // Parameter Attribute
                    typeof(int),
                    typeof(string),
                    typeof(bool),
                    typeof(Type)
                );

        }
    }
    // ReSharper restore InconsistentNaming
}