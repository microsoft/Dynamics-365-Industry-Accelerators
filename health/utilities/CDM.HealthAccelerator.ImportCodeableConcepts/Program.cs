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

namespace CDM.HealthAccelerator.ImportCodeableConcepts
{
    class Program
    {
        private static int failures = 0;
        private static int successes = 0;
        private static int skipped = 0;

        private const int BATCH_SIZE = 1000;

        private static OrganizationServiceProxy _serviceProxy;
        private static Dictionary<string, int> types = null;

        static void Main(string[] args)
        {
            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            Uri OrganizationUri = new Uri("https://healthacceleratormsteamsdev.crm.dynamics.com/XRMServices/2011/Organization.svc");
            Uri HomeRealmUri = null;

            ClientCredentials Credentials = null;
            ClientCredentials DeviceCredentials = null;

            failures = 0;
            successes = 0;
            skipped = 0;

            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            Credentials = new ClientCredentials();
            Credentials.UserName.UserName = "michael@msdynaccelerators.onmicrosoft.com";
            Credentials.UserName.Password = "Kendra#2018";

            try
            {
                // First thing we need to do is load our mapping file
                // so we can insert the codeable concepts in by mapping
                // the string value we have in our codeable concept list
                // to the proper Type (value) we need from our OptionList

                #region Load OptionSet Value to Text Mappings
                // this is the file used by the our Import Utiliity 
                // feel free to change this
                string picklistmappingfilename = @"\optionsetsourcevalues.csv";

                string picklistmappingfilepath = AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ?
                                        AppDomain.CurrentDomain.BaseDirectory + "data" :
                                        AppDomain.CurrentDomain.BaseDirectory + @"\data";

                string picklistmappingfile = picklistmappingfilepath + picklistmappingfilename;

                types = File.ReadAllLines(picklistmappingfile).ToList().ToDictionary(x => x.Split(',')[0].Trim(), x => int.Parse((x.Split(',')[1].Trim())));

                if ((types == null) || (types.Count == 0))
                {
                    Console.WriteLine("Could not find any optionset values, which are required for mappings");
                    System.Threading.Thread.Sleep(2000);
                    return;
                }

                #endregion

                #region Load Codeable Concepts
                // now we need to load up all the codeable concepts
                // remember that the all the values have to be parsed
                // so we can load them all into a list
                string codeableconceptsfilename = @"\CodeableConceptValues.txt";

                string codeableconceptsfilepath = AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ?
                                        AppDomain.CurrentDomain.BaseDirectory + "data" :
                                        AppDomain.CurrentDomain.BaseDirectory + @"\data";

                string codeableconceptsfile = codeableconceptsfilepath + codeableconceptsfilename;

                List<string> codeableconcepts = File.ReadAllLines(codeableconceptsfile).ToList();

                if ((codeableconcepts == null) || (codeableconcepts.Count == 0))
                {
                    Console.WriteLine("Could not find any codeable concepts values, which are required for mappings");
                    System.Threading.Thread.Sleep(2000);
                    return;
                }

                #endregion

                // we need this to support communicating with Dynamics Online Instances 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (_serviceProxy = new OrganizationServiceProxy(OrganizationUri, HomeRealmUri, Credentials, DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

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

                    }while(true);

                    Console.WriteLine("Total created[" + successes.ToString() + "] failed[" + failures.ToString() + "] skipped[" + skipped.ToString()+ "]");
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
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

                        codeableconcept["msemr_type"] = optionSetType;
                        codeableconcept["msemr_text"] = (concept.Split('\t')[1]).Trim(); // set same as name
                        //I changed the below to create the name so that it would show up properly in the subgrid of active patients
                        codeableconcept["msemr_name"] = (concept.Split('\t')[1]).Trim();  //((concept.Split('\t')[0]).Trim() + "-" + (concept.Split('\t')[2]).Trim());

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
