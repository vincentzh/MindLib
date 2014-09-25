using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace MindHarbor.GenClassLib.MessageBoard {
	///<summary>
	/// A listener filter to prevent a listener listen a message more than once
	///</summary>
	public class DuplicationPreventionListenerFilter : IListenerFilter {
		private readonly ISet<IMessageListener> listenedListeners = new HashSet<IMessageListener>();
		private readonly IMessage msg;

		///<summary>
		///</summary>
		///<param name="msg"></param>
		public DuplicationPreventionListenerFilter(IMessage msg) {
			this.msg = msg;
		}

		#region IListenerFilter Members

		public bool Accept(IMessageListener listener, IMessage msgToSend) {
			if (!msg.Equals(msgToSend))
				throw new Exception("This filter can only be used to send the message it is created with.");
			return listenedListeners.Add(listener);
		}

		#endregion
	}
}