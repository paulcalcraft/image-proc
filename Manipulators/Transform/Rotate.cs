using System.Drawing;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.BitmapInfo;
using ImageProcessor.ManipulatorInterfaces.Parameters;

namespace ImageProcessor.Manipulators.Process
{
	public class Rotate : IManipulator
	{
		#region IManipulator Properties
		string IManipulator.Name
		{
			get { return "Rotate"; }
		}

		ManipulatorType IManipulator.Type
		{
			get { return ManipulatorType.Transform; }
		}
		#endregion
		
		#region Parameters
		enum Rotation
		{
			opt90_degrees_Clockwise,
			opt90_degrees_Anticlockwise,
			opt180_degrees
		}
		private EnumerationParameter param_rotation = new EnumerationParameter("Rotation", "The type of rotation to apply.", typeof(Rotation));
		
		ParameterBase[] IManipulator.Parameters
		{
			get { return new ParameterBase[] { param_rotation }; }
		}
		#endregion

		void IManipulator.Execute(BitmapStore bitmap)
		{
			BitmapManipulator bm = bitmap.DirectManipulator;
			BitmapManipulator scratch = bitmap.GetScratchData();

			bm.BeginEdit();
			scratch.BeginRead();

			Rotation rotation = (Rotation)param_rotation.Value;
			
			int height = bitmap.Height;
			int width = bitmap.Width;
			int ymax = height - 1;
			int xmax = width - 1;

			int newWidth, newHeight;
			
			switch (rotation)
			{
				case Rotation.opt180_degrees:
					for (int y = 0; y < height; y++)
						for (int x = 0; x < width; x++)
							bm[y, x] = scratch[ymax - y, xmax - x];
					break;
				case Rotation.opt90_degrees_Clockwise:
					newWidth = height;
					newHeight = width;
					bm.CreateNewCanvas(new Size(newWidth, newHeight));
					
					for (int y = 0; y < newHeight; y++)
						for (int x = 0; x < newWidth; x++)
							bm[y, x] = scratch[ymax - x, y];
					break;
				case Rotation.opt90_degrees_Anticlockwise:
					newWidth = height;
					newHeight = width;
					bm.CreateNewCanvas(new Size(newWidth, newHeight));

					for (int y = 0; y < newHeight; y++)
						for (int x = 0; x < newWidth; x++)
							bm[y, x] = scratch[x, xmax - y];
					break;
			}
			
			scratch.EndRead();
			bm.EndEdit();

			bitmap.DisposeScratchData(scratch);
		}
	}
}
