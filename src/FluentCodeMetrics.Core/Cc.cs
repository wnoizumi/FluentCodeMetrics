using System.Linq;
using System.Reflection;

using FluentCodeMetrics.Core.Cecil;
using Mono.Cecil.Cil;

namespace FluentCodeMetrics.Core
{
    public class Cc : CodeMetric
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
            var type = method.DeclaringType;
            var typeDef = type.ToDefiniton();

            var methodDef = typeDef.Methods
                .First(m => m.Name == method.Name);

            var ccBranchInstructions =
                from instruction in methodDef.Body.Instructions
                where ccBranchOpCodes.Contains(instruction.OpCode)
                select instruction;

            var ccSwitchInstructions =
                from instruction in methodDef.Body.Instructions
                where instruction.OpCode == OpCodes.Switch
                from operand in (Instruction []) instruction.Operand
                select operand;

            var ccRetInstructions =
                from instruction in methodDef.Body.Instructions
                where instruction.OpCode == OpCodes.Ret
                select instruction;


            return new Cc(
                ccBranchInstructions.Count() +
                ccSwitchInstructions.Count() +
                ccRetInstructions.Count()
            );
        }
    }
}
