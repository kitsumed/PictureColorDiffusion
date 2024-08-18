using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureColorDiffusion.Enums
{
	public enum ApplicationStatesEnum
	{
		/// <summary>
		/// Waiting for API endpoint confirmation.
		/// Everything is disabled
		/// </summary>
		waiting_for_api,
		/// <summary>
		/// Waiting for new inferences.
		/// Everything is enabled
		/// </summary>
		waiting_for_inference,
		/// <summary>
		/// Currently doing inferences.
		/// Most controls are disabled but some are enabled to control inference state
		/// </summary>
		currently_in_inference
	}
}
