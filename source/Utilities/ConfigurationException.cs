using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Utilities
{
    [Serializable()]
    public class ConfigurationException : Exception
    {
        private bool m_blnCriticalConfigurationException = false;

        public ConfigurationException() { }
        public ConfigurationException(string message) : base(message) { }

        public ConfigurationException(string message, System.Exception inner) : base(message, inner) { }

        // constructor needed for serialization when exception propagates from a remoting server to the client.
        protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info,context) { }

        public ConfigurationException(string message, bool blnCritical) : base(message)
        {
            m_blnCriticalConfigurationException = blnCritical;
        }

        public bool Critical
        {
            get { return m_blnCriticalConfigurationException;  }
        }
    }
}
