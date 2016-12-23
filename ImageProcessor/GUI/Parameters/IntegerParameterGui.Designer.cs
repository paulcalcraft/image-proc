namespace ImageProcessor.GUI.Parameters
{
	partial class IntegerParameterGui
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtValue = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(208, 8);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(80, 20);
			this.txtValue.TabIndex = 1;
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(14, 8);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(188, 20);
			this.lblName.TabIndex = 2;
			// 
			// IntegerParameterGui
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtValue);
			this.Name = "IntegerParameterGui";
			this.Size = new System.Drawing.Size(420, 40);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Label lblName;
	}
}
