using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.ManipulatorInterfaces.BitmapInfo
{
	/// <summary>
	/// Encapsulates the storage and management of a manipulatable image.
	/// Creates and tracks BitmapManipulators which allow external classes
	/// to modify and read the bitmap data in a controlled way.
	/// Provides events and public methods to interact with GUI.
	/// </summary>
	public class BitmapStore
	{
		/// <summary>
		/// Raised when the underlying bitmap data is changed.
		/// </summary>
		public event EventHandler BitmapChanged;

		#region Public Properties
		/// <summary>
		/// Provides access to the current Bitmap object so it can be displayed.
		/// </summary>
		public Bitmap Bitmap
		{
			get { return m_bitmap; }
		}

		public bool Loaded
		{
			get { return m_bitmap != null; }
		}
		
		public bool Modified
		{
			get { return m_modified; }
		}
		
		public string FileName
		{
			get { return m_fileName; }
		}
		#endregion

		#region Bitmap Properties
		public int Width
		{
			get { return m_bitmap.Width; }
		}
		
		public int Height
		{
			get { return m_bitmap.Height; }
		}
		#endregion

		private string m_fileName;
		private Bitmap m_bitmap;
		private BitmapManipulator m_directManipulator;
		private List<BitmapManipulator> m_accessors = new List<BitmapManipulator>(); // A list of the current BitmapManipulators on loan to external classes
		private bool m_modified;

		#region Constructor
		/// <summary>
		/// Creates an empty BitmapStore with no image loaded.
		/// </summary>
		public BitmapStore() { }
		
		/// <summary>
		/// Creates a BitmapStore loaded with the image from the given path.
		/// </summary>
		/// <param name="fileName">Path of the image file.</param>
		public BitmapStore(string fileName)
		{
			Load(fileName);
		}
		#endregion

		#region Un/Loading
		/// <summary>
		/// Loads an image into the BitmapStore from a file on disk.
		/// Converts the PixelFormat to standard 24bppRgb. Constructs the
		/// image resources and sets the file status to unmodified.
		/// </summary>
		/// <param name="fileName"></param>
		public void Load(string fileName)
		{
			m_fileName = fileName;
			Bitmap bitmap = new Bitmap(Bitmap.FromFile(m_fileName, false));
			
			if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
				bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);

			ConstructResources(bitmap);
			
			m_modified = false;
		}
		
		/// <summary>
		/// If the given bitmap is of a valid format, sets it as the current
		/// image of the BitmapStore, and initialises associated resources.
		/// </summary>
		/// <param name="bitmap"></param>
		private void ConstructResources(Bitmap bitmap)
		{
			if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
				throw new Exception("Invalid pixel format.");
			
			m_bitmap = bitmap;
			m_directManipulator = new BitmapManipulator(m_bitmap, true, false);
			m_directManipulator.BitmapPostEdit += new EventHandler(m_directManipulator_BitmapPostEdit);
		}
		
		/// <summary>
		/// Destructs resources and unsets the previous state.
		/// </summary>
		public void Unload()
		{
			DestructResources();
			m_previousState = null;
		}
		
		/// <summary>
		/// Frees the current bitmap and bitmap manipulator references,
		/// and clears all current accessors.
		/// </summary>
		private void DestructResources()
		{
			m_bitmap = null;
			m_directManipulator = null;
			m_accessors.Clear();
		}
		#endregion

		#region Saving
		/// <summary>
		/// Saves the current bitmap back to the file it was opened from.
		/// </summary>
		public void Save()
		{
			m_bitmap.Save(m_fileName);
			m_modified = false;
		}
		
		/// <summary>
		/// Sets a new file name and then saves to this new location.
		/// </summary>
		/// <param name="fileName">The path and file name of the new loction.</param>
		public void SaveAs(string fileName)
		{
			m_fileName = fileName;
			Save();
		}
		#endregion

		#region Backup
		private Bitmap m_previousState; // The previous state image
		private bool m_previousStateModified; // The previous state's modified status
		
		/// <summary>
		/// Indicates whether the image can be reverted to a previous state.
		/// </summary>
		public bool CanRevert
		{
			get { return m_previousState != null; }
		}
		
		/// <summary>
		/// Backs up the current image to the previous state storage.
		/// </summary>
		public void Backup()
		{
			m_previousState = m_bitmap.Clone(new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height), PixelFormat.Format24bppRgb);
			m_previousStateModified = m_modified;
		}
		
		/// <summary>
		/// Swaps two objects between references of parameter type T.
		/// </summary>
		/// <typeparam name="T">The type of the objects to swap.</typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		private void Swap<T>(ref T first, ref T second)
		{
			T temp = first; // Create a new reference of type T to reference the first
			first = second; // Set the first to refer to the second
			second = temp; // Set the second to refer to the first by using the temp reference of first
		}
		
		/// <summary>
		/// Swaps the current state and the previous state, so the image
		/// is reverted.
		/// Reconstructs resources and raises the BitmapChanged event to
		/// inform external classes of the change to the image.
		/// </summary>
		public void Revert()
		{
			if (m_previousState == null)
				throw new Exception("There is no data to revert to.");
			
			Swap<bool>(ref m_modified, ref m_previousStateModified);
			Bitmap bitmap = m_bitmap;
			Swap<Bitmap>(ref bitmap, ref m_previousState);
			
			DestructResources();
			ConstructResources(bitmap);

			if (BitmapChanged != null)
				BitmapChanged(null, null);
		}
		#endregion

		#region Manipulation
		/// <summary>
		/// Provides an accessor to a read-writable BitmapManipulator
		/// representing the underlying image of the BitmapStore.
		/// </summary>
		public BitmapManipulator DirectManipulator
		{
			get { return m_directManipulator; }
		}
		
		/// <summary>
		/// Creates a read-only accessor to the bitmap data for external
		/// "scratch usage". Adds a reference to the list of accessors
		/// to keep track of it.
		/// </summary>
		/// <returns>The image accessor.</returns>
		public BitmapManipulator GetScratchData()
		{
			BitmapManipulator accessor = new BitmapManipulator(m_bitmap, false, true);
			m_accessors.Add(accessor);
			return accessor;
		}
		
		/// <summary>
		/// Removes the internal reference to the given accessor as it
		/// is no longer needed by the entity that requested it.
		/// </summary>
		/// <param name="accessor">The accessor that is no longer required.</param>
		public void DisposeScratchData(BitmapManipulator accessor)
		{
			m_accessors.Remove(accessor);	
		}
		
		/// <summary>
		/// Executed after the main writable BitmapManipulator has been
		/// used to edit the underlying image of the BitmapStore.
		/// Sets the state to modified and raises an event to notify external
		/// classes of the change.
		/// </summary>
		/// <param name="sender">A reference to the new Bitmap object.</param>
		/// <param name="e"></param>
		private void m_directManipulator_BitmapPostEdit(object sender, EventArgs e)
		{
			m_bitmap = sender as Bitmap;
			m_modified = true;
			
			if (BitmapChanged != null)
				BitmapChanged(sender, null);
		}
		#endregion
	}
}
