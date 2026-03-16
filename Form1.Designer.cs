namespace smartscanner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            scan = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.DarkKhaki;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(477, 34);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(390, 393);
            dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(78, 34);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(379, 393);
            textBox1.TabIndex = 1;
            textBox1.Text = "int x := 10;\r\nfloat y := 3.14;\r\nwrite \"Hello\";\r\nif x > 5 && y < 10 then\r\n    x := x + 1;\r\nend\r\n";
            // 
            // scan
            // 
            scan.BackColor = Color.White;
            scan.FlatStyle = FlatStyle.Flat;
            scan.Font = new Font("Microsoft JhengHei", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            scan.Location = new Point(316, 472);
            scan.Name = "scan";
            scan.Size = new Size(299, 66);
            scan.TabIndex = 2;
            scan.Text = "scan";
            scan.UseVisualStyleBackColor = false;
            scan.Click += scan_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(961, 582);
            Controls.Add(scan);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBox1;
        private Button scan;
    }
}
