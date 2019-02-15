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
using System.Net;
using Microsoft.Xrm.Sdk;
using System.ServiceModel.Description;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Condition : BaseFunctions
    {
        #region Attributes

        private int verificationStatus;
        private string description;
        private string severity;
        private int subjectType;
        private string patientId;
        private string practitionerId;
        private string conditionId;
        private DateTime onsetDate;
        private string carePlanId;
        private string appointmentId;
        private int onsetType;
        private int onsetAge;
        private int contextType;
        private int clinicalStatus;
        private string asserterId;
        private DateTime assertDate;

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

        public string ConditionId
        {
            get
            {
                return conditionId;
            }

            set
            {
                conditionId = value;
            }
        }

        public DateTime OnsetDate
        {
            get
            {
                return onsetDate;
            }

            set
            {
                onsetDate = value;
            }
        }

        public string Severity
        {
            get
            {
                return severity;
            }

            set
            {
                severity = value;
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

        public int VerificationStatus
        {
            get
            {
                return verificationStatus;
            }

            set
            {
                verificationStatus = value;
            }
        }

        public string CarePlanId
        {
            get
            {
                return carePlanId;
            }

            set
            {
                carePlanId = value;
            }
        }

        public string AppointmentId
        {
            get
            {
                return appointmentId;
            }

            set
            {
                appointmentId = value;
            }
        }

        public int OnsetType
        {
            get
            {
                return onsetType;
            }

            set
            {
                onsetType = value;
            }
        }

        public int OnsetAge
        {
            get
            {
                return onsetAge;
            }

            set
            {
                onsetAge = value;
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

        public int ClinicalStatus
        {
            get
            {
                return clinicalStatus;
            }

            set
            {
                clinicalStatus = value;
            }
        }

        public string AsserterId
        {
            get
            {
                return asserterId;
            }

            set
            {
                asserterId = value;
            }
        }

        public DateTime AssertDate
        {
            get
            {
                return assertDate;
            }

            set
            {
                assertDate = value;
            }
        }

        #endregion

        public Condition ()
        {
            InitializeEntity();
        }

        public Condition(string patientid, string practitionerid, string careplanid, string asserterid)
        {
            PatientId = patientid;
            PractitionerId = practitionerId;
            CarePlanId = careplanid;
            AsserterId = asserterid;
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();

            subjectType = (int)HealthCDMEnums.Condition_Subjecttype.Patient;
            OnsetDate = startDate;

            Description = SampleDataCache.Conditions[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Conditions.Count - 1)];
            OnsetType = (int)HealthCDMEnums.Condition_Onsettype.Age;

            OnsetAge = int.Parse(GenerateRandomNumber(2));

            ContextType = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Condition_Contexttype>();
            ClinicalStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Condition_Clinicalstatus>();

            AsserterId = PatientId;
            AssertDate = DateTime.Now;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientConditionId = Guid.Empty;

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

                    HealthCDM.msemr_condition addCondition = new HealthCDM.msemr_condition();

                    patientConditionId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientConditionId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientConditionId = Guid.Empty;

            HealthCDM.msemr_condition addCondition = new HealthCDM.msemr_condition();

            addCondition.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addCondition.msemr_Practitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            addCondition.msemr_SubjectType = new OptionSetValue(SubjectType);
            addCondition.msemr_OnsetDate = OnsetDate;
            addCondition.msemr_name = Description;
            addCondition.msemr_VerificationStatus = new OptionSetValue(VerificationStatus);
            addCondition.msemr_CarePlan = new EntityReference(HealthCDM.msemr_careplan.EntityLogicalName, Guid.Parse((CarePlanId)));
            addCondition.msemr_Appointment = new EntityReference(HealthCDM.msemr_appointmentemr.EntityLogicalName, Guid.Parse((AppointmentId)));
            addCondition.msemr_ContextType = new OptionSetValue(ContextType);
            addCondition.msemr_ClinicalStatus = new OptionSetValue(ClinicalStatus);
            addCondition.msemr_Asserter = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((AsserterId)));
            addCondition.msemr_AssertedDate = AssertDate;

            try
            {
                patientConditionId = _serviceProxy.Create(addCondition);

                if (patientConditionId != Guid.Empty)
                {
                    ConditionId = patientConditionId.ToString();
                    Console.WriteLine("Created Condition [" + ConditionId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("ConditionId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return patientConditionId;
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
