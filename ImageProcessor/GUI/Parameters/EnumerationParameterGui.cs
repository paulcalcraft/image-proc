using System;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;
using ImageProcessor.ManipulatorInterfaces.Parameters;

namespace ImageProcessor.GUI.Parameters
{
	public partial class EnumerationParameterGui : ParameterGuiBase
	{
		private EnumerationParameter m_parameter;
		
		public EnumerationParameterGui()
		{
			InitializeComponent();
		}
		
		public override void LoadParameter(ParameterBase parameter)
		{
			m_parameter = parameter as EnumerationParameter;
			Reset();
		}
		
		private void Reset()
		{
			lblName.Text = m_parameter.Name;
			new ToolTip().SetToolTip(lblName, m_parameter.Help);

			foreach (Control c in Controls)
				if (c is RadioButton)
					Controls.Remove(c);

			const int verticalSpacing = 20;
			for (int i = 0; i < m_parameter.Enumeration.Length; i++)
			{
				RadioButton rdbOption = new RadioButton();
				rdbOption.AutoSize = true;
				rdbOption.Location = new System.Drawing.Point(209, 10 + verticalSpacing*i);
				rdbOption.TabStop = true;
				rdbOption.Text = m_parameter.Enumeration[i];
				rdbOption.Tag = i; // Attach the index of this value within the enumeration

				// If this is the currently selected value, mark the radio button as checked
				if (i == m_parameter.Value)
					rdbOption.Checked = true;

				Controls.Add(rdbOption); // Add the radio button to the form
			}

			Height = m_parameter.Enumeration.Length*verticalSpacing + 20; // Resize the control to fit the radio buttons
		}

		public override bool ValidateInput(out string error)
		{
			// An enumeration selected via a radio group cannot hold an invalid value, so always return true.
			error = null;
			return true;
		}
		
		public override void CommitParameter()
		{
			int value = 0;
			
			foreach (Control c in Controls)
				if (c is RadioButton)
					if ((c as RadioButton).Checked)
					{
						value = (int)c.Tag;
						break;
					}
			
			m_parameter.Value = value;
		}

		public override Type GetUnderlyingType()
		{
			return typeof(EnumerationParameter);
		}
	}
}
