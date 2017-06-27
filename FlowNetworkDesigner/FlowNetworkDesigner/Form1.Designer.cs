namespace FlowNetworkDesigner
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Lb_Pump = new System.Windows.Forms.Label();
            this.Lb_Sink = new System.Windows.Forms.Label();
            this.Lb_Merger = new System.Windows.Forms.Label();
            this.Lb_Splitter = new System.Windows.Forms.Label();
            this.Lb_AdjSplitter = new System.Windows.Forms.Label();
            this.Lb_Pipe = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.Lb_Pipe);
            this.panel1.Controls.Add(this.Lb_AdjSplitter);
            this.panel1.Controls.Add(this.Lb_Splitter);
            this.panel1.Controls.Add(this.Lb_Merger);
            this.panel1.Controls.Add(this.Lb_Sink);
            this.panel1.Controls.Add(this.Lb_Pump);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-2, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 632);
            this.panel1.TabIndex = 0;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(3, 583);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 33);
            this.button7.TabIndex = 1;
            this.button7.Text = "Remove pipe";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 526);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Flow:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(14, 546);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(92, 22);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = global::FlowNetworkDesigner.Properties.Resources.Pipe1;
            this.button6.Location = new System.Drawing.Point(16, 427);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(63, 63);
            this.button6.TabIndex = 0;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::FlowNetworkDesigner.Properties.Resources.AdjSplitter;
            this.button5.Location = new System.Drawing.Point(14, 337);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 63);
            this.button5.TabIndex = 0;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::FlowNetworkDesigner.Properties.Resources.Splitter;
            this.button4.Location = new System.Drawing.Point(16, 259);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 63);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::FlowNetworkDesigner.Properties.Resources.Merger;
            this.button3.Location = new System.Drawing.Point(14, 180);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 63);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::FlowNetworkDesigner.Properties.Resources.Sink;
            this.button2.Location = new System.Drawing.Point(16, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 63);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::FlowNetworkDesigner.Properties.Resources.Pump;
            this.button1.Location = new System.Drawing.Point(16, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 63);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Lb_Pump
            // 
            this.Lb_Pump.AutoSize = true;
            this.Lb_Pump.Location = new System.Drawing.Point(87, 39);
            this.Lb_Pump.Name = "Lb_Pump";
            this.Lb_Pump.Size = new System.Drawing.Size(44, 17);
            this.Lb_Pump.TabIndex = 1;
            this.Lb_Pump.Text = "Pump";
            // 
            // Lb_Sink
            // 
            this.Lb_Sink.AutoSize = true;
            this.Lb_Sink.Location = new System.Drawing.Point(85, 119);
            this.Lb_Sink.Name = "Lb_Sink";
            this.Lb_Sink.Size = new System.Drawing.Size(35, 17);
            this.Lb_Sink.TabIndex = 1;
            this.Lb_Sink.Text = "Sink";
            // 
            // Lb_Merger
            // 
            this.Lb_Merger.AutoSize = true;
            this.Lb_Merger.Location = new System.Drawing.Point(82, 213);
            this.Lb_Merger.Name = "Lb_Merger";
            this.Lb_Merger.Size = new System.Drawing.Size(53, 17);
            this.Lb_Merger.TabIndex = 1;
            this.Lb_Merger.Text = "Merger";
            // 
            // Lb_Splitter
            // 
            this.Lb_Splitter.AutoSize = true;
            this.Lb_Splitter.Location = new System.Drawing.Point(82, 282);
            this.Lb_Splitter.Name = "Lb_Splitter";
            this.Lb_Splitter.Size = new System.Drawing.Size(52, 17);
            this.Lb_Splitter.TabIndex = 1;
            this.Lb_Splitter.Text = "Splitter";
            // 
            // Lb_AdjSplitter
            // 
            this.Lb_AdjSplitter.AutoSize = true;
            this.Lb_AdjSplitter.Location = new System.Drawing.Point(82, 360);
            this.Lb_AdjSplitter.Name = "Lb_AdjSplitter";
            this.Lb_AdjSplitter.Size = new System.Drawing.Size(48, 17);
            this.Lb_AdjSplitter.TabIndex = 1;
            this.Lb_AdjSplitter.Text = "AdjSpl";
            // 
            // Lb_Pipe
            // 
            this.Lb_Pipe.AutoSize = true;
            this.Lb_Pipe.Location = new System.Drawing.Point(85, 450);
            this.Lb_Pipe.Name = "Lb_Pipe";
            this.Lb_Pipe.Size = new System.Drawing.Size(36, 17);
            this.Lb_Pipe.TabIndex = 1;
            this.Lb_Pipe.Text = "Pipe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 626);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label Lb_Pipe;
        private System.Windows.Forms.Label Lb_AdjSplitter;
        private System.Windows.Forms.Label Lb_Splitter;
        private System.Windows.Forms.Label Lb_Merger;
        private System.Windows.Forms.Label Lb_Sink;
        private System.Windows.Forms.Label Lb_Pump;
    }
}

