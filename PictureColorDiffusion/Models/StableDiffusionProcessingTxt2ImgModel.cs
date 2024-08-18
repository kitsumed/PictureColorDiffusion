namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model template for a StableDiffusionProcessingTxt2Img from the
	/// stable diffusion webui api. Some values are hard-coded.
	/// </summary>
	public class StableDiffusionProcessingTxt2ImgModel
	{
		public string prompt { get; set; } = string.Empty;

		public string negative_prompt { get; set; } = string.Empty;

		public long seed { get; set; } = -1;

		public string sampler_name { get; set; } = "Euler";

		public string scheduler { get; set; } = "Automatic";

		public int steps { get; set; } = 12;

		public double cfg_scale { get; set; } = 7;

		public int width { get; set; } = 512;

		public int height { get; set; } = 512;

		/// <summary>
		/// Contains configurations for external extensions of the stable diffusion webui
		/// </summary>
		public StableDiffusionProcessingAlwaysonScriptsModel alwayson_scripts { get; set; } = new StableDiffusionProcessingAlwaysonScriptsModel();

		// Force reply to contains base64 version of generated image
		public bool send_images { get => true; }

		// Prevent generated images from beeing saved in stable diffusion webui output folder
		public bool save_images { get => false; }
		public bool do_not_save_samples { get => true; }
		public bool do_not_save_grid { get => true; }

		// Prevent face restoration from running, this app is more about 2D drawing than realistic
		public bool restore_faces { get => false; }

		// Prevent generating more than one picture per request
		public int batch_size { get => 1; }
		public int n_iter { get => 1; }

		// Colorizing pictures does not need to create symbols paterns (tiling)
		public bool tiling { get => false; }
	}
}
