namespace PictureColorDiffusion
{
	partial class PreviewForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pictureBoxPreview = new PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
			SuspendLayout();
			// 
			// pictureBoxPreview
			// 
			pictureBoxPreview.Cursor = Cursors.Hand;
			pictureBoxPreview.Dock = DockStyle.Fill;
			pictureBoxPreview.Location = new Point(0, 0);
			pictureBoxPreview.Name = "pictureBoxPreview";
			pictureBoxPreview.Size = new Size(800, 473);
			pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxPreview.TabIndex = 0;
			pictureBoxPreview.TabStop = false;
			pictureBoxPreview.MouseClick += pictureBoxPreview_MouseClick;
			pictureBoxPreview.MouseDown += pictureBoxPreview_MouseDown;
			pictureBoxPreview.MouseMove += pictureBoxPreview_MouseMove;
			// 
			// PreviewForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			ClientSize = new Size(800, 473);
			Controls.Add(pictureBoxPreview);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "PreviewForm";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Preview";
			TopMost = true;
			((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private PictureBox pictureBoxPreview;
	}
}