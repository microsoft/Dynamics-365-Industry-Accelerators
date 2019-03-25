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
    public class Procedure : BaseFunctions
    {
        private string patientId;
        private string description;
        private int status;
        private int subjectType;
        private DateTime procedureDateTime;
        private string procedureId;
        private string identifier;
        private bool notDone;
        private string encounterId;
        private string locationId;

        public Procedure()
        {
            InitializeEntity();
        }

        public Procedure(string patientid)
        {
            PatientId = patientid;
            InitializeEntity();
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

        public DateTime ProcedureDateTime
        {
            get
            {
                return procedureDateTime;
            }

            set
            {
                procedureDateTime = value;
            }
        }

        public string ProcedureId
        {
            get
            {
                return procedureId;
            }

            set
            {
                procedureId = value;
            }
        }

        public string Identifier
        {
            get
            {
                return identifier;
            }

            set
            {
                identifier = value;
            }
        }

        public bool NotDone
        {
            get
            {
                return notDone;
            }

            set
            {
                notDone = value;
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

        public string LocationId
        {
            get
            {
                return locationId;
            }

            set
            {
                locationId = value;
            }
        }

        public override void InitializeEntity()
        {
            Description = SampleDataCache.Procedures[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Procedures.Count - 1)];
            Status = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Procedure_Status>();
            SubjectType = (int)HealthCDMEnums.Procedure_Subjecttype.Patient;

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, (DateTime.Today.AddDays(90)));

            ProcedureDateTime = rdt.Next();
            identifier = GenerateRandomNumber(8);

            NotDone = int.Parse(GenerateRandomNumber(1)) > 5 ? true : false;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientProcedureId = Guid.Empty;

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

                    patientProcedureId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientProcedureId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientProcedureId = Guid.Empty;

            HealthCDM.msemr_procedure addProcedure = new HealthCDM.msemr_procedure();

            addProcedure.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addProcedure.msemr_Status = new OptionSetValue(Status);
            addProcedure.msemr_DateTime = ProcedureDateTime;
            addProcedure.msemr_SubjectType = new OptionSetValue(SubjectType);
            addProcedure.msemr_description = Description;
            addProcedure.msemr_ProcedureIdentifier = Identifier;
            addProcedure.msemr_Location = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, Guid.Parse((LocationId)));
            addProcedure.msemr_Encounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse((EncounterId)));


            try
            {
                patientProcedureId = _serviceProxy.Create(addProcedure);

                if (patientProcedureId != Guid.Empty)
                {
                    ProcedureId = patientProcedureId.ToString();
                    Console.WriteLine("Created Patient Procedure [" + ProcedureId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("ProcedueId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientProcedureId;
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
