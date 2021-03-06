﻿using System;
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
        private readonly Stl2AbtManager _stl2AbtManager;
        private readonly Config _config;
        
        public Stl2AbtMainForm(string stlFileName, string abtFileName)
        {
            InitializeComponent();

            bindingSourceConfig.DataSource = new Config();
            
            Stl2AbtMainForm_Resize(null, null);
            labelStlFileName.Text = Path.GetFileName(stlFileName);

            _config = GetConfig();
            _stl2AbtManager = new Stl2AbtManager(stlFileName, abtFileName);
            _stl2AbtManager.GenerateModel(_config.ResAngular);

            DrawStlFile();
        }

        private void DrawStlFile()
        {
            var wi1 = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(glControl1.Handle);
            glControl1.Context.MakeCurrent(wi1);
            var wi2 = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(glControl2.Handle);
            glControl2.Context.MakeCurrent(wi2);
        }


        
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
            _stl2AbtManager.StlAbutment.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            GL.Ortho(yMax + 1, yMin - 1, zMin - 1, zMax + 1, xMin - 1, xMax + 1);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();

            GL.PolygonMode(MaterialFace.FrontAndBack, radioButtonPoint.Checked ? PolygonMode.Point : PolygonMode.Line);
            GL.Begin(PrimitiveType.Triangles);
            foreach (var facet in _stl2AbtManager.StlAbutment.ShellFacets)
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

        private void SetLight()
        {
            float xMin;
            float yMin;
            float zMin;
            float xMax;
            float yMax;
            float zMax;
            _stl2AbtManager.StlDocument.GetLimits(out xMin, out yMin, out zMin, out xMax, out yMax, out zMax);
            GL.Light(LightName.Light0, LightParameter.SpotDirection,
                new Vector4(new Vector3((xMin + xMax)/2, (yMin + yMax)/2, zMax*2)));
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            glControl1.Invalidate();
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
            foreach (var boundaryByRotStep in _stl2AbtManager.Geratrizes.Values)
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
            foreach (var boundaryByRotStep in _stl2AbtManager.AbtBoundary.Values)
            {
                GL.Begin(PrimitiveType.LineStrip);
                foreach (var vertex in boundaryByRotStep)
                {
                    GL.Vertex3(vertex.ToVector3(AxisOrder.Y));
                }
                GL.End();
            }

            //if (AbtBoundary.Any())
            //{
            //    GL.Begin(PrimitiveType.LineStrip);
            //    var values = AbtBoundary.OrderBy(_ => _.Key).Select(_ => _.Value).ToList();
            //    for (int i = 0; i <= values.Count; i++)
            //    {
            //        var boundaryByRotStep = values[i % values.Count];
            //        GL.Vertex3(boundaryByRotStep[2].ToVector3(AxisOrder.Y));
            //    }
            //    GL.End();
            //}

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

        private Config GetConfig()
        {
            return (Config) bindingSourceConfig.DataSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Abt.WriteAbt(_stl2AbtManager.AbtFileName, _stl2AbtManager.AbtBoundary);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _stl2AbtManager.GenerateModel(_config.ResAngular);
            Redraw();
            var currentState = WindowState;
            WindowState = FormWindowState.Minimized;
            WindowState = currentState;
        }
    }

    class Config
    {
        public int ResAngular { get; set; }

        public Config()
        {
            ResAngular = 9;
        }
    }
}
