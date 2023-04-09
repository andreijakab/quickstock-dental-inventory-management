using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class clsEmailer
    {
        public struct EmailMessage
        {
            public string           From_Address;
            public string           From_Name;
            public string           To;
            public string           Subject;
            public string           Body;
            public string           Attachment;
            public MailPriority     Priority;
            public clsSMTPServer    SMTP;
        }

        /// <summary>
        /// Transmit an email message containing the order as an attachment.
        /// </summary>
        /// <param name="oeEmail">Order email object that represent the order to be sent.</param>
        /// <param name="intTimeout">Number of seconds after which the operation will timeout.</param>
        /// <returns>true if the order was emailed successfully; otherwise, false.</returns>
        public static void SendMessageWithAttachment(EmailMessage emEmail)
        {
            // create the basic message
            MailMessage mmEmail = new MailMessage();
            if(emEmail.From_Name != null && emEmail.From_Name.Length > 0)
                mmEmail.From = new MailAddress(emEmail.From_Address, emEmail.From_Name, Encoding.GetEncoding("iso-8859-1"));
            else
                mmEmail.From = new MailAddress(emEmail.From_Address);
            mmEmail.To.Add(emEmail.To);

            mmEmail.Subject = emEmail.Subject;
            mmEmail.Body = emEmail.Body;
            mmEmail.Priority = emEmail.Priority;

            // add the attachment to the message
            Attachment attached = new Attachment(emEmail.Attachment, MediaTypeNames.Application.Octet);
            mmEmail.Attachments.Add(attached);
          
            // create smtp client at mail server location
            SmtpClient scSMTPClient = new SmtpClient(emEmail.SMTP.Address, emEmail.SMTP.Port);
            
            // set the timeout value
            scSMTPClient.Timeout = emEmail.SMTP.Timeout * 60000;
            
            // if necessary, add credentials
            if (emEmail.SMTP.CredentialsRequired)
            {
                scSMTPClient.UseDefaultCredentials = false;
                scSMTPClient.Credentials = new NetworkCredential(emEmail.SMTP.UserName, emEmail.SMTP.Password);
            }
            else
                scSMTPClient.UseDefaultCredentials = true;

            try
            {
                // send message
                scSMTPClient.Send(mmEmail);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("No recipient was specified in the 'To' field. Please add a valid destination\ne-mail address.");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("The specified outgoing email (SMTP) server is invalid. Please ensure that the SMTP configuration is correct.");
            }
            catch (SmtpException ex)
            {
                switch (ex.StatusCode)
                { 
                    case SmtpStatusCode.ExceededStorageAllocation:
                        throw new Exception("The e-mail message is too large to be stored in the destination mailbox. Please send this e-mail again once the recipient has made some room in his mailbox.");

                    case SmtpStatusCode.MailboxUnavailable:
                        throw new Exception("The destination mailbox was not found or could not be accessed. Please make sure that the destination e-mail address is valid.");

                    case SmtpStatusCode.GeneralFailure:
                        throw new Exception("The specified SMTP server could not be found or the configured timeout value is too small.\n Please ensure that the outgoing mail server configuration is correct.");

                    default:
                        throw new Exception("An error occured while attemtping to send the e-mail message. Please ensure that the outgoing mail server configuration is correct\nand that the destination address is valid.");
                }
            }
        }
    }

    public class SMTPServersCollection : IList, ICollection, IEnumerable
    {
        private ArrayList m_alContents;

        public SMTPServersCollection()
        {
            m_alContents = new ArrayList();
        }

        #region IList_Members
        /// <summary>
        /// Adds a SMTP server to the list.
        /// </summary>
        /// <param name="obj">The SMTP server to add to the list.</param>
        /// <returns>The position into which the new element was inserted.</returns>
        public int Add(object obj)
        {
            return m_alContents.Add(obj);
        }

        /// <summary>
        /// Removes all SMTP servers from the list.
        /// </summary>
        public void Clear()
        {
            m_alContents.Clear();
        }

        /// <summary>
        /// Determines whether the list contains the specific SMTP server.
        /// </summary>
        /// <param name="obj">The SMTP server to locate in the list.</param>
        /// <returns>true if the SMTP server is found in the list; otherwise, false.</returns>
        public bool Contains(object value)
        {
            bool inList = false;
            clsSMTPServer server = (clsSMTPServer)value;

            for (int i = 0; i < m_alContents.Count; i++)
            {
                if (String.Compare(((clsSMTPServer)m_alContents[i]).AccountName, server.AccountName) == 0)
                {
                    inList = true;
                    break;
                }
            }

            return inList;
        }

        /// <summary>
        /// Determines the index of a specific SMTP server in the list.
        /// </summary>
        /// <param name="obj">The SMTP server to locate in the list.</param>
        /// <returns>The index of the SMTP server if found in the list; otherwise, -1.</returns>
        public int IndexOf(object value)
        {
            int itemIndex = -1;
            clsSMTPServer server = (clsSMTPServer)value;

            for (int i = 0; i < m_alContents.Count; i++)
            {
                if (String.Compare(((clsSMTPServer)m_alContents[i]).AccountName, server.AccountName) == 0)
                {
                    itemIndex = i;
                    break;
                }
            }
            
            return itemIndex;
        }

        /// <summary>
        /// Inserts a SMTP server in the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the SMTP server should be inserted. </param>
        /// <param name="value">The SMTP server to insert in the list.</param>
        public void Insert(int index, object value)
        {
            m_alContents.Insert(index, value);
        }

        /// <summary>
        /// Gets a value indicating whether the list has a fixed size.
        /// </summary>
        /// <returns>true if the list has a fixed size; otherwise, false.</returns>
        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the list is read-only.
        /// </summary>
        /// <returns>true if the list is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific SMTP server from the list.
        /// </summary>
        /// <param name="value">The SMTP server to be removed from the list.</param>
        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
        }

        /// <summary>
        /// Removes the SMTP server at the specified list index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            m_alContents.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the SMTP server at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the SMTP server to get or set.</param>
        public object this[int index]
        {
            get
            {
                return m_alContents[index];
            }
            set
            {
                m_alContents[index] = value;
            }
        }
        #endregion

        #region ICollection_Members
        // ICollection Members

        /// <summary>
        /// Copies the SMTP server to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the SMTP servers. The array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            int j = index;
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(m_alContents[i], j);
                j++;
            }
        }

        /// <summary>
        /// Gets the number of SMTP servers contained in the list.
        /// </summary>
        /// <returns>The number of SMTP servers contained in the list.</returns>
        public int Count
        {
            get
            {
                return m_alContents.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the list is synchronized (thread safe).
        /// </summary>
        /// <returns>true if access to the list is synchronized (thread safe); otherwise, false.</returns>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the list.
        /// </summary>
        /// <returns>An object that can be used to synchronize access to the list.</returns>
        public object SyncRoot
        {
            // Return the current instance since the underlying store is not
            // publicly available.
            get
            {
                return this;
            }
        }
        #endregion

        #region IEnumerable_Members
        // IEnumerable Members
        /// <summary>
        /// Returns an enumerator that iterates through the SMTP server list.
        /// </summary>
        /// <param name="value">An IEnumerator object that can be used to iterate through the SMTP server list.</param>
        public IEnumerator GetEnumerator()
        {
            return m_alContents.GetEnumerator();
        }
        #endregion
    }
}
