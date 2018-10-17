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
using HealthCDM;
using Microsoft.Xrm.Sdk.Query;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class MedicineUoM : BaseFunctions
    {

        #region Attributes

        private string uomId;
        private string groupId;

        public string UomId
        {
            get
            {
                return uomId;
            }

            set
            {
                uomId = value;
            }
        }

        public string GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
            }
        }

        #endregion

        public MedicineUoM()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid productMedicationId = Guid.Empty;

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

                    productMedicationId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return productMedicationId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid productMedicationId = Guid.Empty;

            HealthCDM.UoM adduom = new HealthCDM.UoM();

            try
            {

                UoMSchedule newUnitGroup = new UoMSchedule
                {
                    Name = "Sample Unit Group " + GenerateRandomNumber(),
                    BaseUoMName = "Sample Primary Unit " + GenerateRandomNumber()
                };

                GroupId = (_serviceProxy.Create(newUnitGroup)).ToString();

                // retrieve the unit id.
                QueryExpression unitQuery = new QueryExpression
                {
                    EntityName = UoM.EntityLogicalName,
                    ColumnSet = new ColumnSet("uomid", "name"),
                    Criteria = new FilterExpression(),
                    PageInfo = new PagingInfo
                    {
                        PageNumber = 1,
                        Count = 1
                    }
                };

                unitQuery.Criteria.AddCondition("uomscheduleid", ConditionOperator.Equal, Guid.Parse(GroupId));

                // Retrieve the unit.
                productMedicationId = _serviceProxy.RetrieveMultiple(unitQuery).Entities[0].Id;

                if (productMedicationId != Guid.Empty)
                {
                     UomId = productMedicationId.ToString();
                }
                else
                {
                    throw new Exception("UomId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return productMedicationId;
        }
    }
}
