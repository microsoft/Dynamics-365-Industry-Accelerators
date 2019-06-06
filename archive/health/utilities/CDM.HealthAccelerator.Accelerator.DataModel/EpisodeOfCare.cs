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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xrm.Sdk;
using System.ServiceModel.Description;
using System.Net;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class EpisodeOfCare : BaseFunctions
    {
        #region Attributes

        private string practitionerId; // caremanager 
        private string description;
        private DateTime endDateTime;
        private DateTime startDateTime;
        private string patientId;
        private string accountId;
        private string episodeOfCareId;
        private string encounterId;

        public string PractitionerId
        {
            get
            {
                return practitionerId;
            }

            set
            {
                practitionerId = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                return endDateTime;
            }

            set
            {
                endDateTime = value;
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                return startDateTime;
            }

            set
            {
                startDateTime = value;
            }
        }

        public string PatientId
        {
            get
            {
                return patientId;
            }

            set
            {
                patientId = value;
            }
        }

        public string AccountId
        {
            get
            {
                return accountId;
            }

            set
            {
                accountId = value;
            }
        }

        public string EpisodeOfCareId
        {
            get
            {
                return episodeOfCareId;
            }

            set
            {
                episodeOfCareId = value;
            }
        }

        public string EncounterId
        {
            get
            {
                return encounterId;
            }

            set
            {
                encounterId = value;
            }
        }

        #endregion

        public EpisodeOfCare()
        {
            InitializeEntity();
        }

        public EpisodeOfCare(string patientId, string accountId, string practitionerId)
        {
            PatientId = patientId;

            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 5, 10);

            StartDateTime = startDate;
            EndDateTime = endDate;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid episodeId = Guid.Empty;

            try
            {

                #region Get our Login Information

                // setup the variables
                OrganizationServiceProxy _serviceProxy;

                // homeRealmUri will stay null for now
                Uri homeRealmUri = null;

                // setup credentials from whatever is in the app.config
                ClientCredentials credentials;

                // same for organizationuri comes from app.config
                Uri organizationUri;

                // set the organization uri from what was in the app.config
                organizationUri = new Uri(cdsUrl);

                credentials = new ClientCredentials();
                credentials.UserName.UserName = cdsUserName;
                credentials.UserName.Password = cdsPassword;

                #endregion

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (_serviceProxy = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials, null))
                {
                    // To impersonate set the GUID of CRM user here (which I merely took from CRM itself
                    // would need not to use this caller id in the future (as it will change per instance of CRM)
                    //_serviceProxy.CallerId = new Guid("14D40CB7-81D5-E311-93F5-00155D00330C");
                    _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                    //enable using proxy types
                    _serviceProxy.EnableProxyTypes();

                    episodeId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return episodeId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid episodeId = Guid.Empty;

            try
            {
                HealthCDM.msemr_episodeofcare episode = new HealthCDM.msemr_episodeofcare();

                episode.msemr_CareManager = new EntityReference(HealthCDM.Contact.EntityLogicalName, new Guid(PractitionerId));
                //episode.msemr_msemr_episodeofcare_msemr_encounter_ContextEpisodeofCare = new EntityReference(HealthCDM.Contact.EntityLogicalName, new Guid(PractitionerId));
                episode.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, new Guid(PatientId));
                episode.msemr_Organization = new EntityReference(HealthCDM.Contact.EntityLogicalName, new Guid(AccountId));
                episode.msemr_description = Description;
                episode.msemr_StartDateTime = StartDateTime;
                episode.msemr_EndDateTime = EndDateTime;
        
                episodeId = _serviceProxy.Create(episode);

                if (episodeId != Guid.Empty)
                {
                    EpisodeOfCareId = episodeId.ToString();
                    Console.WriteLine("Created EpisodeOfcare [" + episodeId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("episodeId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return episodeId;
        }
    }
}
