using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Adis.Log.server.Tests
{
	class ContextChannelMock : IContextChannel
	{
		#region IContextChannel Members

		public bool AllowOutputBatching
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public System.ServiceModel.Channels.IInputSession InputSession
		{
			get { throw new NotImplementedException(); }
		}

		public EndpointAddress LocalAddress
		{
			get { throw new NotImplementedException(); }
		}

		public TimeSpan OperationTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public System.ServiceModel.Channels.IOutputSession OutputSession
		{
			get { throw new NotImplementedException(); }
		}

		public EndpointAddress RemoteAddress
		{
			get { throw new NotImplementedException(); }
		}

		public string SessionId
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region IChannel Members

		public T GetProperty<T>() where T : class
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ICommunicationObject Members

		public void Abort()
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public void Close(TimeSpan timeout)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public event EventHandler Closed;

		public event EventHandler Closing;

		public void EndClose(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public void EndOpen(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public event EventHandler Faulted;

		public void Open(TimeSpan timeout)
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			throw new NotImplementedException();
		}

		public event EventHandler Opened;

		public event EventHandler Opening;

		public CommunicationState State
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region IExtensibleObject<IContextChannel> Members

		public IExtensionCollection<IContextChannel> Extensions
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}
