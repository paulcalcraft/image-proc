using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	public class Blur : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Blur"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		private IntegerRangeParameter param_radius = new IntegerRangeParameter("Radius", "Controls the radius of the blurring effect.", 2, 1, 1, 10);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_radius }; }
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
			int radius = param_radius.Value;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte[] result = PixelOperations.Average(scratch.GetSurroundingPixels(y, x, radius));
					bm[y, x, ColourComponent.Blue] = result[(int)ColourComponent.Blue];
					bm[y, x, ColourComponent.Green] = result[(int)ColourComponent.Green];
					bm[y, x, ColourComponent.Red] = result[(int)ColourComponent.Red];
				}
			}

			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
