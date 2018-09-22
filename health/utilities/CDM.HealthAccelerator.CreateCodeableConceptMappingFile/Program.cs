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

namespace CDM.HealthAccelerator.ParseCodeableConcepts
{
    class Program
    {
        #region Class Members

        private OrganizationServiceProxy _serviceProxy;
        private IOrganizationService _service;
        
        #endregion


        static void Main(string[] args)
        {
            // variables used to collect the codeable concepts picklist values.
            // we need these before we can actually import the codeable concepts themselves

            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            OrganizationServiceProxy _serviceProxy;
            Uri OrganizationUri = new Uri("https://healthacceleratordevmgernfull.crm.dynamics.com/XRMServices/2011/Organization.svc");
            Uri HomeRealmUri = null; 
            ClientCredentials Credentials = null;
            ClientCredentials DeviceCredentials = null; 

            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            Credentials = new ClientCredentials();
            Credentials.UserName.UserName = "apiaccount@msdynaccelerators.onmicrosoft.com";
            Credentials.UserName.Password = "Sadie#2013";


            // this is the file used by the our Import Utiliity 
            // feel free to change this
            string picklistmappingfilename = @"\ccpicklistvaluesource.csv"; 

            string picklistmappingfilepath = AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ?
                                    AppDomain.CurrentDomain.BaseDirectory + "data" :
                                    AppDomain.CurrentDomain.BaseDirectory + @"\data";

            string picklistmappingfile = picklistmappingfilepath + picklistmappingfilename;

            try
            {
                // we need this to support communicating with Dynamics Online Instances 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (_serviceProxy = new OrganizationServiceProxy(OrganizationUri, HomeRealmUri, Credentials, DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

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

                        // add to our stirng list of options
                        picklistmappingdata.Add(option.Text.Trim() + "," + option.Value.ToString().Trim()); //default to first string of the label

                        // write out our option count
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write("Total Options Found [" + totalOptions.ToString() + "]");

                        System.Threading.Thread.Sleep(100);
                    }

                    // if we don't have any don't write anything
                    if (picklistmappingdata.Count > 0)
                    {
                        File.Delete(picklistmappingfile); // Remove this line if you don't want to erase the current version
                        File.WriteAllLines(picklistmappingfile, picklistmappingdata);
                    }
                    else
                    {
                        Console.WriteLine("Could not find any Codeable Concept Types");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
