using System;

namespace Org.W3C.Events
{
	public class MouseEvent : UIEvent
	{
		/// <summary>
		/// Initializes a new instance of the Org.W3C.Events.DOMUIEvent class.
		/// </summary>
		internal extern MouseEvent ();

		#region properties

		/// <summary>
		/// The vertical coordinate at which the event occurred relative to the origin of the screen coordinate system.
		/// </summary>
		public extern uint ScreenX { get; }

		#endregion
	}
}

