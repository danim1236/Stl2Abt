using System;
using System.IO;
using System.Windows.Forms;
using BioGenie.Stl;

namespace BioGenie.Stl2Abt.Gui
{
    public partial class Stl2AbtMainForm : Form
    {
        public string StlFileName { get; set; }
        public string AbtFileName { get; set; }
        
        public StlDocument StlDocument { get; set; }

        public Stl2AbtMainForm(string stlFileName, string abtFileName)
        {
            StlFileName = stlFileName;
            AbtFileName = abtFileName;

            InitializeComponent();
            Stl2AbtMainForm_Resize(null, null);
            labelStlFileName.Text = Path.GetFileName(stlFileName);

            ReadStlFile();
        }

        private void ReadStlFile()
        {
            try
            {
                using (var reader = new StreamReader(StlFileName))
                {
                    StlDocument = StlDocument.Read(reader);
                }
            }
            catch (FormatException)
            {
                using (var reader = new BinaryReader(File.Open(StlFileName, FileMode.Open)))
                {
                    StlDocument = StlDocument.Read(reader);
                }
            }
            StlDocument.Name = Path.GetFileNameWithoutExtension(StlFileName);
        }

        private void Stl2AbtMainForm_Resize(object sender, EventArgs e)
        {
            panelStl.Width = (Width - 12)/2;
        }
    }
}
