using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BioGenie.Stl;
using OpenTK.Graphics.OpenGL;

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
            DrawStlFile();
        }

        private void DrawStlFile()
        {
            var wi = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(glControl1.Handle);
            glControl1.Context.MakeCurrent(wi);
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
            panelStl.Width = (Width - 20)/2;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            var min = Math.Min(glControl1.Width, glControl1.Height) - 5;
            GL.Viewport(0, 0, min, min);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float xMin;
            float yMin;
            float zMin;
            float xMax;
            float yMax;
            float zMax;
            StlDocument.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            GL.Ortho(xMin, xMax, yMin, yMax, zMin, zMax);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
            foreach (var facet in StlDocument.Facets)
            {
                if (facet.Vertices.Count != 3)
                {
                    throw new FormatException("There must be only triangles");
                }
                GL.Begin(PrimitiveType.Triangles);
                GL.Normal3(facet.Normal.ToVector3());
                foreach (var vertex in facet.Vertices)
                {
                    GL.Vertex3(vertex.ToVector3());
                }
                GL.End();
            }
            glControl1.Context.SwapBuffers();
        }
    }
}
