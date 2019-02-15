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
    public class PractitionerQualification : BaseFunctions
    {
        #region Attributes

        private string qualificationId;

        private string practitionerId;

        private string display;

        private DateTime periodEndDate;

        private DateTime periodStartDate;

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

        public string Display
        {
            get
            {
                return display;
            }

            set
            {
                display = value;
            }
        }

        public DateTime PeriodEndDate
        {
            get
            {
                return periodEndDate;
            }

            set
            {
                periodEndDate = value;
            }
        }

        public DateTime PeriodStartDate
        {
            get
            {
                return periodStartDate;
            }

            set
            {
                periodStartDate = value;
            }
        }

        public string QualificationId
        {
            get
            {
                return qualificationId;
            }

            set
            {
                qualificationId = value;
            }
        }


        #endregion

        public PractitionerQualification()
        {
            InitializeEntity();
        }

        public PractitionerQualification(string practitionerId)
        {
            PractitionerId = practitionerId;
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            Display = SampleDataCache.PractitionerQualifications[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.PractitionerQualifications.Count - 1)];

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 5, 10);

            PeriodStartDate = startDate;
            PeriodEndDate = endDate;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid practitionerQualificationId = Guid.Empty;

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

                    practitionerQualificationId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return practitionerQualificationId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid practitionerQualificationId = Guid.Empty;

            HealthCDM.msemr_practitionerqualification addPractitionerQualification = new HealthCDM.msemr_practitionerqualification();

            addPractitionerQualification.msemr_Practitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PractitionerId));
            addPractitionerQualification.msemr_PeriodEnd = PeriodEndDate;
            addPractitionerQualification.msemr_PeriodStart = PeriodStartDate;
            addPractitionerQualification.msemr_display = Display;

            try
            {
                practitionerQualificationId = _serviceProxy.Create(addPractitionerQualification);

                if (practitionerQualificationId != Guid.Empty)
                {
                    QualificationId = practitionerQualificationId.ToString();
                    Console.WriteLine("Created Practitioner Qualification [" + QualificationId + "] for Practitioner [" + PractitionerId + "]");
                }
                else
                {
                    throw new Exception("QualificationId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return practitionerQualificationId;
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
