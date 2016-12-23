
namespace ImageProcessor.ManipulatorInterfaces
{
	public abstract class ParameterBase
	{
		private string m_name;
		private string m_help;

		#region Public Properties
		/// <summary>
		/// The name of the parameter in its context.
		/// </summary>
		public string Name
		{
			get { return m_name; }
		}

		/// <summary>
		/// A help string explaining boundaries and usage.
		/// </summary>
		public string Help
		{
			get { return m_help; }
		}
		#endregion

		public ParameterBase(string name, string help)
		{
			m_name = name;
			m_help = help;
		}

		/// <summary>
		/// Resets the current value of the parameter to default.
		/// </summary>
		public abstract void Reset();
		
		/// <summary>
		/// Resets an array of parameters using the base class's interface.
		/// </summary>
		/// <param name="parameters">The array of parameters to reset.</param>
		public static void ResetParameters(ParameterBase[] parameters)
		{
			if (parameters == null)
				return;

			foreach (ParameterBase parameter in parameters)
				parameter.Reset();
		}
	}
}
