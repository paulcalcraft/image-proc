using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	public class Saturation : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Adjust Saturation"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		private IntegerRangeParameter param_saturation = new IntegerRangeParameter("Saturation", "Controls the strength of the de/saturation. A value of -100 fully desaturates the image.", 0, -100, 1, 100);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_saturation }; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;

			bm.BeginEdit();

			int height = bitmap.Height;
			int width = bitmap.Width;
			float saturation = param_saturation.Value * 0.01f;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte b = bm[y, x, ColourComponent.Blue];
					byte g = bm[y, x, ColourComponent.Green];
					byte r = bm[y, x, ColourComponent.Red];
					
					byte result =
						PixelOperations.ClampColourComponent((b + g +
																									r) / 3);

					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(b + saturation * (b - result));
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(g + saturation * (g - result));
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(r + saturation * (r - result));
				}
			}

			bm.EndEdit();
		}
	}
}
