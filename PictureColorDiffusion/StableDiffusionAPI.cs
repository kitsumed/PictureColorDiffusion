using PictureColorDiffusion.Models;
using System.Text;
using System.Text.Json;

namespace PictureColorDiffusion
{
	/// <summary>
	/// This class contains all methods that allow to interact with the
	/// stable diffusion webui API.
	/// </summary>
	public class StableDiffusionAPI
	{
		private readonly HttpClient _httpClient = new HttpClient() 
		{
			// Since the request will wait for the generation to finish, we disable timeout
			Timeout = Timeout.InfiniteTimeSpan,
		};

		/// <summary>
		/// The api endpoint used by this class instance
		/// </summary>
		public readonly Uri ApiEndpoint;

		/// <summary>
		/// Called when a StableDiffusionAPI instance is created
		/// </summary>
		/// <param name="apiEndpoint">The uri of the stable diffusion webui api endpoint</param>
        public StableDiffusionAPI(Uri apiEndpoint)
		{
			ApiEndpoint = apiEndpoint;
		}

		/// <summary>
		/// Will ping the endpoint for a response code in the 200-299 range.
		/// </summary>
		/// <returns>True if response status is between 200-299, else false</returns>
		public async Task<bool> VerifyEndpointAsync() 
		{
			try
			{
				using HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri(ApiEndpoint, "/internal/ping"));
				return responseMessage.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				// HttpRequestException are thrown when destination host is unreachable
				// so it's a invalid endpoint.
				if (ex is not HttpRequestException) 
				{
					throw;
				}
			}
			// Return false if the destination host is unreachable
			return false;
		}

