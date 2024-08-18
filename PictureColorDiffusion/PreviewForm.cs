namespace PictureColorDiffusion
{
	public partial class PreviewForm : Form
	{
		private Point lastMousePosition;

		/// <summary>
		/// Open a always-on-top form that show a picture
		/// </summary>
		/// <param name="previewPicture">The picture to show</param>
		public PreviewForm(Image previewPicture)
		{
			InitializeComponent();
			// Set the image by cloning it to prevent issues if the "original" picture get disposed
			pictureBoxPreview.Image = (Image)previewPicture.Clone();
			ResizeFormToImageSize(pictureBoxPreview.Image);
			MoveFormToCenterScreen();
		}

		/// <summary>
		/// Will resize the form size to the image size, but with a limit of the current screen resolution
		/// </summary>
		/// <param name="picture">The image</param>
		private void ResizeFormToImageSize(Image picture)
		{
			// Get the display screen where the form is currently in
			Screen currentScreen = Screen.FromControl(this);
			// Get the bounds of the screen
			Rectangle currentScreenBounds = currentScreen.Bounds;
			// Set the maximum size of the form according to the current screen bounds
			MaximumSize = new Size(currentScreenBounds.Width, currentScreenBounds.Height);
			// Set the current size of the form to the size of the image (but limited by max size)
			Size = new Size(picture.Width, picture.Height);

		}

		/// <summary>
		/// Move the form to the center of the screen it is currently in
		/// </summary>
		private void MoveFormToCenterScreen() 
		{
			// Get the display screen where the form is currently in
			Screen currentScreen = Screen.FromControl(this);
			// Get the bounds of the screen
			Rectangle currentScreenBounds = currentScreen.Bounds;
			// Get the center position of the current screen
			int centerX = (currentScreenBounds.Width - Width) / 2 + currentScreenBounds.X;
			int centerY = (currentScreenBounds.Height - Height) / 2 + currentScreenBounds.Y;
			// Set the form position
			Location = new Point(centerX, centerY);
		}


		private void pictureBoxPreview_MouseClick(object sender, MouseEventArgs e)
		{
			// Close the form on right / middle click on the image preview
			if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
			{
				Close();
			}
		}

		private void pictureBoxPreview_MouseMove(object sender, MouseEventArgs e)
		{
			// If the mouse is moved while holding left, move the form
			if (e.Button == MouseButtons.Left)
			{
				Point currentMousePosition = Control.MousePosition;
				// Add the last mouse position as offset to ensuire the form new position isn't misaligned
				currentMousePosition.Offset(lastMousePosition);
				// Update form position
				Location = currentMousePosition;
			}
		}

		private void pictureBoxPreview_MouseDown(object sender, MouseEventArgs e)
		{
			/* Save the last position of the mouse
			* NOTE: little tweak here, adding +6 & +32 to axis since we still have a topbar (style of the form not set to none)
			* visible, without them, the form would move to a position slighly off from where it was originally.
			* The tweak is not perfect, but it reduce the effect greatly
			*/
			lastMousePosition = new Point(-(e.X + 6),-(e.Y +32));
		}
	}
}
