using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	public class Contrast : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Adjust Contrast"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		private IntegerRangeParameter param_contrast = new IntegerRangeParameter("Contrast", "Controls the strength of contrast adjustment.", 0, -100, 1, 100);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_contrast }; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;

			bm.BeginEdit();

			int height = bitmap.Height;
			int width = bitmap.Width;
			float contrast = param_contrast.Value * 0.01f;

			long[] total = new long[3] {0, 0, 0};
			int pixelCount = 0;
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					total[(int) ColourComponent.Blue] += bm[y, x, ColourComponent.Blue];
					total[(int)ColourComponent.Green] += bm[y, x, ColourComponent.Green];
					total[(int)ColourComponent.Red] += bm[y, x, ColourComponent.Red];
					pixelCount++;
				}
			}

			byte[] average = new byte[]
				{
					PixelOperations.ClampColourComponent(total[0]/(float)pixelCount),
					PixelOperations.ClampColourComponent(total[1]/(float)pixelCount),
					PixelOperations.ClampColourComponent(total[2]/(float)pixelCount)
				};

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte b = bm[y, x, ColourComponent.Blue];
					byte g = bm[y, x, ColourComponent.Green];
					byte r = bm[y, x, ColourComponent.Red];

					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(b + contrast * (b - average[(int)ColourComponent.Blue]));
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(g + contrast * (g - average[(int)ColourComponent.Green]));
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(r + contrast * (r - average[(int)ColourComponent.Red]));
				}
			}

			bm.EndEdit();
		}
	}
}
