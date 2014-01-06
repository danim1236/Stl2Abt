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
            this.panelStl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // Stl2AbtMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 511);
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
    }
}