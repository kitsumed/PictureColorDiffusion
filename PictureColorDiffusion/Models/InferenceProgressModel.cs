namespace PictureColorDiffusion.Models
{
	/// <summary>
	/// This class is used by <see cref="MainForm"/> to report progress information on
	/// the inference status
	/// </summary>
	public class InferenceProgressModel
	{
        /// <summary>
        /// Text explaining the current work
        /// </summary>
        public required string status;

		private int _completionPercent;
		/// <summary>
		/// % of work done on 100%
		/// </summary>
		public required int completionPercent
		{
			get => _completionPercent;
			set
			{
				if (value > 100 || value < 0) throw new ArgumentException("Percent cannot be bigger than 100 or smaller than 0");
				_completionPercent = value;
			}
		}
	}
}
