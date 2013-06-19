/*

This file is part of net.js.

net.js is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see `<http://www.gnu.org/licenses/>`.

*/

using System;

namespace Org.W3C.Events
{
	//http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113/events.html

	/// <summary>
	/// An integer indicating which phase of event flow is being processed.
	/// </summary>
	public enum EventPhase
	{
		/// <summary>
		/// The event is currently being evaluated at the target EventTarget.
		/// </summary>
		AtTarget = 1,

		/// <summary>
		/// The current event phase is the bubbling phase.
		/// </summary>
		BubblingPhase = 2,

		/// <summary>
		/// The current event phase is the capturing phase.
		/// </summary>
		CapturingPhase = 3
	}
}

