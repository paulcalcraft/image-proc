namespace ImageProcessor.GUI
{
	partial class ParameterDialog
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
			this.parameterDialogButtons = new ImageProcessor.GUI.ParameterDialogButtons();
			this.SuspendLayout();
			// 
			// parameterDialogButtons
			// 
			this.parameterDialogButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.parameterDialogButtons.Location = new System.Drawing.Point(0, 0);
			this.parameterDialogButtons.Name = "parameterDialogButtons";
			this.parameterDialogButtons.Size = new System.Drawing.Size(422, 40);
			this.parameterDialogButtons.TabIndex = 0;
			// 
			// ParameterDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(422, 39);
			this.Controls.Add(this.parameterDialogButtons);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ParameterDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "ParameterDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ParameterDialog_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private ParameterDialogButtons parameterDialogButtons;
	}
}