using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ImageProcessor.ManipulatorInterfaces;

namespace ImageProcessor.GUI
{
	/// <summary>
	/// Provides an automatically generated dialog box of parameters for a
	/// manipulator. Ensures input data is valid before committing it to
	/// backend classes.
	/// </summary>
	public partial class ParameterDialog : Form
	{
		#region Parameter Gui Mapping
		private static Dictionary<Type, Type> parameterGuiMapping; // A mapping between backend parameter data classes and their GUI implementations

		/// <summary>
		/// Creates the backend to GUI parameter mapping.
		/// Finds all types in the assembly that derive from ParameterGuiBase.
		/// Creates an instance of this class and uses GetUnderlyingType() to
		/// identify the equivalent backend data type. These are then mapped
		/// by a Dictionary for later use.
		/// </summary>
		public static void MapParameterGuis()
		{
			if (parameterGuiMapping != null)
				throw new Exception("Parameter Gui mapping already exists.");

			parameterGuiMapping = new Dictionary<Type, Type>();

			Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			foreach (Type guiType in types)
			{
				// If this type does not derive from ParameterGuiBase, continue to the next
				if (!guiType.IsSubclassOf(typeof(ParameterGuiBase)))
					continue;

				// Create a new instance of this subclass, and get its underlying (backend data) type
				ParameterGuiBase guiInstance = Activator.CreateInstance(guiType) as ParameterGuiBase;
				Type dataType = guiInstance.GetUnderlyingType();
				
				// Add the two types to the type mapping
				parameterGuiMapping.Add(dataType, guiType);
			}
		}
		#endregion
			
		private ParameterGuiBase[] m_parameterGuis;
		
		/// <summary>
		/// Main constructor that builds a parameter dialog from the parameters
		/// in a given manipulator.
		/// Uses the mapping Dictionary to identify the GUI equivalent type
		/// for each parameter in the manipulator, and then instantiates them
		/// and adds them to the dialog. Then sets the title of the dialog box.
		/// </summary>
		/// <param name="manipulator">The manipulator from which to generate the ParameterDialog.</param>
		private ParameterDialog(IManipulator manipulator)
		{
			InitializeComponent();
			
			if (manipulator.Parameters == null)
				return;

			List<ParameterGuiBase> parameterGuiList = new List<ParameterGuiBase>(manipulator.Parameters.Length);
			int maxParameterIndex = manipulator.Parameters.Length - 1;

			for (int i = maxParameterIndex; i >= 0; i--) // Traverse in reverse order, as Controls are shown in the opposite order to that of when they were added to the form
			{
				ParameterGuiBase parameterGuiInstance;

				// Get the corresponding Gui object Type for the parameter using the mapping created earlier. If there isn't an equivalent, continue to the next parameter.
				Type parameterGuiType;
				if (!parameterGuiMapping.TryGetValue(manipulator.Parameters[i].GetType(), out parameterGuiType))
					continue;
				
				// Create a new instance of the Gui object type
				parameterGuiInstance = Activator.CreateInstance(parameterGuiType) as ParameterGuiBase;

				// Configure the new object accordingly
				parameterGuiInstance.Name = manipulator.Parameters[i].Name;
				parameterGuiInstance.TabIndex = i;
				parameterGuiInstance.Dock = DockStyle.Top;
				
				// Load the backend parameter object into the Gui component
				parameterGuiInstance.LoadParameter(manipulator.Parameters[i]);

				// Add the new parameter Gui object to the list
				parameterGuiList.Add(parameterGuiInstance);
			}

			// Return if no parameters have corresponding Guis
			if (parameterGuiList.Count == 0)
				return;

			// Store the list of Guis as a private array member variable
			m_parameterGuis = parameterGuiList.ToArray();

			// Add all the parameter Guis to the form
			Controls.AddRange(m_parameterGuis);
			
			// Set the title bar text of this ParameterDialog
			Text = manipulator.Type.ToString() + ": " + manipulator.Name;
		}
		
		/// <summary>
		/// Hides the base class ShowDialog method to insert a check for
		/// the existance of parameters. If there are none, OK is returned.
		/// </summary>
		/// <returns>OK if the manipulation should continue, otherwise Cancel.</returns>
		private new DialogResult ShowDialog()
		{
			if (m_parameterGuis == null)
				return DialogResult.OK;
			else
				return base.ShowDialog();
		}
		
		/// <summary>
		/// Instantiates and shows a new ParameterDialog for the given manipulator.
		/// </summary>
		/// <param name="manipulator">The manipulator from which to generate the ParameterDialog.</param>
		/// <returns>OK if the manipulation should continue, otherwise Cancel.</returns>
		public static DialogResult ShowDialog(IManipulator manipulator)
		{
			ParameterDialog dialog = new ParameterDialog(manipulator);
			return dialog.ShowDialog();
		}

		/// <summary>
		/// Executed when the ParameterDialog is closing.
		/// If the OK button was clicked, each parameter entry is validated.
		/// If there are any errors, an error dialog is created and shown,
		/// else each ParameterGui value is committed to its backend data instance.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">Used to optionally cancel the closing of the form.</param>
		private void ParameterDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				List<string> errors = new List<string>(m_parameterGuis.Length); // Create an error list with the maximum capacity set as the number of parameters.
				
				foreach (ParameterGuiBase parameterGui in m_parameterGuis)
				{
					string returnedError;
					
					if (parameterGui.ValidateInput(out returnedError))
						parameterGui.CommitParameter(); // If valid, commit
					else
						errors.Add(parameterGui.Name + ": " + returnedError); // Else add to the error list
				}
				
				if (errors.Count > 0) // If any errors occurred
				{
					StringBuilder message = new StringBuilder("The following error" + (errors.Count > 1 ? "s" : "") + " occurred:\r\n\r\n");
					
					// Append each error string to the message
					foreach (string error in errors)
						message.Append(error + "\r\n");
					
					MessageBox.Show(message.ToString(), "Input error", MessageBoxButtons.OK); // Show the error pop-up
					e.Cancel = true; // Cancel the closing of the form
				}
			}
		}
	}
}