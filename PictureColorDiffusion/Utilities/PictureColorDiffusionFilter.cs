namespace PictureColorDiffusion.Utilities
{
	/// <summary>
	/// This class manage the filtering of interoggate caption result to remove bad words from prompts 
	/// </summary>
	public static class PictureColorDiffusionFilter
	{
		/// <summary>
		/// Apply the filtering to the prompt
		/// </summary>
		/// <param name="caption">The interoggate caption prompt to filter. The words in the caption need to be separated by comma (,)</param>
		/// <returns>A string with the filtered prompt</returns>
		public static string Process(string caption)
		{
			// Search if the word is part of the BadWords array,
			// we invert the bool to exlude it from the list if the word is inside the array
			string[] allowedWords = caption.Split(',').Where(word => !BadWords.Any(badWord => word.Contains(badWord))).ToArray();
			// Join all words of the allowedWords array by adding "," between them
			return string.Join(",", allowedWords);
		}

		/// <summary>
		/// The list of bad words to be removed from a caption
		/// NOTE: Words are removed using Contains, meaning words like "username" will remove everything
		/// as long as "username" is a part of it
		/// </summary>
		private static readonly string[] BadWords =
		{
			// Colors
			"greyscale","monochrome", "partially colored", "muted color", "brown theme", "white theme",
			"grey theme", "spot color", "scan", "sepia", "negative", "white skin",
			"purple theme", "blue theme", "red theme", "color issue", "neon palette", "black theme",
			"orange theme", "light brown background", "high contrast",
			// Quality
			"low quality", "lowres", "worst quality", "deformed", "signature", "watermark",
			"username", "cropped", "web address", "scan artifacts", "bar censor", "censored",
			"thought bubble", "mosaic censoring", "oversaturated", "circle name", "artist name", "copyright name",
			"company name", "character name", "blur censor", "pointless censoring", "transparent censoring", "crease",
			"jpeg artifacts", "screentones",
			// Text
			"chinese text", "bopomofo text", "cantonese text", "minnan text", "shenyang mandarin text", "sichuanese text",
			"traditional chinese text", "pinyin text", "archaic japanese text", "furigana", "romaji text", "kiriji text",
			"hmong text", "indonesian text", "javanese text", "khmer text", "korean text", "gyeongsang korean text",
			"romaja text", "lao text", "malay text", "tagalog text", "filipino text", "thai text",
			"tibetan text", "vietnamese text", "english text", "ebonics", "engrish text", "singlish text",
			"middle english text", "australian english text", "irish english text", "scottish english text", "breton text", "danish text",
			"dutch text", "german text", "icelandic text", "luxembourgish text", "norwegian text", "scots text",
			"swedish text", "welsh text", "yiddish text", "catalan text", "french text", "italian text",
			"latin text", "maltese text", "romanian text", "spanish text", "portuguese text", "ranguage"

		};
    }
}
