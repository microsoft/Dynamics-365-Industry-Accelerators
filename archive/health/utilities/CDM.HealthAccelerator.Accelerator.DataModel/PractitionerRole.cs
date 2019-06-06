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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class PractitionerRole : BaseFunctions
    {
        #region Properties

        private string roleId;

        private string practitionerId;

        private DateTime periodEndDate;

        private DateTime periodStartDate;

        private DateTime notAvailableStartDate;

        private DateTime notAvailableEndDate;

        private string organizationId;

        private string notAvailableDescription;

        private string practitionerRoleNumber;

        private string availabilityExceptions;

        private string name;

        public string AvailabilityExceptions
        {
            get
            {
                return availabilityExceptions;
            }

            set
            {
                availabilityExceptions = value;
            }
        }

        public string PractitionerRoleNumber
        {
            get
            {
                return practitionerRoleNumber;
            }

            set
            {
                practitionerRoleNumber = value;
            }
        }

        public string NotAvailableDescription
        {
            get
            {
                return notAvailableDescription;
            }

            set
            {
                notAvailableDescription = value;
            }
        }

        public string OrganizationId
        {
            get
            {
                return organizationId;
            }

            set
            {
                organizationId = value;
            }
        }

        public DateTime NotAvailableEndDate
        {
            get
            {
                return notAvailableEndDate;
            }

            set
            {
                notAvailableEndDate = value;
            }
        }

        public DateTime NotAvailableStartDate
        {
            get
            {
                return notAvailableStartDate;
            }

            set
            {
                notAvailableStartDate = value;
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

        public string RoleId
        {
            get
            {
                return roleId;
            }

            set
            {
                roleId = value;
            }
        }

        #endregion
 
        public PractitionerRole()
        {
            InitializeEntity();
        }

        public PractitionerRole(string practitionerId)
        {
            this.PractitionerId = practitionerId;
            InitializeEntity();
        }


        public override sealed void InitializeEntity()
        {
            Name = SampleDataCache.PractitionerRoles[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.PractitionerRoles.Count - 1)];

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 5, 10);

            PeriodStartDate = startDate;
            PeriodEndDate = endDate;
        }


        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid practitionerRoleId = Guid.Empty;

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

                    practitionerRoleId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return practitionerRoleId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid practitionerRoleId = Guid.Empty;

            try
            {
                HealthCDM.msemr_practitionerrole addPractitionerRole = new HealthCDM.msemr_practitionerrole();

                addPractitionerRole.msemr_Practitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PractitionerId));
                addPractitionerRole.msemr_PeriodEnddatetime = PeriodEndDate;
                addPractitionerRole.msemr_PeriodStartdatetime = PeriodStartDate;
                addPractitionerRole.msemr_name = Name;

                practitionerRoleId = _serviceProxy.Create(addPractitionerRole);

                if (practitionerRoleId != Guid.Empty)
                {
                    RoleId = practitionerRoleId.ToString();
                    Console.WriteLine("Created Practitioner Role [" + RoleId + "] for Practitioner [" + PractitionerId + "]");
                }
                else
                {
                    throw new Exception("RoleId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return practitionerRoleId;
        }

    }
}
