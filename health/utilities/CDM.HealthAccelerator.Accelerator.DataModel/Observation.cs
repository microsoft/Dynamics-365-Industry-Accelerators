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
    public class Observation : BaseFunctions
    {
        #region Attributes

        private string observationId;
        private string patientId;
        private string locationId;
        private int deviceType;
        private string deviceId;
        private string description;
        private string episodeOfCareId;
        private int contextType;
        private int effectiveType;
        private DateTime issuedDate;
        private DateTime startDate;
        private DateTime endDate;
        private int valueTypeQuantityComparator;
        private decimal valueTypeQuantityValue;
        private DateTime effectiveDateTime;

        public string ObservationId
        {
            get
            {
                return observationId;
            }

            set
            {
                observationId = value;
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

        public int DeviceType
        {
            get
            {
                return deviceType;
            }

            set
            {
                deviceType = value;
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

        public int ContextType
        {
            get
            {
                return contextType;
            }

            set
            {
                contextType = value;
            }
        }

        public int EffectiveType
        {
            get
            {
                return effectiveType;
            }

            set
            {
                effectiveType = value;
            }
        }

        public DateTime IssuedDate
        {
            get
            {
                return issuedDate;
            }

            set
            {
                issuedDate = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public int ValueTypeQuantityComparator
        {
            get
            {
                return valueTypeQuantityComparator;
            }

            set
            {
                valueTypeQuantityComparator = value;
            }
        }

        public decimal ValueTypeQuantityValue
        {
            get
            {
                return valueTypeQuantityValue;
            }

            set
            {
                valueTypeQuantityValue = value;
            }
        }

        public string LocationId
        {
            get
            {
                return locationId;
            }

            set
            {
                locationId = value;
            }
        }

        public string DeviceId
        {
            get
            {
                return deviceId;
            }

            set
            {
                deviceId = value;
            }
        }

        public DateTime EffectiveDateTime
        {
            get
            {
                return effectiveDateTime;
            }

            set
            {
                effectiveDateTime = value;
            }
        }



        #endregion

        public Observation()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2018, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddDays(startDate, 5, 10);

            StartDate = startDate;
            EndDate = endDate;

            IssuedDate = rdt.Next();

            DeviceType = (int)HealthCDMEnums.Observation_Devicetype.Device;

            ValueTypeQuantityComparator = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Observation_Valuerationumeratorcomparator>();
            ValueTypeQuantityValue = int.Parse(GenerateRandomNumber(2));

            EffectiveType = (int)HealthCDMEnums.Observation_Effectivetype.Datetime;
            EffectiveDateTime = rdt.Next();

            ContextType = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Observation_Contexttype>();
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientRiskAssessmentId = Guid.Empty;

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

                    patientRiskAssessmentId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientRiskAssessmentId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid participantObservationId = Guid.Empty;

            HealthCDM.msemr_observation observation = new HealthCDM.msemr_observation();
            observation.msemr_EpisodeofCare = new EntityReference(HealthCDM.msemr_episodeofcare.EntityLogicalName, Guid.Parse(EpisodeOfCareId));
            observation.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
            observation.msemr_ContextType = new OptionSetValue(contextType);
            observation.msemr_DeviceType = new OptionSetValue(DeviceType);
            observation.msemr_EffectiveEnd = EndDate;
            observation.msemr_EffectiveStart = startDate;
            observation.msemr_EffectiveType = new OptionSetValue(EffectiveType);
            observation.msemr_EffectiveTypeDateTime = EffectiveDateTime;
            observation.msemr_IssuedDate = IssuedDate;
            observation.msemr_SubjectTypeDevice = new EntityReference(HealthCDM.msemr_device.EntityLogicalName, Guid.Parse(DeviceId));
            observation.msemr_SubjectTypeLocation = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, Guid.Parse(LocationId));
            observation.msemr_ValueTypeQuantityComparator = new OptionSetValue(valueTypeQuantityComparator);
            observation.msemr_ValueTypeQuantityValue = ValueTypeQuantityValue;
            observation.msemr_description = Description;

            try
            {
                participantObservationId = _serviceProxy.Create(observation);

                if (participantObservationId != Guid.Empty)
                {
                    ObservationId = participantObservationId.ToString();
                    Console.WriteLine("Created Observation [" + ObservationId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("ObservationId  == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return participantObservationId;
        }

    }
}
