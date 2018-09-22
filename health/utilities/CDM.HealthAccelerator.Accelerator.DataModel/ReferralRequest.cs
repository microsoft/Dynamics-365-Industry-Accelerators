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

        public ReferralRequest(string patientid, string practitionerid)
        {
            PatientId = patientid;
            PractitionerId = practitionerid;
            InitializeEntity();
        }

        private string patientId;
        private string practitionerId;
        private string description;
        private int priority;
        private int status;
        private int type;
        private DateTime occurrendateDate;
        private int intent;
        private string referralRequestId;

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

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
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

        public override void InitializeEntity()
        {
            Description = SampleDataCache.ReferralRequests[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.ReferralRequests.Count - 1)];
            Status = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Status>();
            Intent = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Intent>();
            Priority = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.ReferralRequest_Priority>();

            SampleDataCache.RandomDateTime rdt = new SampleDataCache.RandomDateTime(2017, 1, 1, (DateTime.Today.AddDays(90)));
            OccurrendateDate = rdt.Next();
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword, string cdsEmailDomain)
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

                    HealthCDM.msemr_referralrequest addReferrralRequest = new HealthCDM.msemr_referralrequest();

                    addReferrralRequest.msemr_SubjectPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
                    addReferrralRequest.msemr_Requestor = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
                    addReferrralRequest.msemr_Status = new OptionSetValue(Status);
                    addReferrralRequest.msemr_OccurrenceDate = OccurrendateDate;
                    addReferrralRequest.msemr_Description = Description;
                    addReferrralRequest.msemr_Priority = new OptionSetValue(Priority);
                    addReferrralRequest.msemr_Intent = new OptionSetValue(Intent);
                    addReferrralRequest.msemr_name = Description;

                    try
                    {
                        referralrequestId = _serviceProxy.Create(addReferrralRequest);

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

            HealthCDM.msemr_referralrequest addReferrralRequest = new HealthCDM.msemr_referralrequest();

            addReferrralRequest.msemr_SubjectPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addReferrralRequest.msemr_Requestor = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            addReferrralRequest.msemr_Status = new OptionSetValue(Status);
            addReferrralRequest.msemr_OccurrenceDate = OccurrendateDate;
            addReferrralRequest.msemr_Description = Description;
            addReferrralRequest.msemr_Priority = new OptionSetValue(Priority);
            addReferrralRequest.msemr_Intent = new OptionSetValue(Intent);
            addReferrralRequest.msemr_name = Description;

            try
            {
                referralrequestId = _serviceProxy.Create(addReferrralRequest);

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

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
