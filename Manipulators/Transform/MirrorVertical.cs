using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;

namespace ImageProcessor.Manipulators.Process
{
	public class MirrorVertical : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Mirror Vertical"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Transform; }
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
			int ymax = height - 1;

			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
					bm[y, x] = scratch[ymax - y, x];

			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
