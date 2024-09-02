using PictureColorDiffusion.Utilities;

namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model that contains multiples configurations for 1 mode inside <see cref="PictureColorDiffusionModes"/>
	/// </summary>
	public class PictureColorDiffusionModeModel
	{
		/// <summary>
		/// Generation prompt
		/// </summary>
		public required string prompt;

		/// <summary>
		/// Generation negative prompt
		/// </summary>
		public required string negative_prompt;

		/// <summary>
		/// The maximum picture size allowed before it get dynamically resized
		/// </summary>
		public required ImageSharp.Size dynamicResizeMax;

		/// <summary>
		/// Name of the model used for the interogation on the original image
		/// </summary>
		public required string interogateModel;

		/// <summary>
		/// Array of model name shown to the user in the UI.
		/// Theses are the models the user need to manually select for each units.
		/// The first array would be for the first controlNet unit
		/// </summary>
		public required string[] controlNetModelNamePerUnit;

		/// <summary>
		/// Configurations for controlnet units. Each item in the array is one unit
		/// </summary>
		public required StableDiffusionExtensionControlNetArg[] controlNetUnits;
	}
}
