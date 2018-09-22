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
using System.ServiceModel.Description;
using System.Net;
using Microsoft.Xrm.Sdk;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class MedicationRequest : BaseFunctions
    {
        private string patientId;
        private string practitionerId;
        private string name;
        private int status;
        private DateTime authoredOn;
        private int priority;
        private int subjectType;
        private string medicationId;
        private int medicationType;

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

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public DateTime AuthoredOn
        {
            get
            {
                return authoredOn;
            }

            set
            {
                authoredOn = value;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
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

        public string MedicationId
        {
            get
            {
                return medicationId;
            }

            set
            {
                medicationId = value;
            }
        }

        public int MedicationType
        {
            get
            {
                return medicationType;
            }

            set
            {
                medicationType = value;
            }
        }

        public MedicationRequest()
        {
            InitializeEntity();
        }
        
        public MedicationRequest(string patientid, string practitionerid)
        {
            PatientId = patientId;
            PractitionerId = practitionerid;
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            Name = SampleDataCache.Medications[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.Medications.Count - 1)];
            Status = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.MedicationRequest_Status>();
            Priority = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.MedicationRequest_Priority>();
            MedicationType = (int)HealthCDMEnums.MedicationRequest_Medicationtype.MedicationReference;

            SampleDataCache.RandomDateTime rdt = new SampleDataCache.RandomDateTime(2017, 1, 1, DateTime.Today);
            authoredOn = rdt.Next();

            SubjectType = (int)HealthCDMEnums.MedicationRequest_Subjecttype.Patient;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword, string cdsEmailDomain)
        {
            Guid medicationrequestId = Guid.Empty;

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

                    HealthCDM.msemr_medicationrequest addMedicationRequest = new HealthCDM.msemr_medicationrequest();

                    addMedicationRequest.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
                    addMedicationRequest.msemr_RequesterAgentTypePractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
                    addMedicationRequest.msemr_Status = new OptionSetValue(Status);
                    addMedicationRequest.msemr_AuthoredOn = AuthoredOn;
                    addMedicationRequest.msemr_name = Name;
                    addMedicationRequest.msemr_Priority = new OptionSetValue(Priority);
                    addMedicationRequest.msemr_SubjectType = new OptionSetValue(SubjectType);
                    addMedicationRequest.msemr_MedicationType = new OptionSetValue(MedicationType);
                    // NEED TO add products for this part to work addMedicationRequest.msemr_medicationtypereference = new EntityReference

                    try
                    {
                        medicationrequestId = _serviceProxy.Create(addMedicationRequest);

                        if (medicationrequestId != Guid.Empty)
                        {
                            MedicationId = medicationrequestId.ToString();
                            Console.WriteLine("Created Patient Medication Request [" + MedicationId + "] for Patient [" + PatientId + "]");
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return medicationrequestId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid medicationrequestId = Guid.Empty;

            HealthCDM.msemr_medicationrequest addMedicationRequest = new HealthCDM.msemr_medicationrequest();

            addMedicationRequest.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addMedicationRequest.msemr_RequesterAgentTypePractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            addMedicationRequest.msemr_Status = new OptionSetValue(Status);
            addMedicationRequest.msemr_AuthoredOn = AuthoredOn;
            addMedicationRequest.msemr_name = Name;
            addMedicationRequest.msemr_Priority = new OptionSetValue(Priority);
            addMedicationRequest.msemr_SubjectType = new OptionSetValue(SubjectType);
            addMedicationRequest.msemr_MedicationType = new OptionSetValue(MedicationType);

            try
            {
                medicationrequestId = _serviceProxy.Create(addMedicationRequest);

                if (medicationrequestId != Guid.Empty)
                {
                    MedicationId = medicationrequestId.ToString();
                    Console.WriteLine("Created Patient Medication Request [" + MedicationId + "] for Patient [" + PatientId + "]");
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

            return medicationrequestId;
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
