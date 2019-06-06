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

    public class MedicationAdministration : BaseFunctions
    {

        #region Attributes

        private string medicationAdministrationId;
        private string encounterId;
        private int dosageDose;
        private int dosageRateQuantity;
        private DateTime startDate;
        private DateTime endDate;
        private string patientId;
        private string name;
        private int dosageRateType;
        private int subjectType;

        public string MedicationAdministrationId
        {
            get
            {
                return medicationAdministrationId;
            }

            set
            {
                medicationAdministrationId = value;
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

        public int DosageDose
        {
            get
            {
                return dosageDose;
            }

            set
            {
                dosageDose = value;
            }
        }

        public int DosageRateQuantity
        {
            get
            {
                return dosageRateQuantity;
            }

            set
            {
                dosageRateQuantity = value;
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

        public int DosageRateType
        {
            get
            {
                return dosageRateType;
            }

            set
            {
                dosageRateType = value;
            }
        }

        #endregion

        public MedicationAdministration()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            DosageDose = int.Parse(GenerateRandomNumber(1));
            DosageRateQuantity = 1;

            DosageRateType = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Dosage_Dosetype>();

            SubjectType = (int)HealthCDMEnums.MedicationAdministration_Subjecttype.Patient;

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddDays(startDate, 10, 20);

            StartDate = startDate;
            EndDate = endDate;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientMedicationAdministrationId = Guid.Empty;

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

                    patientMedicationAdministrationId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientMedicationAdministrationId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientMedicationAdministrationId = Guid.Empty;

            HealthCDM.msemr_medicationadministration addMedicationAdministration = new HealthCDM.msemr_medicationadministration();

            addMedicationAdministration.msemr_SubjectTypePatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addMedicationAdministration.msemr_ContextTypeEncounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse((EncounterId)));
            addMedicationAdministration.msemr_name = Name;
            addMedicationAdministration.msemr_SubjectType = new OptionSetValue(SubjectType);
            addMedicationAdministration.msemr_DosageDose = DosageDose;
            addMedicationAdministration.msemr_DosageRateQuantity = DosageRateQuantity;
            addMedicationAdministration.msemr_DosageRateType = new OptionSetValue(DosageRateType);
            addMedicationAdministration.msemr_EffectivePeriodEndDate = endDate;
            addMedicationAdministration.msemr_EffectivePeriodStartDate = startDate;

            try
            {
                patientMedicationAdministrationId = _serviceProxy.Create(addMedicationAdministration);

                if (patientMedicationAdministrationId != Guid.Empty)
                {
                    MedicationAdministrationId = patientMedicationAdministrationId.ToString();
                    Console.WriteLine("Created Patient Medication Administration [" + patientMedicationAdministrationId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("MedicationAdministrationId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientMedicationAdministrationId;
        }
    }
}
