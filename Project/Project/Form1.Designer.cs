namespace Project
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
            this.buttonSelectClusterFiles = new System.Windows.Forms.Button();
            this.buttonSelectDependencyFiles = new System.Windows.Forms.Button();
            this.listBoxClusterFiles = new System.Windows.Forms.ListBox();
            this.listBoxDependencyFiles = new System.Windows.Forms.ListBox();
            this.buttonExtractPackageDiagram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSelectClusterFiles
            // 
            this.buttonSelectClusterFiles.Location = new System.Drawing.Point(244, 24);
            this.buttonSelectClusterFiles.Name = "buttonSelectClusterFiles";
            this.buttonSelectClusterFiles.Size = new System.Drawing.Size(215, 40);
            this.buttonSelectClusterFiles.TabIndex = 1;
            this.buttonSelectClusterFiles.Text = "Select Cluster Files (.dot)";
            this.buttonSelectClusterFiles.UseVisualStyleBackColor = true;
            this.buttonSelectClusterFiles.Click += new System.EventHandler(this.buttonSelectClusterFiles_Click);
            // 
            // buttonSelectDependencyFiles
            // 
            this.buttonSelectDependencyFiles.Location = new System.Drawing.Point(23, 24);
            this.buttonSelectDependencyFiles.Name = "buttonSelectDependencyFiles";
            this.buttonSelectDependencyFiles.Size = new System.Drawing.Size(215, 40);
            this.buttonSelectDependencyFiles.TabIndex = 0;
            this.buttonSelectDependencyFiles.Text = "Select Dependency Files (.txt)";
            this.buttonSelectDependencyFiles.UseVisualStyleBackColor = true;
            this.buttonSelectDependencyFiles.Click += new System.EventHandler(this.buttonSelectDependencyFiles_Click);
            // 
            // listBoxClusterFiles
            // 
            this.listBoxClusterFiles.FormattingEnabled = true;
            this.listBoxClusterFiles.ItemHeight = 16;
            this.listBoxClusterFiles.Location = new System.Drawing.Point(244, 70);
            this.listBoxClusterFiles.Name = "listBoxClusterFiles";
            this.listBoxClusterFiles.Size = new System.Drawing.Size(215, 292);
            this.listBoxClusterFiles.TabIndex = 3;
            // 
            // listBoxDependencyFiles
            // 
            this.listBoxDependencyFiles.FormattingEnabled = true;
            this.listBoxDependencyFiles.ItemHeight = 16;
            this.listBoxDependencyFiles.Location = new System.Drawing.Point(23, 71);
            this.listBoxDependencyFiles.Name = "listBoxDependencyFiles";
            this.listBoxDependencyFiles.Size = new System.Drawing.Size(215, 292);
            this.listBoxDependencyFiles.TabIndex = 2;
            // 
            // buttonExtractPackageDiagram
            // 
            this.buttonExtractPackageDiagram.Location = new System.Drawing.Point(132, 393);
            this.buttonExtractPackageDiagram.Name = "buttonExtractPackageDiagram";
            this.buttonExtractPackageDiagram.Size = new System.Drawing.Size(214, 40);
            this.buttonExtractPackageDiagram.TabIndex = 4;
            this.buttonExtractPackageDiagram.Text = "Extract Package Diagram";
            this.buttonExtractPackageDiagram.UseVisualStyleBackColor = true;
            this.buttonExtractPackageDiagram.Click += new System.EventHandler(this.buttonExtractPackageDiagram_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.buttonExtractPackageDiagram);
            this.Controls.Add(this.listBoxDependencyFiles);
            this.Controls.Add(this.listBoxClusterFiles);
            this.Controls.Add(this.buttonSelectDependencyFiles);
            this.Controls.Add(this.buttonSelectClusterFiles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Package Diagram";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectClusterFiles;
        private System.Windows.Forms.Button buttonSelectDependencyFiles;
        private System.Windows.Forms.ListBox listBoxClusterFiles;
        private System.Windows.Forms.ListBox listBoxDependencyFiles;
        private System.Windows.Forms.Button buttonExtractPackageDiagram;
    }
}

