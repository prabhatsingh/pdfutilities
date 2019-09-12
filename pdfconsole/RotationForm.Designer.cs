namespace PdfConsole
{
    partial class RotationForm
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
            this.bt_Rotate = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lbl_Rotval = new System.Windows.Forms.Label();
            this.pb_Image = new System.Windows.Forms.PictureBox();
            this.bt_Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Rotate
            // 
            this.bt_Rotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_Rotate.Location = new System.Drawing.Point(12, 777);
            this.bt_Rotate.Name = "bt_Rotate";
            this.bt_Rotate.Size = new System.Drawing.Size(385, 23);
            this.bt_Rotate.TabIndex = 0;
            this.bt_Rotate.Text = "Rotate";
            this.bt_Rotate.UseVisualStyleBackColor = true;
            this.bt_Rotate.Click += new System.EventHandler(this.bt_Rotate_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(12, 13);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Minimum = -1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(776, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lbl_Rotval
            // 
            this.lbl_Rotval.AutoSize = true;
            this.lbl_Rotval.Location = new System.Drawing.Point(13, 51);
            this.lbl_Rotval.Name = "lbl_Rotval";
            this.lbl_Rotval.Size = new System.Drawing.Size(0, 13);
            this.lbl_Rotval.TabIndex = 2;
            // 
            // pb_Image
            // 
            this.pb_Image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_Image.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pb_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_Image.ImageLocation = "";
            this.pb_Image.Location = new System.Drawing.Point(12, 65);
            this.pb_Image.Name = "pb_Image";
            this.pb_Image.Size = new System.Drawing.Size(776, 706);
            this.pb_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Image.TabIndex = 3;
            this.pb_Image.TabStop = false;
            this.pb_Image.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Image_Paint);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.Location = new System.Drawing.Point(403, 777);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(385, 23);
            this.bt_Save.TabIndex = 4;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // RotationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.pb_Image);
            this.Controls.Add(this.lbl_Rotval);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.bt_Rotate);
            this.Name = "RotationForm";
            this.Text = "RotationForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Rotate;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lbl_Rotval;
        private System.Windows.Forms.PictureBox pb_Image;
        private System.Windows.Forms.Button bt_Save;
    }
}