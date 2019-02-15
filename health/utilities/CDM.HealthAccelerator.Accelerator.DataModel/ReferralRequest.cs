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
using Microsoft.Xrm.Sdk;
using System.Net;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class ReferralRequest : BaseFunctions
    {
        public ReferralRequest()
        {
            InitializeEntity();
        }

        public ReferralRequest(string patientid, string practitionerid, string encounterid, string appointmentid)
        {
            PatientId = patientid;
            PractitionerId = practitionerid;
            EncounterId = encounterid;
            AppointmentId = appointmentid;
            InitializeEntity();
        }

        #region Attributes

        private string patientId;
        private string practitionerId;
        private string description;
        private int priority;
        private int status;
        private int contextType;
        private DateTime occurrendateDate;
        private int intent;
        private string referralRequestId;
        private int subject;
        private string referralRequestNumber;
        private int requestAgent;
        private int occurrenceType;
        private string encounterId;
        private string appointmentId;

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

        public DateTime OccurrendateDate
        {
            get
            {
                return occurrendateDate;
            }

            set
            {
                occurrendateDate = value;
            }
        }

        public int Intent
        {
            get
            {
                return intent;
            }

            set
            {
                intent = value;
            }
        }

        public string ReferralRequestId
        {
            get
            {
                return referralRequestId;
            }

            set
            {
                referralRequestId = value;
            }
        }

        public int Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        public string ReferralRequestNumber
        {
            get
            {
                return referralRequestNumber;
            }

            set
            {
                referralRequestNumber = value;
            }
        }

        public int RequestAgent
        {
            get
            {
                return requestAgent;
            }

            set
            {
                requestAgent = value;
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

        #endregion


        public override void InitializeEntity()
        {
            Description = SampleDataCache.ReferralRequests[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.ReferralRequests.Count - 1)];
            Status = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Status>();
            Intent = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Intent>();
            Priority = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Priority>();
            ContextType = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Contexttype>();
            Subject = (int)HealthCDMEnums.ReferralRequest_Subject.Patient;
            ReferralRequestNumber = GenerateRandomNumber(8);
            RequestAgent = (int)HealthCDMEnums.ReferralRequest_Requesteragent.Practitioner;
            OccurrenceType = (int)HealthCDMEnums.ReferralRequest_Occurrencetype.Date;

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, (DateTime.Today.AddDays(90)));
            OccurrendateDate = rdt.Next();
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid referralrequestId = Guid.Empty;

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

                    referralrequestId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return referralrequestId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid referralrequestId = Guid.Empty;

            HealthCDM.msemr_referralrequest addReferralRequest = new HealthCDM.msemr_referralrequest();

            addReferralRequest.msemr_SubjectPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addReferralRequest.msemr_Requestor = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            addReferralRequest.msemr_Status = new OptionSetValue(Status);
            addReferralRequest.msemr_OccurrenceDate = OccurrendateDate;
            addReferralRequest.msemr_Description = Description;
            addReferralRequest.msemr_Priority = new OptionSetValue(Priority);
            addReferralRequest.msemr_Intent = new OptionSetValue(Intent);
            addReferralRequest.msemr_name = Description;
            addReferralRequest.msemr_ContextType = new OptionSetValue(contextType);
            addReferralRequest.msemr_Subject = new OptionSetValue(Subject);
            addReferralRequest.msemr_ReferralRequestNumber = ReferralRequestNumber;
            addReferralRequest.msemr_RequesterAgent = new OptionSetValue(RequestAgent);
            addReferralRequest.msemr_OccurrenceType = new OptionSetValue(OccurrenceType);
            addReferralRequest.msemr_InitiatingEncounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse((EncounterId)));
            addReferralRequest.msemr_AppointmentEMR = new EntityReference(HealthCDM.msemr_appointmentemr.EntityLogicalName, Guid.Parse((AppointmentId)));

            try
            {
                referralrequestId = _serviceProxy.Create(addReferralRequest);

                if (referralrequestId != Guid.Empty)
                {
                    ReferralRequestId = referralrequestId.ToString();
                    Console.WriteLine("Created Patient Referral Request [" + ReferralRequestId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("ReferralRequestId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return referralrequestId;
        }

    }
}
