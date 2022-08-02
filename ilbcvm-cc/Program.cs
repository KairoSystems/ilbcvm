using System;

namespace ilbcvm_cc
{
    public class Program
    {
        private static bool UseBuiltinExe = false;

        public static byte[] HelloWorld =
        {
            65, 245, 2, 0, 0, 0,
            65, 253, 250, 0, 0, 0,
            250, 69, 250, 0, 0, 0,
            250, 110, 251, 0, 0, 0,
            250, 116, 252, 0, 0, 0,
            250, 101, 253, 0, 0, 0,
            250, 114, 254, 0, 0, 0,
            250, 32, 255, 0, 0, 0,
            250, 89, 0, 1, 0, 0,
            250, 111, 1, 1, 0, 0,
            250, 117, 2, 1, 0, 0,
            250, 114, 3, 1, 0, 0,
            250, 32, 4, 1, 0, 0,
            250, 78, 5, 1, 0, 0,
            250, 97, 6, 1, 0, 0,
            250, 109, 7, 1, 0, 0,
            250, 101, 8, 1, 0, 0,
            250, 58, 9, 1, 0, 0,
            250, 32, 10, 1, 0, 0,
            65, 247, 17, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            65, 245, 4, 0, 0, 0,
            65, 253, 244, 1, 0, 0,
            235, 1, 0, 0, 0, 0,
            250, 72, 237, 1, 0, 0,
            250, 101, 238, 1, 0, 0,
            250, 108, 239, 1, 0, 0,
            250, 108, 240, 1, 0, 0,
            250, 111, 241, 1, 0, 0,
            250, 44, 242, 1, 0, 0,
            250, 32, 243, 1, 0, 0,
            65, 253, 237, 1, 0, 0,
            68, 247, 7, 0, 0, 0,
            65, 245, 2, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            65, 245, 1, 0, 0, 0,
            65, 246, 10, 0, 0, 0,
            235, 1, 0, 0, 0, 0,
            208, 0, 0, 0, 0, 0,
        };

        /// <summary>
        /// Path to the source to be compiled
        /// </summary>
        private static string FilePath = "";

        public static void Main(string[] args)
        {
            if (UseBuiltinExe == true)
            {
                Decompiler Decompiler = new Decompiler(HelloWorld);
                string SourceCode = Decompiler.Decompile();
                Console.WriteLine(SourceCode);
                Console.ReadKey(true);
            }
            if (args.Length == 0)
            {
                Console.WriteLine("Usage:\t-d <executable.path> <output.path> to decompile a .ilbcvm executable\n\t-c <source.path> <output.path> to compile a .ilbcvs source file");
                Console.WriteLine("\n\tPress any key to continue...");
                Console.ReadKey(true);
            }
            else if (args[0] == "-d")
            {
                if (args[1] != null || args[1] != "")
                {
                    Decompiler Decompiler = new Decompiler(File.ReadAllBytes(args[1]));
                    string SourceCode = Decompiler.Decompile();
                    string outpath;
                    if (args[2] != null || args[2] != "")
                    {
                        outpath = args[2];
                    }
                    else
                    {
                        outpath = args[1].Replace(".ilbcvm", ".ilbcvs");
                    }
                    File.WriteAllText(outpath + ".ilbcvs", SourceCode);
                }
                else
                {
                    Console.WriteLine("No file specified to decompile into ILBCVM Source");
                }
            }
            else if (args[0] == "-c")
            {
                if (args[1] != null || args[1] != "")
                {
                    if (args[2] != null | args[2] != "")
                    {
                        Compile(args[1], args[2]);
                    }
                    else
                    {
                        Compile(args[1], args[1].Replace(".ilbcvs", ".ilbcvm"));
                    }
                }
                else
                {
                    Console.WriteLine("No file specified to compile into ILBCVM Executable");
                }
            }
            else
            {
                Console.WriteLine("Usage:\t-d <executable.path> <output.path> to decompile a .ilbcvm executable\n\t-c <source.path> <output.path> to compile a .ilbcvs source file");
                Console.WriteLine("\n\tPress any key to continue...");
                Console.ReadKey(true);
            }
        }

        private static void Compile(string input, string output)
        {
            #region Compile



            byte[] VMExecutable = null;
            string SourceCode = File.ReadAllText(FilePath);
            Compiler CompiledSource = new Compiler(SourceCode);
            try
            {
                Console.WriteLine("Compiling...");
                VMExecutable = CompiledSource.Compile();
                Console.WriteLine("Please enter where you'd like to save the compiled executable: (Must end in .ilbcvm - will add automatically if not added)");
                string path = Console.ReadLine();
                // If the path doesn't end in a .ila extension, add it
                if (!path.EndsWith(".ilbcvm"))
                {
                    path += ".ilbcvm";
                }
                File.WriteAllBytes(path, VMExecutable);
            }
            catch (BuildException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR]" + ex.Message);
                Console.WriteLine("Error occured at: " + ex.SrcLineNumber);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR]" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
            #endregion
        }
    }
}
