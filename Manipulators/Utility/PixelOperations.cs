using System;

namespace ImageProcessor.Manipulators.Utility
{
	public static class PixelOperations
	{
		public static byte[] Average(byte[,] pixels)
		{
			int pixelCount = pixels.Length/3;

			int[] cumulative = new int[] {0, 0, 0};
			
			for (int i = 0; i < pixelCount; i++)
			{
				cumulative[0] += pixels[i, 0];
				cumulative[1] += pixels[i, 1];
				cumulative[2] += pixels[i, 2];
			}
			
			return new byte[]
				{
					(byte)(cumulative[0]/pixelCount),
					(byte)(cumulative[1]/pixelCount),
					(byte)(cumulative[2]/pixelCount)
				};
		}
		
		public static byte ClampColourComponent(int value)
		{
			return (byte)(Math.Min(Math.Max(value, 0), 255));
		}

		public static byte ClampColourComponent(float value)
		{
			return (byte)(Math.Min(Math.Max(value, 0), 255));
		}
	}
}
