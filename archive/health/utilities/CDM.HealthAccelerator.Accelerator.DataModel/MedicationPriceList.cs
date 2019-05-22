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
    public class MedicationPriceList : BaseFunctions
    {

        #region Attributes

        private List<Medication> products = new List<Medication>();
        private string uomId;
        private string priceListId;
        private string groupId;

        public string PriceListId
        {
            get
            {
                return priceListId;
            }

            set
            {
                priceListId = value;
            }
        }

        public List<Medication> Products
        {
            get
            {
                return products;
            }

            set
            {
                products = value;
            }
        }

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

        public MedicationPriceList()
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
            Guid medicationPriceListId = Guid.Empty;

            try
            {
                // Create a price list
                PriceLevel newPriceList = new PriceLevel
                {
                    Name = "Sample Price List " + GenerateRandomNumber()
                };

                medicationPriceListId = _serviceProxy.Create(newPriceList);

                foreach (Medication product in Products)
                {
                    ProductPriceLevel newPriceListItem = new ProductPriceLevel
                    {
                        PriceLevelId = new EntityReference(PriceLevel.EntityLogicalName, medicationPriceListId),
                        ProductId = new EntityReference(Product.EntityLogicalName, Guid.Parse(product.MedicationId)),
                        UoMId = new EntityReference(UoM.EntityLogicalName, Guid.Parse(UomId)),
                        Amount = new Money(int.Parse(GenerateRandomNumber(2)))
                    };

                    Guid priceListItemId = _serviceProxy.Create(newPriceListItem);

                    if (priceListItemId == null)
                    {
                        break;
                    }
                }

                if (medicationPriceListId != Guid.Empty)
                {
                    PriceListId = medicationPriceListId.ToString();
                    Console.WriteLine("Created Price List [" + PriceListId + "]");
                }
                else
                {
                    throw new Exception("PriceListId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return medicationPriceListId;
        }
    }
}
