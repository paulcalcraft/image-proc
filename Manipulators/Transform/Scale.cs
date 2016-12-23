using System.Drawing;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;

namespace ImageProcessor.Manipulators.Process
{
	public class Scale : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Scale"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Transform; }
		}
		#endregion

		#region Parameters
		private IntegerParameter param_width = new IntegerParameter("Width (%)", "A percentage between 1 and 1000 to scale the image width by.", 100, 1, 1000);
		private IntegerParameter param_height = new IntegerParameter("Height (%)", "A percentage between 1 and 1000 to scale the image height by.", 100, 1, 1000);

		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_width, param_height }; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;

			bm.BeginEdit();

			int newWidth = (int)(param_width.Value * 0.01f * bitmap.Width + 0.5f); // int cast truncates so add 0.5f to round
			int newHeight = (int)(param_height.Value * 0.01f * bitmap.Height + 0.5f); // int cast truncates so add 0.5f to round
			
			bm.Resize(new Size(newWidth, newHeight));
			
			bm.EndEdit();
		}
	}
}
