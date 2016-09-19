using BioGenie.Stl;
using System;
using System.IO;
using BioGenie.Stl.Algorithm;

namespace Biogenie.Stl2Abt.Auto
{
    class Program
    {
        private static Stl2AbtManager _stl2AbtManager;

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Use Biogenie.Stl2Abt.Auto [arquivo.abt]");
            }
            args = new[] {args[0], Path.GetFileNameWithoutExtension(args[0]) + ".abt"};

            string stlFileName = args[0];
            string abtFileName = Path.GetFileNameWithoutExtension(args[0]) + ".abt";

            _stl2AbtManager = new Stl2AbtManager(stlFileName, abtFileName);
            _stl2AbtManager.GenerateModel(9);

            Abt.WriteAbt(_stl2AbtManager.AbtFileName, _stl2AbtManager.AbtBoundary);
        }
    }
}
