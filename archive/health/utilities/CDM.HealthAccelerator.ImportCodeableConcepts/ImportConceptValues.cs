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

using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Samples;
using System.ServiceModel;
using System.Configuration;

namespace CDM.HealthAccelerator.ImportCodeableConcepts
{
    class ImportConceptValues
    {

        #region Class Members

        private static OrganizationServiceProxy _serviceProxy;
        private static IOrganizationService _service;

        private static int failures = 0;
        private static int successes = 0;
        private static int skipped = 0;

        private const int BATCH_SIZE = 1000;

        private static Dictionary<string, int> types = null;
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
                Console.WriteLine("Importing codeable concept values");

                // First thing we need to do is load our mapping file
                // so we can insert the codeable concepts in by mapping
                // the string value we have in our codeable concept list
                // to the proper Type (value) we need from our OptionList

                #region Load OptionSet Value to Text Mappings
                // this is the file used by the our Import Utiliity 
                // feel free to change this
                string picklistmappingfile = ConfigurationManager.AppSettings["cdm:conceptpicklistvalues"];

                types = File.ReadAllLines(picklistmappingfile).ToList().ToDictionary(x => x.Split(',')[0].Trim(), x => int.Parse((x.Split(',')[1].Trim())));

                if ((types == null) || (types.Count == 0))
                {
                    Console.WriteLine("Could not find any optionset values, which are required for mappings");
                    System.Threading.Thread.Sleep(1000);
                    return;
                }

                #endregion

                #region Load Codeable Concepts
                // now we need to load up all the codeable concepts
                // remember that the all the values have to be parsed
                // so we can load them all into a list

                string codeableconceptsfile = ConfigurationManager.AppSettings["cdm:concepinstancetvalues"];

                List<string> codeableconcepts = File.ReadAllLines(codeableconceptsfile).ToList();

                if ((codeableconcepts == null) || (codeableconcepts.Count == 0))
                {
                    Console.WriteLine("Could not find any codeable concepts values, which are required for mappings");
                    System.Threading.Thread.Sleep(1000);
                    return;
                }

                #endregion

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

                    // we have a batch of things to do
                    do
                    {
                        int batchcount = 1;
                        int codecount = 1;

                        List<string> batch = new List<string>();

                        foreach (string concept in codeableconcepts)
                        {
                            batch.Add(concept);
                            codecount++;
                            batchcount++;

                            if ((batchcount == BATCH_SIZE) || (codecount == codeableconcepts.Count()))
                            {
                                BatchImport(batch);

                                Console.WriteLine("Batch Size == [" + batch.Count() + "] Total Entities == [" + codecount.ToString() + "]");

                                batch.Clear();
                                batchcount = 1; // reset
                            }
                        }

                        break;

                    } while (true);

                    Console.WriteLine("Total created[" + successes.ToString() + "] failed[" + failures.ToString() + "] skipped[" + skipped.ToString() + "]");

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

                ImportConceptValues app = new ImportConceptValues();
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

