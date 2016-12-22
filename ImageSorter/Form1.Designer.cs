namespace ImageSorter
{
    partial class MainWindow
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
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_inputFolder = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStrip_label_inputFolder = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip_label_currentDimensions = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip_label_currentImage = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayout_outputFolders = new System.Windows.Forms.FlowLayoutPanel();
            this.button_outputAdd = new System.Windows.Forms.Button();
            this.button_outputDelete = new System.Windows.Forms.Button();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeInputFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(12, 27);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(481, 517);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.image_box_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeInputFolderToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // button_delete
            // 
            this.button_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_delete.Location = new System.Drawing.Point(499, 66);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(273, 50);
            this.button_delete.TabIndex = 3;
            this.button_delete.Text = "Delete Image";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_inputFolder
            // 
            this.button_inputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_inputFolder.Location = new System.Drawing.Point(499, 27);
            this.button_inputFolder.Name = "button_inputFolder";
            this.button_inputFolder.Size = new System.Drawing.Size(88, 33);
            this.button_inputFolder.TabIndex = 5;
            this.button_inputFolder.Text = "Change Input";
            this.button_inputFolder.UseVisualStyleBackColor = true;
            this.button_inputFolder.Click += new System.EventHandler(this.button_inputFolder_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStrip_label_inputFolder,
            this.toolStripStatusLabel2,
            this.statusStrip_label_currentDimensions,
            this.toolStripStatusLabel1,
            this.statusStrip_label_currentImage});
            this.statusStrip.Location = new System.Drawing.Point(0, 547);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusStrip_label_inputFolder
            // 
            this.statusStrip_label_inputFolder.Name = "statusStrip_label_inputFolder";
            this.statusStrip_label_inputFolder.Size = new System.Drawing.Size(137, 17);
            this.statusStrip_label_inputFolder.Text = "No Input Folder Selected";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // statusStrip_label_currentDimensions
            // 
            this.statusStrip_label_currentDimensions.Name = "statusStrip_label_currentDimensions";
            this.statusStrip_label_currentDimensions.Size = new System.Drawing.Size(158, 17);
            this.statusStrip_label_currentDimensions.Text = "No Image Currently Selected";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // statusStrip_label_currentImage
            // 
            this.statusStrip_label_currentImage.Name = "statusStrip_label_currentImage";
            this.statusStrip_label_currentImage.Size = new System.Drawing.Size(158, 17);
            this.statusStrip_label_currentImage.Text = "No Image Currently Selected";
            // 
            // flowLayout_outputFolders
            // 
            this.flowLayout_outputFolders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayout_outputFolders.Location = new System.Drawing.Point(500, 123);
            this.flowLayout_outputFolders.Name = "flowLayout_outputFolders";
            this.flowLayout_outputFolders.Size = new System.Drawing.Size(272, 421);
            this.flowLayout_outputFolders.TabIndex = 7;
            // 
            // button_outputAdd
            // 
            this.button_outputAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_outputAdd.Location = new System.Drawing.Point(593, 27);
            this.button_outputAdd.Name = "button_outputAdd";
            this.button_outputAdd.Size = new System.Drawing.Size(85, 33);
            this.button_outputAdd.TabIndex = 8;
            this.button_outputAdd.Text = "Add Output";
            this.button_outputAdd.UseVisualStyleBackColor = true;
            this.button_outputAdd.Click += new System.EventHandler(this.button_outputAdd_Click);
            // 
            // button_outputDelete
            // 
            this.button_outputDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_outputDelete.Location = new System.Drawing.Point(684, 27);
            this.button_outputDelete.Name = "button_outputDelete";
            this.button_outputDelete.Size = new System.Drawing.Size(88, 33);
            this.button_outputDelete.TabIndex = 9;
            this.button_outputDelete.Text = "Delete Output";
            this.button_outputDelete.UseVisualStyleBackColor = true;
            this.button_outputDelete.Click += new System.EventHandler(this.button_outputDelete_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // changeInputFolderToolStripMenuItem
            // 
            this.changeInputFolderToolStripMenuItem.Name = "changeInputFolderToolStripMenuItem";
            this.changeInputFolderToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.changeInputFolderToolStripMenuItem.Text = "Change Input Folder";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 569);
            this.Controls.Add(this.button_outputDelete);
            this.Controls.Add(this.button_outputAdd);
            this.Controls.Add(this.flowLayout_outputFolders);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.button_inputFolder);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "ImageSorter";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_inputFolder;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusStrip_label_inputFolder;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusStrip_label_currentImage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusStrip_label_currentDimensions;
        private System.Windows.Forms.FlowLayoutPanel flowLayout_outputFolders;
        private System.Windows.Forms.Button button_outputAdd;
        private System.Windows.Forms.Button button_outputDelete;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeInputFolderToolStripMenuItem;
    }
}

