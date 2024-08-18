namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model for Alwayson_Scripts who's part of <see cref="StableDiffusionProcessingTxt2ImgModel"/>.
	/// This class contains configuration for external extensions of the stable diffusion webui
	/// </summary>
	public class StableDiffusionProcessingAlwaysonScriptsModel
	{
		/// <summary>
		/// Controlnet extension configuration
		/// </summary>
		public StableDiffusionExtensionControlNetModel ControlNet { get; set; }
	}
}