        private static void BatchImport(List<string> codeableconcepts)
        {
            try
            {
                ExecuteMultipleRequest requestWithResults = null;

                // Create an ExecuteMultipleRequest object.
                requestWithResults = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };

                // Create several (local, in memory) entities in a collection. 
                EntityCollection input = new EntityCollection();

                input.EntityName = "msemr_codeableconcept";
                
                foreach (string concept in codeableconcepts)
                {
                    Entity codeableconcept = new Entity("msemr_codeableconcept");

                    codeableconcept["msemr_code"] = (concept.Split('\t')[0]).Trim();

                    // we need to get the string value
                    // then get the numeric value from our mapping
                    int typeValue;

                    bool typeExists = types.TryGetValue(((concept.Split('\t')[2]).Trim()), out typeValue);

                    if (typeExists)
                    {
                        // set our type value (it's the OptionSet)
                        OptionSetValue optionSetType = new OptionSetValue(typeValue);

                        string version = ConfigurationManager.AppSettings["cdm:solutionversion"];

                        codeableconcept["msemr_type"] = optionSetType;
                        codeableconcept["msemr_text"] = (concept.Split('\t')[1]).Trim(); // set same as name

                        string tempname = string.Empty;            
                        //I changed the below to create the name so that it would show up properly in the subgrid of active patients
                        switch (version)
                        {
                            case "1.8.1": // temporary fix while we fix the BUG for length to 500
                                {
                                    tempname = (concept.Split('\t')[1]).Trim().Length > 100 ? 
                                                ((concept.Split('\t')[1]).Trim()).Substring(0, 100) : 
                                                (concept.Split('\t')[1]).Trim();
                                    codeableconcept["msemr_name"] = tempname;  //((concept.Split('\t')[0]).Trim() + "-" + (concept.Split('\t')[2]).Trim());
                                }
                                break;
                                default: 
                                {
                                    tempname = (concept.Split('\t')[1]).Trim().Length > 500 ? 
                                                    ((concept.Split('\t')[1]).Trim()).Substring(0, 500) : 
                                                    (concept.Split('\t')[1]).Trim();
                                    codeableconcept["msemr_name"] = tempname;  //((concept.Split('\t')[0]).Trim() + "-" + (concept.Split('\t')[2]).Trim());
                                }
                                break;
                        }

                        input.Entities.Add(codeableconcept);
                    }
                    else
                    {
                        // which type did not exist. the issue is that the
                        // codeable concept csv, had associated optionset values set
                        // for the "type" of codeable concept it was
                        // but that particular type did not exist and therefor
                        // no entity was added to be bulk created
                        Console.WriteLine("Skipped: Type could not be found skipping creation [" + (concept.Split('\t')[2]).Trim() + "]");
                        skipped++;
                    }
                }

                // Add a CreateRequest for each entity to the request collection.
                foreach (var entity in input.Entities)
                {
                    CreateRequest createRequest = new CreateRequest { Target = entity };
                    requestWithResults.Requests.Add(createRequest);
                }

                Console.WriteLine("Entities Processed == [" + input.Entities.Count().ToString() + "]");

                // Execute all the requests in the request collection using a single web method call.
                ExecuteMultipleResponse responseWithResults =
                    (ExecuteMultipleResponse)_serviceProxy.Execute(requestWithResults);

                // Display the results returned in the responses.
                foreach (var responseItem in responseWithResults.Responses)
                {
                    // A valid response.
                    if (responseItem.Response != null)
                        DisplayResponse(requestWithResults.Requests[responseItem.RequestIndex], responseItem.Response);

                    // An error has occurred.
                    else if (responseItem.Fault != null)
                        DisplayFault(requestWithResults.Requests[responseItem.RequestIndex],
                            responseItem.RequestIndex, responseItem.Fault);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Display the response of an organization message request.
        /// </summary>
        /// <param name="organizationRequest">The organization message request.</param>
        /// <param name="organizationResponse">The organization message response.</param>
        private static void DisplayResponse(OrganizationRequest organizationRequest, OrganizationResponse organizationResponse)
        {
            Console.WriteLine("Created " + ((Entity)organizationRequest.Parameters["Target"]).LogicalName
                + " with codeable concept id as " + organizationResponse.Results["id"].ToString());
            successes++;
        }

        /// <summary>
        /// Display the fault that resulted from processing an organization message request.
        /// </summary>
        /// <param name="organizationRequest">The organization message request.</param>
        /// <param name="count">nth request number from ExecuteMultiple request</param>
        /// <param name="organizationServiceFault">A WCF fault.</param>
        private static void DisplayFault(OrganizationRequest organizationRequest, int count,
            OrganizationServiceFault organizationServiceFault)
        {
            Console.WriteLine("A fault occurred when processing {1} request, at index {0} in the request collection with a fault message: {2}", count + 1,
                organizationRequest.RequestName,
                organizationServiceFault.Message);
            failures++;
        }
    }


}
