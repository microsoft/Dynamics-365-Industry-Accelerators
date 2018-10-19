// =====================================================================
//  This file is part of the Microsoft Dynamics Accelerator code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
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
namespace CDM.HealthAccelerator.DataModel//namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Specimen : BaseFunctions
    {
        #region Attributes

        private string specimenId;
        private string accessionNumber;
        private int collectedQuantity;
        private DateTime collectedDateTime;
        private int collectedType;
        private string name;
        private DateTime receivedDateTime;
        private string specimenNumber;
        private int subjectType;
        private string collectedMethod;
        private string collectorId;
        private string deviceId;
        private string patientId;
        private string practitionerId;

        public string SpecimenId
        {
            get
            {
                return specimenId;
            }

            set
            {
                specimenId = value;
            }
        }

        public string AccessionNumber
        {
            get
            {
                return accessionNumber;
            }

            set
            {
                accessionNumber = value;
            }
        }

        public int CollectedQuantity
        {
            get
            {
                return collectedQuantity;
            }

            set
            {
                collectedQuantity = value;
            }
        }

        public DateTime CollectedDateTime
        {
            get
            {
                return collectedDateTime;
            }

            set
            {
                collectedDateTime = value;
            }
        }

        public int CollectedType
        {
            get
            {
                return collectedType;
            }

            set
            {
                collectedType = value;
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

        public DateTime ReceivedDateTime
        {
            get
            {
                return receivedDateTime;
            }

            set
            {
                receivedDateTime = value;
            }
        }

        public string SpecimenNumber
        {
            get
            {
                return specimenNumber;
            }

            set
            {
                specimenNumber = value;
            }
        }

        public int SubjectType
        {
            get
            {
                return subjectType;
            }

            set
            {
                subjectType = value;
            }
        }

        public string CollectedMethod
        {
            get
            {
                return collectedMethod;
            }

            set
            {
                collectedMethod = value;
            }
        }

        public string CollectorId
        {
            get
            {
                return collectorId;
            }

            set
            {
                collectorId = value;
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

        #endregion

        public Specimen()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

            receivedDateTime = rdt.Next();
            collectedDateTime = rdt.Next();

            SpecimenNumber = GenerateRandomNumber();
            AccessionNumber = GenerateRandomNumber();
            SubjectType = (int)HealthCDMEnums.Specimen_Subjecttype.Patient;
            CollectedType = (int)HealthCDMEnums.Specimen_Collectioncollectedtype.Datetime;
            CollectedQuantity = int.Parse(GenerateRandomNumber(1));

            CollectedMethod = SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.SpecimenCollectionMethod.ToString()]
                        .Values.ElementAt(SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.SpecimenCollectionMethod.ToString()]
                        .Values.Count - 1)).Key;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientSpecimeId = Guid.Empty;

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

                    patientSpecimeId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientSpecimeId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientSpecimenId = Guid.Empty;

            HealthCDM.msemr_specimen addSpecimen = new HealthCDM.msemr_specimen();

            addSpecimen.msemr_AccessionNumber = AccessionNumber;
            addSpecimen.msemr_SpecimenNumber = SpecimenNumber;
            addSpecimen.msemr_CollectionCollectedType = new OptionSetValue(CollectedType);
            addSpecimen.msemr_CollectedQuantity = CollectedQuantity;
            addSpecimen.msemr_ReceivedTime = ReceivedDateTime;
            addSpecimen.msemr_CollectionCollectedDateTime = CollectedDateTime;
            addSpecimen.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
            addSpecimen.msemr_SubjectTypeDevice = new EntityReference(HealthCDM.msemr_device.EntityLogicalName, Guid.Parse(DeviceId));
            addSpecimen.msemr_CollectionCollector = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PractitionerId));
            addSpecimen.msemr_CollectedMethod = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, CollectedMethod, (int)HealthCDMEnums.CodeableConcept_Type.SpecimenCollectionMethod));
            addSpecimen.msemr_name = Name;

            try
            {
                patientSpecimenId = _serviceProxy.Create(addSpecimen);

                if (patientSpecimenId != Guid.Empty)
                {
                    SpecimenId = patientSpecimenId.ToString();
                    Console.WriteLine("Created Specimen [" + SpecimenId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("SpecimenId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientSpecimenId;
        }
    }
}
