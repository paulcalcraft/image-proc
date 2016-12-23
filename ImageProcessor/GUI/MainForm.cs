using System;
using System.IO;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.Manipulators;

namespace ImageProcessor.GUI
{
  public partial class MainForm : Form
  {
  	/// <summary>
  	/// Initialises GUI components and adds an event handler to the
  	/// BitmapChanged event of the current image BitmapStore.
  	/// </summary>
    public MainForm()
    {
      InitializeComponent();
			m_currentImage.BitmapChanged += new EventHandler(m_currentImage_BitmapChanged);
    }

		private BitmapStore m_currentImage = new BitmapStore();

  	/// <summary>
  	/// Executed upon loading of the application, this method
  	/// loads the manipulator repository, creates a mapping between
  	/// parameter classes and their guis, and populates the form's
  	/// menus with the manipulators.
  	/// </summary>
  	/// <param name="sender">The form object.</param>
  	/// <param name="e">Other event arguments.</param>
    private void MainForm_Load(object sender, EventArgs e)
    {
      Repository.Load(); // Load the manipulator repository
			ParameterDialog.MapParameterGuis(); // Set up backend to gui mapping for late binding

      foreach (IManipulator m in Repository.Manipulators)
      {
        // Assign the Manipulator to a Menu in the UI
        ToolStripMenuItem parentMenu = null;
        switch (m.Type)
        {
          case ManipulatorType.Process: // This manipulator is an image process
            parentMenu = mainMenu_Process; // Assign the parent menu as the Process menu
            break;
          case ManipulatorType.Transform: // This manipulator is an image transform
            parentMenu = mainMenu_Transform; // Assign the parent menu as the Transform menu
            break;
        }

        if (parentMenu == null) // If the manipulator has not been assigned to a menu, move on
          continue;

        // Create the menu item for this manipulator
        ToolStripMenuItem item = new ToolStripMenuItem(m.Name);
        item.Tag = m; // Associate the manipulator object with the menu item
        item.Click += new EventHandler(manipulatorItem_Click); // Set the event handler for the Click event
      	
      	parentMenu.DropDown.Items.Add(item); // Add the menu item to the parent menu
      }

			UpdateGUI();
    }
  	
    /// <summary>
    /// Executed when a manipulator item is selected through the menu system.
    /// Resets parameters to their default values. Creates and
    /// shows a dialog with parameters where appropriate, then
    /// executes the manipulation.
    /// </summary>
    /// <param name="sender">The menu item that has been clicked.</param>
    /// <param name="e">Other event arguments.</param>
    private void manipulatorItem_Click(object sender, EventArgs e)
    {
      IManipulator m = (sender as ToolStripItem).Tag as IManipulator;

			ParameterBase.ResetParameters(m.Parameters);
    	
			if (ParameterDialog.ShowDialog(m) != DialogResult.OK)
				return;

			Application.DoEvents(); // Forces the application to redraw the underlying window after the dialog has been closed

			Cursor.Current = Cursors.WaitCursor; // Show the egg timer waiting cursor while the manipulation executes

			m_currentImage.Backup();
			m.Execute(m_currentImage);

			Cursor.Current = Cursors.Arrow; // Revert cursor to normal arrow after manipulation is complete
		}

		#region mainMenu_File
  	/// <summary>
  	/// Executed when File > Open is clicked.
  	/// Shows an open file dialog and loads a file if one is selected.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_File_Open_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog.ShowDialog();
			if (result != DialogResult.OK)
				return;
			
			m_currentImage.Load(openFileDialog.FileName);
			UpdateGUI();
		}
  	
  	/// <summary>
		/// Executed when File > Close is clicked.
		/// First checks if the user wants to save changes, if there are
		/// any. Then unloads the image from the BitmapStore and updates the GUI
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_File_Close_Click(object sender, EventArgs e)
		{
			if (!ConfirmClose())
					return;
			
			m_currentImage.Unload();
			UpdateGUI();
		}

  	/// <summary>
		/// Executed when File > Save is clicked or when the user chooses to save
		/// via the ConfirmClose() dialog.
  	/// Saves the current image back to the file it was opened from.
		/// Updates the form text to reflect that the image is no longer in
		/// a modified state.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_File_Save_Click(object sender, EventArgs e)
		{
			m_currentImage.Save();
			UpdateFormText();
		}

