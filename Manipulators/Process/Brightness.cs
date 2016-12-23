using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;
using ImageProcessor.Manipulators.Utility;

namespace ImageProcessor.Manipulators.Process
{
	public class Brightness : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Adjust Brightness"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Process; }
		}
		#endregion

		#region Parameters
		private IntegerRangeParameter param_brightness = new IntegerRangeParameter("Brightness", "Controls the strength of the brightening or darkening effect.", 0, -100, 1, 100);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_brightness }; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;

			bm.BeginEdit();

			int height = bitmap.Height;
			int width = bitmap.Width;
			int brightness = param_brightness.Value;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					byte b = bm[y, x, ColourComponent.Blue];
					byte g = bm[y, x, ColourComponent.Green];
					byte r = bm[y, x, ColourComponent.Red];
					
					bm[y, x, ColourComponent.Blue] = PixelOperations.ClampColourComponent(b + brightness);
					bm[y, x, ColourComponent.Green] = PixelOperations.ClampColourComponent(g + brightness);
					bm[y, x, ColourComponent.Red] = PixelOperations.ClampColourComponent(r + brightness);
				}
			}

			bm.EndEdit();
		}
	}
}
