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

        private Queue<Profile> incomingProfiles = new Queue<Profile>();
        private List<Profile> outgoingContacts = new List<Profile>();
        public Queue<Profile> IncomingProfiles
        {
            get
            {
                return incomingProfiles;
            }
            set
            {
                incomingProfiles = value;
            }
        }
        public List<Profile> OutgoingProfiles
        {
            get
            {
                return outgoingContacts;
            }
        }

        private Profile.ProfileType profileType;

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

        public Profile.ProfileType ProfileType
        {
            get
            {
                return profileType;
            }

            set
            {
                profileType = value;
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
        public string CreateProfiles(OrganizationServiceProxy _service)
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
                            Thread clientthread = new Thread(() => CreateCDSProfile(_service));
                            clientthread.Name = createthreads.ToString() + "-" + profileType.ToString();
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


                        switch (ProfileType)
                        {
                        case Profile.ProfileType.Standard:
                                {
                                    Contact.ExportToJson(FileName, OutgoingProfiles);    
                                    break;
                                }
                            case Profile.ProfileType.Patient:
                                {
                                    Patient.ExportToJson(FileName, OutgoingProfiles);
                                    break;
                                }
                            case Profile.ProfileType.Practitioner:
                                {
                                    Practitioner.ExportToJson(FileName, OutgoingProfiles);
                                    break;
                                }
                            case Profile.ProfileType.RelatedPerson:
                                {
                                    RelatedPerson.ExportToJson(FileName, OutgoingProfiles);
                                    break;
                                }
                            case Profile.ProfileType.Organization:
                                {
                                    Organization.ExportToJson(FileName, OutgoingProfiles);
                                    break;
                                }
                            case Profile.ProfileType.Location:
                                {
                                    Location.ExportToJson(FileName, OutgoingProfiles);
                                    break;
                                }
                        case Profile.ProfileType.Medication:
                            {
                                Location.ExportToJson(FileName, OutgoingProfiles);
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

        private void CreateCDSProfile(OrganizationServiceProxy _service)
        {
            try
            {
                while (true)
                {
                    Profile cdsProfile = GetProfile();

                    if (cdsProfile == null)
                    {
                        break;
                    }

                    cdsProfile.EmailAddressDomain = EmailDomain;

                    Guid entityId = cdsProfile.WriteToCDS(_service);

                    if (entityId != Guid.Empty)
                    {
                        Increment(cdsProfile);
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

        public void CreateProducts(OrganizationServiceProxy _service, List<Medication> products)
        {
            try
            {
                // we need the Uomid/groupid first
                MedicineUoM Uom = new MedicineUoM();

                Uom.WriteToCDS(_service);

                // create the products
                foreach (Medication product in products)
                {
                    product.UomId = Uom.UomId;
                    product.UnitGroupId = Uom.GroupId;
                    product.WriteToCDS(_service);
                }

                // create the pricelist and associate products
                MedicationPriceList PriceList = new MedicationPriceList();
                PriceList.Products = products;
                PriceList.GroupId = Uom.GroupId;
                PriceList.UomId = Uom.UomId;

                PriceList.WriteToCDS(_service);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #region Queueing Help

        private static object addlock = new object();
     
        /// <summary>
        /// list we are writing back to the file
        /// </summary>
        /// <param name="contact"></param>

        private static object getObject = new object();
        private Profile GetProfile()
        {
            lock (getObject)
            {
                if (IncomingProfiles.Count > 0)
                {
                    return IncomingProfiles.Dequeue();
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

    }
}
