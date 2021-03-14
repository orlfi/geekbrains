
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0




// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly 'Lesson-7.Homework1.Source_NF'
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- Следующий настраиваемый атрибут добавлен автоматически, не следует отменять преобразование в комментарий -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 02 00 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 1C 4C 65 73 73 6F 6E 2D 37 2E 48 6F 6D 65   // ...Lesson-7.Home
                                                                                              77 6F 72 6B 31 2E 53 6F 75 72 63 65 5F 4E 46 00   // work1.Source_NF.
                                                                                              00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 1C 4C 65 73 73 6F 6E 2D 37 2E 48 6F 6D 65   // ...Lesson-7.Home
                                                                                                77 6F 72 6B 31 2E 53 6F 75 72 63 65 5F 4E 46 00   // work1.Source_NF.
                                                                                                00 ) 
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 12 43 6F 70 79 72 69 67 68 74 20 C2 A9 20   // ...Copyright .. 
                                                                                                  20 32 30 32 31 00 00 )                            //  2021..
  .custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = ( 01 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = ( 01 00 24 63 35 38 65 66 31 65 35 2D 32 33 65 31   // ..$c58ef1e5-23e1
                                                                                                  2D 34 33 61 30 2D 62 66 30 31 2D 65 37 35 64 64   // -43a0-bf01-e75dd
                                                                                                  62 33 38 66 38 31 62 00 00 )                      // b38f81b..
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..
  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 1C 2E 4E 45 54 46 72 61 6D 65 77 6F 72 6B   // ....NETFramework
                                                                                                        2C 56 65 72 73 69 6F 6E 3D 76 34 2E 37 2E 32 01   // ,Version=v4.7.2.
                                                                                                        00 54 0E 14 46 72 61 6D 65 77 6F 72 6B 44 69 73   // .T..FrameworkDis
                                                                                                        70 6C 61 79 4E 61 6D 65 14 2E 4E 45 54 20 46 72   // playName..NET Fr
                                                                                                        61 6D 65 77 6F 72 6B 20 34 2E 37 2E 32 )          // amework 4.7.2
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module 'Lesson-7.Homework1.Source_NF.exe'
// MVID: {A157B496-A55E-43E1-B969-D625F27E2EB8}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00020003    //  ILONLY 32BITPREFERRED
// Image base: 0x04BA0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit Lesson_7.Homework1.Source_NF.Program
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Размер кода:       70 (0x46)
    .maxstack  2
    .locals init (int32 V_0)
    IL_0000:  ldstr      "Input number: "
    IL_0005:  call       void [mscorlib]System.Console::Write(string)
    IL_000a:  call       string [mscorlib]System.Console::ReadLine()
    IL_000f:  ldloca.s   V_0
    IL_0011:  call       bool [mscorlib]System.Int32::TryParse(string,
                                                               int32&)
    IL_0016:  brfalse.s  IL_0035

    IL_0018:  ldloc.0
    IL_0019:  ldc.i4.2
    IL_001a:  rem
    IL_001b:  brtrue.s   IL_0029

    IL_001d:  ldstr      "the number is even!"
    IL_0022:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0027:  br.s       IL_003f

    IL_0029:  ldstr      "the number is odd!"
    IL_002e:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0033:  br.s       IL_003f

    IL_0035:  ldstr      "Error: you must enter a number!"
    IL_003a:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_003f:  call       valuetype [mscorlib]System.ConsoleKeyInfo [mscorlib]System.Console::ReadKey()
    IL_0044:  pop
    IL_0045:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Размер кода:       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class Lesson_7.Homework1.Source_NF.Program


// =============================================================

// *********** ДИЗАССЕМБЛИРОВАНИЕ ЗАВЕРШЕНО ***********************
// ВНИМАНИЕ: создан файл ресурсов Win32 C:\Projects\GeekBrains\1.C#.Base\Lesson-7.Homework1.ldasm\netframework\source.res
