using PictureColorDiffusion.Models;

namespace PictureColorDiffusion.Utilities
{
	/// <summary>
	/// This class contains a "modes" dictionary with hard-coded values that are used & shared across the whole application 
	/// to get the configurations of generation parameters for a specific mode.
	/// </summary>
	public static class PictureColorDiffusionModes
	{
		/// <summary>
		/// Search & get the hard-coded configuration for a specific mode
		/// </summary>
		/// <param name="mode">The name of the mode</param>
		/// <returns>A PictureColorDiffusionModeModel or null if the mode does not exist</returns>
		public static PictureColorDiffusionModeModel? GetModeConfiguration(string mode)
		{
			PictureColorDiffusionModeModel? modeConfiguration;
			bool success = modes.TryGetValue(mode, out modeConfiguration);
			return success ? modeConfiguration : null;
		}

		/// <summary>
		/// Dictionary that contains all hard-coded configurations for modes.
		/// Modes configuration can be found with the string key
		/// </summary>
		private static readonly Dictionary<string, PictureColorDiffusionModeModel> modes = new Dictionary<string, PictureColorDiffusionModeModel>()
		{
			// Manga mode
			{
				"Manga", new PictureColorDiffusionModeModel()
				{
					prompt = "detailed (official style|style parody), (comic|cover|manga|5koma|4koma|3koma):1.1, masterpiece, highres, blank speech bubble, simple background, textless version, emphasis lines, ",
					negative_prompt = "(greyscale:1.1), monochrome, deformed, jpeg artifacts, lineart, sepia, low quality, lowres, worst quality, brown theme, grey theme, light brown background, bad anatomy, high contrast, bad, screentones, neon palette, color issue, purple theme, black theme, [blue theme|orange theme]:0.5, ",
					// Japanese JB5 paper size (in pixels) divided by 2 (taken from papersizes.io)
					dynamicResizeMax = new Size(1075 ,1518),
					interogateModel = "deepdanbooru",
					controlNetModelNamePerUnit = ["control_v11p_sd15s2_lineart_anime", "control_v11p_sd15_softedge"],
					controlNetUnits = [
					// UNIT 1 CONFIG
					new StableDiffusionExtensionControlNetArg()
					{
						module = "invert (from white bg & black line)",
						weight = 0.8,
						guidance_start = 0,
						guidance_end = 1,
						control_mode = "Balanced",
						resize_mode = "Just Resize",
						//pixel_perfect = true
						pixel_perfect = false,
						processor_res = 0,
					},
					// UNIT 2 CONFIG
					new StableDiffusionExtensionControlNetArg()
					{
						module = "softedge_pidinet",
						weight = 0.6,
						guidance_start = 0,
						guidance_end = 0.45,
						control_mode = "Balanced",
						resize_mode = "Just Resize",
						pixel_perfect = false,
						processor_res = 768,
					}]
				}
			}
		};
	}
}
