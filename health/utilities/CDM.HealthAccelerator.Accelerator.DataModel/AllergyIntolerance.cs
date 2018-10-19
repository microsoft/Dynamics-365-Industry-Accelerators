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
    public class AllergyIntolerance : BaseFunctions
    {
        private string allergyIntoleranceId;

        public AllergyIntolerance()
        {
            InitializeEntity();
        }

        public AllergyIntolerance(string patientId)
        {
            PatientId = patientId;
            InitializeEntity();
        }

        string patientId; // patient assigned too

        int vertificationStatus; // confirmed / unconfirmed etc

        string item; // string in file

        int severity; // high risk etc

        int type; //allergy / intollerance

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

        public int VertificationStatus
        {
            get
            {
                return vertificationStatus;
            }

            set
            {
                vertificationStatus = value;
            }
        }

        public string Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }

        public int Severity
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

        public string AllergyIntoleranceId
        {
            get
            {
                return allergyIntoleranceId;
            }

            set
            {
                allergyIntoleranceId = value;
            }
        }

        public override void InitializeEntity()
        {
            Item = SampleDataCache.AllergyItems[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.AllergyItems.Count - 1)];

            VertificationStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.AllergyIntolerance_Verificationstatus>();
            Severity = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.AllergyIntolerance_Criticality>();
            Type = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.AllergyIntolerance_Type>();
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid allergyIntolernaceId = Guid.Empty;

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

                    allergyIntolernaceId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return allergyIntolernaceId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid allergyIntolernaceId = Guid.Empty;

            HealthCDM.msemr_allergyintolerance addAllergyIntolerance = new HealthCDM.msemr_allergyintolerance();

            addAllergyIntolerance.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addAllergyIntolerance.msemr_name = Item;
            addAllergyIntolerance.msemr_Code = Item;
            addAllergyIntolerance.msemr_Criticality = new OptionSetValue(Severity);
            addAllergyIntolerance.msemr_VerificationStatus = new OptionSetValue(VertificationStatus);
            addAllergyIntolerance.msemr_Type = new OptionSetValue(Type);

            try
            {
                allergyIntolernaceId = _serviceProxy.Create(addAllergyIntolerance);

                if (allergyIntolernaceId != Guid.Empty)
                {
                    AllergyIntoleranceId = allergyIntolernaceId.ToString();
                    Console.WriteLine("Created Allergy Intolerance [" + AllergyIntoleranceId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("AllegyIntoleranceId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return allergyIntolernaceId;
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
