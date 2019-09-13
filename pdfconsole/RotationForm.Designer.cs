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
            this.angleSelector = new System.Windows.Forms.TrackBar();
            this.lbl_Rotval = new System.Windows.Forms.Label();
            this.pb_Image = new System.Windows.Forms.PictureBox();
            this.bt_Save = new System.Windows.Forms.Button();
            this.filelist = new System.Windows.Forms.ComboBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.angleSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // angleSelector
            // 
            this.angleSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.angleSelector.Location = new System.Drawing.Point(12, 40);
            this.angleSelector.Maximum = 1000;
            this.angleSelector.Minimum = -1000;
            this.angleSelector.Name = "angleSelector";
            this.angleSelector.Size = new System.Drawing.Size(776, 45);
            this.angleSelector.TabIndex = 1;
            this.angleSelector.TickStyle = System.Windows.Forms.TickStyle.None;
            this.angleSelector.Scroll += new System.EventHandler(this.AngleSelector_Scroll);
            this.angleSelector.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AngleSelector_MouseUp);
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
            this.pb_Image.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pb_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_Image.ImageLocation = "";
            this.pb_Image.Location = new System.Drawing.Point(12, 91);
            this.pb_Image.Name = "pb_Image";
            this.pb_Image.Size = new System.Drawing.Size(776, 680);
            this.pb_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Image.TabIndex = 3;
            this.pb_Image.TabStop = false;
            this.pb_Image.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Image_Paint);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Save.Location = new System.Drawing.Point(12, 777);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(776, 23);
            this.bt_Save.TabIndex = 4;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // filelist
            // 
            this.filelist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filelist.FormattingEnabled = true;
            this.filelist.Location = new System.Drawing.Point(12, 13);
            this.filelist.Name = "filelist";
            this.filelist.Size = new System.Drawing.Size(641, 21);
            this.filelist.TabIndex = 5;
            this.filelist.SelectionChangeCommitted += new System.EventHandler(this.filelist_SelectionChangeCommitted);
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.ForeColor = System.Drawing.Color.Teal;
            this.lbl_Status.Location = new System.Drawing.Point(659, 12);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(108, 19);
            this.lbl_Status.TabIndex = 6;
            this.lbl_Status.Text = "Please Wait";
            // 
            // RotationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.filelist);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.pb_Image);
            this.Controls.Add(this.lbl_Rotval);
            this.Controls.Add(this.angleSelector);
            this.Name = "RotationForm";
            this.Text = "RotationForm";
            ((System.ComponentModel.ISupportInitialize)(this.angleSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar angleSelector;
        private System.Windows.Forms.Label lbl_Rotval;
        private System.Windows.Forms.PictureBox pb_Image;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.ComboBox filelist;
        private System.Windows.Forms.Label lbl_Status;
    }
}