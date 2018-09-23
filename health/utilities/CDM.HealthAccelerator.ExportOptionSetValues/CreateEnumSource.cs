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
using Microsoft.Crm.Sdk.Samples;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.ExportOptionSetValues
{
    class CreateEnumSource
    {
        #region Class Members

        private static OrganizationServiceProxy _serviceProxy;
        private static IOrganizationService _service;

        private static List<string> picklistmappingdata = new List<string>();
        private static int totalOptions = 0;
        private static List<PickListEnums> enums = new List<PickListEnums>();
        private static string newNumerations = string.Empty;

        #endregion

        #region Create Codeable Concept Maps
        /// <summary>
        /// Demonstrates sharing records by exercising various access messages including:
        /// Grant, Modify, Revoke, RetrievePrincipalAccess, and 
        /// RetrievePrincipalsAndAccess.
        /// </summary>
        /// <param name="serverConfig">Contains server connection information.</param>
        /// <param name="promptforDelete">When True, the user will be prompted to delete all
        /// created entities.</param>
        public void Run(ServerConnection.Configuration serverConfig)
        {
            try
            {
                Console.WriteLine("Looking for picklists to export values for");

                string solutionName = ConfigurationManager.AppSettings["cdm:solution"]; 

                // we need this to support communicating with Dynamics Online Instances 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //<snippetSharingRecords1>
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    _service = (IOrganizationService)_serviceProxy;

                    IEnumerable<EntityMetadata> entities = getSolutionEntities(solutionName, _serviceProxy);

                    foreach (EntityMetadata em in entities)
                    {
                        foreach (AttributeMetadata am in em.Attributes)
                        {
                            if (am.AttributeType == AttributeTypeCode.Picklist)
                            {
                                if ((em.LogicalName.ToLower().StartsWith("msemr_")) || (am.LogicalName.ToLower().StartsWith("msemr_")))
                                {
                                    PickListEnums ple = new PickListEnums();
                                    ple = GetOptionPickListValues(em.LogicalName, em.DisplayName.UserLocalizedLabel.Label.ToString(), am.LogicalName, _serviceProxy);

                                    enums.Add(ple);

                                    newNumerations += ple.GenerateEnum();
                                }
                            }
                        }
                    }


                    string picklistoutputfile = ConfigurationManager.AppSettings["cdm:picklistvaluesoutputfilename"];
                    string enumfile = ConfigurationManager.AppSettings["cdm:enumsourceoutputfilename"];

                    // if we don't have any don't write anything
                    if (picklistmappingdata.Count > 0)
                    {
                        File.Delete(picklistoutputfile); // Remove this line if you don't want to erase the current version
                        File.WriteAllLines(picklistoutputfile, picklistmappingdata);
                    }
                    else
                    {
                        Console.WriteLine("Could not find any pick list Types");
                    }

                    File.Delete(enumfile); // Remove this line if you don't want to erase the current version
                    File.WriteAllText(enumfile, newNumerations);

                }
            }

            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        #endregion Create Codeable Concept Maps     

        static void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                CreateEnumSource app = new CreateEnumSource();
                app.Run(config);
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp);
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode);
                Console.WriteLine("Message: {0}", ex.Detail.Message);
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
            }
            catch (System.TimeoutException ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.InnerException.Message ? "No Inner Fault" : ex.InnerException.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine(ex.Message);

                // Display the details of the inner exception.
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe = ex.InnerException
                        as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
                    if (fe != null)
                    {
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp);
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode);
                        Console.WriteLine("Message: {0}", fe.Detail.Message);
                        Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText);
                        Console.WriteLine("Inner Fault: {0}",
                            null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
                    }
                }
            }
            // Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
            // SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

            finally
            {
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }

        }

        public static IEnumerable<EntityMetadata> getSolutionEntities(string SolutionUniqueName, IOrganizationService Service)
        {
            // get solution components for solution unique name
            QueryExpression componentsQuery = new QueryExpression
            {
                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("objectid"),
                Criteria = new FilterExpression(),
            };

            LinkEntity solutionLink = new LinkEntity("solutioncomponent", "solution", "solutionid", "solutionid", JoinOperator.Inner);
            solutionLink.LinkCriteria = new FilterExpression();
            solutionLink.LinkCriteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.Equal, SolutionUniqueName));

            componentsQuery.LinkEntities.Add(solutionLink);
            componentsQuery.Criteria.AddCondition(new ConditionExpression("componenttype", ConditionOperator.Equal, 1));

            EntityCollection ComponentsResult = Service.RetrieveMultiple(componentsQuery);
            
            //Get all entities
            RetrieveAllEntitiesRequest AllEntitiesrequest = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Entity | Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            };
            RetrieveAllEntitiesResponse AllEntitiesresponse = (RetrieveAllEntitiesResponse)Service.Execute(AllEntitiesrequest);
            //Join entities Id and solution Components Id 
            return AllEntitiesresponse.EntityMetadata.Join(ComponentsResult.Entities.Select(x => x.Attributes["objectid"]), x => x.MetadataId, y => y, (x, y) => x);
        }

        public static PickListEnums GetOptionPickListValues(string entityLogicalName, string entityDisplayName, string attributeName, IOrganizationService _serviceProxy)
        {
            // set the properties to get back the codeable concept picklist values
            // from the picklist (optionset)
            RetrieveAttributeRequest retrieveEntityRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entityLogicalName,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };

            // execute the call and retrieve the actual metadata directly
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveEntityRequest);
            var attributeMetadata = (EnumAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;

            // retrieve each option list item 
            var optionList = (from o in attributeMetadata.OptionSet.Options
                              select new { Value = o.Value, Text = o.Label.UserLocalizedLabel.Label }).ToList();

            // iterate through each option and write out the value and text
            // this will enable us to map the "Text" value given to us in the
            // codeable concepts file that we generated OR if you generate some new ones
            // you can run this program again to create the mapping file
            // or at least have the copy that was generated for you

            PickListEnums ple = new PickListEnums();

            ple.AttributeLogicalName = attributeName;
            ple.EntityDisplayName = entityDisplayName;
            ple.EntityLogicalName = entityLogicalName;

            ple.EnumName = GenerateEnumName(entityDisplayName, attributeName);

            foreach (var option in optionList)
            {
                totalOptions++;
                PickListValues plv = new PickListValues();

                // add to our stirng list of options
                picklistmappingdata.Add(entityDisplayName + "," + entityLogicalName + "," + attributeName + "," + option.Text.Trim() + "," + option.Value.ToString().Trim()); //default to first string of the label

                plv.PickListDisplay = option.Text.Trim();
                plv.PickListValue = option.Value.ToString().Trim();

                ple.EnumValues.Add(option.Text.Trim(), plv);

                // write out our option count
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("Total Options Found [" + totalOptions.ToString() + "]");

                System.Threading.Thread.Sleep(1);
            }

            return ple;
        }

        public static string GenerateEnumName(string entityDisplayName, string attributeLogicalName)
        {
            string entityPart = entityDisplayName.Replace(" ", "");
            string attributePart = attributeLogicalName.Replace("msemr_", "");

            attributePart = attributePart.First().ToString().ToUpper() + attributePart.Substring(1);

            return entityPart + "_" + attributePart;
        }
    }
}
