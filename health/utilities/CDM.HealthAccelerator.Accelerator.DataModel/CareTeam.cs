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
    public class CareTeam : BaseFunctions
    {

        public CareTeam()
        {
            InitializeEntity();
        }

        #region Attributes

        private string careTeamId;
        private string name;
        private string patientId;

        public string CareTeamId
        {
            get
            {
                return careTeamId;
            }

            set
            {
                careTeamId = value;
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

        #endregion

        public override void InitializeEntity()
        {
            // nothing for here yet
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
            Guid patientCareTeamId = Guid.Empty;

            HealthCDM.msemr_careteam addCareTeam = new HealthCDM.msemr_careteam();

            addCareTeam.msemr_SubjectPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addCareTeam.msemr_name = Name;

            try
            {
                patientCareTeamId = _serviceProxy.Create(addCareTeam);

                if (patientCareTeamId != Guid.Empty)
                {
                    CareTeamId = patientCareTeamId.ToString();
                    Console.WriteLine("Created Patient CareTeam [" + patientCareTeamId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("CareTeamId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientCareTeamId;
        }
    }
}
