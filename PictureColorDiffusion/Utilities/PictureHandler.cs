using ImageMagick;

namespace PictureColorDiffusion.Utilities
{
    /// <summary>
    /// PictureHandler is a class used to contains everything related to the handling of pictures
    /// </summary>
    public static class PictureHandler
    {
		/// <summary>
		/// Get a image object from a file
		/// </summary>
		/// <param name="filePath">The picture path</param>
		/// <returns>A image</returns>
		/// <exception cref="FileNotFoundException"></exception>
		public static async Task<Image> LoadAsImage(string filePath)
		{
			if (File.Exists(filePath))
			{
				return await Task.Run(() => Image.FromFile(filePath));
			}
			else
			{
				throw new FileNotFoundException();
			}
		}

		/// <summary>
		/// Convert a image to base64
		/// </summary>
		/// <param name="picture">The image</param>
		/// <returns>A base64 string of the image</returns>
		public static string ImageToBase64(Image picture)
		{
            using MemoryStream memoryStream = new MemoryStream();
            // Write the picture into the memoryStream
            picture.Save(memoryStream, picture.RawFormat);
            // Convert the picture saved into the memoryStream to bytes
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
		}

		/// <summary>
		/// Convert a image to a MagickImage
		/// </summary>
		/// <param name="picture">The image</param>
		/// <returns>A MagickImage</returns>
		public static MagickImage ImageToMagickImage(Image picture)
		{
			using MemoryStream memoryStream = new MemoryStream();
			// Write the picture into the memoryStream
			picture.Save(memoryStream, picture.RawFormat);
			// Move the stream position to 0 to prevent magick error
			memoryStream.Position = 0;
			// Convert the picture stream into a MagickImage
			return new MagickImage(memoryStream);
		}

		/// <summary>
		/// Convert a base64 string to a image
		/// </summary>
		/// <param name="base64Image">A base64 string of the image</param>
		/// <returns>A image</returns>
		public static Image Base64ToImage(string base64Image)
        {
			byte[] base64ImageBytes = Convert.FromBase64String(base64Image);
            using MemoryStream memoryStream = new MemoryStream(base64ImageBytes);
			return Image.FromStream(memoryStream, false, true);
		}

		/// <summary>
		/// Resize the Height and Width without deforming the picture
		/// </summary>
		/// <param name="pictureSize">The current size you want to resize</param>
		/// <param name="maxSize">The maximum size allowed</param>
		/// <returns>A size dynamically resized according to your maxSize</returns>
		public static Size DynamicResize(Size pictureSize, Size maxSize) 
		{
			// We calculate the aspect ratio of the picture to prevent deforming it when we resize it
			float originalPictureAspectRatio = (float)pictureSize.Width / pictureSize.Height;

			// Defaults the new size to the picture size in case no changes are made
			int newWidth = pictureSize.Width;
			int newHeight = pictureSize.Height;

			if (pictureSize.Width > maxSize.Width)
			{
				// Set the Width to the max allowed
				newWidth = maxSize.Width;
				// Resize the Height to keep the picture aspect ratio the same
				newHeight = (int)(maxSize.Width / originalPictureAspectRatio);
			}

			if (pictureSize.Height > maxSize.Height)
			{
				// Set the Height to the max allowed
				newHeight = maxSize.Height;
				// Resize the Width to keep the picture aspect ratio the same
				newWidth = (int)(maxSize.Height * originalPictureAspectRatio);
			}

			return new Size(newWidth, newHeight);
		}
	}
}
