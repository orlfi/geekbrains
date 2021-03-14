
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0




// Metadata version: v4.0.30319
.assembly extern System.Runtime
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )                         // .?_....:
  .ver 4:2:2:0
}
.assembly extern System.Console
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )                         // .?_....:
  .ver 4:1:2:0
}
.assembly Source
{
  .custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [System.Runtime]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                                   63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- Следующий настраиваемый атрибут добавлен автоматически, не следует отменять преобразование в комментарий -------
  //  .custom instance void [System.Runtime]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 02 00 00 00 00 00 ) 

  .custom instance void [System.Runtime]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 18 2E 4E 45 54 43 6F 72 65 41 70 70 2C 56   // ....NETCoreApp,V
                                                                                                              65 72 73 69 6F 6E 3D 76 33 2E 31 01 00 54 0E 14   // ersion=v3.1..T..
                                                                                                              46 72 61 6D 65 77 6F 72 6B 44 69 73 70 6C 61 79   // FrameworkDisplay
                                                                                                              4E 61 6D 65 00 )                                  // Name.
  .custom instance void [System.Runtime]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 06 53 6F 75 72 63 65 00 00 )                // ...Source..
  .custom instance void [System.Runtime]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 07 52 65 6C 65 61 73 65 00 00 )             // ...Release..
  .custom instance void [System.Runtime]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 31 2E 30 2E 30 2E 30 00 00 )             // ...1.0.0.0..
  .custom instance void [System.Runtime]System.Reflection.AssemblyInformationalVersionAttribute::.ctor(string) = ( 01 00 05 31 2E 30 2E 30 00 00 )                   // ...1.0.0..
  .custom instance void [System.Runtime]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 06 53 6F 75 72 63 65 00 00 )                // ...Source..
  .custom instance void [System.Runtime]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 06 53 6F 75 72 63 65 00 00 )                // ...Source..
  .hash algorithm 0x00008004
  .ver 1:0:0:0
}
.module Source.dll
// MVID: {2763B9B7-2D35-485A-B475-C8B5C6C80153}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x04BA0000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit Lesson_7.Homework1.Source.Program
       extends [System.Runtime]System.Object
{
  .method private hidebysig static void  Main(string[] args) cil managed
  {
    .entrypoint
    // Размер кода:       70 (0x46)
    .maxstack  2
    .locals init (int32 V_0)
    IL_0000:  ldstr      "Input number: "
    IL_0005:  call       void [System.Console]System.Console::Write(string)
    IL_000a:  call       string [System.Console]System.Console::ReadLine()
    IL_000f:  ldloca.s   V_0
    IL_0011:  call       bool [System.Runtime]System.Int32::TryParse(string,
                                                                     int32&)
    IL_0016:  brfalse.s  IL_0035

    IL_0018:  ldloc.0
    IL_0019:  ldc.i4.2
    IL_001a:  rem
    IL_001b:  brtrue.s   IL_0029

    IL_001d:  ldstr      "the number is even!"
    IL_0022:  call       void [System.Console]System.Console::WriteLine(string)
    IL_0027:  br.s       IL_003f

    IL_0029:  ldstr      "the number is odd!"
    IL_002e:  call       void [System.Console]System.Console::WriteLine(string)
    IL_0033:  br.s       IL_003f

    IL_0035:  ldstr      "Error: you must enter a number!"
    IL_003a:  call       void [System.Console]System.Console::WriteLine(string)
    IL_003f:  call       valuetype [System.Console]System.ConsoleKeyInfo [System.Console]System.Console::ReadKey()
    IL_0044:  pop
    IL_0045:  ret
  } // end of method Program::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Размер кода:       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [System.Runtime]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Program::.ctor

} // end of class Lesson_7.Homework1.Source.Program


// =============================================================

// *********** ДИЗАССЕМБЛИРОВАНИЕ ЗАВЕРШЕНО ***********************
// ВНИМАНИЕ: создан файл ресурсов Win32 C:\Projects\GeekBrains\1.C#.Base\Lesson-7.Homework1.ldasm\core\source.res
