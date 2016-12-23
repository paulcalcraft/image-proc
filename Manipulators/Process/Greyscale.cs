using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	/// <summary>
	/// This process fully desaturates the image. It averages
	/// each colour component of a given pixel, and sets each
	/// component to this component average.
	/// </summary>
	public class Greyscale : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Greyscale"; }
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
					byte b = bm[y, x, ColourComponent.Blue];
					byte g = bm[y, x, ColourComponent.Green];
					byte r = bm[y, x, ColourComponent.Red];

					byte average = PixelOperations.ClampColourComponent((b + g + r)/3);
					
					bm[y, x, ColourComponent.Blue] = average;
					bm[y, x, ColourComponent.Green] = average;
					bm[y, x, ColourComponent.Red] = average;
				}
			}

			bm.EndEdit();
		}
	}
}
