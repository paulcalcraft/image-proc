using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	public class Sharpen : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Sharpen"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		private IntegerRangeParameter param_intensity = new IntegerRangeParameter("Intensity", "Controls how much to amplify colour differences.", 50, 1, 1, 100);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_intensity }; }
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
			float intensity = param_intensity.Value * 0.1f;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte[,] pixels = scratch.GetSurroundingPixels(y, x, 1);
					byte[] result = PixelOperations.Average(pixels);

					byte b = scratch[y, x, ColourComponent.Blue];
					byte g = scratch[y, x, ColourComponent.Green];
					byte r = scratch[y, x, ColourComponent.Red];

					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(b + intensity * (b - result[(int)ColourComponent.Blue]));
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(g + intensity * (g - result[(int)ColourComponent.Green]));
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(r + intensity * (r - result[(int)ColourComponent.Red]));
				}
			}

			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
