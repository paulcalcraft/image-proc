using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	/// <summary>
	/// Find Edges brings out edges by adding up the differences between
	/// each surrounding pixel's colour and the centre pixel and using
	/// this as the new colour value. This works because big differences
	/// result in higher values, therefore lighter colours, while areas
	/// of low variance will be dark in colour.
	/// The addition operation is achieved here by mutlplying the average
	/// by the number of pixels the average was taken from. This is done to
	/// keep within the framework set by the application.
	/// </summary>
	public class FindEdges : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Find Edges"; }
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
			BitmapManipulator scratch = bitmap.GetScratchData();

			bm.BeginEdit();
			scratch.BeginRead();

			int height = bitmap.Height;
			int width = bitmap.Width;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte[,] pixels = scratch.GetSurroundingPixels(y, x, 1);
					byte[] result = PixelOperations.Average(pixels);

					int multiplier = pixels.Length / 3; // Length represents number of elements in entire multidimensional array, dividing by 3 gets the pixel count (3 components per pixel)

					byte b = scratch[y, x, ColourComponent.Blue];
					byte g = scratch[y, x, ColourComponent.Green];
					byte r = scratch[y, x, ColourComponent.Red];

					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(multiplier * (b - result[(int)ColourComponent.Blue]));
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(multiplier * (g - result[(int)ColourComponent.Green]));
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(multiplier * (r - result[(int)ColourComponent.Red]));
				}
			}

			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
