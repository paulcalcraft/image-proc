using System;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.Parameters;

namespace ImageProcessor.GUI.Parameters
{
	public partial class IntegerParameterGui : ParameterGuiBase
	{
		private IntegerParameter m_parameter;
		
		public IntegerParameterGui()
		{
			InitializeComponent();
		}
		
		public override void LoadParameter(ParameterBase parameter)
		{
			m_parameter = parameter as IntegerParameter;
			Reset();
		}
		
		private void Reset()
		{
			lblName.Text = m_parameter.Name;
			new ToolTip().SetToolTip(lblName, m_parameter.Help);

			txtValue.Text = m_parameter.Value.ToString();
		}

		public override bool ValidateInput(out string error)
		{
			int input;
			
			try
			{
				input = Int32.Parse(txtValue.Text);
			}
			catch
			{
				error = "Not a valid number.";
				return false;
			}
			
			if (input > m_parameter.MaximumValue || input < m_parameter.MinimumValue)
			{
				error = "Number must be between " + m_parameter.MinimumValue + " and " + m_parameter.MaximumValue + ".";
				return false;
			}
			
			error = null;
			return true;
		}
		
		public override void CommitParameter()
		{
			m_parameter.Value = Int32.Parse(txtValue.Text);
		}

		public override Type GetUnderlyingType()
		{
			return typeof(IntegerParameter);
		}
	}
}
