using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioGenie.Stl2Abt.Gui;

namespace BioGenie.Stl2Abt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
            {
                args = StlFileSelect();
            }
            if (args != null)
            {
                Stl2AbtStart.Stl2AbtMainFormStart(args[0], args[1]);
            }
        }

        private static string[] StlFileSelect()
        {
            var dialog = new OpenFileDialog
            {
                Title = @"Abrir Arquivo",
                DefaultExt = "stl",
                Filter = @"Arquivos STL (*.stl)|*.stl",
                AddExtension = true
            };
            return dialog.ShowDialog() == DialogResult.OK ? new[] { dialog.FileName, Stl2AbtExtension(dialog.FileName) } : null;
        }

        private static string Stl2AbtExtension(string fileName)
        {
            var parts = fileName.Split(new[] {'.'}).ToList();
            if (parts.Count > 1 && parts.Last().ToUpper() == "STL")
                parts.Remove(parts.Last());
            parts.Add("abt");
            return string.Join(".", parts);
        }
    }
}
