namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model template for a TextToImageResponse & ImageToImageResponse from the
	/// stable diffusion webui api.
	/// </summary>
	public class StableDiffusionImageResponseModel
	{
		/// <summary>
		/// The generated images in base64 format
		/// </summary>
		public string[] images {get; set;}
	}
}
