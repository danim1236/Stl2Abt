namespace BioGenie.Stl2Abt.Gui
{
    partial class Stl2AbtMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelStl = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonZ = new System.Windows.Forms.RadioButton();
            this.radioButtonY = new System.Windows.Forms.RadioButton();
            this.radioButtonX = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.radioButtonPoint = new System.Windows.Forms.RadioButton();
            this.glControl1 = new OpenTK.GLControl();
            this.labelStlFileName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelGeratrizes = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxResVertical = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxResAngular = new System.Windows.Forms.TextBox();
            this.glControl2 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.panelAbt = new System.Windows.Forms.Panel();
            this.glControl3 = new OpenTK.GLControl();
            this.label2 = new System.Windows.Forms.Label();
            this.bindingSourceConfig = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.panelStl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelGeratrizes.SuspendLayout();
            this.panelAbt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStl
            // 
            this.panelStl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelStl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelStl.Controls.Add(this.groupBox2);
            this.panelStl.Controls.Add(this.groupBox1);
            this.panelStl.Controls.Add(this.glControl1);
            this.panelStl.Controls.Add(this.labelStlFileName);
            this.panelStl.Controls.Add(this.textBox1);
            this.panelStl.Location = new System.Drawing.Point(12, 12);
            this.panelStl.Name = "panelStl";
            this.panelStl.Size = new System.Drawing.Size(333, 487);
            this.panelStl.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.radioButtonZ);
            this.groupBox2.Controls.Add(this.radioButtonY);
            this.groupBox2.Controls.Add(this.radioButtonX);
            this.groupBox2.Location = new System.Drawing.Point(130, 411);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 32);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // radioButtonZ
            // 
            this.radioButtonZ.AutoSize = true;
            this.radioButtonZ.Location = new System.Drawing.Point(82, 9);
            this.radioButtonZ.Name = "radioButtonZ";
            this.radioButtonZ.Size = new System.Drawing.Size(32, 17);
            this.radioButtonZ.TabIndex = 2;
            this.radioButtonZ.Text = "Z";
            this.radioButtonZ.UseVisualStyleBackColor = true;
            this.radioButtonZ.CheckedChanged += new System.EventHandler(this.radioButtonX_CheckedChanged);
            // 
            // radioButtonY
            // 
            this.radioButtonY.AutoSize = true;
            this.radioButtonY.Checked = true;
            this.radioButtonY.Location = new System.Drawing.Point(44, 9);
            this.radioButtonY.Name = "radioButtonY";
            this.radioButtonY.Size = new System.Drawing.Size(32, 17);
            this.radioButtonY.TabIndex = 1;
            this.radioButtonY.TabStop = true;
            this.radioButtonY.Text = "Y";
            this.radioButtonY.UseVisualStyleBackColor = true;
            this.radioButtonY.CheckedChanged += new System.EventHandler(this.radioButtonX_CheckedChanged);
            // 
            // radioButtonX
            // 
            this.radioButtonX.AutoSize = true;
            this.radioButtonX.Location = new System.Drawing.Point(6, 9);
            this.radioButtonX.Name = "radioButtonX";
            this.radioButtonX.Size = new System.Drawing.Size(32, 17);
            this.radioButtonX.TabIndex = 0;
            this.radioButtonX.Text = "X";
            this.radioButtonX.UseVisualStyleBackColor = true;
            this.radioButtonX.CheckedChanged += new System.EventHandler(this.radioButtonX_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.radioButtonLine);
            this.groupBox1.Controls.Add(this.radioButtonPoint);
            this.groupBox1.Location = new System.Drawing.Point(3, 411);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 32);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Location = new System.Drawing.Point(66, 9);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(51, 17);
            this.radioButtonLine.TabIndex = 1;
            this.radioButtonLine.Text = "Linha";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButtonPoint
            // 
            this.radioButtonPoint.AutoSize = true;
            this.radioButtonPoint.Checked = true;
            this.radioButtonPoint.Location = new System.Drawing.Point(6, 9);
            this.radioButtonPoint.Name = "radioButtonPoint";
            this.radioButtonPoint.Size = new System.Drawing.Size(53, 17);
            this.radioButtonPoint.TabIndex = 0;
            this.radioButtonPoint.TabStop = true;
            this.radioButtonPoint.Text = "Ponto";
            this.radioButtonPoint.UseVisualStyleBackColor = true;
            // 
            // glControl1
            // 
            this.glControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl1.Location = new System.Drawing.Point(3, 25);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(323, 380);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // labelStlFileName
            // 
            this.labelStlFileName.AutoSize = true;
            this.labelStlFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelStlFileName.Location = new System.Drawing.Point(3, 7);
            this.labelStlFileName.Name = "labelStlFileName";
            this.labelStlFileName.Size = new System.Drawing.Size(37, 15);
            this.labelStlFileName.TabIndex = 1;
            this.labelStlFileName.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 449);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 31);
            this.textBox1.TabIndex = 0;
            // 
            // panelGeratrizes
            // 
            this.panelGeratrizes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelGeratrizes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGeratrizes.Controls.Add(this.progressBar1);
            this.panelGeratrizes.Controls.Add(this.button1);
            this.panelGeratrizes.Controls.Add(this.label4);
            this.panelGeratrizes.Controls.Add(this.textBoxResVertical);
            this.panelGeratrizes.Controls.Add(this.label3);
            this.panelGeratrizes.Controls.Add(this.textBoxResAngular);
            this.panelGeratrizes.Controls.Add(this.glControl2);
            this.panelGeratrizes.Controls.Add(this.label1);
            this.panelGeratrizes.Location = new System.Drawing.Point(351, 12);
            this.panelGeratrizes.Name = "panelGeratrizes";
            this.panelGeratrizes.Size = new System.Drawing.Size(333, 487);
            this.panelGeratrizes.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(7, 463);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(319, 17);
            this.progressBar1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(251, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Gerar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 440);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Resolução Vertical";
            // 
            // textBoxResVertical
            // 
            this.textBoxResVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxResVertical.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceConfig, "ResVertical", true));
            this.textBoxResVertical.Location = new System.Drawing.Point(107, 437);
            this.textBoxResVertical.Name = "textBoxResVertical";
            this.textBoxResVertical.Size = new System.Drawing.Size(100, 20);
            this.textBoxResVertical.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Resolução Angular";
            // 
            // textBoxResAngular
            // 
            this.textBoxResAngular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxResAngular.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceConfig, "ResAngular", true));
            this.textBoxResAngular.Location = new System.Drawing.Point(107, 411);
            this.textBoxResAngular.Name = "textBoxResAngular";
            this.textBoxResAngular.Size = new System.Drawing.Size(100, 20);
            this.textBoxResAngular.TabIndex = 2;
            // 
            // glControl2
            // 
            this.glControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl2.BackColor = System.Drawing.Color.Black;
            this.glControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl2.Location = new System.Drawing.Point(3, 25);
            this.glControl2.Name = "glControl2";
            this.glControl2.Size = new System.Drawing.Size(323, 380);
            this.glControl2.TabIndex = 0;
            this.glControl2.VSync = false;
            this.glControl2.Load += new System.EventHandler(this.glControl2_Load);
            this.glControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl2_Paint);
            this.glControl2.Resize += new System.EventHandler(this.glControl2_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // panelAbt
            // 
            this.panelAbt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelAbt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAbt.Controls.Add(this.button2);
            this.panelAbt.Controls.Add(this.glControl3);
            this.panelAbt.Controls.Add(this.label2);
            this.panelAbt.Location = new System.Drawing.Point(690, 12);
            this.panelAbt.Name = "panelAbt";
            this.panelAbt.Size = new System.Drawing.Size(333, 487);
            this.panelAbt.TabIndex = 2;
            // 
            // glControl3
            // 
            this.glControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl3.BackColor = System.Drawing.Color.Black;
            this.glControl3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl3.Location = new System.Drawing.Point(3, 25);
            this.glControl3.Name = "glControl3";
            this.glControl3.Size = new System.Drawing.Size(323, 380);
            this.glControl3.TabIndex = 0;
            this.glControl3.VSync = false;
            this.glControl3.Load += new System.EventHandler(this.glControl3_Load);
            this.glControl3.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl3_Paint);
            this.glControl3.Resize += new System.EventHandler(this.glControl3_Resize);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // bindingSourceConfig
            // 
            this.bindingSourceConfig.DataSource = typeof(BioGenie.Stl2Abt.Gui.Config);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(3, 447);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(323, 33);
            this.button2.TabIndex = 7;
            this.button2.Text = "Escrever e Sair";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Stl2AbtMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 511);
            this.Controls.Add(this.panelAbt);
            this.Controls.Add(this.panelGeratrizes);
            this.Controls.Add(this.panelStl);
            this.Name = "Stl2AbtMainForm";
            this.Text = "Stl2AbtMainForm";
            this.Resize += new System.EventHandler(this.Stl2AbtMainForm_Resize);
            this.panelStl.ResumeLayout(false);
            this.panelStl.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelGeratrizes.ResumeLayout(false);
            this.panelGeratrizes.PerformLayout();
            this.panelAbt.ResumeLayout(false);
            this.panelAbt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceConfig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelStlFileName;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonLine;
        private System.Windows.Forms.RadioButton radioButtonPoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonZ;
        private System.Windows.Forms.RadioButton radioButtonY;
        private System.Windows.Forms.RadioButton radioButtonX;
        private System.Windows.Forms.Panel panelGeratrizes;
        private System.Windows.Forms.Label label1;
        private OpenTK.GLControl glControl2;
        private System.Windows.Forms.Panel panelAbt;
        private OpenTK.GLControl glControl3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxResVertical;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxResAngular;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.BindingSource bindingSourceConfig;
        private System.Windows.Forms.Button button2;
    }
}