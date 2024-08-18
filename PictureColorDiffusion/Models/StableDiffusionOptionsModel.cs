namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model for Options from the
	/// stable diffusion webui api.
	/// </summary>
	public class StableDiffusionOptionsModel
	{
		/// <summary>
		/// Model name to load
		/// </summary>
		public string sd_model_checkpoint { get; set; }

		/// <summary>
		/// Clip skip
		/// </summary>
		public int CLIP_stop_at_last_layers { get; set; } = 2;

		/// <summary>
		/// VAE
		/// </summary>
		public string sd_vae { get; set; } = "Automatic";
	}
}
