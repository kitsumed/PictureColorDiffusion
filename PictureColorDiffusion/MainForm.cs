using Compunet.YoloV8;
using Compunet.YoloV8.Data;
using Compunet.YoloV8.Plotting;
using PictureColorDiffusion.Enums;
using PictureColorDiffusion.Models;
using PictureColorDiffusion.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Png.Chunks;
using SixLabors.ImageSharp.Processing;

namespace PictureColorDiffusion
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// Class that contain the current instance to interact with the stable diffusion api
		/// </summary>
		private StableDiffusionAPI StableAPI;

		/// <summary>
		/// Report the inference progress status
		/// </summary>
		private readonly IProgress<InferenceProgressModel> InferenceProgress;

		/// <summary>
		/// Cancellation token used to stop a ongoing inference task
		/// </summary>
		private CancellationTokenSource? InferenceCancellationTokenSource;

		/// <summary>
		/// Selected mode for Picture Color Diffusion
		/// </summary>
		private string? SelectedMode = null;

		/// <summary>
		/// Hard-coded path for the models directory. Same directory as main executable (./models/)
		/// </summary>
		private readonly string ModelsDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "models";

		public MainForm()
		{
			InitializeComponent();
			InferenceProgress = new Progress<InferenceProgressModel>(values =>
			{
				progressBarInference.Value = values.completionPercent;
				labelProgressStatus.Text = $"Status ({values.completionPercent}%) : {values.status}";
			});
		}

		/// <summary>
		/// Method executed when the MainForm load
		/// </summary>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// Set default sampler to Euler
			comboBoxSampler.SelectedIndex = 0;
			// Set supported files as the only allowed files on the open file dialog
			string supportedFilesFilter = "*." + string.Join(";*.", PictureHandler.SupportedExtensions);
			openFileDialog1.Filter = "Pictures | " + supportedFilesFilter;
			// Verify if the default ApiEndpoint is valid or invalid at startup
			SetApplicationState(ApplicationStatesEnum.waiting_for_api);
			buttonVerifyApiEndpoint.PerformClick();
			// Ensure that the models directory exist
			Directory.CreateDirectory(ModelsDirectoryPath);
			// Set the fileSystemWatcherRefreshONNXModels monitoring to the models directory
			fileSystemWatcherYoloV8ONNXModels.Path = ModelsDirectoryPath;
			// Force a refresh of the YoloV8 ONNX model list
			fileSystemWatcherRefreshONNXModels(null, null);
		}

		/// <summary>
		/// Method executed when a keyboard key is pressed down on the MainForm
		/// </summary>
		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Ctrl+Shift+B shortcut to bypass API verification
			if (e.Control && e.Shift && e.KeyCode == Keys.B)
			{
				SetApplicationState(ApplicationStatesEnum.waiting_for_inference);
			}
		}

		#region Buttons Methods
		// == Stable Diffusion API Configuration ==

		/// <summary>
		/// Called when the user click on the "Verify" button for the API configuration.
		/// </summary>
		private async void buttonVerifyApiEndpoint_Click(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			try
			{
				// Convert the api string to a uri
				Uri newApiEndpoint = new Uri(textBoxApiEndpoint.Text);
				// Create the stable diffusion api class to interact with the api
				StableAPI = new StableDiffusionAPI(newApiEndpoint);

				// Verify that the api endpoint is working
				bool isVerified = await StableAPI.VerifyEndpointAsync();
				SetApplicationState(isVerified ? ApplicationStatesEnum.waiting_for_inference : ApplicationStatesEnum.waiting_for_api);
				if (isVerified)
				{
					// Refresh models list by simulating a button click
					buttonRefreshModels.PerformClick();
					buttonRefreshControlNetModels.PerformClick();
				}
				else
				{
					MessageBox.Show($"Failed to ping the endpoint on '{newApiEndpoint.AbsoluteUri}'", "API Endpoint Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				if (ex is UriFormatException)
				{
					MessageBox.Show(ex.Message + "\nPlease make sure your URL start with 'http' or 'https'.", "Invalid API Endpoint String", MessageBoxButtons.OK, MessageBoxIcon.Error);
					// Stop executing the method.
					return;
				}
				else
				{
					throw;
				}
			}
			finally
			{
				UseWaitCursor = false;
			}
		}

		// == Stable Diffusion WebUI Configuration ==

		/// <summary>
		/// Called when the user click on the "Refresh" button for stable diffusion & controlnet.
		/// </summary>
		private async void buttonRefreshModels_Click(object sender, EventArgs e)
		{
			Button currentButton = (Button)sender;
			UseWaitCursor = true;
			switch (currentButton.Tag)
			{
				case "stableDiffusion":
					comboBoxModels.Items.Clear();
					comboBoxVaes.Items.Clear();
					// Get Models & Vae from the API
					List<StableDiffusionModelModel>? stableDiffusionModels = await StableAPI.GetModels();
					List<StableDiffusionVAEModel>? stableDiffusionVAEs = await StableAPI.GetVaes();
					if (stableDiffusionModels != null && stableDiffusionVAEs != null)
					{
						// Add VAE "pre-existing" options
						comboBoxVaes.Items.AddRange(["Automatic", "None"]);
						// Add the results to the comboBox
						stableDiffusionModels?.ForEach(model => comboBoxModels.Items.Add(model.model_name));
						stableDiffusionVAEs?.ForEach(vae => comboBoxVaes.Items.Add(vae.model_name));
						// Set default selected item
						comboBoxModels.SelectedIndex = 0;
						comboBoxVaes.SelectedIndex = 0;
						// Enable apply change button since valid values have been added the two comboBox
						buttonApplyChanges.Enabled = true;
					}
					else
					{
						MessageBox.Show("Failed to load the list of stable-diffusion/VAE models.", "Refresh Models", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					break;
				case "controlnet":
					comboBoxUnit1ControlNetModel.Items.Clear();
					comboBoxUnit2ControlNetModel.Items.Clear();
					// Get Controlnet models from the API
					StableDiffusionControlNetModelListModel? stableDiffusionControlNetModels = await StableAPI.GetControlNetModels();
					if (stableDiffusionControlNetModels != null)
					{
						// Add the results to the comboBox
						foreach (string controlNetModelName in stableDiffusionControlNetModels.model_list)
						{
							comboBoxUnit1ControlNetModel.Items.Add(controlNetModelName);
							comboBoxUnit2ControlNetModel.Items.Add(controlNetModelName);
						}
						// Set default selected item
						comboBoxUnit1ControlNetModel.SelectedIndex = 0;
						comboBoxUnit2ControlNetModel.SelectedIndex = 0;
					}
					else
					{
						MessageBox.Show("Failed to load the list of controlnet models.", "Refresh Models", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					break;
				default:
					throw new NotImplementedException();
			}
			UseWaitCursor = false;
		}

		/// <summary>
		/// Called when the user click on the "Apply Changes" button.
		/// </summary>
		private async void buttonApplyChanges_Click(object sender, EventArgs e)
		{
			UseWaitCursor = true;
			// Create the new settings model
			StableDiffusionOptionsModel optionsModel = new StableDiffusionOptionsModel()
			{
				sd_model_checkpoint = comboBoxModels.Text,
				sd_vae = comboBoxVaes.Text,
				CLIP_stop_at_last_layers = (int)numericUpDownClipSkip.Value,
			};
			// Send the new settings to the API
			bool isSuccess = await StableAPI.PostOptions(optionsModel);
			if (isSuccess)
			{
				MessageBox.Show($"Updated stable diffusion settings!", "Post Options Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show($"Failed to update stable diffusion settings.", "Post Options Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			UseWaitCursor = false;
		}

		// == Picture Color Diffusion Configuration ==

		/// <summary>
		/// Called when the user click on "Select File" or "Select folder" button for the picture input path.
		/// Set the file/folder path in textBoxPicturePath. Use the button "tag" property.
		/// </summary>
		private void buttonSelectPicture_Click(object sender, EventArgs e)
		{
			Button currentButton = (Button)sender;
			DialogResult dialogResult;
			switch (currentButton.Tag)
			{
				case "file":
					dialogResult = openFileDialog1.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						textBoxPicturePath.Text = openFileDialog1.FileName;
					}
					break;
				case "folder":
					dialogResult = folderBrowserDialog1.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						textBoxPicturePath.Text = folderBrowserDialog1.SelectedPath;
					}
					break;
				default:
					MessageBox.Show($"Function 'buttonSelectPicture_Click' was called with TAG value '{currentButton.Tag}'.", "Invalid TAG Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
			}
		}

		/// <summary>
		/// Called when the user click on the "Select folder" button for the picture output path.
		/// </summary>
		private void buttonSelectPictureOutputFolder_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				textBoxPictureOutputPath.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		/// <summary>
		/// Called when the user click on the "Select file" button to select the reference picture path.
		/// </summary>
		private void buttonSelectReference_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = openFileDialog1.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				textBoxReferencePicturePath.Text = openFileDialog1.FileName;
			}
		}

		/// <summary>
		/// Called when the user click on the "clear" button to clear the reference picture path.
		/// </summary>
		private void buttonClearReference_Click(object sender, EventArgs e)
		{
			textBoxReferencePicturePath.Text = string.Empty;
		}

		/// <summary>
		/// Called by all radio buttons for the "Mode" label
		/// </summary>
		private void radioButtonSelectMode_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton currentRadioButton = (RadioButton)sender;
			if (currentRadioButton.Checked)
			{
				SelectedMode = currentRadioButton.Text;
				PictureColorDiffusionModeModel? currentConfiguration = PictureColorDiffusionModes.GetModeConfiguration(SelectedMode);
				if (currentConfiguration != null)
				{
					// Update the labels inside the Controlnet configurations groupbox to show the required model name
					// Loop trought every controls/items of the controlnet configuration groupbox
					foreach (Control itemControl in groupBoxControlnetConfiguration.Controls)
					{
						// Verify that the TAG proprety is a number
						if (int.TryParse(itemControl?.Tag?.ToString(), out int itemControlTag))
						{
							// The TAG proprety contains the UNIT number, starting from 1.
							// We remove 1 here since index start at position 0 insead of 1
							int currentUnitIndex = itemControlTag - 1;

							// All labels of in groupbox are for units, so we only ensure that the current control is a label before continuing
							if (itemControl is Label)
							{
								// We update the text in a try statement to prevent throwing a error if we have less or more than
								// the maximum amount possible of units (2 as of now, since there is 2 labels) in pictureColorDiffusionModesConfig
								try
								{
									itemControl.Text = $"UNIT {itemControl.Tag} Controlnet model for '{currentConfiguration.controlNetModelNamePerUnit[currentUnitIndex]}' :";
								}
								catch { }
							}
							else if (itemControl is ComboBox)
							{
								ComboBox comboBox = (ComboBox)itemControl;
								// Search the comboBox for a entry containing the required model name
								int matchingIndex = comboBox.Items.Cast<string>().ToList().FindIndex(value => value.Contains(currentConfiguration.controlNetModelNamePerUnit[currentUnitIndex]));
								// If a matching entry was found, overwrite the selected index with the found index
								if (matchingIndex != -1)
								{
									comboBox.SelectedIndex = matchingIndex;
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show($"Failed to find the configurations for the mode '{SelectedMode}'.", "Select Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		// == Inference ==

		/// <summary>
		/// Called when the checked value of the "Enable preview" checkbox is updated.
		/// </summary>
		private void checkBoxEnablePreview_CheckedChanged(object sender, EventArgs e)
		{
			pictureBoxPreview.Enabled = checkBoxEnablePreview.Checked;
		}

		/// <summary>
		/// Called when the checked value of the "Use YoloV8 segmentation" checkbox is updated.
		/// </summary>
		private void checkBoxUseYoloV8_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxYoloV8ONNXModels.Enabled = checkBoxUseYoloV8.Checked;
		}

		/// <summary>
		/// Called when the user click on the "Interence" button
		/// </summary>
		private async void buttonInference_Click(object sender, EventArgs e)
		{
			// Verify if required fields on the UI have been filed
			if (string.IsNullOrEmpty(textBoxPicturePath.Text) || string.IsNullOrEmpty(textBoxPictureOutputPath.Text))
			{
				MessageBox.Show("Please select a valid input & output path.", "Inference", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return; // stop method execution
			}
			if (SelectedMode == null)
			{
				MessageBox.Show("Please select a Picture Color Diffusion mode.", "Inference", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return; // stop method execution
			}
			if (checkBoxUseYoloV8.Checked && comboBoxYoloV8ONNXModels.SelectedIndex == -1)
			{
				MessageBox.Show("Please select a YoloV8 ONNX model or disable YoloV8 segmentation.", "Inference", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return; // stop method execution
			}

			// If the picture path is a directory, add the path of all file into the array, else (picture path is a file), only add one path to the array
			string[] filesPath = Directory.Exists(textBoxPicturePath.Text) ? PictureHandler.GetSupportedFilesFromDirectory(textBoxPicturePath.Text) : [textBoxPicturePath.Text];
			// Load selected mode configuration
			PictureColorDiffusionModeModel? currentModeConfiguration = PictureColorDiffusionModes.GetModeConfiguration(SelectedMode);

			if (currentModeConfiguration != null)
			{
				// Verify if required controlnet fields match the selected mode requirements
				if (comboBoxUnit1ControlNetModel.SelectedIndex == -1 || comboBoxUnit2ControlNetModel.SelectedIndex == -1 && currentModeConfiguration.controlNetUnits.Length >= 2)
				{
					MessageBox.Show("Please select a controlnet model for all UNIT fields required by your current mode.", "Inference", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return; // stop method execution
				}

				// Update application state to inference mode
				SetApplicationState(ApplicationStatesEnum.currently_in_inference);
				// Create cancellation token for the inference
				InferenceCancellationTokenSource = new CancellationTokenSource();


				// Store the number of generation completed
				int generationCompleted = 0;
				// Timespan of when the inference was started
				TimeSpan startTimeSpan = new TimeSpan(DateTime.Now.Ticks);
				// Store the YoloV8 segmentation model for the whole inference loop, if null, no model are loaded.
				YoloV8Predictor? yoloV8Predictor = null;
				// Store the name of the current yoloV8Predictor
				string yoloV8CurrentModelName = string.Empty;

				// Send fake progress at 0%
				InferenceProgress.Report(new InferenceProgressModel()
				{
					status = "Preparing...",
					completionPercent = 0,
				});

				// Additional controlnet unit used for a reference image, if null, then reference is not enabled
				StableDiffusionExtensionControlNetArg? controlNetReferenceUnitArg = null;
				// Verify if the reference picture exist (assume that the option is enabled if a path is defined)
				if (!string.IsNullOrEmpty(textBoxReferencePicturePath.Text) && File.Exists(textBoxReferencePicturePath.Text))
				{
					controlNetReferenceUnitArg = new StableDiffusionExtensionControlNetArg()
					{
						enabled = true,
						control_mode = "Balanced",
						model = "None",
						// This unit is the only unit that won't update the low_vram state if checked DURING the foreach loop
						low_vram = checkBoxControlNetLowvram.Checked,
						pixel_perfect = false,
						processor_res = 768,
						resize_mode = "Crop and Resize",
						weight = 0.5,
						module = "reference_only",
						guidance_start = 0,
						guidance_end = 0.5,
						image = new StableDiffusionExtensionControlNetArgImage()
						{
							image = PictureHandler.ImageSharpToBase64(await PictureHandler.LoadAsImageSharp(textBoxReferencePicturePath.Text))
						}
					};
				}
				// Loop trought all files selected by the user
				foreach (string filePath in filesPath)
				{
					// If a cancellation was requested, exit the foreach loop
					if (InferenceCancellationTokenSource.IsCancellationRequested) break;
					// Calculate the batch completion on a 0-100% (generationCompleted of filesPath.Length on 100%)
					int completionPercent = (int)Math.Round((double)generationCompleted / filesPath.Length * 100);

					ImageSharp.Image<Rgba32> originalImage = await PictureHandler.LoadAsImageSharp(filePath);
					string originalImageBase64 = PictureHandler.ImageSharpToBase64(originalImage);
					string newPrompt = currentModeConfiguration.prompt + textBoxPrompt.Text;
					string newNegativePrompt = currentModeConfiguration.negative_prompt + textBoxNegativePrompt.Text;
					// Dynamically resize the picture to the max size allowed by the current mode
					// if the keep original size checkbox isn't checked
					ImageSharp.Size originalImageSize = checkBoxKeepOriginalSize.Checked ? originalImage.Size : PictureHandler.DynamicResize(originalImage.Size, currentModeConfiguration.dynamicResizeMax);

					// If interrogation is enabled
					if (checkBoxUseInterrogation.Checked)
					{
						// Make the interogation request to get prompt that describe the original picture
						StableDiffusionInterrogateRequestResponseModel? interrogateResponse = await StableAPI.PostInterrogate(originalImageBase64, currentModeConfiguration.interogateModel);
						// If the request was a success, add the prompt to the end of the current mode one
						if (interrogateResponse != null)
						{
							newPrompt += PictureColorDiffusionFilter.Process(interrogateResponse.caption);
						}
					}

					// Store the content of the originalImage that matched the mask of the YoloV8 model
					Image<Rgba32>? originalImageMaskContent = null;
					// If YoloV8 segmentation is enabled
					if (checkBoxUseYoloV8.Checked)
					{
						InferenceProgress.Report(new InferenceProgressModel()
						{
							status = "Interrogating YoloV8 model",
							completionPercent = completionPercent,
						});
						// Load the yoloV8 model if it wasn't loaded before OR if the user selectioned a new model during inference
						if (yoloV8Predictor == null || yoloV8CurrentModelName != comboBoxYoloV8ONNXModels.Text)
						{
							// If a user selectioned a new model, dispose of the previous one
							yoloV8Predictor?.Dispose();
							// Load the model
							yoloV8Predictor = YoloV8Predictor.Create(GetONNXModelPathFromName(comboBoxYoloV8ONNXModels.Text));
						}
						// Inference the original image on the model
						SegmentationResult segResult = await yoloV8Predictor.SegmentAsync(originalImage);

						// Get the mask from the inference segmentation results
						Image<Rgba32> resultMask;
						PictureHandler.GetImageMaskFromSegmentationResult(segResult, out resultMask);

						// Get the content on originalImage that matches the mask
						originalImageMaskContent = PictureHandler.ExtractImageFromMask(originalImage, resultMask);
						// Free memory from the results mask
						resultMask?.Dispose();
					}

					// Get the controlnet extension units configuration for the current mode
					StableDiffusionExtensionControlNetModel extensionControlNetConfiguration = GetControlNetExtensionConfiguration(SelectedMode, originalImageBase64, [comboBoxUnit1ControlNetModel.Text, comboBoxUnit2ControlNetModel.Text]);
					// If reference is enabled (picture path was previously defined)
					if (controlNetReferenceUnitArg != null)
					{
						// Add the reference picture unit config into the current mode configuration
						extensionControlNetConfiguration.args = extensionControlNetConfiguration.args.Append(controlNetReferenceUnitArg).ToArray();
					}

					StableDiffusionProcessingTxt2ImgModel requestModel = new StableDiffusionProcessingTxt2ImgModel()
					{
						prompt = newPrompt,
						negative_prompt = newNegativePrompt,
						width = originalImageSize.Width,
						height = originalImageSize.Height,
						seed = (int)numericUpDownSeed.Value,
						// The number of steps for samplers are taken from https://youtu.be/Ek5r0eRJvy8?t=143
						steps = 12,
						cfg_scale = 7,
						sampler_name = comboBoxSampler.Text,
						alwayson_scripts = new StableDiffusionProcessingAlwaysonScriptsModel()
						{
							ControlNet = extensionControlNetConfiguration,
						}
					};
					// We don't need the original picture anymore, we dispose of it to allow the GB to collect
					originalImage.Dispose();

					// Update inference progress
					InferenceProgress.Report(new InferenceProgressModel()
					{
						status = "Generating picture",
						completionPercent = completionPercent,
					});

					// Generate the picture
					StableDiffusionImageResponseModel? result = await StableAPI.PostTxt2Img(requestModel);
					if (result != null)
					{
						// Convert the base64 result to a image
						ImageSharp.Image generatedImage = await PictureHandler.Base64ToImageSharp(result.images[0]);

						// If YoloV8 segmentation is enabled and mask exist
						if (checkBoxUseYoloV8.Checked && originalImageMaskContent != null)
						{
							// Resize the mask to the same size as the generated picture
							originalImageMaskContent.Mutate(m => m.Resize(generatedImage.Size));
							// Add the mask on top of the generatedImage
							generatedImage.Mutate(m => m.DrawImage(originalImageMaskContent, 1f));
							// Free memory from the content mask
							originalImageMaskContent?.Dispose();
						}

						// If metadata is enabled, set the comments metadata, stable diffusion webui already include the parameters metadata
						PngMetadata? generatedImageMetadata;
						bool isMetadataFound = generatedImage.Metadata.TryGetPngMetadata(out generatedImageMetadata);
						if (checkBoxIncludeMetadata.Checked)
						{
							// If the metadata exist as a png
							if (isMetadataFound && generatedImageMetadata != null)
							{
								// Add the "comments" key
								generatedImageMetadata.TextData.Add(new PngTextData("comments", $"Made with PictureColorDiffusion v{Application.ProductVersion} using mode '{SelectedMode}'.\n" +
								/// Additional info about the generation
								$"\nControlnet lowvram: {checkBoxControlNetLowvram.Checked}" +
								$"\nReference picture: {controlNetReferenceUnitArg != null}" +
								$"\nKeep original size: {checkBoxKeepOriginalSize.Checked}" +
								$"\nUse interrogation: {checkBoxUseInterrogation.Checked}", string.Empty, string.Empty));
							}
						}
						else // Remove the metadata added by stable diffusion webui
						{
							// If the metadata exist as a png
							if (isMetadataFound && generatedImageMetadata != null)
							{
								// Get the list of all text data stored
								List<PngTextData> textDataList = (List<PngTextData>)generatedImageMetadata.TextData;
								// Remove the content of keyword named "parameters" since Stable diffusion webui add the keyword by default
								textDataList.RemoveAll(item => item.Keyword == "parameters");
							}
						}

						// Save the picture in the output directory with the same name
						// We save as PNG as the generated images returned by stable diffusion seems to always have the PNG mime type
						await generatedImage.SaveAsPngAsync(Path.Combine(textBoxPictureOutputPath.Text, Path.GetFileNameWithoutExtension(filePath) + ".png"), new PngEncoder() { CompressionLevel = PngCompressionLevel.DefaultCompression });

						if (checkBoxEnablePreview.Checked)
						{
							// Dispose of the previous generated image
							pictureBoxPreview.Image?.Dispose();
							// Set the current image
							pictureBoxPreview.Image = PictureHandler.ImageSharpToBitmap(generatedImage);
						}
						else
						{
							// Dispose of the current image
							generatedImage.Dispose();
						}
					}
					else
					{
						DialogResult dialogResult = MessageBox.Show("Failed to get results from stable diffusion api.\nPress 'Cancel' to stop the inference.", "Inference", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
						if (dialogResult == DialogResult.Cancel)
						{
							InferenceCancellationTokenSource.Cancel();
						}
					}
					// Increment by 1 the number of generation completed, even if it failed since we don't retry failed generation
					generationCompleted++;
				}
				// All images have been generated
				TimeSpan endTimeSpan = new TimeSpan(DateTime.Now.Ticks);
				InferenceProgress.Report(new InferenceProgressModel()
				{
					status = $"Completed in {endTimeSpan.Subtract(startTimeSpan).ToString("c")}",
					completionPercent = 100,
				});
				// Free the ram from the yolov8 model since inference ended
				yoloV8Predictor?.Dispose();
				// Force GC to collect to prevent high memory usage
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				// Change application state to recieve a new inference request
				SetApplicationState(ApplicationStatesEnum.waiting_for_inference);
			}
			else
			{
				MessageBox.Show($"Failed to find the configurations for the mode '{SelectedMode}'.", "Inference", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Called when the user click on the "Stop" button
		/// </summary>
		private void buttonStopInference_Click(object sender, EventArgs e)
		{
			InferenceCancellationTokenSource?.Cancel();
			InferenceProgress.Report(new InferenceProgressModel()
			{
				status = "Waiting the end of the current generation before cancelling...",
				completionPercent = 0,
			});
		}

		/// <summary>
		/// Called when the user click on the "Inference YoloV8 Detections " toolStripMenu on the Inference button
		/// Used to debug the detections of the YoloV8 model
		/// </summary>
		private async void InferenceYoloV8DetectionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Verify that a input & output path was set along with a YoloV8 onnx model
			if (!string.IsNullOrEmpty(textBoxPicturePath.Text) && !string.IsNullOrEmpty(textBoxPictureOutputPath.Text) && comboBoxYoloV8ONNXModels.SelectedIndex == -1)
			{
				// Create cancellation token for the inference
				InferenceCancellationTokenSource = new CancellationTokenSource();
				// Set application to inference mode
				SetApplicationState(ApplicationStatesEnum.currently_in_inference);

				string[] filesPath = Directory.Exists(textBoxPicturePath.Text) ? PictureHandler.GetSupportedFilesFromDirectory(textBoxPicturePath.Text) : [textBoxPicturePath.Text];
				// Load the yoloV8 model
				using YoloV8Predictor yoloV8Predictor = YoloV8Predictor.Create(GetONNXModelPathFromName(comboBoxYoloV8ONNXModels.Text));

				int interrogationCompleted = 0;
				foreach (string filePath in filesPath)
				{
					// If a cancellation was requested, exit the foreach loop
					if (InferenceCancellationTokenSource.IsCancellationRequested) break;

					// Load the picture as a Imagesharp
					using ImageSharp.Image currentPicture = await PictureHandler.LoadAsImageSharp(filePath);

					// Update progress
					InferenceProgress.Report(new InferenceProgressModel()
					{
						status = "DEBUG | Interrogating YoloV8 model",
						// Get % of interrogationCompleted on filesPath.Length from 0-100%
						completionPercent = (int)Math.Round((double)interrogationCompleted / filesPath.Length * 100),
					});
					// Inference the picture on the model
					SegmentationResult result = await yoloV8Predictor.SegmentAsync(currentPicture);
					// Take the model detections (mask & confidences) and put them on the original image
					using ImageSharp.Image imageWithResults = await result.PlotImageAsync(currentPicture);
					// Save the picture as a png with best compression (smaller file size)
					await imageWithResults.SaveAsPngAsync(Path.Combine(textBoxPictureOutputPath.Text, Path.GetFileNameWithoutExtension(filePath) + ".png"), new PngEncoder() { CompressionLevel = PngCompressionLevel.BestCompression });
					interrogationCompleted++;
				}

				// Free the ram from the yolov8 model since inference ended
				yoloV8Predictor?.Dispose();
				// Update progress as completed
				InferenceProgress.Report(new InferenceProgressModel()
				{
					status = "DEBUG | Finished interrogating YoloV8 model",
					completionPercent = 100,
				});
				// Set application to waiting for inference mode
				SetApplicationState(ApplicationStatesEnum.waiting_for_inference);
			}
			else
			{
				MessageBox.Show("Please select a valid input & output path & YoloV8 ONNX model.", "Inference YoloV8 Detections", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Called when the user click on the "Inference YoloV8 Mask Difference " toolStripMenu on the Inference button
		/// Used to debug the YoloV8 Mask separation
		/// </summary>
		private async void InferenceYoloV8MaskDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Verify that a input & output path was set along with a YoloV8 onnx model
			if (!string.IsNullOrEmpty(textBoxPicturePath.Text) && !string.IsNullOrEmpty(textBoxPictureOutputPath.Text) && comboBoxYoloV8ONNXModels.SelectedIndex == -1)
			{
				// Create cancellation token for the inference
				InferenceCancellationTokenSource = new CancellationTokenSource();
				// Set application to inference mode
				SetApplicationState(ApplicationStatesEnum.currently_in_inference);

				string[] filesPath = Directory.Exists(textBoxPicturePath.Text) ? PictureHandler.GetSupportedFilesFromDirectory(textBoxPicturePath.Text) : [textBoxPicturePath.Text];
				// Load the yoloV8 model
				using YoloV8Predictor yoloV8Predictor = YoloV8Predictor.Create(GetONNXModelPathFromName(comboBoxYoloV8ONNXModels.Text));

				int interrogationCompleted = 0;
				foreach (string filePath in filesPath)
				{
					// If a cancellation was requested, exit the foreach loop
					if (InferenceCancellationTokenSource.IsCancellationRequested) break;

					// Load the picture as a Imagesharp Image<Rgba32>
					using ImageSharp.Image<Rgba32> currentPicture = await PictureHandler.LoadAsImageSharp(filePath);

					// Update progress
					InferenceProgress.Report(new InferenceProgressModel()
					{
						status = "DEBUG | Interrogating YoloV8 model",
						// Get % of interrogationCompleted on filesPath.Length from 0-100%
						completionPercent = (int)Math.Round((double)interrogationCompleted / filesPath.Length * 100),
					});
					// Inference the picture on the model
					SegmentationResult result = await yoloV8Predictor.SegmentAsync(currentPicture);

					// Get the mask from the inference results
					Image<Rgba32> resultMask;
					PictureHandler.GetImageMaskFromSegmentationResult(result, out resultMask);
					// Save the mask
					await resultMask.SaveAsPngAsync(Path.Combine(textBoxPictureOutputPath.Text, Path.GetFileNameWithoutExtension(filePath) + "_mask.png"), new PngEncoder() { CompressionLevel = PngCompressionLevel.BestCompression });

					// Get the content on currentPicture that matches the mask
					using Image<Rgba32> currentPictureMaskContent = PictureHandler.ExtractImageFromMask(currentPicture, resultMask);

					// Save the content that matched the mask
					await currentPictureMaskContent.SaveAsPngAsync(Path.Combine(textBoxPictureOutputPath.Text, Path.GetFileNameWithoutExtension(filePath) + "_mask_result.png"), new PngEncoder() { CompressionLevel = PngCompressionLevel.BestCompression });
					interrogationCompleted++;
					// Disposes of images that could not use "using"
					resultMask?.Dispose();
				}

				// Free the ram from the yolov8 model since inference ended
				yoloV8Predictor?.Dispose();
				// Update progress as completed
				InferenceProgress.Report(new InferenceProgressModel()
				{
					status = "DEBUG | Finished interrogating YoloV8 model",
					completionPercent = 100,
				});
				// Set application to waiting for inference mode
				SetApplicationState(ApplicationStatesEnum.waiting_for_inference);
			}
			else
			{
				MessageBox.Show("Please select a valid input & output path & YoloV8 ONNX model.", "Inference YoloV8 Mask Difference", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Called when the user click on the preview picture box
		/// </summary>
		private void pictureBoxPreview_Click(object sender, EventArgs e)
		{
			// Verify that a picture is currently in the preview
			if (pictureBoxPreview.Image != null)
			{
				// Try to close the previous preview form saved into the Tag value of pictureBoxPreview
				// before opening a new one
				PreviewForm? previousPreviewForm = (PreviewForm?)pictureBoxPreview.Tag;
				previousPreviewForm?.Close();

				PreviewForm previewForm = new PreviewForm(pictureBoxPreview.Image);
				// Since we don't ask any input to the user, we need show the form
				// and it will be disposed when he closes it.
				previewForm.Show();

				// We save the current from into the Tag value of pictureBoxPreview to force the closure later
				pictureBoxPreview.Tag = previewForm;
			}
		}
		#endregion

		/// <summary>
		/// Refresh the YoloV8 ONNX model list of the inference section. Called when files are added, removed or renamed from the models folder.
		/// </summary>
		private void fileSystemWatcherRefreshONNXModels(object? sender, FileSystemEventArgs? e)
		{
			// Get the path of files inside the directory that fileSystemWatcher watch (only .onnx models)
			string[] modelsPath = Directory.GetFiles(fileSystemWatcherYoloV8ONNXModels.Path, "*.onnx");
			comboBoxYoloV8ONNXModels.Items.Clear();
			comboBoxYoloV8ONNXModels.Items.AddRange(modelsPath.Select(modelPath => Path.GetFileNameWithoutExtension(modelPath)).ToArray());
		}

		/// <summary>
		/// This method enable controls and disable controls depending of the application state
		/// </summary>
		/// <param name="currentState">The current state of the application</param>
		private void SetApplicationState(ApplicationStatesEnum currentState)
		{
			switch (currentState)
			{
				case ApplicationStatesEnum.waiting_for_api:
					// Set stable diffusion api configuration controls
					groupBoxStableDiffusionAPIConfig.Enabled = true;
					// Set stable diffusion webui configuration controls
					groupBoxStableDiffusionWebUIConfig.Enabled = false;
					// Set picture color diffusion configuration controls
					groupBoxPictureColorDiffusionConfig.Enabled = false;
					// set inferance controls
					groupBoxInference.Enabled = false;
					break;
				case ApplicationStatesEnum.waiting_for_inference:
					// Set stable diffusion api configuration controls
					groupBoxStableDiffusionAPIConfig.Enabled = true;
					// Set stable diffusion webui configuration controls
					groupBoxStableDiffusionWebUIConfig.Enabled = true;
					// Set picture color diffusion configuration controls
					groupBoxPictureColorDiffusionConfig.Enabled = true;
					// set inferance controls
					groupBoxInference.Enabled = true;
					buttonInference.Enabled = true;
					buttonStopInference.Enabled = false;
					break;
				case ApplicationStatesEnum.currently_in_inference:
					// Set stable diffusion api configuration controls
					groupBoxStableDiffusionAPIConfig.Enabled = false;
					// Set stable diffusion webui configuration controls
					groupBoxStableDiffusionWebUIConfig.Enabled = false;
					// Set picture color diffusion configuration controls
					groupBoxPictureColorDiffusionConfig.Enabled = false;
					// set inferance controls
					buttonInference.Enabled = false;
					buttonStopInference.Enabled = true;
					break;
			}
		}

		/// <summary>
		/// Get the full path of the onnx model by it's name
		/// </summary>
		/// <param name="name">The name of the onnx model, without the extension</param>
		/// <returns>The full path of the onnx model</returns>
		/// <exception cref="FileNotFoundException"></exception>
		private string GetONNXModelPathFromName(string name)
		{
			string modelPath = $"{ModelsDirectoryPath}/{name}.onnx";
			if (File.Exists(modelPath)) return modelPath;
			throw new FileNotFoundException($"Could not find the onnx model under '{modelPath}'");
		}

		/// <summary>
		/// Get a configuration model for the controlnet extension
		/// </summary>
		/// <param name="mode">The mode is the value which will determine what hard-coded values to load from <see cref="PictureColorDiffusionModel.modes"/></param>
		/// <param name="originalPictureBase64">The original picture converted in base64</param>
		/// <param name="modelNamePerUnit">A array where each item is equal to one controlnet unit in ascending order (0 to 3). The value need to be the model name</param>
		/// <returns>A controlnet extension configuration for <see cref="StableDiffusionProcessingAlwaysonScriptsModel"/></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// <exception cref="ArgumentException"></exception>
		private StableDiffusionExtensionControlNetModel GetControlNetExtensionConfiguration(string mode, string originalPictureBase64, string[] modelNamePerUnit)
		{

			// The new controlnet extension configuration
			StableDiffusionExtensionControlNetModel controlNetExtensionConfiguration = new StableDiffusionExtensionControlNetModel();

			// Get the hard-coded configuration of the current 'mode'
			PictureColorDiffusionModeModel? currentModeConfigurations = PictureColorDiffusionModes.GetModeConfiguration(mode);
			if (currentModeConfigurations == null)
			{
				throw new NotImplementedException($"The mode '{mode}' does not exist.");
			}

			// Set the configuration of the units for the controlnet extension with the hard-coded values set for the current mode
			controlNetExtensionConfiguration.args = currentModeConfigurations.controlNetUnits;

			// Ensure that there is enought modelNamePerUnit to configure all controlnet units
			if (controlNetExtensionConfiguration.args.Length > modelNamePerUnit.Length)
			{
				throw new ArgumentException("There is not enought modelNamePerUnit for the total number of Controlnet units configured.");
			}

			// Add base64 original picture inside units & configure global settings
			for (int i = 0; i < controlNetExtensionConfiguration.args.Length; i++)
			{
				// Enable all existing units
				controlNetExtensionConfiguration.args[i].enabled = true;
				// Set model according to modelPerUnit array
				controlNetExtensionConfiguration.args[i].model = modelNamePerUnit[i];
				// Enable/disable lowvram
				controlNetExtensionConfiguration.args[i].low_vram = checkBoxControlNetLowvram.Checked;
				// Add base64 original picture
				controlNetExtensionConfiguration.args[i].image = new StableDiffusionExtensionControlNetArgImage()
				{
					image = originalPictureBase64,
				};
			}

			return controlNetExtensionConfiguration;
		}
	}
}
