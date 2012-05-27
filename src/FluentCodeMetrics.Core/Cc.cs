using System;

using System.Linq;
using System.Reflection;

using Mono.Cecil.Cil;
using FluentCodeMetrics.Core.Cecil;
using ThrowHelper;

namespace FluentCodeMetrics.Core
{
    public sealed class Cc : CodeMetric
    {
        private Cc(int value)
        {
            this.value = value;
        }

        private readonly int value;
        public override int Value
        {
            get { return value; }
        }

        static OpCode[] ccBranchOpCodes = new[]
            {
                OpCodes.Beq, OpCodes.Beq_S, OpCodes.Bge, OpCodes.Bge_S,
                OpCodes.Bge_Un, OpCodes.Bge_Un_S, OpCodes.Bgt, OpCodes.Bgt_S,
                OpCodes.Bgt_Un, OpCodes.Bgt_Un_S, OpCodes.Ble, OpCodes.Ble_S,
                OpCodes.Ble_Un, OpCodes.Ble_Un_S, OpCodes.Blt, OpCodes.Blt_S,
                OpCodes.Blt_Un, OpCodes.Blt_Un_S, OpCodes.Bne_Un, OpCodes.Bne_Un_S,
                OpCodes.Brfalse, OpCodes.Brfalse_S,
                OpCodes.Brtrue, OpCodes.Brtrue_S
            };

        // TODO: Support to overloaded methods
        public static Cc For(MethodInfo method)
        {
            Throw.IfArgumentNull(method, "method");

            var methodBody = method.ToDefinition().Body;
            var methodInstructions = methodBody.Instructions;

            var ccInstructions =
                from instruction in methodInstructions
                where (
                    ccBranchOpCodes.Contains(instruction.OpCode) ||
                    instruction.OpCode == OpCodes.Switch ||
                    instruction.OpCode == OpCodes.Ret
                )
                select instruction;

            Func<Instruction, int> ccWeight = instruction => 
                instruction.OpCode == OpCodes.Switch
                ? ((Instruction[]) instruction.Operand).Length
                : 1;

            var ccCatchs = methodBody.ExceptionHandlers.Count(c => c.CatchType != null);

            var value = ccInstructions.Sum(ccWeight) + ccCatchs;

            return new Cc(value);
        }
    }
}
