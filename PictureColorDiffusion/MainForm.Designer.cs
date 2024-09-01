namespace PictureColorDiffusion
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			labelPicturePath = new Label();
			folderBrowserDialog1 = new FolderBrowserDialog();
			textBoxPicturePath = new TextBox();
			buttonSelectPicture = new Button();
			buttonSelectPicturesFolder = new Button();
			openFileDialog1 = new OpenFileDialog();
			groupBoxStableDiffusionAPIConfig = new GroupBox();
			buttonVerifyApiEndpoint = new Button();
			labelApiEndpoint = new Label();
			textBoxApiEndpoint = new TextBox();
			groupBoxPictureColorDiffusionConfig = new GroupBox();
			buttonClearReference = new Button();
			labelReferencePicturePath = new Label();
			textBoxReferencePicturePath = new TextBox();
			buttonSelectReference = new Button();
			groupBoxControlnetConfiguration = new GroupBox();
			buttonRefreshControlNetModels = new Button();
			comboBoxUnit2ControlNetModel = new ComboBox();
			comboBoxUnit1ControlNetModel = new ComboBox();
			labelUnit2ControlNetModel = new Label();
			labelUnit1ControlNetModel = new Label();
			radioButtonModeManga = new RadioButton();
			labelMode = new Label();
			labelPictureOutputPath = new Label();
			textBoxPictureOutputPath = new TextBox();
			buttonSelectPictureOutputFolder = new Button();
			groupBoxStableDiffusionWebUIConfig = new GroupBox();
			buttonApplyChanges = new Button();
			labelClipSkip = new Label();
			numericUpDownClipSkip = new NumericUpDown();
			comboBoxVaes = new ComboBox();
			labelVae = new Label();
			comboBoxModels = new ComboBox();
			buttonRefreshModels = new Button();
			labelModel = new Label();
			groupBoxInference = new GroupBox();
			checkBoxIncludeMetadata = new CheckBox();
			labelSampler = new Label();
			comboBoxSampler = new ComboBox();
			labelAdditionalPrompt = new Label();
			textBoxNegativePrompt = new TextBox();
			textBoxPrompt = new TextBox();
			labelSeed = new Label();
			numericUpDownSeed = new NumericUpDown();
			checkBoxUseInterrogation = new CheckBox();
			checkBoxKeepOriginalSize = new CheckBox();
			checkBoxControlNetLowvram = new CheckBox();
			labelProgressStatus = new Label();
			progressBarInference = new ProgressBar();
			pictureBoxPreview = new PictureBox();
			checkBoxEnablePreview = new CheckBox();
			buttonStopInference = new Button();
			buttonInference = new Button();
			contextMenuStripButtonInference = new ContextMenuStrip(components);
			DebugYoloV8SegmentationToolStripMenuItem = new ToolStripMenuItem();
			InferenceYoloV8DetectionsToolStripMenuItem = new ToolStripMenuItem();
			InferenceYoloV8MaskDifferenceToolStripMenuItem = new ToolStripMenuItem();
			toolTip1 = new ToolTip(components);
			groupBoxStableDiffusionAPIConfig.SuspendLayout();
			groupBoxPictureColorDiffusionConfig.SuspendLayout();
			groupBoxControlnetConfiguration.SuspendLayout();
			groupBoxStableDiffusionWebUIConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownClipSkip).BeginInit();
			groupBoxInference.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownSeed).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
			contextMenuStripButtonInference.SuspendLayout();
			SuspendLayout();
			// 
			// labelPicturePath
			// 
			labelPicturePath.AutoSize = true;
			labelPicturePath.ForeColor = Color.WhiteSmoke;
			labelPicturePath.Location = new Point(6, 28);
			labelPicturePath.Name = "labelPicturePath";
			labelPicturePath.Size = new Size(144, 17);
			labelPicturePath.TabIndex = 0;
			labelPicturePath.Text = "Picture(s) Path :";
			// 
			// folderBrowserDialog1
			// 
			folderBrowserDialog1.AddToRecent = false;
			folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// textBoxPicturePath
			// 
			textBoxPicturePath.Location = new Point(156, 28);
			textBoxPicturePath.Name = "textBoxPicturePath";
			textBoxPicturePath.PlaceholderText = "Please select a input file/folder.";
			textBoxPicturePath.ReadOnly = true;
			textBoxPicturePath.Size = new Size(451, 23);
			textBoxPicturePath.TabIndex = 100;
			// 
			// buttonSelectPicture
			// 
			buttonSelectPicture.Location = new Point(613, 28);
			buttonSelectPicture.Name = "buttonSelectPicture";
			buttonSelectPicture.Size = new Size(120, 23);
			buttonSelectPicture.TabIndex = 5;
			buttonSelectPicture.Tag = "file";
			buttonSelectPicture.Text = "Select File";
			buttonSelectPicture.UseVisualStyleBackColor = true;
			buttonSelectPicture.Click += buttonSelectPicture_Click;
			// 
			// buttonSelectPicturesFolder
			// 
			buttonSelectPicturesFolder.Location = new Point(739, 28);
			buttonSelectPicturesFolder.Name = "buttonSelectPicturesFolder";
			buttonSelectPicturesFolder.Size = new Size(145, 23);
			buttonSelectPicturesFolder.TabIndex = 6;
			buttonSelectPicturesFolder.Tag = "folder";
			buttonSelectPicturesFolder.Text = "Select Folder";
			buttonSelectPicturesFolder.UseVisualStyleBackColor = true;
			buttonSelectPicturesFolder.Click += buttonSelectPicture_Click;
			// 
			// groupBoxStableDiffusionAPIConfig
			// 
			groupBoxStableDiffusionAPIConfig.Controls.Add(buttonVerifyApiEndpoint);
			groupBoxStableDiffusionAPIConfig.Controls.Add(labelApiEndpoint);
			groupBoxStableDiffusionAPIConfig.Controls.Add(textBoxApiEndpoint);
			groupBoxStableDiffusionAPIConfig.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBoxStableDiffusionAPIConfig.Location = new Point(12, 12);
			groupBoxStableDiffusionAPIConfig.Name = "groupBoxStableDiffusionAPIConfig";
			groupBoxStableDiffusionAPIConfig.Size = new Size(890, 54);
			groupBoxStableDiffusionAPIConfig.TabIndex = 0;
			groupBoxStableDiffusionAPIConfig.TabStop = false;
			groupBoxStableDiffusionAPIConfig.Text = "Stable Diffusion API Configuration";
			// 
			// buttonVerifyApiEndpoint
			// 
			buttonVerifyApiEndpoint.Location = new Point(809, 15);
			buttonVerifyApiEndpoint.Name = "buttonVerifyApiEndpoint";
			buttonVerifyApiEndpoint.Size = new Size(75, 30);
			buttonVerifyApiEndpoint.TabIndex = 1;
			buttonVerifyApiEndpoint.Text = "Verify";
			buttonVerifyApiEndpoint.UseVisualStyleBackColor = true;
			buttonVerifyApiEndpoint.Click += buttonVerifyApiEndpoint_Click;
			// 
			// labelApiEndpoint
			// 
			labelApiEndpoint.AutoSize = true;
			labelApiEndpoint.ForeColor = Color.WhiteSmoke;
			labelApiEndpoint.Location = new Point(6, 22);
			labelApiEndpoint.Name = "labelApiEndpoint";
			labelApiEndpoint.Size = new Size(120, 17);
			labelApiEndpoint.TabIndex = 101;
			labelApiEndpoint.Text = "API Endpoint :";
			// 
			// textBoxApiEndpoint
			// 
			textBoxApiEndpoint.Location = new Point(132, 22);
			textBoxApiEndpoint.MaxLength = 1000;
			textBoxApiEndpoint.Name = "textBoxApiEndpoint";
			textBoxApiEndpoint.Size = new Size(663, 23);
			textBoxApiEndpoint.TabIndex = 100;
			textBoxApiEndpoint.Text = "http://127.0.0.1:7860";
			// 
			// groupBoxPictureColorDiffusionConfig
			// 
			groupBoxPictureColorDiffusionConfig.Controls.Add(buttonClearReference);
			groupBoxPictureColorDiffusionConfig.Controls.Add(labelReferencePicturePath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(textBoxReferencePicturePath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(buttonSelectReference);
			groupBoxPictureColorDiffusionConfig.Controls.Add(groupBoxControlnetConfiguration);
			groupBoxPictureColorDiffusionConfig.Controls.Add(radioButtonModeManga);
			groupBoxPictureColorDiffusionConfig.Controls.Add(labelMode);
			groupBoxPictureColorDiffusionConfig.Controls.Add(labelPictureOutputPath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(textBoxPictureOutputPath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(buttonSelectPictureOutputFolder);
			groupBoxPictureColorDiffusionConfig.Controls.Add(labelPicturePath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(textBoxPicturePath);
			groupBoxPictureColorDiffusionConfig.Controls.Add(buttonSelectPicturesFolder);
			groupBoxPictureColorDiffusionConfig.Controls.Add(buttonSelectPicture);
			groupBoxPictureColorDiffusionConfig.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBoxPictureColorDiffusionConfig.Location = new Point(12, 163);
			groupBoxPictureColorDiffusionConfig.Name = "groupBoxPictureColorDiffusionConfig";
			groupBoxPictureColorDiffusionConfig.Size = new Size(890, 240);
			groupBoxPictureColorDiffusionConfig.TabIndex = 0;
			groupBoxPictureColorDiffusionConfig.TabStop = false;
			groupBoxPictureColorDiffusionConfig.Text = "Picture Color Diffusion Configuration";
			// 
			// buttonClearReference
			// 
			buttonClearReference.Location = new Point(669, 94);
			buttonClearReference.Name = "buttonClearReference";
			buttonClearReference.Size = new Size(64, 25);
			buttonClearReference.TabIndex = 111;
			buttonClearReference.Tag = "file";
			buttonClearReference.Text = "Clear";
			buttonClearReference.UseVisualStyleBackColor = true;
			buttonClearReference.Click += buttonClearReference_Click;
			// 
			// labelReferencePicturePath
			// 
			labelReferencePicturePath.AutoSize = true;
			labelReferencePicturePath.ForeColor = Color.WhiteSmoke;
			labelReferencePicturePath.Location = new Point(6, 94);
			labelReferencePicturePath.Name = "labelReferencePicturePath";
			labelReferencePicturePath.Size = new Size(200, 17);
			labelReferencePicturePath.TabIndex = 108;
			labelReferencePicturePath.Text = "Reference Picture Path :";
			// 
			// textBoxReferencePicturePath
			// 
			textBoxReferencePicturePath.Location = new Point(212, 94);
			textBoxReferencePicturePath.Name = "textBoxReferencePicturePath";
			textBoxReferencePicturePath.PlaceholderText = "Please select a reference picture. (optional)";
			textBoxReferencePicturePath.ReadOnly = true;
			textBoxReferencePicturePath.Size = new Size(451, 23);
			textBoxReferencePicturePath.TabIndex = 110;
			toolTip1.SetToolTip(textBoxReferencePicturePath, "The reference picture path is a additional controlnet unit that use a picture as it's reference for all colorisations.\r\nThis option is optional.");
			// 
			// buttonSelectReference
			// 
			buttonSelectReference.Location = new Point(739, 94);
			buttonSelectReference.Name = "buttonSelectReference";
			buttonSelectReference.Size = new Size(145, 23);
			buttonSelectReference.TabIndex = 109;
			buttonSelectReference.Tag = "folder";
			buttonSelectReference.Text = "Select File";
			buttonSelectReference.UseVisualStyleBackColor = true;
			buttonSelectReference.Click += buttonSelectReference_Click;
			// 
			// groupBoxControlnetConfiguration
			// 
			groupBoxControlnetConfiguration.Controls.Add(buttonRefreshControlNetModels);
			groupBoxControlnetConfiguration.Controls.Add(comboBoxUnit2ControlNetModel);
			groupBoxControlnetConfiguration.Controls.Add(comboBoxUnit1ControlNetModel);
			groupBoxControlnetConfiguration.Controls.Add(labelUnit2ControlNetModel);
			groupBoxControlnetConfiguration.Controls.Add(labelUnit1ControlNetModel);
			groupBoxControlnetConfiguration.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBoxControlnetConfiguration.ForeColor = SystemColors.ActiveCaption;
			groupBoxControlnetConfiguration.Location = new Point(11, 150);
			groupBoxControlnetConfiguration.Name = "groupBoxControlnetConfiguration";
			groupBoxControlnetConfiguration.Size = new Size(873, 78);
			groupBoxControlnetConfiguration.TabIndex = 107;
			groupBoxControlnetConfiguration.TabStop = false;
			groupBoxControlnetConfiguration.Text = "Controlnet Configuration";
			// 
			// buttonRefreshControlNetModels
			// 
			buttonRefreshControlNetModels.ForeColor = SystemColors.ControlText;
			buttonRefreshControlNetModels.Location = new Point(728, 16);
			buttonRefreshControlNetModels.Name = "buttonRefreshControlNetModels";
			buttonRefreshControlNetModels.Size = new Size(139, 52);
			buttonRefreshControlNetModels.TabIndex = 110;
			buttonRefreshControlNetModels.Tag = "controlnet";
			buttonRefreshControlNetModels.Text = "Refresh";
			buttonRefreshControlNetModels.UseVisualStyleBackColor = true;
			buttonRefreshControlNetModels.Click += buttonRefreshModels_Click;
			// 
			// comboBoxUnit2ControlNetModel
			// 
			comboBoxUnit2ControlNetModel.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxUnit2ControlNetModel.FormattingEnabled = true;
			comboBoxUnit2ControlNetModel.Location = new Point(363, 43);
			comboBoxUnit2ControlNetModel.Name = "comboBoxUnit2ControlNetModel";
			comboBoxUnit2ControlNetModel.Size = new Size(360, 25);
			comboBoxUnit2ControlNetModel.TabIndex = 109;
			comboBoxUnit2ControlNetModel.Tag = "2";
			toolTip1.SetToolTip(comboBoxUnit2ControlNetModel, "Please select the controlnet model that matches the processor name");
			// 
			// comboBoxUnit1ControlNetModel
			// 
			comboBoxUnit1ControlNetModel.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxUnit1ControlNetModel.FormattingEnabled = true;
			comboBoxUnit1ControlNetModel.Location = new Point(363, 16);
			comboBoxUnit1ControlNetModel.Name = "comboBoxUnit1ControlNetModel";
			comboBoxUnit1ControlNetModel.Size = new Size(360, 25);
			comboBoxUnit1ControlNetModel.TabIndex = 108;
			comboBoxUnit1ControlNetModel.Tag = "1";
			toolTip1.SetToolTip(comboBoxUnit1ControlNetModel, "Please select the controlnet model that matches the processor name");
			// 
			// labelUnit2ControlNetModel
			// 
			labelUnit2ControlNetModel.AutoEllipsis = true;
			labelUnit2ControlNetModel.ForeColor = Color.WhiteSmoke;
			labelUnit2ControlNetModel.Location = new Point(6, 46);
			labelUnit2ControlNetModel.Name = "labelUnit2ControlNetModel";
			labelUnit2ControlNetModel.Size = new Size(351, 22);
			labelUnit2ControlNetModel.TabIndex = 107;
			labelUnit2ControlNetModel.Tag = "2";
			labelUnit2ControlNetModel.Text = "UNIT 2 Controlnet model for 'N/A' :";
			// 
			// labelUnit1ControlNetModel
			// 
			labelUnit1ControlNetModel.AutoEllipsis = true;
			labelUnit1ControlNetModel.ForeColor = Color.WhiteSmoke;
			labelUnit1ControlNetModel.Location = new Point(6, 19);
			labelUnit1ControlNetModel.Name = "labelUnit1ControlNetModel";
			labelUnit1ControlNetModel.Size = new Size(351, 22);
			labelUnit1ControlNetModel.TabIndex = 106;
			labelUnit1ControlNetModel.Tag = "1";
			labelUnit1ControlNetModel.Text = "UNIT 1 Controlnet model for 'N/A' :";
			// 
			// radioButtonModeManga
			// 
			radioButtonModeManga.AutoSize = true;
			radioButtonModeManga.Location = new Point(73, 124);
			radioButtonModeManga.Name = "radioButtonModeManga";
			radioButtonModeManga.Size = new Size(66, 21);
			radioButtonModeManga.TabIndex = 106;
			radioButtonModeManga.Text = "Manga";
			radioButtonModeManga.UseVisualStyleBackColor = true;
			radioButtonModeManga.CheckedChanged += radioButtonSelectMode_CheckedChanged;
			// 
			// labelMode
			// 
			labelMode.AutoSize = true;
			labelMode.ForeColor = Color.WhiteSmoke;
			labelMode.Location = new Point(11, 124);
			labelMode.Name = "labelMode";
			labelMode.Size = new Size(56, 17);
			labelMode.TabIndex = 105;
			labelMode.Text = "Mode :";
			// 
			// labelPictureOutputPath
			// 
			labelPictureOutputPath.AutoSize = true;
			labelPictureOutputPath.ForeColor = Color.WhiteSmoke;
			labelPictureOutputPath.Location = new Point(6, 61);
			labelPictureOutputPath.Name = "labelPictureOutputPath";
			labelPictureOutputPath.Size = new Size(200, 17);
			labelPictureOutputPath.TabIndex = 101;
			labelPictureOutputPath.Text = "Picture(s) Output Path :";
			// 
			// textBoxPictureOutputPath
			// 
			textBoxPictureOutputPath.Location = new Point(212, 61);
			textBoxPictureOutputPath.Name = "textBoxPictureOutputPath";
			textBoxPictureOutputPath.PlaceholderText = "Please select a output folder.";
			textBoxPictureOutputPath.ReadOnly = true;
			textBoxPictureOutputPath.Size = new Size(521, 23);
			textBoxPictureOutputPath.TabIndex = 104;
			// 
			// buttonSelectPictureOutputFolder
			// 
			buttonSelectPictureOutputFolder.Location = new Point(739, 61);
			buttonSelectPictureOutputFolder.Name = "buttonSelectPictureOutputFolder";
			buttonSelectPictureOutputFolder.Size = new Size(145, 23);
			buttonSelectPictureOutputFolder.TabIndex = 103;
			buttonSelectPictureOutputFolder.Tag = "folder";
			buttonSelectPictureOutputFolder.Text = "Select Folder";
			buttonSelectPictureOutputFolder.UseVisualStyleBackColor = true;
			buttonSelectPictureOutputFolder.Click += buttonSelectPictureOutputFolder_Click;
			// 
			// groupBoxStableDiffusionWebUIConfig
			// 
			groupBoxStableDiffusionWebUIConfig.Controls.Add(buttonApplyChanges);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(labelClipSkip);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(numericUpDownClipSkip);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(comboBoxVaes);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(labelVae);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(comboBoxModels);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(buttonRefreshModels);
			groupBoxStableDiffusionWebUIConfig.Controls.Add(labelModel);
			groupBoxStableDiffusionWebUIConfig.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBoxStableDiffusionWebUIConfig.Location = new Point(12, 72);
			groupBoxStableDiffusionWebUIConfig.Name = "groupBoxStableDiffusionWebUIConfig";
			groupBoxStableDiffusionWebUIConfig.Size = new Size(890, 85);
			groupBoxStableDiffusionWebUIConfig.TabIndex = 104;
			groupBoxStableDiffusionWebUIConfig.TabStop = false;
			groupBoxStableDiffusionWebUIConfig.Text = "Stable Diffusion WebUI Configuration (Optional)";
			// 
			// buttonApplyChanges
			// 
			buttonApplyChanges.Enabled = false;
			buttonApplyChanges.ForeColor = Color.Green;
			buttonApplyChanges.Location = new Point(739, 51);
			buttonApplyChanges.Name = "buttonApplyChanges";
			buttonApplyChanges.Size = new Size(145, 28);
			buttonApplyChanges.TabIndex = 108;
			buttonApplyChanges.Text = "Apply Changes";
			toolTip1.SetToolTip(buttonApplyChanges, "Apply changes to your stable diffusion webui. These changes are persistent");
			buttonApplyChanges.UseVisualStyleBackColor = true;
			buttonApplyChanges.Click += buttonApplyChanges_Click;
			// 
			// labelClipSkip
			// 
			labelClipSkip.AutoSize = true;
			labelClipSkip.ForeColor = Color.WhiteSmoke;
			labelClipSkip.Location = new Point(6, 55);
			labelClipSkip.Name = "labelClipSkip";
			labelClipSkip.Size = new Size(96, 17);
			labelClipSkip.TabIndex = 107;
			labelClipSkip.Text = "Clip Skip :";
			// 
			// numericUpDownClipSkip
			// 
			numericUpDownClipSkip.Location = new Point(108, 53);
			numericUpDownClipSkip.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
			numericUpDownClipSkip.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numericUpDownClipSkip.Name = "numericUpDownClipSkip";
			numericUpDownClipSkip.Size = new Size(74, 23);
			numericUpDownClipSkip.TabIndex = 106;
			numericUpDownClipSkip.Value = new decimal(new int[] { 2, 0, 0, 0 });
			// 
			// comboBoxVaes
			// 
			comboBoxVaes.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxVaes.FormattingEnabled = true;
			comboBoxVaes.Items.AddRange(new object[] { "Automatic", "None" });
			comboBoxVaes.Location = new Point(476, 22);
			comboBoxVaes.Name = "comboBoxVaes";
			comboBoxVaes.Size = new Size(319, 25);
			comboBoxVaes.TabIndex = 3;
			// 
			// labelVae
			// 
			labelVae.AutoSize = true;
			labelVae.ForeColor = Color.WhiteSmoke;
			labelVae.Location = new Point(422, 22);
			labelVae.Name = "labelVae";
			labelVae.Size = new Size(48, 17);
			labelVae.TabIndex = 105;
			labelVae.Text = "VAE :";
			// 
			// comboBoxModels
			// 
			comboBoxModels.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxModels.FormattingEnabled = true;
			comboBoxModels.Location = new Point(76, 22);
			comboBoxModels.Name = "comboBoxModels";
			comboBoxModels.Size = new Size(303, 25);
			comboBoxModels.TabIndex = 2;
			// 
			// buttonRefreshModels
			// 
			buttonRefreshModels.Location = new Point(809, 15);
			buttonRefreshModels.Name = "buttonRefreshModels";
			buttonRefreshModels.Size = new Size(75, 30);
			buttonRefreshModels.TabIndex = 4;
			buttonRefreshModels.Tag = "stableDiffusion";
			buttonRefreshModels.Text = "Refresh";
			buttonRefreshModels.UseVisualStyleBackColor = true;
			buttonRefreshModels.Click += buttonRefreshModels_Click;
			// 
			// labelModel
			// 
			labelModel.AutoSize = true;
			labelModel.ForeColor = Color.WhiteSmoke;
			labelModel.Location = new Point(6, 22);
			labelModel.Name = "labelModel";
			labelModel.Size = new Size(64, 17);
			labelModel.TabIndex = 101;
			labelModel.Text = "Model :";
			// 
			// groupBoxInference
			// 
			groupBoxInference.Controls.Add(checkBoxIncludeMetadata);
			groupBoxInference.Controls.Add(labelSampler);
			groupBoxInference.Controls.Add(comboBoxSampler);
			groupBoxInference.Controls.Add(labelAdditionalPrompt);
			groupBoxInference.Controls.Add(textBoxNegativePrompt);
			groupBoxInference.Controls.Add(textBoxPrompt);
			groupBoxInference.Controls.Add(labelSeed);
			groupBoxInference.Controls.Add(numericUpDownSeed);
			groupBoxInference.Controls.Add(checkBoxUseInterrogation);
			groupBoxInference.Controls.Add(checkBoxKeepOriginalSize);
			groupBoxInference.Controls.Add(checkBoxControlNetLowvram);
			groupBoxInference.Controls.Add(labelProgressStatus);
			groupBoxInference.Controls.Add(progressBarInference);
			groupBoxInference.Controls.Add(pictureBoxPreview);
			groupBoxInference.Controls.Add(checkBoxEnablePreview);
			groupBoxInference.Controls.Add(buttonStopInference);
			groupBoxInference.Controls.Add(buttonInference);
			groupBoxInference.Font = new Font("Cascadia Mono", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBoxInference.Location = new Point(12, 409);
			groupBoxInference.Name = "groupBoxInference";
			groupBoxInference.Size = new Size(890, 315);
			groupBoxInference.TabIndex = 105;
			groupBoxInference.TabStop = false;
			groupBoxInference.Text = "Inference";
			// 
			// checkBoxIncludeMetadata
			// 
			checkBoxIncludeMetadata.AutoSize = true;
			checkBoxIncludeMetadata.Checked = true;
			checkBoxIncludeMetadata.CheckState = CheckState.Checked;
			checkBoxIncludeMetadata.ForeColor = Color.WhiteSmoke;
			checkBoxIncludeMetadata.Location = new Point(11, 103);
			checkBoxIncludeMetadata.Name = "checkBoxIncludeMetadata";
			checkBoxIncludeMetadata.Size = new Size(155, 21);
			checkBoxIncludeMetadata.TabIndex = 115;
			checkBoxIncludeMetadata.Text = "Include metadata";
			toolTip1.SetToolTip(checkBoxIncludeMetadata, "Include metadata about generation settings in the output picture");
			checkBoxIncludeMetadata.UseVisualStyleBackColor = true;
			// 
			// labelSampler
			// 
			labelSampler.AutoSize = true;
			labelSampler.ForeColor = Color.WhiteSmoke;
			labelSampler.Location = new Point(11, 162);
			labelSampler.Name = "labelSampler";
			labelSampler.Size = new Size(80, 17);
			labelSampler.TabIndex = 114;
			labelSampler.Text = "Sampler :";
			// 
			// comboBoxSampler
			// 
			comboBoxSampler.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxSampler.FormattingEnabled = true;
			comboBoxSampler.Items.AddRange(new object[] { "Euler", "Euler a", "DDIM", "LCM", "DPM++ 2M Karras" });
			comboBoxSampler.Location = new Point(97, 159);
			comboBoxSampler.Name = "comboBoxSampler";
			comboBoxSampler.Size = new Size(141, 25);
			comboBoxSampler.TabIndex = 113;
			toolTip1.SetToolTip(comboBoxSampler, "Change the generation sampler.\r\nEuler is recommended.");
			// 
			// labelAdditionalPrompt
			// 
			labelAdditionalPrompt.AutoSize = true;
			labelAdditionalPrompt.ForeColor = Color.WhiteSmoke;
			labelAdditionalPrompt.Location = new Point(275, 22);
			labelAdditionalPrompt.Name = "labelAdditionalPrompt";
			labelAdditionalPrompt.Size = new Size(104, 17);
			labelAdditionalPrompt.TabIndex = 109;
			labelAdditionalPrompt.Text = "Additional :";
			// 
			// textBoxNegativePrompt
			// 
			textBoxNegativePrompt.Location = new Point(385, 128);
			textBoxNegativePrompt.Multiline = true;
			textBoxNegativePrompt.Name = "textBoxNegativePrompt";
			textBoxNegativePrompt.PlaceholderText = "Negative prompt";
			textBoxNegativePrompt.ScrollBars = ScrollBars.Vertical;
			textBoxNegativePrompt.Size = new Size(319, 105);
			textBoxNegativePrompt.TabIndex = 112;
			toolTip1.SetToolTip(textBoxNegativePrompt, "Additional negative prompt added after the mode negative prompt");
			// 
			// textBoxPrompt
			// 
			textBoxPrompt.Location = new Point(385, 22);
			textBoxPrompt.Multiline = true;
			textBoxPrompt.Name = "textBoxPrompt";
			textBoxPrompt.PlaceholderText = "Prompt";
			textBoxPrompt.ScrollBars = ScrollBars.Vertical;
			textBoxPrompt.Size = new Size(319, 105);
			textBoxPrompt.TabIndex = 111;
			toolTip1.SetToolTip(textBoxPrompt, "Additional prompt added between the mode prompt and the interrogation prompt (if enabled)");
			// 
			// labelSeed
			// 
			labelSeed.AutoSize = true;
			labelSeed.ForeColor = Color.WhiteSmoke;
			labelSeed.Location = new Point(11, 132);
			labelSeed.Name = "labelSeed";
			labelSeed.Size = new Size(56, 17);
			labelSeed.TabIndex = 110;
			labelSeed.Text = "Seed :";
			// 
			// numericUpDownSeed
			// 
			numericUpDownSeed.Location = new Point(73, 130);
			numericUpDownSeed.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
			numericUpDownSeed.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
			numericUpDownSeed.Name = "numericUpDownSeed";
			numericUpDownSeed.Size = new Size(165, 23);
			numericUpDownSeed.TabIndex = 109;
			toolTip1.SetToolTip(numericUpDownSeed, "Seed used to generate the picture.\r\nA fixed seed can give more consistent results.\r\nSet -1 for a random seed");
			numericUpDownSeed.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
			// 
			// checkBoxUseInterrogation
			// 
			checkBoxUseInterrogation.AutoSize = true;
			checkBoxUseInterrogation.Checked = true;
			checkBoxUseInterrogation.CheckState = CheckState.Checked;
			checkBoxUseInterrogation.ForeColor = Color.WhiteSmoke;
			checkBoxUseInterrogation.Location = new Point(11, 76);
			checkBoxUseInterrogation.Name = "checkBoxUseInterrogation";
			checkBoxUseInterrogation.Size = new Size(163, 21);
			checkBoxUseInterrogation.TabIndex = 8;
			checkBoxUseInterrogation.Text = "Use interrogation";
			toolTip1.SetToolTip(checkBoxUseInterrogation, "Improve prompting using the results of an integration model on the content of the input picture");
			checkBoxUseInterrogation.UseVisualStyleBackColor = true;
			// 
			// checkBoxKeepOriginalSize
			// 
			checkBoxKeepOriginalSize.AutoSize = true;
			checkBoxKeepOriginalSize.ForeColor = Color.WhiteSmoke;
			checkBoxKeepOriginalSize.Location = new Point(11, 49);
			checkBoxKeepOriginalSize.Name = "checkBoxKeepOriginalSize";
			checkBoxKeepOriginalSize.Size = new Size(235, 21);
			checkBoxKeepOriginalSize.TabIndex = 7;
			checkBoxKeepOriginalSize.Text = "Keep original picture size";
			toolTip1.SetToolTip(checkBoxKeepOriginalSize, "Disable the dynamic resize of input pictures");
			checkBoxKeepOriginalSize.UseVisualStyleBackColor = true;
			// 
			// checkBoxControlNetLowvram
			// 
			checkBoxControlNetLowvram.AutoSize = true;
			checkBoxControlNetLowvram.ForeColor = Color.WhiteSmoke;
			checkBoxControlNetLowvram.Location = new Point(11, 22);
			checkBoxControlNetLowvram.Name = "checkBoxControlNetLowvram";
			checkBoxControlNetLowvram.Size = new Size(227, 21);
			checkBoxControlNetLowvram.TabIndex = 6;
			checkBoxControlNetLowvram.Text = "Enable controlnet lowvram";
			toolTip1.SetToolTip(checkBoxControlNetLowvram, "Enable controlnet lowvram mode");
			checkBoxControlNetLowvram.UseVisualStyleBackColor = true;
			// 
			// labelProgressStatus
			// 
			labelProgressStatus.AutoSize = true;
			labelProgressStatus.Location = new Point(11, 248);
			labelProgressStatus.Name = "labelProgressStatus";
			labelProgressStatus.Size = new Size(104, 17);
			labelProgressStatus.TabIndex = 5;
			labelProgressStatus.Text = "Status : N/A";
			// 
			// progressBarInference
			// 
			progressBarInference.Location = new Point(11, 268);
			progressBarInference.Name = "progressBarInference";
			progressBarInference.Size = new Size(693, 35);
			progressBarInference.Step = 1;
			progressBarInference.TabIndex = 4;
			// 
			// pictureBoxPreview
			// 
			pictureBoxPreview.BackColor = Color.Silver;
			pictureBoxPreview.BorderStyle = BorderStyle.Fixed3D;
			pictureBoxPreview.Cursor = Cursors.Hand;
			pictureBoxPreview.Enabled = false;
			pictureBoxPreview.Location = new Point(710, 22);
			pictureBoxPreview.Name = "pictureBoxPreview";
			pictureBoxPreview.Size = new Size(175, 211);
			pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBoxPreview.TabIndex = 3;
			pictureBoxPreview.TabStop = false;
			pictureBoxPreview.Click += pictureBoxPreview_Click;
			// 
			// checkBoxEnablePreview
			// 
			checkBoxEnablePreview.AutoSize = true;
			checkBoxEnablePreview.ForeColor = Color.WhiteSmoke;
			checkBoxEnablePreview.Location = new Point(710, 241);
			checkBoxEnablePreview.Name = "checkBoxEnablePreview";
			checkBoxEnablePreview.Size = new Size(139, 21);
			checkBoxEnablePreview.TabIndex = 2;
			checkBoxEnablePreview.Text = "Enable preview";
			toolTip1.SetToolTip(checkBoxEnablePreview, "Show output image into the preview box");
			checkBoxEnablePreview.UseVisualStyleBackColor = true;
			checkBoxEnablePreview.CheckedChanged += checkBoxEnablePreview_CheckedChanged;
			// 
			// buttonStopInference
			// 
			buttonStopInference.ForeColor = Color.Red;
			buttonStopInference.Location = new Point(710, 268);
			buttonStopInference.Name = "buttonStopInference";
			buttonStopInference.Size = new Size(65, 35);
			buttonStopInference.TabIndex = 1;
			buttonStopInference.Text = "Stop";
			buttonStopInference.UseVisualStyleBackColor = true;
			buttonStopInference.Click += buttonStopInference_Click;
			// 
			// buttonInference
			// 
			buttonInference.ContextMenuStrip = contextMenuStripButtonInference;
			buttonInference.ForeColor = Color.Green;
			buttonInference.Location = new Point(784, 268);
			buttonInference.Name = "buttonInference";
			buttonInference.Size = new Size(100, 35);
			buttonInference.TabIndex = 0;
			buttonInference.Text = "Inference";
			buttonInference.UseVisualStyleBackColor = true;
			buttonInference.Click += buttonInference_Click;
			// 
			// contextMenuStripButtonInference
			// 
			contextMenuStripButtonInference.Items.AddRange(new ToolStripItem[] { DebugYoloV8SegmentationToolStripMenuItem });
			contextMenuStripButtonInference.Name = "contextMenuStripButtonInference";
			contextMenuStripButtonInference.Size = new Size(226, 48);
			// 
			// DebugYoloV8SegmentationToolStripMenuItem
			// 
			DebugYoloV8SegmentationToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
			DebugYoloV8SegmentationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { InferenceYoloV8DetectionsToolStripMenuItem, InferenceYoloV8MaskDifferenceToolStripMenuItem });
			DebugYoloV8SegmentationToolStripMenuItem.Name = "DebugYoloV8SegmentationToolStripMenuItem";
			DebugYoloV8SegmentationToolStripMenuItem.Size = new Size(225, 22);
			DebugYoloV8SegmentationToolStripMenuItem.Text = "Debug YoloV8 Segmentation";
			// 
			// InferenceYoloV8DetectionsToolStripMenuItem
			// 
			InferenceYoloV8DetectionsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
			InferenceYoloV8DetectionsToolStripMenuItem.Name = "InferenceYoloV8DetectionsToolStripMenuItem";
			InferenceYoloV8DetectionsToolStripMenuItem.Size = new Size(249, 22);
			InferenceYoloV8DetectionsToolStripMenuItem.Text = "Inference YoloV8 detections";
			InferenceYoloV8DetectionsToolStripMenuItem.ToolTipText = "Only Inference all input picture into the YoloV8\r\nmodel and save the results in the output path.";
			InferenceYoloV8DetectionsToolStripMenuItem.Click += InferenceYoloV8DetectionsToolStripMenuItem_Click;
			// 
			// InferenceYoloV8MaskDifferenceToolStripMenuItem
			// 
			InferenceYoloV8MaskDifferenceToolStripMenuItem.Name = "InferenceYoloV8MaskDifferenceToolStripMenuItem";
			InferenceYoloV8MaskDifferenceToolStripMenuItem.Size = new Size(249, 22);
			InferenceYoloV8MaskDifferenceToolStripMenuItem.Text = "Inference YoloV8 mask difference";
			InferenceYoloV8MaskDifferenceToolStripMenuItem.ToolTipText = "Inference all input picture into the YoloV8\r\nmodel and separate the result mask from the\r\noriginal picture. Mask file suffix will be \"_mask\"\r\nand \"_mask_result\".";
			InferenceYoloV8MaskDifferenceToolStripMenuItem.Click += InferenceYoloV8MaskDifferenceToolStripMenuItem_Click;
			// 
			// toolTip1
			// 
			toolTip1.ToolTipIcon = ToolTipIcon.Info;
			toolTip1.ToolTipTitle = "Info";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			BackColor = SystemColors.ControlDarkDark;
			ClientSize = new Size(914, 736);
			Controls.Add(groupBoxInference);
			Controls.Add(groupBoxStableDiffusionWebUIConfig);
			Controls.Add(groupBoxPictureColorDiffusionConfig);
			Controls.Add(groupBoxStableDiffusionAPIConfig);
			Font = new Font("Cascadia Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			MaximizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Picture Color Diffusion";
			Load += MainForm_Load;
			groupBoxStableDiffusionAPIConfig.ResumeLayout(false);
			groupBoxStableDiffusionAPIConfig.PerformLayout();
			groupBoxPictureColorDiffusionConfig.ResumeLayout(false);
			groupBoxPictureColorDiffusionConfig.PerformLayout();
			groupBoxControlnetConfiguration.ResumeLayout(false);
			groupBoxStableDiffusionWebUIConfig.ResumeLayout(false);
			groupBoxStableDiffusionWebUIConfig.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownClipSkip).EndInit();
			groupBoxInference.ResumeLayout(false);
			groupBoxInference.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownSeed).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
			contextMenuStripButtonInference.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Label labelPicturePath;
		private FolderBrowserDialog folderBrowserDialog1;
		private TextBox textBoxPicturePath;
		private Button buttonSelectPicture;
		private Button buttonSelectPicturesFolder;
		private OpenFileDialog openFileDialog1;
		private GroupBox groupBoxStableDiffusionAPIConfig;
		private GroupBox groupBoxPictureColorDiffusionConfig;
		private Button buttonVerifyApiEndpoint;
		private Label labelApiEndpoint;
		private TextBox textBoxApiEndpoint;
		private GroupBox groupBoxStableDiffusionWebUIConfig;
		private Button buttonRefreshModels;
		private Label labelModel;
		private ComboBox comboBoxVaes;
		private Label labelVae;
		private ComboBox comboBoxModels;
		private Label labelPictureOutputPath;
		private TextBox textBoxPictureOutputPath;
		private Button buttonSelectPictureOutputFolder;
		private GroupBox groupBoxInference;
		private Button buttonStopInference;
		private Button buttonInference;
		private PictureBox pictureBoxPreview;
		private CheckBox checkBoxEnablePreview;
		private ProgressBar progressBarInference;
		private Label labelProgressStatus;
		private NumericUpDown numericUpDownClipSkip;
		private Label labelClipSkip;
		private Button buttonApplyChanges;
		private CheckBox checkBoxControlNetLowvram;
		private Label labelMode;
		private RadioButton radioButtonModeManga;
		private GroupBox groupBoxControlnetConfiguration;
		private ComboBox comboBoxUnit2ControlNetModel;
		private ComboBox comboBoxUnit1ControlNetModel;
		private Label labelUnit2ControlNetModel;
		private Label labelUnit1ControlNetModel;
		private Button buttonRefreshControlNetModels;
		private CheckBox checkBoxKeepOriginalSize;
		private ToolTip toolTip1;
		private CheckBox checkBoxUseInterrogation;
		private Label labelSeed;
		private NumericUpDown numericUpDownSeed;
		private TextBox textBoxNegativePrompt;
		private TextBox textBoxPrompt;
		private Label labelAdditionalPrompt;
		private Label labelReferencePicturePath;
		private TextBox textBoxReferencePicturePath;
		private Button buttonSelectReference;
		private Button buttonClearReference;
		private Label labelSampler;
		private ComboBox comboBoxSampler;
		private CheckBox checkBoxIncludeMetadata;
		private ContextMenuStrip contextMenuStripButtonInference;
		private ToolStripMenuItem DebugYoloV8SegmentationToolStripMenuItem;
		private ToolStripMenuItem InferenceYoloV8DetectionsToolStripMenuItem;
		private ToolStripMenuItem InferenceYoloV8MaskDifferenceToolStripMenuItem;
	}
}
