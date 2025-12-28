using Compunet.YoloV8.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace PictureColorDiffusion.Utilities
{
	/// <summary>
	/// PictureHandler is a class used to contains everything related to the handling of pictures
	/// </summary>
	public static class PictureHandler
	{
		/// <summary>
		/// List of supported picture extension
		/// </summary>
		public static readonly string[] SupportedExtensions = { "png", "jpeg", "jpg", "bmp", "pbm", "tiff", "tga", "webp" };

		/// <summary>
		/// Get the path of all files inside a directory that are supported extensions
		/// </summary>
		/// <param name="directory">The directory path</param>
		/// <returns>A string[] of full path to all files that have a supported extension</returns>
		/// <exception cref="DirectoryNotFoundException"></exception>
		public static string[] GetSupportedFilesFromDirectory(string directory) 
		{
			if (Directory.Exists(directory))
			{
				// Get all files in the directory and filter them to only keep thoses who have a
				// extension that is part of the SupportedExtensions array
				return Directory.GetFiles(directory).Where(filePath => SupportedExtensions.Any(supportedExtension => Path.GetExtension(filePath).Replace(".", "") == supportedExtension)).ToArray();
			}
			else 
			{
				throw new DirectoryNotFoundException();
			}
		}

		/// <summary>
		/// Get a ImageSharp image object from a file
		/// </summary>
		/// <param name="filePath">The picture path</param>
		/// <returns>A ImageSharp image</returns>
		/// <exception cref="FileNotFoundException"></exception>
		public static async Task<ImageSharp.Image<Rgba32>> LoadAsImageSharp(string filePath)
		{
			if (File.Exists(filePath))
			{
				
				return await ImageSharp.Image.LoadAsync<Rgba32>(filePath);
			}
			else
			{
				throw new FileNotFoundException();
			}
		}

		/// <summary>
		/// Convert a ImageSharp image to a base64 png file
		/// </summary>
		/// <param name="picture">The image</param>
		/// <returns>A base64 png string of the image</returns>
		public static string ImageSharpToBase64(ImageSharp.Image picture)
		{
            return picture.ToBase64String(PngFormat.Instance);
		}

		/// <summary>
		/// Convert a ImageSharp image to a Bitmap
		/// </summary>
		/// <param name="picture">The image</param>
		/// <returns>A base64 png string of the image</returns>
		public static Bitmap ImageSharpToBitmap(ImageSharp.Image picture)
		{
			using MemoryStream memoryStream = new MemoryStream();
			// Save the picture to the memory stream
			picture.SaveAsBmp(memoryStream);
			// Create a bitmap with the memory stream
			return new Bitmap(memoryStream);
		}

		/// <summary>
		/// Convert a base64 string to a ImageSharp image
		/// </summary>
		/// <param name="base64Image">A base64 string of the image</param>
		/// <returns>A ImageSharp image</returns>
		public async static Task<ImageSharp.Image> Base64ToImageSharp(string base64Image)
        {
			byte[] base64ImageBytes = Convert.FromBase64String(base64Image);
            using MemoryStream memoryStream = new MemoryStream(base64ImageBytes);
			return await ImageSharp.Image.LoadAsync(memoryStream);
		}

		/// <summary>
		/// Convert a SegmentationResult to a ImageSharp Image containing all the masks that matches
		/// a valid confidence threshold.
		/// </summary>
		/// <param name="segResult">The SegmentationResult</param>
		/// <param name="imageMask">Out parameter containing all masks</param>
		public static void GetImageMaskFromSegmentationResult(SegmentationResult segResult, out Image<Rgba32> imageMask) 
		{
			// Image that will contain all the boxes mask
			imageMask = new Image<Rgba32>(segResult.Image.Width, segResult.Image.Height);

			// Loop trought all detected boxes
			foreach (SegmentationBoundingBox currentBox in segResult.Boxes)
			{
				// Create a image the size of the current box to contain the box mask
				using Image<Rgba32> currentBoxMask = new Image<Rgba32>(currentBox.Bounds.Width, currentBox.Bounds.Height);
				// If the box detection confidence from the model is over 0.5float
				if (currentBox.Confidence > 0.5f)
				{
					// Loop trought every pixels of the box mask
					for (int iWidth = 0; iWidth < currentBox.Mask.Width; iWidth++)
					{
						for (int iHeight = 0; iHeight < currentBox.Mask.Height; iHeight++)
						{
							// Get the mask current pixel detection confidence, continue if it's over 0.5float
							// NOTE: Per box detection, each pixels that make the mask have their own confidence score.
							// Filtering pixels with low scores allow to get a more clean mask
							if (currentBox.Mask.GetConfidence(iWidth, iHeight) > 0.5f)
							{
								// Set the color of the pixel in position [iWidth, iHeight]
								currentBoxMask[iWidth, iHeight] = ImageSharp.Color.Red;
							}
						}
					}
					// Draw the box mask to the imageMask image
					imageMask.Mutate(m => m.DrawImage(currentBoxMask, new ImageSharp.Point(currentBox.Bounds.X, currentBox.Bounds.Y), 1f));
				}
			}
		}

		/// <summary>
		/// Extract the content of a image that matches the mask image
		/// </summary>
		/// <param name="originalPicture">The image to extract its content from</param>
		/// <param name="imageMask">The mask image</param>
		/// <param name="smoothEdges">If true, applies morphological operations to create sharp, linear edges by removing irregular pixels (default: false)</param>
		/// <returns>A ImageSharp Image that contains the content of the originalPicture that matched against the mask</returns>
		public static Image<Rgba32> ExtractImageFromMask(Image<Rgba32> originalPicture, Image<Rgba32> imageMask, bool smoothEdges = false) 
		{
			// Iterate over each pixel to apply the mask to the original picture
			Image<Rgba32> resultImage = new Image<Rgba32>(originalPicture.Width, originalPicture.Height);
			// Loop trought every pixels of the originalPicture
			for (int y = 0; y < originalPicture.Height; y++)
			{
				for (int x = 0; x < originalPicture.Width; x++)
				{
					// Get the mask pixel
					Rgba32 maskPixel = imageMask[x, y];
					// Get the original image pixel
					Rgba32 originalPixel = originalPicture[x, y];

					// Determine the alpha value from the mask pixel
					byte maskAlpha = maskPixel.A;

					// If maskAlpha is not zero (not a transparent pixel), copy the originalPicture pixel to the resultImage pixel
					if (maskAlpha > 0)
					{
						resultImage[x, y] = originalPixel;
					}
					else
					{
						// Make the resultImage pixel transparent if the mask pixel is also transparent
						resultImage[x, y] = new Rgba32(0, 0, 0, 0);
					}
				}
			}

			// Apply morphological edge smoothing if requested - creates sharp, linear edges
			if (smoothEdges)
			{
				// Create a binary mask from the extracted content
				Image<Rgba32> binaryMask = new Image<Rgba32>(resultImage.Width, resultImage.Height);
				
				// Build binary mask where white = content exists, black = transparent
				for (int y = 0; y < resultImage.Height; y++)
				{
					for (int x = 0; x < resultImage.Width; x++)
					{
						if (resultImage[x, y].A > 0)
						{
							binaryMask[x, y] = new Rgba32(255, 255, 255, 255);
						}
					}
				}

				// Apply very aggressive morphological operations for round/oval shapes
				// Multiple closing passes to fill gaps and smooth edges progressively
				
				// First closing - very aggressive to handle large irregularities
				Image<Rgba32> dilatedMask1 = MorphologicalDilate(binaryMask, 8);
				Image<Rgba32> closedMask1 = MorphologicalErode(dilatedMask1, 8);
				dilatedMask1.Dispose();
				
				// Second closing - moderate to further smooth
				Image<Rgba32> dilatedMask2 = MorphologicalDilate(closedMask1, 4);
				Image<Rgba32> closedMask2 = MorphologicalErode(dilatedMask2, 4);
				closedMask1.Dispose();
				dilatedMask2.Dispose();
				
				// Third closing - fine smoothing
				Image<Rgba32> dilatedMask3 = MorphologicalDilate(closedMask2, 2);
				Image<Rgba32> closedMask3 = MorphologicalErode(dilatedMask3, 2);
				closedMask2.Dispose();
				dilatedMask3.Dispose();
				
				// Opening to remove any remaining spikes and protrusions
				Image<Rgba32> erodedMask = MorphologicalErode(closedMask3, 4);
				Image<Rgba32> openedMask = MorphologicalDilate(erodedMask, 4);
				closedMask3.Dispose();
				erodedMask.Dispose();
				
				// Second opening pass for stubborn spikes
				Image<Rgba32> erodedMask2 = MorphologicalErode(openedMask, 2);
				Image<Rgba32> smoothedMask = MorphologicalDilate(erodedMask2, 2);
				openedMask.Dispose();
				erodedMask2.Dispose();

				// Apply the morphologically processed mask back to the result
				Image<Rgba32> smoothedResult = new Image<Rgba32>(resultImage.Width, resultImage.Height);
				
				for (int y = 0; y < resultImage.Height; y++)
				{
					for (int x = 0; x < resultImage.Width; x++)
					{
						// If the processed mask says this pixel should exist
						if (smoothedMask[x, y].A > 0)
						{
							Rgba32 originalPixel = resultImage[x, y];
							
							// If we have original content here, use it
							if (originalPixel.A > 0)
							{
								smoothedResult[x, y] = originalPixel;
							}
							else
							{
								// Pixel was added by morphological operations - interpolate from nearby
								Rgba32 interpolatedColor = InterpolateNearbyPixels(resultImage, x, y, 6);
								if (interpolatedColor.A > 0)
								{
									smoothedResult[x, y] = interpolatedColor;
								}
							}
						}
					}
				}

				binaryMask.Dispose();
				smoothedMask.Dispose();
				resultImage.Dispose();
				return smoothedResult;
			}

			return resultImage;
		}

		/// <summary>
		/// Morphological dilation operation - expands white regions
		/// </summary>
		private static Image<Rgba32> MorphologicalDilate(Image<Rgba32> mask, int iterations)
		{
			Image<Rgba32> result = mask.Clone();

			for (int iter = 0; iter < iterations; iter++)
			{
				Image<Rgba32> temp = result.Clone();
				
				// Only process pixels that are near edges (have at least one transparent neighbor)
				for (int y = 1; y < result.Height - 1; y++)
				{
					for (int x = 1; x < result.Width - 1; x++)
					{
						// Skip if pixel is already white and surrounded by white pixels (interior)
						if (temp[x, y].A > 0)
						{
							// Check if all neighbors are also white - if so, skip (interior pixel)
							if (temp[x - 1, y].A > 0 && temp[x + 1, y].A > 0 && 
								temp[x, y - 1].A > 0 && temp[x, y + 1].A > 0 &&
								temp[x - 1, y - 1].A > 0 && temp[x + 1, y - 1].A > 0 && 
								temp[x - 1, y + 1].A > 0 && temp[x + 1, y + 1].A > 0)
							{
								continue; // Skip interior pixels
							}
						}
						
						// Process edge pixels: if any neighbor has alpha > 0, set this pixel to white
						if (temp[x - 1, y].A > 0 || temp[x + 1, y].A > 0 || 
							temp[x, y - 1].A > 0 || temp[x, y + 1].A > 0 ||
							temp[x - 1, y - 1].A > 0 || temp[x + 1, y - 1].A > 0 || 
							temp[x - 1, y + 1].A > 0 || temp[x + 1, y + 1].A > 0)
						{
							result[x, y] = new Rgba32(255, 255, 255, 255);
						}
					}
				}
				
				temp.Dispose();
			}

			return result;
		}

		/// <summary>
		/// Morphological erosion operation - shrinks white regions
		/// </summary>
		private static Image<Rgba32> MorphologicalErode(Image<Rgba32> mask, int iterations)
		{
			Image<Rgba32> result = mask.Clone();

			for (int iter = 0; iter < iterations; iter++)
			{
				Image<Rgba32> temp = result.Clone();
				
				// Only process pixels that are near edges (have at least one transparent neighbor)
				for (int y = 1; y < result.Height - 1; y++)
				{
					for (int x = 1; x < result.Width - 1; x++)
					{
						// Only process white pixels that are on or near the edge
						if (temp[x, y].A == 0) continue; // Skip transparent pixels
						
						// Check if all neighbors are white - if so, skip (deep interior pixel)
						if (temp[x - 1, y].A > 0 && temp[x + 1, y].A > 0 && 
							temp[x, y - 1].A > 0 && temp[x, y + 1].A > 0 &&
							temp[x - 1, y - 1].A > 0 && temp[x + 1, y - 1].A > 0 && 
							temp[x - 1, y + 1].A > 0 && temp[x + 1, y + 1].A > 0)
						{
							continue; // Skip deep interior pixels
						}
						
						// Process edge pixels: if any neighbor has alpha == 0, set this pixel to transparent
						if (temp[x - 1, y].A == 0 || temp[x + 1, y].A == 0 || 
							temp[x, y - 1].A == 0 || temp[x, y + 1].A == 0 ||
							temp[x - 1, y - 1].A == 0 || temp[x + 1, y - 1].A == 0 || 
							temp[x - 1, y + 1].A == 0 || temp[x + 1, y + 1].A == 0)
						{
							result[x, y] = new Rgba32(0, 0, 0, 0);
						}
					}
				}
				
				temp.Dispose();
			}

			return result;
		}

		/// <summary>
		/// Interpolate color from nearby non-transparent pixels
		/// </summary>
		/// <param name="image">The source image</param>
		/// <param name="centerX">Center X coordinate</param>
		/// <param name="centerY">Center Y coordinate</param>
		/// <param name="radius">Search radius</param>
		/// <returns>Interpolated color or transparent if no nearby pixels found</returns>
		private static Rgba32 InterpolateNearbyPixels(Image<Rgba32> image, int centerX, int centerY, int radius)
		{
			int totalR = 0, totalG = 0, totalB = 0, count = 0;

			// Search in a square around the center point
			for (int dy = -radius; dy <= radius; dy++)
			{
				for (int dx = -radius; dx <= radius; dx++)
				{
					int x = centerX + dx;
					int y = centerY + dy;

					// Check bounds
					if (x >= 0 && x < image.Width && y >= 0 && y < image.Height)
					{
						Rgba32 pixel = image[x, y];
						if (pixel.A > 0)
						{
							totalR += pixel.R;
							totalG += pixel.G;
							totalB += pixel.B;
							count++;
						}
					}
				}
			}

			// Return averaged color if we found nearby pixels
			if (count > 0)
			{
				return new Rgba32((byte)(totalR / count), (byte)(totalG / count), (byte)(totalB / count), 255);
			}

			return new Rgba32(0, 0, 0, 0);
		}

		/// <summary>
		/// Resize the Height and Width without deforming the picture
		/// </summary>
		/// <param name="pictureSize">The current size you want to resize</param>
		/// <param name="maxSize">The maximum size allowed</param>
		/// <returns>A size dynamically resized according to your maxSize</returns>
		public static ImageSharp.Size DynamicResize(ImageSharp.Size pictureSize, ImageSharp.Size maxSize) 
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

			return new ImageSharp.Size(newWidth, newHeight);
		}
	}
}
