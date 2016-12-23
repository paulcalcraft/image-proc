using System;

namespace ImageProcessor.ManipulatorInterfaces.Parameters
{
	public class EnumerationParameter : ParameterBase
	{
		private string[] m_enumeration;
		
		private int m_value;

		#region Public Properties
		public string[] Enumeration
		{
			get { return m_enumeration; }
		}

		public int Value
		{
			get { return m_value; }
			set { m_value = value; }
		}
		#endregion

		public EnumerationParameter(string name, string help, Type enumeration)
			: base(name, help)
		{
			m_value = 0;
			m_enumeration = Enum.GetNames(enumeration);
			for (int i = 0; i < m_enumeration.Length; i++)
				m_enumeration[i] = m_enumeration[i].Remove(0, 3).Replace("_", " ");
		}

		public override void Reset()
		{
			m_value = 0;
		}
	}
}