  	/// <summary>
		/// Executed when File > Save As is clicked.
  	/// Saves the current image to a new file name after prompting the
  	/// user for the new location via a save file dialog.
  	/// Updates the form text to reflect that the image is no longer in
  	/// a modified state.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_File_SaveAs_Click(object sender, EventArgs e)
		{
			DialogResult result = saveFileDialog.ShowDialog();

			if (result != DialogResult.OK)
				return;
  		
			m_currentImage.SaveAs(saveFileDialog.FileName);
			UpdateFormText();
		}

  	/// <summary>
		/// Executed when File > Exit is clicked.
  	/// Confirms whether to close the application, and if so, exits.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_File_Exit_Click(object sender, EventArgs e)
		{
			if (ConfirmClose())
				Application.Exit();
		}
		#endregion

		#region mainMenu_Edit
  	/// <summary>
		/// Executed when Edit > Undo is clicked.
		/// Uses the BitmapStore class functionality to revert the image
		/// to its previous state.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void mainMenu_Edit_Undo_Click(object sender, EventArgs e)
		{
			m_currentImage.Revert();
		}
		#endregion

  	/// <summary>
  	/// Confirms whether the current image should be closed. If there
  	/// is an image loaded which has unsaved changes, it will prompt
  	/// the user, asking if they wish to save.
  	/// </summary>
  	/// <returns>Whether to continue with closing the image.</returns>
		private bool ConfirmClose()
  	{
  		if (!m_currentImage.Loaded || !m_currentImage.Modified)
  			return true;
  		
			DialogResult result =
				MessageBox.Show("Save changes to the image before closing?", Text,
				                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

  		if (result == DialogResult.Yes)
				m_currentImage.Save();
  		
			return result != DialogResult.Cancel;
  	}
  	
  	/// <summary>
  	/// Updates the title of the form to indicate whether there is
  	/// currently an image loaded, and what its file name is, and
  	/// whether it is modified.
  	/// </summary>
  	private void UpdateFormText()
  	{
			Text = (m_currentImage.Loaded
							? " " + Path.GetFileName(m_currentImage.FileName) + (m_currentImage.Modified ? "*" : "") + " - "
							: "") + "ImageProcessor";
  	}
  	
  	/// <summary>
  	/// Executed after the current image has been modified, causes
  	/// the image display to redraw the current image, and updates
  	/// the form text and undo button depending on whether the image
  	/// is now in a modified state.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e"></param>
		private void m_currentImage_BitmapChanged(object sender, EventArgs e)
		{
			imageDisplay.Image = m_currentImage.Bitmap;
			imageDisplay.Invalidate();
			mainMenu_Edit_Undo.Enabled = m_currentImage.CanRevert;
			UpdateFormText();
		}
  	
  	/// <summary>
  	/// Updates all elements of the GUI according to the application's
  	/// current status. This includes enabling/disabling menu items,
  	/// updating the form title, and refreshing the image display.
  	/// </summary>
  	private void UpdateGUI()
  	{
			imageDisplay.Image = m_currentImage.Bitmap;
			imageDisplay.Invalidate();

			UpdateFormText();
  		
			mainMenu_File_Open.Enabled = !m_currentImage.Loaded;
			mainMenu_File_Save.Enabled = m_currentImage.Loaded;
			mainMenu_File_SaveAs.Enabled = m_currentImage.Loaded;
			mainMenu_File_Close.Enabled = m_currentImage.Loaded;

			mainMenu_Edit_Undo.Enabled = m_currentImage.CanRevert;
  		
			foreach (ToolStripItem i in mainMenu_Transform.DropDown.Items)
				i.Enabled = m_currentImage.Loaded;

			foreach (ToolStripItem i in mainMenu_Process.DropDown.Items)
				i.Enabled = m_currentImage.Loaded;
  	}

  	/// <summary>
  	/// Executed as the user attempts to close the application.
  	/// Checks if a modified image is loaded with ConfirmClose(),
  	/// cancels the closing of the form if the uses chooses to.
  	/// </summary>
  	/// <param name="sender"></param>
  	/// <param name="e">Event arguments used to cancel the form close.</param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!ConfirmClose())
				e.Cancel = true;
		}
	}
}
