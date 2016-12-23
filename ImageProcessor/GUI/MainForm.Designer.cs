namespace ImageProcessor.GUI
{
  partial class MainForm
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
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mainMenu_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_File_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_File_Seperator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenu_File_Save = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_Edit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_Edit_Undo = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_Transform = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu_Process = new System.Windows.Forms.ToolStripMenuItem();
			this.imageDisplay = new System.Windows.Forms.PictureBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageDisplay)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenu_File,
            this.mainMenu_Edit,
            this.mainMenu_Transform,
            this.mainMenu_Process});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(529, 24);
			this.mainMenu.TabIndex = 0;
			// 
			// mainMenu_File
			// 
			this.mainMenu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenu_File_Open,
            this.mainMenu_File_Close,
            this.mainMenu_File_Seperator1,
            this.mainMenu_File_Save,
            this.mainMenu_File_SaveAs,
            this.toolStripSeparator1,
            this.mainMenu_File_Exit});
			this.mainMenu_File.Name = "mainMenu_File";
			this.mainMenu_File.Size = new System.Drawing.Size(35, 20);
			this.mainMenu_File.Text = "File";
			// 
			// mainMenu_File_Open
			// 
			this.mainMenu_File_Open.Name = "mainMenu_File_Open";
			this.mainMenu_File_Open.ShortcutKeyDisplayString = "";
			this.mainMenu_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mainMenu_File_Open.Size = new System.Drawing.Size(204, 22);
			this.mainMenu_File_Open.Text = "Open...";
			this.mainMenu_File_Open.Click += new System.EventHandler(this.mainMenu_File_Open_Click);
			// 
			// mainMenu_File_Close
			// 
			this.mainMenu_File_Close.Name = "mainMenu_File_Close";
			this.mainMenu_File_Close.Size = new System.Drawing.Size(204, 22);
			this.mainMenu_File_Close.Text = "Close";
			this.mainMenu_File_Close.Click += new System.EventHandler(this.mainMenu_File_Close_Click);
			// 
			// mainMenu_File_Seperator1
			// 
			this.mainMenu_File_Seperator1.Name = "mainMenu_File_Seperator1";
			this.mainMenu_File_Seperator1.Size = new System.Drawing.Size(201, 6);
			// 
			// mainMenu_File_Save
			// 
			this.mainMenu_File_Save.Name = "mainMenu_File_Save";
			this.mainMenu_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mainMenu_File_Save.Size = new System.Drawing.Size(204, 22);
			this.mainMenu_File_Save.Text = "Save";
			this.mainMenu_File_Save.Click += new System.EventHandler(this.mainMenu_File_Save_Click);
			// 
			// mainMenu_File_SaveAs
			// 
			this.mainMenu_File_SaveAs.Name = "mainMenu_File_SaveAs";
			this.mainMenu_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
									| System.Windows.Forms.Keys.S)));
			this.mainMenu_File_SaveAs.Size = new System.Drawing.Size(204, 22);
			this.mainMenu_File_SaveAs.Text = "Save As...";
			this.mainMenu_File_SaveAs.Click += new System.EventHandler(this.mainMenu_File_SaveAs_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
			// 
			// mainMenu_File_Exit
			// 
			this.mainMenu_File_Exit.Name = "mainMenu_File_Exit";
			this.mainMenu_File_Exit.Size = new System.Drawing.Size(204, 22);
			this.mainMenu_File_Exit.Text = "E&xit";
			this.mainMenu_File_Exit.Click += new System.EventHandler(this.mainMenu_File_Exit_Click);
			// 
			// mainMenu_Edit
			// 
			this.mainMenu_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenu_Edit_Undo});
			this.mainMenu_Edit.Name = "mainMenu_Edit";
			this.mainMenu_Edit.Size = new System.Drawing.Size(37, 20);
			this.mainMenu_Edit.Text = "Edit";
			// 
			// mainMenu_Edit_Undo
			// 
			this.mainMenu_Edit_Undo.Name = "mainMenu_Edit_Undo";
			this.mainMenu_Edit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.mainMenu_Edit_Undo.Size = new System.Drawing.Size(148, 22);
			this.mainMenu_Edit_Undo.Text = "Undo";
			this.mainMenu_Edit_Undo.Click += new System.EventHandler(this.mainMenu_Edit_Undo_Click);
			// 
			// mainMenu_Transform
			// 
			this.mainMenu_Transform.Name = "mainMenu_Transform";
			this.mainMenu_Transform.Size = new System.Drawing.Size(68, 20);
			this.mainMenu_Transform.Text = "Transform";
			// 
			// mainMenu_Process
			// 
			this.mainMenu_Process.Name = "mainMenu_Process";
			this.mainMenu_Process.Size = new System.Drawing.Size(56, 20);
			this.mainMenu_Process.Text = "Process";
			// 
			// imageDisplay
			// 
			this.imageDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageDisplay.Location = new System.Drawing.Point(0, 24);
			this.imageDisplay.Name = "imageDisplay";
			this.imageDisplay.Size = new System.Drawing.Size(529, 368);
			this.imageDisplay.TabIndex = 1;
			this.imageDisplay.TabStop = false;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Windows Bitmap (*.bmp)|*.bmp|JPEG (*.jpg, *.jpeg)|*.jpg;*.jpeg|All Supported Form" +
					"ats (*.bmp, *.jpg, *.jpeg)|*.bmp;*.jpg;*.jpeg";
			this.openFileDialog.FilterIndex = 3;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Windows Bitmap (*.bmp)|*.bmp|JPEG (*.jpg, *.jpeg)|*.jpg;*.jpeg";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(529, 392);
			this.Controls.Add(this.imageDisplay);
			this.Controls.Add(this.mainMenu);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "ImageProcessor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageDisplay)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File_Open;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_Edit;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File_Close;
    private System.Windows.Forms.ToolStripSeparator mainMenu_File_Seperator1;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File_Save;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File_SaveAs;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_File_Exit;
    private System.Windows.Forms.ToolStripMenuItem mainMenu_Transform;
		private System.Windows.Forms.ToolStripMenuItem mainMenu_Process;
		private System.Windows.Forms.PictureBox imageDisplay;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem mainMenu_Edit_Undo;
  }
}