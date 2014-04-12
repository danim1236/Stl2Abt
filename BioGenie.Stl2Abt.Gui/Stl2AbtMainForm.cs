using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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

        public StlDocument StlDocument { get; set; }

        public StlAbutment StlAbutment { get; set; }

        public Dictionary<float, List<Vertex>> Geratrizes { get; set; }

        private bool _firstGenerate = true;

        public Stl2AbtMainForm(string stlFileName, string abtFileName)
        {
            StlFileName = stlFileName;
            AbtFileName = abtFileName;

            InitializeComponent();

            bindingSourceConfig.DataSource = new Config();
            
            Stl2AbtMainForm_Resize(null, null);
            labelStlFileName.Text = Path.GetFileName(stlFileName);

            AbtBoundary = new Dictionary<float, List<Vertex>>();
            Geratrizes = new Dictionary<float, List<Vertex>>();
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
            GenerateModel();
        }

        public Dictionary<float, List<Vertex>> AbtBoundary { get; set; }
        
        private void Stl2AbtMainForm_Resize(object sender, EventArgs e)
        {
            var width = (Width - 30) / 3;
            panelStl.Width = width;
            panelGeratrizes.Width = width;
            panelAbt.Width = width;
            panelGeratrizes.Left = width + 10;
            panelAbt.Left = 2 * width + 10;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            SetOrtho();
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
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
            GL.Ortho(yMax + 1, yMin - 1, zMin - 1, zMax + 1, xMin - 1, xMax + 1);
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
                GL.Normal3(facet.Normal.ToVector3(AxisOrder.Y));
                foreach (var vertex in facet.Vertices)
                {
                    GL.Vertex3(vertex.ToVector3(AxisOrder.Y));
                }
            }
            GL.End();

            SetLight();
            glControl1.Context.SwapBuffers();
        }

        public AxisOrder GetAxisOrder()
        {
            AxisOrder result;
            if (radioButtonX.Checked)
                result=AxisOrder._X;
            else if (radioButtonY.Checked)
                result = AxisOrder._Y;
            else if (radioButtonZ.Checked)
                result = AxisOrder._Z;
            else if (radioButton_X.Checked)
                result = AxisOrder.X;
            else if (radioButton_Y.Checked)
                result = AxisOrder.Y;
            else if (radioButton_Z.Checked)
                result = AxisOrder.Z;
            else
                throw new InvalidEnumArgumentException();
            return result;
        }

        private void SetLight()
        {
            float xMin;
            float yMin;
            float zMin;
            float xMax;
            float yMax;
            float zMax;
            StlDocument.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            GL.Light(LightName.Light0, LightParameter.SpotDirection,
                new Vector4(new Vector3((xMin + xMax)/2, (yMin + yMax)/2, zMax*2)));
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void radioButtonX_CheckedChanged(object sender, EventArgs e)
        {
            GenerateModel();
            Redraw();
        }

        private void GenerateModel()
        {
            StlAbutment = new StlAbutment(StlDocument);
            StlAbutment.AlignAndCenterAbutment();
            var config = (Config)bindingSourceConfig.DataSource;
            Geratrizes = new AngularBoundaryDetector(StlAbutment, config.ResAngular).GetBoundaries(cbFiltrar.Checked);
        }

        private void Redraw()
        {
            glControl1.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            glControl1.Invalidate();
            glControl1_Resize(null, null);
            glControl1_Paint(null, null);

            glControl2.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            glControl2.Invalidate();
            glControl2_Resize(null, null);
            glControl2_Paint(null, null);

            glControl3.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            glControl3.Invalidate();
            glControl3_Resize(null, null);
            glControl3_Paint(null, null);
        }

        private void glControl2_Load(object sender, EventArgs e)
        {
            glControl2.MakeCurrent();

            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void glControl2_Paint(object sender, PaintEventArgs e)
        {
            glControl2.MakeCurrent();

            GL.PolygonMode(MaterialFace.FrontAndBack, radioButtonPoint.Checked ? PolygonMode.Point : PolygonMode.Line);
            GL.Color3(Color.LightBlue);
            foreach (var boundaryByRotStep in Geratrizes.Values)
            {
                GL.Begin(PrimitiveType.LineStrip);
                foreach (var vertex in boundaryByRotStep)
                {
                    GL.Vertex3(vertex.ToVector3(AxisOrder.Y));
                }
                GL.End();
            }

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
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void glControl3_Load(object sender, EventArgs e)
        {
            glControl3.MakeCurrent();

            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void glControl3_Paint(object sender, PaintEventArgs e)
        {
            glControl3.MakeCurrent();

            GL.PolygonMode(MaterialFace.FrontAndBack, radioButtonPoint.Checked ? PolygonMode.Point : PolygonMode.Line);
            GL.Color3(Color.LightBlue);
            foreach (var boundaryByRotStep in AbtBoundary.Values)
            {
                GL.Begin(PrimitiveType.LineStrip);
                foreach (var vertex in boundaryByRotStep)
                {
                    GL.Vertex3(vertex.ToVector3(AxisOrder.Y));
                }
                GL.End();
            }

            SetLight();
            glControl3.Context.SwapBuffers();
        }

        private void glControl3_Resize(object sender, EventArgs e)
        {
            glControl3.MakeCurrent();

            GL.Viewport(0, 0, glControl3.Width, glControl3.Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            SetOrtho();
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var config = (Config) bindingSourceConfig.DataSource;
            var abt = new Abt(Geratrizes);
            AbtBoundary = abt.GetPoints(config.ResVertical, cbP3Maior.Checked);
            if (_firstGenerate)
            {
                Redraw();
                _firstGenerate = false;
            }
            else
            {
                if (WindowState == FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Minimized;
                    Redraw();
                    WindowState = FormWindowState.Normal;
                    Redraw();
                }
                else if (WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Minimized;
                    Redraw();
                    WindowState = FormWindowState.Maximized;
                    Redraw();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abt.WriteAbt(AbtFileName, AbtBoundary);
            Close();
        }
    }

    class Config
    {
        public int ResAngular { get; set; }
        public int ResVertical { get; set; }

        public Config()
        {
            ResAngular = 12;
            ResVertical = 6;
        }
    }
}
