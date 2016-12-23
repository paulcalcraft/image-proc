using System;
using System.Collections.Generic;
using System.Reflection;
using ImageProcessor.ManipulatorInterfaces;

namespace ImageProcessor.Manipulators
{
  public static class Repository
  {
    public static List<IManipulator> Manipulators;

  	/// <summary>
  	/// Checks every class in the assembly, if it implements the
  	/// IManipulator interface, a new instance is created and added
  	/// to the repository list.
  	/// </summary>
    public static void Load()
    {
      Manipulators = new List<IManipulator>();

      Type[] types = Assembly.GetExecutingAssembly().GetTypes();
      foreach (Type t in types)
      {
        if (t.GetInterface(typeof(IManipulator).FullName) == null)
          continue;

				IManipulator m = Activator.CreateInstance(t) as IManipulator;

        Manipulators.Add(m);
      }
    }
  }
}
