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

            MethodInfo baseMethod = typeof(CodeBuilder).GetMethod("foo");
            MethodBody baseBody = baseMethod.GetMethodBody();
            
            AssemblyBuilder assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("Test, Version=1.0.0.0"), AssemblyBuilderAccess.Save);
            ModuleBuilder module = assembly.DefineDynamicModule("Test", outFile);
            TypeBuilder type = module.DefineType("Test.MapperOne", TypeAttributes.Class | TypeAttributes.Public);
            
            MethodBuilder dynMethod = type.DefineMethod("main", MethodAttributes.Static, typeof(void), new System.Type[] { typeof(string[]) });
            dynMethod.DefineParameter(1, ParameterAttributes.None, "argv");
            
            var sig = SignatureHelper.GetLocalVarSigHelper(module);
            sig.AddArgument(typeof(int));
            sig.AddArgument(typeof(string));
            dynMethod.SetMethodBody(baseBody.GetILAsByteArray(), baseBody.MaxStackSize, sig.GetSignature(), null, null);
            
            type.CreateType();
            assembly.SetEntryPoint(dynMethod, PEFileKinds.ConsoleApplication);
            assembly.Save(outFile);
        }

        public static void foo(string[] argv)
        {
            System.Console.WriteLine("Test done.");
        }
    }
}