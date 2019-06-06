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
    public class CarePlan : BaseFunctions
    {

        #region Attributes

        private string carePlanId;
        private string encounterId;
        private string patientId;
        private string description;
        private DateTime startDate;
        private DateTime endDate;
        private int subjectType;
        private string title;
        private int contextType;
        private int intent;

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

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
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

        #endregion

        public CarePlan()
        {
            InitializeEntity();
        }

        public CarePlan(string patientid, string encounterid)
        {
            PatientId = patientid;
            EncounterId = encounterId;
        }

        public override void InitializeEntity()
        {
            ContextType = (int)HealthCDMEnums.CarePlan_Contexttype.Encounter;
            SubjectType = (int)HealthCDMEnums.CarePlan_Subjecttype.Patient;
            Intent = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.CarePlan_Planintent>();

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2013, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 5, 10);

            StartDate = startDate;
            EndDate = endDate;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientCarePlanId = Guid.Empty;

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

                    patientCarePlanId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientCarePlanId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientCarePlanId = Guid.Empty;

            HealthCDM.msemr_careplan addCarePlan = new HealthCDM.msemr_careplan();

            addCarePlan.msemr_ContextType = new OptionSetValue(ContextType);
            addCarePlan.msemr_PatientIdentifier = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
            addCarePlan.msemr_EncounterIdentifier = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse(EncounterId));
            addCarePlan.msemr_PlanStartDate = StartDate;
            addCarePlan.msemr_PlanEndDate = EndDate;
            addCarePlan.msemr_SubjectType = new OptionSetValue(SubjectType);
            addCarePlan.msemr_PlanIntent = new OptionSetValue(Intent);
            addCarePlan.msemr_PlanDescription = Description;
            addCarePlan.msemr_title = title;

            try
            {
                patientCarePlanId = _serviceProxy.Create(addCarePlan);

                if (patientCarePlanId != Guid.Empty)
                {
                    CarePlanId = patientCarePlanId.ToString();
                    Console.WriteLine("Created Care Plan [" + patientCarePlanId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("CarePlanId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientCarePlanId;
        }
    }
}
