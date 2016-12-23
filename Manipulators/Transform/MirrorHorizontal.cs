using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;

namespace ImageProcessor.Manipulators.Process
{
	public class MirrorHorizontal : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Mirror Horizontal"; }
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
			int xmax = width - 1;

			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
					bm[y, x] = scratch[y, xmax - x];

			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
