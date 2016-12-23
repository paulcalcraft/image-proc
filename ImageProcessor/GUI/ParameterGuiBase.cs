using System;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;

namespace ImageProcessor.GUI
{
	public abstract class ParameterGuiBase : UserControl
	{
		/// <summary>
		/// Loads the backend data parameter instance into the GUI display.
		/// </summary>
		/// <param name="parameter">The parameter to load.</param>
		public abstract void LoadParameter(ParameterBase parameter);
		
		/// <summary>
		/// Validates the value currently held by the GUI.
		/// </summary>
		/// <param name="error">A reason why the input is invalid, if the method returns false.</param>
		/// <returns>Whether the current value is valid.</returns>
	  public abstract bool ValidateInput(out string error);
		
		/// <summary>
		/// Commits the value of the GUI to the backend data instance.
		/// </summary>
		public abstract void CommitParameter();
		
		/// <summary>
		/// Exposes what backend data type the GUI component supports.
		/// </summary>
		/// <returns>The Type reference of the backend data class that the GUI supports.</returns>
	  public abstract Type GetUnderlyingType();
	}
}
