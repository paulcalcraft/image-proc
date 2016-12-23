using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	/// <summary>
	/// This process creates a negative of the image by
	/// subtracting each colour component value from the
	/// maximum value of that component (255 for the pixel
	/// format used by this application).
	/// </summary>
	public class Invert : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Invert"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		ParameterBase[] IManipulator.Parameters
		{
			get { return null; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;

			bm.BeginEdit();

			int height = bitmap.Height;
			int width = bitmap.Width;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(255 - bm[y, x, ColourComponent.Red]);
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(255 - bm[y, x, ColourComponent.Green]);
					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(255 - bm[y, x, ColourComponent.Blue]);
				}
			}
			
			bm.EndEdit();
		}
	}
}