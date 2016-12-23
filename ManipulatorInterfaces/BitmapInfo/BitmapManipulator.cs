using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor.ManipulatorInterfaces.BitmapInfo
{
	public unsafe class BitmapManipulator
	{
		public event EventHandler BitmapPreEdit;
		public event EventHandler BitmapPostEdit;

		private Bitmap m_bitmap;
		private bool m_readOnly;
		private BitmapData m_bitmapData;
		private int m_width, m_height;
		private byte *m_rawData = null;

		public BitmapManipulator(Bitmap bitmap, bool takeReference, bool readOnly)
		{
			m_readOnly = readOnly;
			
			if (takeReference)
				m_bitmap = bitmap;
			else
				m_bitmap = bitmap.Clone(new Rectangle(new Point(0, 0), bitmap.Size), PixelFormat.Format24bppRgb);

			m_width = bitmap.Width;
			m_height = bitmap.Height;
			
			if (m_bitmap.PixelFormat != PixelFormat.Format24bppRgb)
				throw new Exception("Unable to convert pixel format.");
		}
		
		~BitmapManipulator()
		{
			End();
		}
		
		#region Begin/End Data Locking
		private void Begin(ImageLockMode mode)
		{
			if (m_bitmapData != null)
				throw new Exception("The bitmap data is being used by another process branch.");
			
			m_bitmapData = m_bitmap.LockBits(new Rectangle(new Point(0, 0), m_bitmap.Size),
																			 mode,
																			 m_bitmap.PixelFormat);

			m_rawData = (byte*)m_bitmapData.Scan0;
		}

		private void End()
		{
			if (m_bitmapData == null)
				return;

			m_bitmap.UnlockBits(m_bitmapData);
			m_bitmapData = null;
			m_rawData = null;
		}
		#endregion

		#region Read
		public void BeginRead()
		{
			Begin(ImageLockMode.ReadOnly);
		}
		
		public void EndRead()
		{
			End();
		}
		#endregion

		#region Edit
		public void BeginEdit()
		{
			if (m_readOnly)
				throw new Exception("Attempted to edit read only bitmap data.");
			if (BitmapPreEdit != null)
				BitmapPreEdit(null, null);
			Begin(ImageLockMode.ReadWrite);
		}
		
		public void EndEdit()
		{
			End();
			if (BitmapPostEdit != null)
				BitmapPostEdit(m_bitmap, null);
		}
		#endregion

		#region Access
		public void Resize(Size newSize)
		{
			if (m_bitmapData == null || m_readOnly)
				throw new Exception("Attempted resize while not in editing mode.");
			
			End();
			Bitmap temp = new Bitmap(m_bitmap, newSize);
			m_bitmap = temp.Clone(new Rectangle(new Point(0, 0), newSize), PixelFormat.Format24bppRgb);
			Begin(ImageLockMode.ReadWrite);
		}
		
		public void CreateNewCanvas(Size newSize)
		{
			if (m_bitmapData == null || m_readOnly)
				throw new Exception("Attempted canvas creation while not in editing mode.");

			End();
			m_bitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb);
			Begin(ImageLockMode.ReadWrite);
		}
		
		public byte this[int row, int column, ColourComponent component]
		{
			get
			{
				int offset = row * m_bitmapData.Stride + column * 3 + (int)component;
				return m_rawData[offset];
			}
			set
			{
				int offset = row * m_bitmapData.Stride + column * 3 + (int)component;
				m_rawData[offset] = value;
			}
		}

		public byte[] this[int row, int column]
		{
			get
			{
				int offset = row * m_bitmapData.Stride + column * 3;
				byte[] colour = new byte[3];
				
				for (int i = 0; i < 3; i++)
					colour[i] = m_rawData[offset + i];
				
				return colour;
			}
			set
			{
				int offset = row * m_bitmapData.Stride + column * 3;

				for (int i = 0; i < 3; i++)
					m_rawData[offset + i] = value[i];
			}
		}
		
		public byte this[int offset]
		{
			get
			{
				return m_rawData[offset];
			}
			set
			{
				m_rawData[offset] = value;
			}
		}

		public byte[,] GetSurroundingPixels(int row, int column, int radius)
		{
			int xmin = column - radius;
			if (xmin < 0)
				xmin = 0;

			int xmax = column + radius;
			if (xmax > m_width - 1)
				xmax = m_width - 1;

			int ymin = row - radius;
			if (ymin < 0)
				ymin = 0;

			int ymax = row + radius;
			if (ymax > m_height - 1)
				ymax = m_height - 1;
			
			int pixelCount = (ymax - ymin + 1)*(xmax - xmin + 1) - 1;
			byte[,] pixels = new byte[pixelCount, 3];

			int index = 0;
			for (int y = ymin; y <= ymax; y++)
			{
				for (int x = xmin; x <= xmax; x++)
				{
					if (y == row && x == column)
						continue;
					pixels[index, (int)ColourComponent.Blue] = this[y, x, ColourComponent.Blue];
					pixels[index, (int)ColourComponent.Green] = this[y, x, ColourComponent.Green];
					pixels[index, (int)ColourComponent.Red] = this[y, x, ColourComponent.Red];
					index++;
				}
			}
			
			return pixels;
		}
		#endregion
}
	
	public enum ColourComponent
	{
		Blue,
		Green,
		Red
	}
}
