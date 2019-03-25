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

namespace CDM.HealthAccelerator.GenerateEnums
{
    class CreateCodeableConceptEnums
    {
        #region Class Members

        private static List<PickListEnums> enums = new List<PickListEnums>();
        private static string newNumerations = string.Empty;

        private Dictionary<string, CodeableConceptEnum> conceptenums = new Dictionary<string, CodeableConceptEnum>();

        #endregion

        #region Create Enums from Picklists (Optionsets) and from CodeableConcepts 
        /// <summary>
        /// Demonstrates sharing records by exercising various access messages including:
        /// Grant, Modify, Revoke, RetrievePrincipalAccess, and 
        /// RetrievePrincipalsAndAccess.
        /// </summary>
        /// <param name="serverConfig">Contains server connection information.</param>
        /// <param name="promptforDelete">When True, the user will be prompted to delete all
        /// created entities.</param>
        public void Run()
        {
            try
            {
                Console.WriteLine("Looking for codeable concepts to export values for");

                #region Load Codeable Concepts
                // now we need to load up all the codeable concepts
                // remember that the all the values have to be parsed
                // so we can load them all into a list

                string codeableconceptsfile = ConfigurationManager.AppSettings["cdm:concepinstancetvalues"];

                List<string> codeableconcepts = File.ReadAllLines(codeableconceptsfile).ToList();

                if ((codeableconcepts != null) || (codeableconcepts.Count > 0))
                {
                    foreach (string concept in codeableconcepts)
                    {
                        if (!string.IsNullOrEmpty(concept.Trim()))
                        {
                            try
                            {
                                string code = (concept.Split('\t')[0]).Trim();
                                string codeconcept = (concept.Split('\t')[2]).Trim();

                                CodeableConceptEnum outEnum;

                                bool exists = conceptenums.TryGetValue(codeconcept, out outEnum);

                                if (!exists)
                                {
                                    outEnum = new CodeableConceptEnum();
                                    outEnum.EnumName = codeconcept;
                                    conceptenums.Add(codeconcept, outEnum);
                                }

                                outEnum.Values.Add(code);
                            }
                            catch (Exception tt)
                            {
                                System.Diagnostics.Debug.WriteLine(tt.ToString());
                            }
                        }
                    }

                    foreach (KeyValuePair<string,CodeableConceptEnum> codeEnum in conceptenums)
                    {
                        newNumerations += codeEnum.Value.GenerateEnum();
                    }

                    string enumfile = ConfigurationManager.AppSettings["cdm:codeableenumsourceoutputfilename"];

                    File.Delete(enumfile); // Remove this line if you don't want to erase the current version
                    File.WriteAllText(enumfile, newNumerations);

                }

                #endregion
            }

            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        #endregion Create Enums     

        static void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
             
                CreateCodeableConceptEnums app = new CreateCodeableConceptEnums();
                app.Run();
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
    }
}
