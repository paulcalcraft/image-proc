
namespace ImageProcessor.ManipulatorInterfaces.Parameters
{
	public class IntegerParameter : ParameterBase
	{
		private int m_defaultValue;
		private int m_minimumValue;
		private int m_maximumValue;

		private int m_value;

		#region Public Properties
		public int DefaultValue
		{
			get { return m_defaultValue; }
		}

		public int MinimumValue
		{
			get { return m_minimumValue; }
		}

		public int MaximumValue
		{
			get { return m_maximumValue; }
		}

		public int Value
		{
			get { return m_value; }
			set { m_value = value; }
		}
		#endregion

		public IntegerParameter(string name, string help, int defaultValue, int minimumValue, int maximumValue)
			: base(name, help)
		{
			m_value = m_defaultValue = defaultValue;
			m_minimumValue = minimumValue;
			m_maximumValue = maximumValue;
		}

		public override void Reset()
		{
			m_value = m_defaultValue;
		}
	}
}
