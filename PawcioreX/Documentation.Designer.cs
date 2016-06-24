namespace PawcioreX
{
    partial class Documentation
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
            this.rtf = new PawcioreX.FixedRichTextBox();
            this.label_title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtf
            // 
            this.rtf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtf.DetectUrls = false;
            this.rtf.Location = new System.Drawing.Point(8, 37);
            this.rtf.Name = "rtf";
            this.rtf.Size = new System.Drawing.Size(767, 535);
            this.rtf.TabIndex = 0;
            this.rtf.Text = "";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_title.Location = new System.Drawing.Point(3, 5);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(0, 29);
            this.label_title.TabIndex = 1;
            // 
            // Documentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 581);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.rtf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Documentation";
            this.Text = "Dokumentacja użytkownika";
            this.Load += new System.EventHandler(this.UserDocumentation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private FixedRichTextBox rtf;
    }
}