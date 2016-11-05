using System;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace MAIDE.VM
{
    public static class OperationManager
    {
        private static readonly Dictionary<Type, Action<object, BinaryWriter>> cdType;
        private static readonly Dictionary<Type, Func<Type, BinaryReader, object>> dcType;
        public static readonly MethodInfo[] Operations;

        #region Main
        static OperationManager()
        {
            var ms = typeof(Operators).GetMethods();
            Operations = ms.Where(w => w.CustomAttributes.Any(e => e.AttributeType == typeof(DescriptorAttribute))).ToArray();

            if (Operations.Count() > 256)
                throw new Exception("Dont support more 256 operations");

            cdType = new Dictionary<Type, Action<object, BinaryWriter>>();
            cdType.Add(typeof(int), cdNumber);
            cdType.Add(typeof(short), cdNumber);
            cdType.Add(typeof(char), cdNumber);
            cdType.Add(typeof(byte), cdNumber);
            cdType.Add(typeof(Register8), cdRegister);
            cdType.Add(typeof(Register16), cdRegister);
            cdType.Add(typeof(Register32), cdRegister);

            dcType = new Dictionary<Type, Func<Type, BinaryReader, object>>();
            dcType.Add(typeof(int), dcNumber);
            dcType.Add(typeof(short), dcNumber);
            dcType.Add(typeof(char), dcNumber);
            dcType.Add(typeof(byte), dcNumber);
            dcType.Add(typeof(Register8), dcRegister);
            dcType.Add(typeof(Register16), dcRegister);
            dcType.Add(typeof(Register32), dcRegister);
        }

        public static MethodInfo GetMethod(string name)
        {
            return Operations.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        #endregion

        #region CD
        public static void Code(BinaryWriter writer, Operation op)
        {
            byte index = 0;

            while (Operations[index] != op.Method)
                index++;

            foreach (var arg in op.Args)
                cdType[arg.GetType()](arg, writer);

            writer.Write(index);
        }

        private static void cdRegister(object obj, BinaryWriter writer)
        {
            byte index = 0;

            while (RegisterManager.Registers[index] != obj)
                index++;

            writer.Write(index);
        }

        private static void cdNumber(object obj, BinaryWriter writer)
        {
            writer.Write((int)Convert.ChangeType(obj, TypeCode.Int32));
        }
        #endregion

        #region DC
        public static Operation Decode(BinaryReader reader)
        {
            byte index = reader.ReadByte();

            if (Operations.Count() >= index)
                throw new Exception(string.Format("Opcode {0} dont support", index.ToString("X")));

            Operation op = new Operation(Operations[index]);
            var param = op.Method.GetParameters();

            if (param.Length > 0)
                op.Args[0] = dcType[param[0].ParameterType](param[0].ParameterType, reader);
            if (param.Length > 1)
                op.Args[1] = dcType[param[1].ParameterType](param[1].ParameterType, reader);

            return op;
        }

        private static object invokeDC(ParameterInfo[] param, int index, BinaryReader reader)
        {
            Type type = param[index].ParameterType;
            return dcType[type](type, reader);
        }

        private static object dcRegister(Type type, BinaryReader reader)
        {
            byte index = reader.ReadByte();

            return RegisterManager.Registers[index];
        }

        private static object dcNumber(Type type, BinaryReader reader)
        {
            return Convert.ChangeType(reader.ReadInt32(), type);
        }
        #endregion
    }
}