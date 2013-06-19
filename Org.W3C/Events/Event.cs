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

namespace Org.W3C.Events
{
	//http://www.w3.org/TR/2000/REC-DOM-Level-2-Events-20001113/events.html

	/// <summary>
	/// The Event interface is used to provide contextual information about an event to the handler processing the event. An object which implements the Event interface is generally passed as the first parameter to an event handler. More specific context information is passed to event handlers by deriving additional interfaces from Event which contain information directly relating to the type of event they accompany. These derived interfaces are also implemented by the object passed to the event listener.
	/// </summary>
	public class Event
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the Event class.
		/// </summary>
		internal extern Event();

		#endregion

		#region properties

		/// <summary>
		/// Used to indicate whether or not an event is a bubbling event. If the event can bubble the value is true, else the value is false.
		/// </summary>
		public extern bool Bubbles { get; }

		/// <summary>
		/// Used to indicate whether or not an event can have its default action prevented. If the default action can be prevented the value is true, else the value is false.
		/// </summary>
		public extern bool Cancelable { get; }

		/// <summary>
		/// Used to indicate the EventTarget whose EventListeners are currently being processed. This is particularly useful during capturing and bubbling.
		/// </summary>
		public extern object CurrentTarget { get; }

		/// <summary>
		/// Used to indicate which phase of event flow is currently being evaluated.
		/// </summary>
		/// <value>The event phase.</value>
		public extern EventPhase EventPhase { get; }

		/// <summary>
		/// Used to indicate the EventTarget to which the event was originally dispatched.
		/// </summary>
		public extern object Target { get; }

		/// <summary>
		/// Used to specify the time (in milliseconds relative to the epoch) at which the event was created. Due to the fact that some systems may not provide this information the value of timeStamp may be not available for all events. When not available, a value of 0 will be returned. Examples of epoch time are the time of the system start or 0:0:0 UTC 1st January 1970.
		/// </summary>
		public extern System.DateTime TimeStamp { get; }

		/// <summary>
		/// The name of the event (case-insensitive). The name must be an XML name.
		/// </summary>
		public extern string Type { get; }

		#endregion

		#region methods

		/// <summary>
		/// The initEvent method is used to initialize the value of an Event created through the DocumentEvent interface. This method may only be called before the Event has been dispatched via the dispatchEvent method, though it may be called multiple times during that phase if necessary. If called multiple times the final invocation takes precedence. If called from a subclass of Event interface only the values specified in the initEvent method are modified, all other attributes are left unchanged.
		/// </summary>
		public extern void InitEvent(string eventTypeArg, bool canBubbleArg, bool cancelableArg);

		/// <summary>
		/// If an event is cancelable, the preventDefault method is used to signify that the event is to be canceled, meaning any default action normally taken by the implementation as a result of the event will not occur. If, during any stage of event flow, the preventDefault method is called the event is canceled. Any default action associated with the event will not occur. Calling this method for a non-cancelable event has no effect. Once preventDefault has been called it will remain in effect throughout the remainder of the event's propagation. This method may be used during any stage of event flow.
		/// </summary>
		public extern void PreventDefault();

		/// <summary>
		/// The stopPropagation method is used prevent further propagation of an event during event flow. If this method is called by any EventListener the event will cease propagating through the tree. The event will complete dispatch to all listeners on the current EventTarget before event flow stops. This method may be used during any stage of event flow.
		/// </summary>
		public extern void StopPropagation ();

		#endregion


	}
}

