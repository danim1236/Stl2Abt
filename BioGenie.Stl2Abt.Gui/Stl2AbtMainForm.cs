using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BioGenie.Stl;
using BioGenie.Stl.Algorithm;
using BioGenie.Stl.Objects;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BioGenie.Stl2Abt.Gui
{
    public partial class Stl2AbtMainForm : Form
    {
        public string StlFileName { get; set; }
        public string AbtFileName { get; set; }
        
        public StlAbutment StlAbutment { get; set; }
        public RevBoundary RevBoundary { get; set; }

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
            var wi1 = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(glControl1.Handle);
            glControl1.Context.MakeCurrent(wi1);
            var wi2 = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(glControl2.Handle);
            glControl2.Context.MakeCurrent(wi2);
        }

        private void ReadStlFile()
        {
            try
            {
                using (var reader = new StreamReader(StlFileName))
                {
                    StlAbutment = new StlAbutment(StlDocument.Read(reader));
                }
            }
            catch (FormatException)
            {
                using (var reader = new BinaryReader(File.Open(StlFileName, FileMode.Open)))
                {
                    StlAbutment = new StlAbutment(StlDocument.Read(reader));
                }
            }
            StlAbutment.AlignAndCenterAbutment();
            StlAbutment.Name = Path.GetFileNameWithoutExtension(StlFileName);
            RevBoundary = new RevBoundaryDetector(StlAbutment, 100, 20).GetRevolutionBoundary();
        }

        private void Stl2AbtMainForm_Resize(object sender, EventArgs e)
        {
            var width = (Width - 20) / 2;
            panelStl.Width = width;
            panelAbt.Width = width;
            panelAbt.Left = width + 10;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            GL.ClearColor(Color.MidnightBlue);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            SetOrtho();
        }

        private void SetOrtho()
        {
            float xMin;
            float yMin;
            float zMin;
            float xMax;
            float yMax;
            float zMax;
            StlAbutment.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            switch (GetAxisOrder())
            {
                case AxisOrder.X:
                    GL.Ortho(xMin - 1, xMax + 1, yMin - 1, yMax + 1, zMin - 1, zMax + 1);
                    break;
                case AxisOrder.Y:
                    GL.Ortho(yMax + 1, yMin - 1, zMin - 1, zMax + 1, xMin - 1, xMax + 1);
                    break;
                case AxisOrder.Z:
                    GL.Ortho(zMin - 1, zMax + 1, xMin - 1, xMax + 1, yMin - 1, yMax + 1);
                    break;
            }
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();

            GL.PolygonMode(MaterialFace.FrontAndBack, radioButtonPoint.Checked ? PolygonMode.Point : PolygonMode.Line);
            GL.Begin(PrimitiveType.Triangles);
            foreach (var facet in StlAbutment.Facets)
            {
                if (facet.Vertices.Count != 3)
                {
                    throw new FormatException("There must be only triangles");
                }
                GL.Color3(Color.LightBlue);
                GL.Normal3(facet.Normal.ToVector3(GetAxisOrder()));
                foreach (var vertex in facet.Vertices)
                {
                    GL.Vertex3(vertex.ToVector3(GetAxisOrder()));
                }
            }
            GL.End();

            SetLight();
            glControl1.Context.SwapBuffers();
        }

        private AxisOrder GetAxisOrder()
        {
            return radioButtonX.Checked ? AxisOrder.X : (radioButtonY.Checked ? AxisOrder.Y : AxisOrder.Z);
        }

        private void SetLight()
        {
            float xMin;
            float yMin;
            float zMin;
            float xMax;
            float yMax;
            float zMax;
            StlAbutment.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            GL.Light(LightName.Light0, LightParameter.SpotDirection,
                new Vector4(new Vector3((xMin + xMax)/2, (yMin + yMax)/2, zMax*2)));

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void radioButtonX_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void glControl2_Load(object sender, EventArgs e)
        {
            glControl2.MakeCurrent();

            GL.ClearColor(Color.MidnightBlue);
        }

        private void glControl2_Paint(object sender, PaintEventArgs e)
        {
            glControl2.MakeCurrent();

            GL.PolygonMode(MaterialFace.FrontAndBack, radioButtonPoint.Checked ? PolygonMode.Point : PolygonMode.Line);
            GL.Color3(Color.LightBlue);
            GL.Begin(PrimitiveType.Points);
            foreach (var boundaryByRotStep in RevBoundary.GetOrderedBoundariesByRotStep())
            {
                foreach (var vertex in boundaryByRotStep.Value)
                {
                    GL.Vertex3(vertex.ToVector3(GetAxisOrder()));
                }
            }
            GL.End();

            SetLight();
            glControl2.Context.SwapBuffers();
        }

        private void glControl2_Resize(object sender, EventArgs e)
        {
            glControl2.MakeCurrent();

            GL.Viewport(0, 0, glControl2.Width, glControl2.Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            SetOrtho();
        }
    }
}
