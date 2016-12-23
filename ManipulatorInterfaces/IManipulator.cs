using ImageProcessor.ManipulatorInterfaces.BitmapInfo;

namespace ImageProcessor.ManipulatorInterfaces
{
	public enum ManipulatorType
	{
		Process,
		Transform
	}
	
	public interface IManipulator
	{
		/// <summary>
		/// The name of the manipulator.
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// Whether the manipulator is a transform or a process.
		/// </summary>
		ManipulatorType Type { get; }
		
		/// <summary>
		/// An array of parameters for this manipulator, all objects
		/// in the array should be subclasses of ParameterBase.
		/// </summary>
		ParameterBase[] Parameters { get; }

		/// <summary>
		/// The method that executes the manipulation on a given image.
		/// </summary>
		/// <param name="bitmap">The BitmapStore to execute the manipulation on.</param>
		void Execute(BitmapStore bitmap);
	}
}