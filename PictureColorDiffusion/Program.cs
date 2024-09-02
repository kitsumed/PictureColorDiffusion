// We globally declare SixLabors.ImageSharp with the "ImageSharp" alias since it conflict with System.Drawing
// when using simple class names, and repeating "SixLabors.ImageSharp.***" each time feel messy
global using ImageSharp = SixLabors.ImageSharp;

namespace PictureColorDiffusion
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new MainForm());
		}
	}
}