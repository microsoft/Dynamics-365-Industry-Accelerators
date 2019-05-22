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
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using System.Linq;

using cdm;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Samples;
using System.ServiceModel;
using System.Configuration;

namespace CDM.HealthAccelerator.ParseCodeableConcepts
{
    class ExportConcepts
    {
        #region Class Members

        private OrganizationServiceProxy _serviceProxy;
        private IOrganizationService _service;

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
                Console.WriteLine("Export codeable concept pick list values");

                string picklistmappingfilename = ConfigurationManager.AppSettings["cdm:conceptpicklistvalues"];

                if (string.IsNullOrEmpty(picklistmappingfilename))
                {
                    Console.WriteLine("Error: could not find the pick list mapping file name value");
                    return;
                }

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


                    // set the properties to get back the codeable concept picklist values
                    // from the picklist (optionset)
                    RetrieveAttributeRequest retrieveEntityRequest = new RetrieveAttributeRequest
                    {
                        EntityLogicalName = "msemr_codeableconcept",
                        LogicalName = "msemr_type",
                        RetrieveAsIfPublished = true
                    };

                    // execute the call and retrieve the actual metadata directly
                    RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveEntityRequest);
                    var attributeMetadata = (EnumAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;

                    // retrieve each option list item 
                    var optionList = (from o in attributeMetadata.OptionSet.Options
                                      select new { Value = o.Value, Text = o.Label.UserLocalizedLabel.Label }).ToList();

                    // the data we will write to the CSV file that get's used by our import program
                    // remember what we are doing here, is making the mapping file between
                    // the codeable concepts actual values, and the OptionSet "attribute" that
                    // needs to be set when you import a codeable concept
                    // so you have can either use the file we produce for you
                    // or create a fresh one based on any new values that are added over time
                    List<string> picklistmappingdata = new List<string>();

                    // iterate through each option and write out the value and text
                    // this will enable us to map the "Text" value given to us in the
                    // codeable concepts file that we generated OR if you generate some new ones
                    // you can run this program again to create the mapping file
                    // or at least have the copy that was generated for you

                    int totalOptions = 0;

                    foreach (var option in optionList)
                    {
                        totalOptions++;

                        // add to our string list of options
                        picklistmappingdata.Add(option.Text.Trim() + "," + option.Value.ToString().Trim()); //default to first string of the label

                        // write out our option count
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write("Total Options Found [" + totalOptions.ToString() + "]");

                        System.Threading.Thread.Sleep(0);
                    }

                    // if we don't have any don't write anything
                    if (picklistmappingdata.Count > 0)
                    {
                        File.Delete(picklistmappingfilename); // Remove this line if you don't want to erase the current version
                        File.WriteAllLines(picklistmappingfilename, picklistmappingdata);

                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("Created codeable concepts mapping file [" + picklistmappingfilename + "]");
                    }
                    else
                    { 
                        Console.WriteLine("Could not find any Codeable Concept Types");
                    }
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

        #region Main method

        /// <summary>
        /// Standard Main() method used by most SDK samples.
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                ExportConcepts app = new ExportConcepts();
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

        #endregion Main method
    }
}
