// =====================================================================
//  This file is part of the Microsoft Dynamics Accelerator code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================
using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System.Net;
using System.Configuration;
using System.Threading;
using System.IO;
using CDM.HealthAccelerator.DataModel;

namespace CDM.HealthAccelerator.GenerateSampleData
{
    public class CreateCDMHealthData
    {
        /// <summary>
        /// We will only support a handfull of attributes for this
        /// you could extend this easy enough but I only needed them for
        /// sending emails and doing some validation of profile information
        /// </summary>

        #region Properties

        private Queue<Profile> incomingContacts = new Queue<Profile>();
        private List<Profile> outgoingContacts = new List<Profile>();
        public Queue<Profile> IncomingContacts
        {
            get
            {
                return incomingContacts;
            }
            set
            {
                incomingContacts = value;
            }
        }
        public List<Profile> OutgoingContacts
        {
            get
            {
                return outgoingContacts;
            }
        }

        private Profile.ContactType contactType;

        private string fileName;

        private int totalcreated = 0;

        private int createCount;

        public int CreateCount
        {
            get { return createCount; }
            set { createCount = value; }
        }

        private int clients;

        public int Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        private string emailDomain;

        public string EmailDomain
        {
            get
            {
                return emailDomain;
            }
            set
            {
                emailDomain = value;
            }

        }

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public Profile.ContactType ContactType
        {
            get
            {
                return contactType;
            }

            set
            {
                contactType = value;
            }
        }

        #endregion

        /// <summary>
        /// Our Primary Thread in this class
        /// It will be created 1 time by the caller
        /// Then it will create secondary worker threads
        /// which it will then block (wait on)
        /// until they have completed or failed or shut down 
        /// </summary>
        public string CreateContacts(OrganizationServiceProxy _service)
        {
            // We need to create our file if it exists
            // or if it's already open, generate a second one.

                /// <summary>
                /// validate if we want to add directly to CRM or not
                /// </summary>
                try
                {
                    if (this.Clients > 0)
                    {
                        // since we are dividing up the creates per thread
                        // we have to check if it is equal across all threads
                        // if not then we want to add the rest to the last thread
                        // technically we could create another client thread
                        // but we are going by what they told us.
                        // this shouldn't cause too many issues based on count and remainder

                        List<Thread> threads = new List<Thread>();

                        for (int createthreads = 0; createthreads < this.Clients; createthreads++)
                        {

                            // create our thread and start it
                            // it's a parameterized query so pass in the proper values from above
                            Thread clientthread = new Thread(() => CreateCDSContacts(_service));
                            clientthread.Name = createthreads.ToString() + "-" + contactType.ToString();
                            clientthread.Start();

                            // add to our list of threads
                            // we do this so that we can essentially block the main console
                            // thread..otherwise it would exit before these guys are done
                            // add these to the total list first
                            threads.Add(clientthread);
                        }

                        // once all threads are created
                        // join with them.. if it fails that only means the thread
                        // was done (possibly a weird other situation.. but doubtful)
                        // so it's ok to fail on any threads (or all)
                        // which would either make us block for them to complete
                        // or if they were magically done.. join (0) and exit.
                        foreach (Thread t in threads)
                        {
                            try
                            {
                                t.Join();
                            }
                            catch (Exception JoinEx)
                            {
                                // ignore for now as it's not useful to the process
                                // if we error out because the thread was already done
                                // which might be possible in a multi-threaded usage scenario
                                // but that is ok
                                System.Diagnostics.Debug.WriteLine("Thread Join Error: " + JoinEx.ToString());
                            }
                        }


                        switch (ContactType)
                        {
                        case Profile.ContactType.Standard:
                                {
                                    Contact.ExportToJson(FileName, OutgoingContacts);    
                                    break;
                                }
                            case Profile.ContactType.Patient:
                                {
                                    Patient.ExportToJson(FileName, OutgoingContacts);
                                    break;
                                }
                            case Profile.ContactType.Practitioner:
                                {
                                    Practitioner.ExportToJson(FileName, OutgoingContacts);
                                    break;
                                }
                            case Profile.ContactType.RelatedPerson:
                                {
                                    RelatedPerson.ExportToJson(FileName, OutgoingContacts);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Client value. Client must be > 0");
                    }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return FileName;

        }

        private void CreateCDSContacts(OrganizationServiceProxy _service)
        {
            try
            {
                while (true)
                {
                    Profile cdsContact = GetContact();

                    if (cdsContact == null)
                    {
                        break;
                    }

                    cdsContact.EmailAddressDomain = EmailDomain;

                    Guid entityId = cdsContact.WriteToCDS(_service);

                    if (entityId != Guid.Empty)
                    {
                        Increment(cdsContact);
                    }
                    else
                    {
                        Console.WriteLine("Thread [" + Thread.CurrentThread.Name + "] failed to create");
                    }
                }
            }
            catch (Exception crmex)
            {
                Console.WriteLine("Error executing on CRM \r\n\r\n" + crmex.ToString());
                return;
            }

        }

        #region Queueing Help

        private static object addlock = new object();
     
        /// <summary>
        /// list we are writing back to the file
        /// </summary>
        /// <param name="contact"></param>

        private static object getObject = new object();
        private Profile GetContact()
        {
            lock (getObject)
            {
                if (IncomingContacts.Count > 0)
                {
                    return IncomingContacts.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }

        private static object addUpLock = new object();

        private void Increment(Profile profile)
        {
            lock (addUpLock)
            {
                this.totalcreated++;
                this.outgoingContacts.Add(profile);
                Console.WriteLine("Created " + this.totalcreated.ToString() + " " + profile.CDMContactType.ToString() + " Records");
            }
        }

        #endregion

        /// <summary>
        /// These are to make it easier to log into CRM
        /// whether on premise or online
        /// supports 3 instances (Online, OnPremise, Dev)
        /// </summary>
        /// <returns></returns>

    }
}
