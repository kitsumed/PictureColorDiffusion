namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is used to contains the settings of the controlnet extension during a generation request.
	/// This class is a child of <see cref="StableDiffusionProcessingAlwaysonScriptsModel"/>
	/// </summary>
	public class StableDiffusionExtensionControlNetModel
	{
		public StableDiffusionExtensionControlNetArg[] args { get; set; }
	}

	/// <summary>
	/// This class contains the arguments for one of the 3 controlnet units inside a 
	/// <see cref="StableDiffusionExtensionControlNetModel"/>. Some values are hard-coded
	/// </summary>
	public class StableDiffusionExtensionControlNetArg
	{
		/// <summary>
		/// Enable the current unit
		/// </summary>
		public bool enabled { get; set; } = true;

		/// <summary>
		/// Ending Control Step (0.0 to 1.0)
		/// </summary>
		public double guidance_end { get; set; } = 1.0;

		/// <summary>
		/// Starting Control Step (0.0 to 1.0)
		/// </summary>
		public double guidance_start { get; set; } = 0.0;

		/// <summary>
		/// Control Weight (0.0 to 2.0)
		/// </summary>
		public double weight { get; set; } = 1.0;

		/// <summary>
		/// Preprocessor module name
		/// </summary>
		public string? module { get; set; }

		/// <summary>
		/// Name of the model to use
		/// </summary>
		public string model { get; set; } = "None";

		/// <summary>
		/// Contains original picture
		/// </summary>
		public StableDiffusionExtensionControlNetArgImage? image { get; set; }

		public string control_mode { get; set; } = "Balanced";
		public string resize_mode { get; set; } = "Just Resize";

		public bool pixel_perfect { get; set; } = false;

		/// <summary>
		/// Pre-processor resolution
		/// Only available when pixel_perfect isn't true
		/// </summary>
		public int processor_res { get; set; } = 512;

		/// <summary>
		/// Enable low vram mode
		/// </summary>
		public bool low_vram { get; set; } = false;

		// Disable masks
		public bool mask { get => false; }


	}

	/// <summary>
	/// This class contains the base64 picture for one of the 3 controlnet units. 
	/// Child of <see cref="StableDiffusionExtensionControlNetArg"/>. Some values are hard-coded
	/// </summary>
	public class StableDiffusionExtensionControlNetArgImage 
	{
		/// <summary>
		/// Base64 picture
		/// </summary>
		public required string image { get; set; }

		// Force mask to be disabled
		public string? mask { get => null; }
	}
}