		/// <summary>
		/// Get a list of all stable diffusion models and their informations.
		/// </summary>
		/// <returns>A list of StableDiffusionModelModel or null if no models where found</returns>
		public async Task<List<StableDiffusionModelModel>?> GetModels() 
		{
			using HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri(ApiEndpoint, "/sdapi/v1/sd-models"));
			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				List<StableDiffusionModelModel>? stableDiffusionModels = await JsonSerializer.DeserializeAsync<List<StableDiffusionModelModel>>(responseContentStream);
				return stableDiffusionModels;
			}
			return null;
		}

		/// <summary>
		/// Get a list of all controlnet models and their informations.
		/// </summary>
		/// <returns>A list of StableDiffusionControlNetModelListModel or null if no models where found</returns>
		public async Task<StableDiffusionControlNetModelListModel?> GetControlNetModels()
		{
			using HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri(ApiEndpoint, "/controlnet/model_list"));
			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				StableDiffusionControlNetModelListModel? stableDiffusionControlNetModels = await JsonSerializer.DeserializeAsync<StableDiffusionControlNetModelListModel>(responseContentStream);
				return stableDiffusionControlNetModels;
			}
			return null;
		}

		/// <summary>
		/// Get a list of all stable diffusion VAEs and their informations.
		/// </summary>
		/// <param name="ApiEndpoint">The base url.</param>
		/// <returns>A list of StableDiffusionVAEModel or null if no models where found</returns>
		public async Task<List<StableDiffusionVAEModel>?> GetVaes()
		{
			using HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri(ApiEndpoint, "/sdapi/v1/sd-vae"));
			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				List<StableDiffusionVAEModel>? stableDiffusionVAEs = await JsonSerializer.DeserializeAsync<List<StableDiffusionVAEModel>>(responseContentStream);
				return stableDiffusionVAEs;
			}
			return null;
		}

		/// <summary>
		/// Make a get request to get the options (settings) of stable diffusion webui.
		/// Currently only returning the seed and name of the SD/XL model & VAE loaded.
		/// </summary>
		/// <returns>A StableDiffusionOptionsModel with the current settings, or null if the API response isn't a success status</returns>
		public async Task<StableDiffusionOptionsModel?> GetOptions()
		{
			using HttpResponseMessage responseMessage = await _httpClient.GetAsync(new Uri(ApiEndpoint, "/sdapi/v1/options"));
			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				StableDiffusionOptionsModel? stableDiffusionOptions = await JsonSerializer.DeserializeAsync<StableDiffusionOptionsModel>(responseContentStream);
				return stableDiffusionOptions;
			}
			return null;
		}

		/// <summary>
		/// Make a post request to change the options (settings) of stable diffusion webui
		/// </summary>
		/// <param name="stableDiffusionProcessingTxt2ImgModel">Model with request informations with the new options</param>
		/// <returns>True if settings where applied, false if it failed</returns>
		public async Task<bool> PostOptions(StableDiffusionOptionsModel stableDiffusionOptionsModel)
		{
			string serializedOptionsModel = JsonSerializer.Serialize(stableDiffusionOptionsModel);
			using StringContent requestContent = new StringContent(serializedOptionsModel, Encoding.UTF8, "application/json");
			using HttpResponseMessage responseMessage = await _httpClient.PostAsync(new Uri(ApiEndpoint, "/sdapi/v1/options"), requestContent);

			// Since the PostOptions api request sometimes return a 200 OK when it fail, we manually verify if the settings where applied
			StableDiffusionOptionsModel? currentStableDiffusionOptions = await GetOptions();
			if (currentStableDiffusionOptions != null) 
			{
				// Verify if the SD checkpoint and VAE have the same name, CLIP SKIP is the same and that the response is a success status
				return (currentStableDiffusionOptions.sd_model_checkpoint == stableDiffusionOptionsModel.sd_model_checkpoint
					&& currentStableDiffusionOptions.sd_vae == stableDiffusionOptionsModel.sd_vae
					&& currentStableDiffusionOptions.CLIP_stop_at_last_layers == stableDiffusionOptionsModel.CLIP_stop_at_last_layers
					&& responseMessage.IsSuccessStatusCode);
			}
			return responseMessage.IsSuccessStatusCode;
		}

		/// <summary>
		/// Make a post request to generate a Text to Img picture with stable diffusion webui api.
		/// </summary>
		/// <param name="stableDiffusionProcessingTxt2ImgModel">Model with request informations</param>
		/// <returns>A StableDiffusionImageResponseModel containing pictures in base64 or null if the request failed</returns>
		public async Task<StableDiffusionImageResponseModel?> PostTxt2Img(StableDiffusionProcessingTxt2ImgModel stableDiffusionProcessingTxt2ImgModel)
		{
			string serializedProcessingTxt2ImgModel = JsonSerializer.Serialize(stableDiffusionProcessingTxt2ImgModel);
			using StringContent requestContent = new StringContent(serializedProcessingTxt2ImgModel, Encoding.UTF8, "application/json");

			using HttpResponseMessage responseMessage = await _httpClient.PostAsync(new Uri(ApiEndpoint, "/sdapi/v1/txt2img"), requestContent);
			
			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				StableDiffusionImageResponseModel? imageResponseModel = await JsonSerializer.DeserializeAsync<StableDiffusionImageResponseModel>(responseContentStream);
				return imageResponseModel;
			}

			return null;
		}

		/// <summary>
		/// Make a post request to interrogate a model about the content of a picture.
		/// </summary>
		/// <param name="base64Picture">A base64 picture</param>
		/// <param name="interrogateModelName">The name of the model used for the interogation</param>
		/// <returns>A StableDiffusionInterrogateRequestResponseModel or null if the request failed</returns>
		public async Task<StableDiffusionInterrogateRequestResponseModel?> PostInterrogate(string base64Picture, string interrogateModelName)
		{
			string serializedProcessingTxt2ImgModel = JsonSerializer.Serialize(new StableDiffusionInterrogateRequestModel() 
			{
				image = base64Picture,
				model = interrogateModelName
			});

			using StringContent requestContent = new StringContent(serializedProcessingTxt2ImgModel, Encoding.UTF8, "application/json");
			using HttpResponseMessage responseMessage = await _httpClient.PostAsync(new Uri(ApiEndpoint, "/sdapi/v1/interrogate"), requestContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				using Stream responseContentStream = await responseMessage.Content.ReadAsStreamAsync();
				StableDiffusionInterrogateRequestResponseModel? interrogateResponseModel = await JsonSerializer.DeserializeAsync<StableDiffusionInterrogateRequestResponseModel>(responseContentStream);
				return interrogateResponseModel;
			}

			return null;
		}
	}
}
