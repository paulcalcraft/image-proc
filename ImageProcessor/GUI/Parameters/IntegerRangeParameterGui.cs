using System;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.Parameters;

namespace ImageProcessor.GUI.Parameters
{
	public partial class IntegerRangeParameterGui : ParameterGuiBase
	{
		private IntegerRangeParameter m_parameter;
		
		public IntegerRangeParameterGui()
		{
			InitializeComponent();
			txtValue.DataBindings.Add("Text", trkValue, "Value");
		}
		
		public override void LoadParameter(ParameterBase parameter)
		{
			m_parameter = parameter as IntegerRangeParameter;
			Reset();
		}
		
		private void Reset()
		{
			lblName.Text = m_parameter.Name;
			new ToolTip().SetToolTip(lblName, m_parameter.Help);

			trkValue.Minimum = m_parameter.MinimumValue;
			trkValue.SmallChange = m_parameter.Step;
			trkValue.LargeChange = m_parameter.Step*4;
			trkValue.Maximum = m_parameter.MaximumValue;
			trkValue.Value = m_parameter.Value;
		}

		public override bool ValidateInput(out string error)
		{
			// A ranged value selected via a trackbar cannot be invalid, so always return true.
			error = null;
			return true;
		}

		public override void CommitParameter()
		{
			m_parameter.Value = trkValue.Value;
		}

		/// <summary>
		/// Called when the user moves the trackbar position.
		/// Snaps the bar to the nearest valid value (according to the step).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trkValue_Scroll(object sender, EventArgs e)
		{
			// Round the trackbar position to the nearest step
			int modulus = (trkValue.Value - m_parameter.MinimumValue) % m_parameter.Step;
			trkValue.Value -= modulus;

			float differenceRatio = modulus/(float)m_parameter.Step;
			if (differenceRatio >= 0.5f)
				trkValue.Value += m_parameter.Step;
		}

		public override Type GetUnderlyingType()
		{
			return typeof(IntegerRangeParameter);
		}
	}
}
