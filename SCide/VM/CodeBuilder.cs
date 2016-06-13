using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;
using System.IO;

namespace ASM.VM
{
    public class CodeBuilder
    {
        public CodeBuilder(Core core, string assemblyName)
        {
            string outFile = assemblyName + ".exe";
            
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Save);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName, outFile);
            TypeBuilder tb = moduleBuilder.DefineType("Program", TypeAttributes.Class | TypeAttributes.Public);

            var main = copyMethod(tb, typeof(Core), "Main", new Type[] { typeof(string[]) });

            tb.CreateType();
            assemblyBuilder.SetEntryPoint(main, PEFileKinds.ConsoleApplication);
            assemblyBuilder.Save(outFile);

            var opCodes = typeof(OpCodes).GetFields().Select(fi => (OpCode)fi.GetValue(null));
        }

        private static MethodBuilder copyMethod(TypeBuilder typeBuilder, Type type, string name, Type[] args)
        {
            MethodInfo baseMethod = type.GetMethod(name, args);
            MethodBody baseBody = baseMethod.GetMethodBody();
            Module baseModule = type.Module;
            
            MethodBuilder newMethod = typeBuilder.DefineMethod(name, baseMethod.Attributes, baseMethod.ReturnType, args);
            var tokens = type.Module.ResolveSignature(baseBody.LocalSignatureMetadataToken);

            newMethod.SetMethodBody(baseBody.GetILAsByteArray(), baseBody.MaxStackSize, tokens, null, null);
            return newMethod;
        }
    }
}