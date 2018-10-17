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
using System.IO;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using Newtonsoft.Json;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Encounter : BaseFunctions
    {
        #region Primary Attributes

        private string encounterId;
        private string appointmentemrId;
        private int encounterClass;
        private DateTime encounterEndDate;
        private string encounterIdentifier;
        private DateTime encounterStartDate;
        private string hospitalizationDestinationId;
        private string hospitalizationOriginId;
        private string hospitalizationPreadmissionNumber;
        private string name;
        //private DateTime periodStartDate;
        private string subjectPatientId;

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

        public string AppointmentEmrId
        {
            get
            {
                return appointmentemrId;
            }

            set
            {
                appointmentemrId = value;
            }
        }

        public int EncounterClass
        {
            get
            {
                return encounterClass;
            }

            set
            {
                encounterClass = value;
            }
        }

        public DateTime EncounterEndDate
        {
            get
            {
                return encounterEndDate;
            }

            set
            {
                encounterEndDate = value;
            }
        }

        public string EncounterIdentifier
        {
            get
            {
                return encounterIdentifier;
            }

            set
            {
                encounterIdentifier = value;
            }
        }

        public DateTime EncounterStartDate
        {
            get
            {
                return encounterStartDate;
            }

            set
            {
                encounterStartDate = value;
            }
        }

        public string HospitalizationDestinationId
        {
            get
            {
                return hospitalizationDestinationId;
            }

            set
            {
                hospitalizationDestinationId = value;
            }
        }

        public string HospitalizationOriginId
        {
            get
            {
                return hospitalizationOriginId;
            }

            set
            {
                hospitalizationOriginId = value;
            }
        }

        public string HospitalizationPreadmissionNumber
        {
            get
            {
                return hospitalizationPreadmissionNumber;
            }

            set
            {
                hospitalizationPreadmissionNumber = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string SubjectPatientId
        {
            get
            {
                return subjectPatientId;
            }

            set
            {
                subjectPatientId = value;
            }
        }

        #endregion

        public Encounter()
        {
            InitializeEntity();
        }

        public Encounter(string originId, string destinationId, string patientId, string appointmentId, string name)
        {
            SubjectPatientId = patientId;
            Name = name;
            HospitalizationDestinationId = destinationId;
            HospitalizationOriginId = originId;
            AppointmentEmrId = appointmentId;

            InitializeEntity();
        }


        public override void InitializeEntity()
        {
            EncounterClass = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Encounter_Class>();

            HospitalizationPreadmissionNumber = GenerateRandomNumber(12);
            EncounterIdentifier = GenerateRandomNumber(15);

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 1, 2);

            EncounterStartDate = startDate;
            EncounterEndDate = endDate;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientEncounterId = Guid.Empty;

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

                    patientEncounterId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientEncounterId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientEncounterId = Guid.Empty;

            HealthCDM.msemr_encounter addEncounter = new HealthCDM.msemr_encounter();

            if (!string.IsNullOrEmpty(SubjectPatientId))
            {
                addEncounter.msemr_SubjectPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((SubjectPatientId)));
            }

            if (!string.IsNullOrEmpty(HospitalizationOriginId))
            {
                addEncounter.msemr_HospitalizationOrigin = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, Guid.Parse((HospitalizationOriginId)));
            }

            if (!string.IsNullOrEmpty(HospitalizationDestinationId))
            {
                addEncounter.msemr_HospitalizationDestination = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, Guid.Parse((HospitalizationDestinationId)));
            }

            if (!string.IsNullOrEmpty(AppointmentEmrId))
            {
                addEncounter.msemr_AppointmentEMR = new EntityReference(HealthCDM.msemr_appointmentemr.EntityLogicalName, Guid.Parse((AppointmentEmrId)));
            }

            addEncounter.msemr_name = Name;
            addEncounter.msemr_EncounterEndDate = EncounterEndDate;
            addEncounter.msemr_EncounterStartDate = EncounterStartDate;
            addEncounter.msemr_EncounterIdentifier = EncounterIdentifier;
            //addEncounter.msemr_EncounterClass = new OptionSetValue(EncounterClass);
            addEncounter.msemr_HospitalizationPreAdmissionNumber = HospitalizationPreadmissionNumber;

            try
            {
                patientEncounterId = _serviceProxy.Create(addEncounter);

                if (patientEncounterId != Guid.Empty)
                {
                    EncounterId = patientEncounterId.ToString();
                    Console.WriteLine("Created Encounter Request [" + EncounterId + "] for Patient [" + SubjectPatientId + "]");
                }
                else
                {
                    throw new Exception("Medication Request Id == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientEncounterId;
        }
    }
}
