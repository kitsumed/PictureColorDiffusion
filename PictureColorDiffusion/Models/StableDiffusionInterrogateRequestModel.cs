namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is a model template for a InterrogateRequest from the
	/// stable diffusion webui api.
	/// </summary>
	public class StableDiffusionInterrogateRequestModel
	{
		/// <summary>
		/// Base64 picture
		/// </summary>
		public required string image { get; set; }

		/// <summary>
		/// The interrogate model used
		/// </summary>
		public required string model { get; set; } = "clip";
	}


	/// <summary>
	/// This class is a model template for the response of a interrogate request from the
	/// stable diffusion webui api.
	/// </summary>
	public class StableDiffusionInterrogateRequestResponseModel
	{
		/// <summary>
		/// Contain the result of the interogation
		/// </summary>
		public string caption { get; set; }
	}
}
