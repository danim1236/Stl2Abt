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
            this.panelGeratrizes = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.glControl2 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panelAbt = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.glControl3 = new OpenTK.GLControl();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panelStl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelGeratrizes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panelAbt.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.panelGeratrizes.Controls.Add(this.groupBox3);
            this.panelGeratrizes.Controls.Add(this.groupBox4);
            this.panelGeratrizes.Controls.Add(this.glControl2);
            this.panelGeratrizes.Controls.Add(this.label1);
            this.panelGeratrizes.Controls.Add(this.textBox2);
            this.panelGeratrizes.Location = new System.Drawing.Point(351, 12);
            this.panelGeratrizes.Name = "panelGeratrizes";
            this.panelGeratrizes.Size = new System.Drawing.Size(333, 487);
            this.panelGeratrizes.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Location = new System.Drawing.Point(130, 411);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 32);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(82, 9);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(32, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Z";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(44, 9);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(32, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Y";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 9);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(32, 17);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "X";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton5);
            this.groupBox4.Location = new System.Drawing.Point(3, 411);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(121, 32);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(66, 9);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(51, 17);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Linha";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Checked = true;
            this.radioButton5.Location = new System.Drawing.Point(6, 9);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(53, 17);
            this.radioButton5.TabIndex = 0;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Ponto";
            this.radioButton5.UseVisualStyleBackColor = true;
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
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(3, 449);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(323, 31);
            this.textBox2.TabIndex = 0;
            // 
            // panelAbt
            // 
            this.panelAbt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelAbt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAbt.Controls.Add(this.groupBox5);
            this.panelAbt.Controls.Add(this.groupBox6);
            this.panelAbt.Controls.Add(this.glControl3);
            this.panelAbt.Controls.Add(this.label2);
            this.panelAbt.Controls.Add(this.textBox3);
            this.panelAbt.Location = new System.Drawing.Point(690, 12);
            this.panelAbt.Name = "panelAbt";
            this.panelAbt.Size = new System.Drawing.Size(333, 487);
            this.panelAbt.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.radioButton6);
            this.groupBox5.Controls.Add(this.radioButton7);
            this.groupBox5.Controls.Add(this.radioButton8);
            this.groupBox5.Location = new System.Drawing.Point(130, 411);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(174, 32);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(82, 9);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(32, 17);
            this.radioButton6.TabIndex = 2;
            this.radioButton6.Text = "Z";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(44, 9);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(32, 17);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Y";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(6, 9);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(32, 17);
            this.radioButton8.TabIndex = 0;
            this.radioButton8.Text = "X";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.radioButton9);
            this.groupBox6.Controls.Add(this.radioButton10);
            this.groupBox6.Location = new System.Drawing.Point(3, 411);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(121, 32);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Location = new System.Drawing.Point(66, 9);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(51, 17);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.Text = "Linha";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Checked = true;
            this.radioButton10.Location = new System.Drawing.Point(6, 9);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(53, 17);
            this.radioButton10.TabIndex = 0;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "Ponto";
            this.radioButton10.UseVisualStyleBackColor = true;
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
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(3, 449);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(323, 31);
            this.textBox3.TabIndex = 0;
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panelAbt.ResumeLayout(false);
            this.panelAbt.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private OpenTK.GLControl glControl2;
        private System.Windows.Forms.Panel panelAbt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private OpenTK.GLControl glControl3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
    }
}