namespace PawcioreX
{
    partial class Search
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
            this.modalDialogTxtBox = new System.Windows.Forms.TextBox();
            this.modalDialogbtnFindText = new System.Windows.Forms.Button();
            this.modalDialogbtnFindTextNext = new System.Windows.Forms.Button();
            this.modalDialogbtnFindTextPrevious = new System.Windows.Forms.Button();
            this.modalDialogBtnFindFirst = new System.Windows.Forms.Button();
            this.modalDialogBtnFindLast = new System.Windows.Forms.Button();
            this.btn_More = new System.Windows.Forms.Button();
            this.modalDialogbtnChangeText = new System.Windows.Forms.Button();
            this.modalDialogTxtBoxChange = new System.Windows.Forms.TextBox();
            this.modalDialogLabel1 = new System.Windows.Forms.Label();
            this.modalDialogbtnChangeAllText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modalDialogTxtBox
            // 
            this.modalDialogTxtBox.Location = new System.Drawing.Point(77, 24);
            this.modalDialogTxtBox.Name = "modalDialogTxtBox";
            this.modalDialogTxtBox.Size = new System.Drawing.Size(210, 20);
            this.modalDialogTxtBox.TabIndex = 0;
            // 
            // modalDialogbtnFindText
            // 
            this.modalDialogbtnFindText.Location = new System.Drawing.Point(306, 19);
            this.modalDialogbtnFindText.Name = "modalDialogbtnFindText";
            this.modalDialogbtnFindText.Size = new System.Drawing.Size(82, 29);
            this.modalDialogbtnFindText.TabIndex = 1;
            this.modalDialogbtnFindText.Text = "Znajdź...";
            this.modalDialogbtnFindText.UseVisualStyleBackColor = true;
            this.modalDialogbtnFindText.Click += new System.EventHandler(this.modalDialogbtnFindText_Click);
            // 
            // modalDialogbtnFindTextNext
            // 
            this.modalDialogbtnFindTextNext.Location = new System.Drawing.Point(138, 128);
            this.modalDialogbtnFindTextNext.Name = "modalDialogbtnFindTextNext";
            this.modalDialogbtnFindTextNext.Size = new System.Drawing.Size(117, 29);
            this.modalDialogbtnFindTextNext.TabIndex = 2;
            this.modalDialogbtnFindTextNext.Text = "Nastepny >";
            this.modalDialogbtnFindTextNext.UseVisualStyleBackColor = true;
            this.modalDialogbtnFindTextNext.Visible = false;
            this.modalDialogbtnFindTextNext.Click += new System.EventHandler(this.modalDialogbtnFindTextNext_Click);
            // 
            // modalDialogbtnFindTextPrevious
            // 
            this.modalDialogbtnFindTextPrevious.Location = new System.Drawing.Point(15, 128);
            this.modalDialogbtnFindTextPrevious.Name = "modalDialogbtnFindTextPrevious";
            this.modalDialogbtnFindTextPrevious.Size = new System.Drawing.Size(117, 29);
            this.modalDialogbtnFindTextPrevious.TabIndex = 3;
            this.modalDialogbtnFindTextPrevious.Text = "< Poprzedni";
            this.modalDialogbtnFindTextPrevious.UseVisualStyleBackColor = true;
            this.modalDialogbtnFindTextPrevious.Visible = false;
            this.modalDialogbtnFindTextPrevious.Click += new System.EventHandler(this.modalDialogButtonFindTextPrevious_Click);
            // 
            // modalDialogBtnFindFirst
            // 
            this.modalDialogBtnFindFirst.Enabled = false;
            this.modalDialogBtnFindFirst.Location = new System.Drawing.Point(15, 179);
            this.modalDialogBtnFindFirst.Name = "modalDialogBtnFindFirst";
            this.modalDialogBtnFindFirst.Size = new System.Drawing.Size(117, 29);
            this.modalDialogBtnFindFirst.TabIndex = 4;
            this.modalDialogBtnFindFirst.Text = "<< Pierwszy";
            this.modalDialogBtnFindFirst.UseVisualStyleBackColor = true;
            this.modalDialogBtnFindFirst.Click += new System.EventHandler(this.modalDialogBtnFindFirst_Click);
            // 
            // modalDialogBtnFindLast
            // 
            this.modalDialogBtnFindLast.Enabled = false;
            this.modalDialogBtnFindLast.Location = new System.Drawing.Point(138, 179);
            this.modalDialogBtnFindLast.Name = "modalDialogBtnFindLast";
            this.modalDialogBtnFindLast.Size = new System.Drawing.Size(117, 29);
            this.modalDialogBtnFindLast.TabIndex = 5;
            this.modalDialogBtnFindLast.Text = "Ostatni >>";
            this.modalDialogBtnFindLast.UseVisualStyleBackColor = true;
            this.modalDialogBtnFindLast.Click += new System.EventHandler(this.modalDialgoBtnFindLast_Click);
            // 
            // btn_More
            // 
            this.btn_More.Location = new System.Drawing.Point(306, 130);
            this.btn_More.Name = "btn_More";
            this.btn_More.Size = new System.Drawing.Size(82, 25);
            this.btn_More.TabIndex = 6;
            this.btn_More.UseVisualStyleBackColor = true;
            this.btn_More.Click += new System.EventHandler(this.btn_More_Click);
            // 
            // modalDialogbtnChangeText
            // 
            this.modalDialogbtnChangeText.Enabled = false;
            this.modalDialogbtnChangeText.Location = new System.Drawing.Point(306, 55);
            this.modalDialogbtnChangeText.Name = "modalDialogbtnChangeText";
            this.modalDialogbtnChangeText.Size = new System.Drawing.Size(82, 29);
            this.modalDialogbtnChangeText.TabIndex = 8;
            this.modalDialogbtnChangeText.Text = "Zamień..";
            this.modalDialogbtnChangeText.UseVisualStyleBackColor = true;
            this.modalDialogbtnChangeText.Click += new System.EventHandler(this.modalDialogbtnChangeText_Click);
            // 
            // modalDialogTxtBoxChange
            // 
            this.modalDialogTxtBoxChange.Location = new System.Drawing.Point(77, 60);
            this.modalDialogTxtBoxChange.Name = "modalDialogTxtBoxChange";
            this.modalDialogTxtBoxChange.Size = new System.Drawing.Size(210, 20);
            this.modalDialogTxtBoxChange.TabIndex = 7;
            // 
            // modalDialogLabel1
            // 
            this.modalDialogLabel1.AutoSize = true;
            this.modalDialogLabel1.Location = new System.Drawing.Point(12, 63);
            this.modalDialogLabel1.Name = "modalDialogLabel1";
            this.modalDialogLabel1.Size = new System.Drawing.Size(60, 13);
            this.modalDialogLabel1.TabIndex = 9;
            this.modalDialogLabel1.Text = "Zamień na:";
            // 
            // modalDialogbtnChangeAllText
            // 
            this.modalDialogbtnChangeAllText.Enabled = false;
            this.modalDialogbtnChangeAllText.Location = new System.Drawing.Point(252, 90);
            this.modalDialogbtnChangeAllText.Name = "modalDialogbtnChangeAllText";
            this.modalDialogbtnChangeAllText.Size = new System.Drawing.Size(136, 29);
            this.modalDialogbtnChangeAllText.TabIndex = 10;
            this.modalDialogbtnChangeAllText.Text = "Zamień wszystkie...";
            this.modalDialogbtnChangeAllText.UseVisualStyleBackColor = true;
            this.modalDialogbtnChangeAllText.Click += new System.EventHandler(this.modalDialogbtnChangeAllText_Click);
            // 
            // modalDialogFindText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 165);
            this.Controls.Add(this.modalDialogbtnChangeAllText);
            this.Controls.Add(this.modalDialogLabel1);
            this.Controls.Add(this.modalDialogbtnChangeText);
            this.Controls.Add(this.modalDialogTxtBoxChange);
            this.Controls.Add(this.btn_More);
            this.Controls.Add(this.modalDialogBtnFindLast);
            this.Controls.Add(this.modalDialogBtnFindFirst);
            this.Controls.Add(this.modalDialogbtnFindTextPrevious);
            this.Controls.Add(this.modalDialogbtnFindTextNext);
            this.Controls.Add(this.modalDialogbtnFindText);
            this.Controls.Add(this.modalDialogTxtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "modalDialogFindText";
            this.Text = "Znajdź tekst...";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox modalDialogTxtBox;
        private System.Windows.Forms.Button modalDialogbtnFindText;
        private System.Windows.Forms.Button modalDialogbtnFindTextNext;
        private System.Windows.Forms.Button modalDialogbtnFindTextPrevious;
        private System.Windows.Forms.Button modalDialogBtnFindFirst;
        private System.Windows.Forms.Button modalDialogBtnFindLast;
        private System.Windows.Forms.Button btn_More;
        private System.Windows.Forms.Button modalDialogbtnChangeText;
        private System.Windows.Forms.TextBox modalDialogTxtBoxChange;
        private System.Windows.Forms.Label modalDialogLabel1;
        private System.Windows.Forms.Button modalDialogbtnChangeAllText;
    }
}