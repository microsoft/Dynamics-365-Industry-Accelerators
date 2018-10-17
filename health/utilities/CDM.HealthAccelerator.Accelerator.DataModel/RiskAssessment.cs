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
    public class RiskAssessment : BaseFunctions
    {

        #region Attributes

        private string riskAssessmentId;
        private string patientId;
        private string practitionerId;
        private string conditionId;
        private string encounterId;
        private int contextType;
        private string name;
        private int occurrenceType;
        private DateTime occurrenceDateTime;
        private int subjectType;
        private int performerType;
        private int reasonType;
        private string riskAssessmentNumber;

        public string RiskAssessmentId
        {
            get
            {
                return riskAssessmentId;
            }

            set
            {
                riskAssessmentId = value;
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

        public int OccurrenceType
        {
            get
            {
                return occurrenceType;
            }

            set
            {
                occurrenceType = value;
            }
        }

        public DateTime OccurrenceDateTime
        {
            get
            {
                return occurrenceDateTime;
            }

            set
            {
                occurrenceDateTime = value;
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

        public int PerformerType
        {
            get
            {
                return performerType;
            }

            set
            {
                performerType = value;
            }
        }

        public int ReasonType
        {
            get
            {
                return reasonType;
            }

            set
            {
                reasonType = value;
            }
        }

        public string RiskAssessmentNumber
        {
            get
            {
                return riskAssessmentNumber;
            }

            set
            {
                riskAssessmentNumber = value;
            }
        }


        #endregion

        public RiskAssessment()
        {
            InitializeEntity();
        }

        public RiskAssessment(string conditionid, string encounterid, string patientid, string practitionerid, string name)
        {
            Name = name;
            ConditionId = conditionid;
            PatientId = patientid;
            EncounterId = encounterid;
            PractitionerId = practitionerid;
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2018, 1, 1, DateTime.Today);
            OccurrenceDateTime= rdt.Next();

            SubjectType = (int)HealthCDMEnums.RiskAssessment_Subjecttype.Patient;
            RiskAssessmentNumber = GenerateRandomNumber();
            ReasonType = (int)HealthCDMEnums.RiskAssessment_Reasontype.CodeableConcept;
            ContextType = (int)HealthCDMEnums.RiskAssessment_Contexttype.Encounter;
            PerformerType = (int)HealthCDMEnums.RiskAssessment_Performertype.Pratitioner;
            OccurrenceType = (int)HealthCDMEnums.RiskAssessment_Occurrencetype.Time;

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
            Guid patientRiskAssessmentId = Guid.Empty;

            HealthCDM.msemr_riskassessment assessment = new HealthCDM.msemr_riskassessment();

            assessment.msemr_name = Name;
            assessment.msemr_Condition = new EntityReference(HealthCDM.msemr_condition.EntityLogicalName, Guid.Parse(ConditionId));
            assessment.msemr_subjectpatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
            assessment.msemr_performerPractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PractitionerId));
            assessment.msemr_contextencounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse(EncounterId));
            assessment.msemr_RiskAssessmentNumber = RiskAssessmentNumber;
            assessment.msemr_OccurrenceType = new OptionSetValue(OccurrenceType);
            assessment.msemr_occurrencedatetime = OccurrenceDateTime;
            assessment.msemr_ReasonType = new OptionSetValue(ReasonType);
            assessment.msemr_PerformerType = new OptionSetValue(PerformerType);
            assessment.msemr_SubjectType = new OptionSetValue(SubjectType);

            try
            {
                patientRiskAssessmentId = _serviceProxy.Create(assessment);

                if (patientRiskAssessmentId != Guid.Empty)
                {
                    RiskAssessmentId = patientRiskAssessmentId.ToString();
                    Console.WriteLine("Created Risk Assessment [" + RiskAssessmentId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("RiskAssessmentId  == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return patientRiskAssessmentId;
        }
    }
}
