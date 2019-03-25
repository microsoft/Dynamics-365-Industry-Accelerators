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
using System.Configuration;

using CDM.HealthAccelerator.DataModel;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using System.Net;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Samples;

namespace CDM.HealthAccelerator.GenerateSampleData
{
    class GenerateData
    {
        #region Class Members

        private static OrganizationServiceProxy _serviceProxy;

        #endregion

        #region Create Sample Data
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
                // we need this to support communicating with Dynamics Online Instances 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //<snippetSharingRecords1>
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    int accountcount = int.Parse(ConfigurationManager.AppSettings["cdm:createaccountcount"]);
                    int locationcount = int.Parse(ConfigurationManager.AppSettings["cdm:createlocationcount"]);

                    int productcount = int.Parse(ConfigurationManager.AppSettings["cdm:createproductcount"]);

                    int patientcount = int.Parse(ConfigurationManager.AppSettings["cdm:createpatientcount"]);
                    int practitionercount = int.Parse(ConfigurationManager.AppSettings["cdm:createpractitionercount"]);
                    int relatedpersoncount = int.Parse(ConfigurationManager.AppSettings["cdm:createrelatedpersoncount"]);
                    int contactcount = int.Parse(ConfigurationManager.AppSettings["cdm:createcontactcount"]);

                    int practitionerrolecount = int.Parse(ConfigurationManager.AppSettings["cdm:practitionerrolecount"]);
                    int practitionerqualificationcount = int.Parse(ConfigurationManager.AppSettings["cdm:practitionerqualificationcount"]);

                    int patientallergycount = int.Parse(ConfigurationManager.AppSettings["cdm:patientallergycount"]);
                    int patientnutritionordercount = int.Parse(ConfigurationManager.AppSettings["cdm:patientnutritionordercount"]);
                    int patientconditioncount = int.Parse(ConfigurationManager.AppSettings["cdm:patientconditioncount"]);
                    int patientdevicecount = int.Parse(ConfigurationManager.AppSettings["cdm:patientdevicecount"]);
                    int patientprocedurecount = int.Parse(ConfigurationManager.AppSettings["cdm:patientprocedurecount"]);
                    int patientreferralcount = int.Parse(ConfigurationManager.AppSettings["cdm:patientreferralcount"]);
                    int patientmedicationrequestcount = int.Parse(ConfigurationManager.AppSettings["cdm:patientmedicationrequestcount"]);
                    int patientepisodesofcarecount = int.Parse(ConfigurationManager.AppSettings["cdm:patientepisodesofacarecount"]);
                    int patientappointmentcount = int.Parse(ConfigurationManager.AppSettings["cdm:patientappointmentcount"]);
                    int patientencountercount = int.Parse(ConfigurationManager.AppSettings["cdm:patientencountercount"]);
                    int patientcareplancount = int.Parse(ConfigurationManager.AppSettings["cdm:patientcareplancount"]);

                    string filepath = ConfigurationManager.AppSettings["cdm:temporaryfilepath"];

                    Console.WriteLine("Start Time: " + DateTime.Now.ToString());

                    List<Profile> localcontacts = null;
                    List<Profile> localpatients = null;
                    List<Profile> localpractitioners = null;
                    List<Profile> localrelatedpersons = null;
                    List<Profile> localaccounts = null;
                    List<Profile> locallocations = null;
                    List<Medication> localmedications = null;

                    string practitonerFile = string.Empty;
                    string relatedpersonsFile = string.Empty;
                    string patientsFile = string.Empty;
                    string accountsFile = string.Empty;
                    string locationsFile = string.Empty;
                    string medicationsFile = string.Empty;


                    #region Create Products, Pricelists
                    if (productcount > 0)
                    {
                        CreateCDMHealthData createProducts = new CreateCDMHealthData();
                        createProducts.ProfileType = Profile.ProfileType.Medication;
                        createProducts.FileName = filepath + "medications_" + accountcount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";

                        // we only ever generate a single instance, unless you want more than 1 product on seperate price lists
                        // this will generate N products on the same price list through one object instance (unlike others)
                        localmedications = Medication.GenerateProfilesByCount(productcount, productcount);

                        createProducts.CreateCount = createProducts.IncomingProfiles.Count;
                        createProducts.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]);
                        Console.WriteLine("\r\nCreating [" + createProducts.CreateCount.ToString() + "]  Medications [products]\r\n");

                        createProducts.CreateProducts(_serviceProxy, localmedications);
                    }

                    #endregion

                    #region Create Accounts (Organizations)

                    if (accountcount > 0)
                    {
                        CreateCDMHealthData createAccounts = new CreateCDMHealthData();
                        createAccounts.ProfileType = Profile.ProfileType.Organization;
                        createAccounts.FileName = filepath + "organizations_" + accountcount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";

                        localaccounts = Organization.GenerateProfilesByCount(accountcount, "NA");

                        foreach (Organization account in localaccounts)
                        {
                            createAccounts.IncomingProfiles.Enqueue(account);
                        }

                        createAccounts.CreateCount = createAccounts.IncomingProfiles.Count;
                        createAccounts.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]);
                        Console.WriteLine("\r\nCreating [" + createAccounts.CreateCount.ToString() + "]  Organizations\r\n");

                        accountsFile = createAccounts.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    #region Create Locations

                    if (locationcount > 0)
                    {
                        CreateCDMHealthData createLocations = new CreateCDMHealthData();
                        createLocations.ProfileType = Profile.ProfileType.Location;
                        createLocations.FileName = filepath + "locations_" + locationcount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";

                        locallocations = Location.GenerateProfilesByCount(locationcount, accountsFile);

                        foreach (Location location in locallocations)
                        {
                            createLocations.IncomingProfiles.Enqueue(location);
                        }

                        createLocations.CreateCount = createLocations.IncomingProfiles.Count;
                        createLocations.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]);

                        Console.WriteLine("\r\nCreating [" + createLocations.CreateCount.ToString() + "]  Locations\r\n");

                        locationsFile = createLocations.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    #region Create Standard Contacts 

                    if (contactcount > 0)
                    {
                        CreateCDMHealthData createContacts = new CreateCDMHealthData();
                        createContacts.ProfileType = Profile.ProfileType.Standard;
                        createContacts.FileName = filepath + "relatedpersons_" + relatedpersoncount.ToString() + "_" + Guid.NewGuid().ToString() + ".tab";

                        localcontacts = Contact.GenerateProfilesByCount(contactcount, "NA");

                        foreach (Profile contact in localcontacts)
                        {
                            createContacts.IncomingProfiles.Enqueue(contact);
                        }

                        createContacts.CreateCount = createContacts.IncomingProfiles.Count;
                        createContacts.EmailDomain = ConfigurationManager.AppSettings["cdm:emaildomain"];
                        createContacts.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]); 

                        Console.WriteLine("\r\nCreating [" + createContacts.CreateCount.ToString() + "]  Contacts\r\n");

                        practitonerFile = createContacts.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    #region Create Practitioners 

                    if (practitionercount > 0)
                    {
                        PractitionerConfiguration configuration = new PractitionerConfiguration();
                        configuration.Qualifications = practitionerqualificationcount;
                        configuration.Roles = practitionerrolecount;

                        CreateCDMHealthData createPractitioners = new CreateCDMHealthData();
                        createPractitioners.ProfileType = Profile.ProfileType.Practitioner;
                        createPractitioners.FileName = filepath + "practitioners_" + practitionercount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";

                        localpractitioners = Practitioner.GenerateProfilesByCount(practitionercount, configuration);

                        foreach (Profile practitioner in localpractitioners)
                        {
                            createPractitioners.IncomingProfiles.Enqueue(practitioner);
                        }

                        createPractitioners.CreateCount = createPractitioners.IncomingProfiles.Count;
                        createPractitioners.EmailDomain = ConfigurationManager.AppSettings["cdm:emaildomain"];
                        createPractitioners.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]); //;

                        Console.WriteLine("\r\nCreating [" + createPractitioners.CreateCount.ToString() + "]  Practitioners\r\n");

                        practitonerFile = createPractitioners.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    #region Create Related Persons
                    if (relatedpersoncount > 0)
                    {
                        CreateCDMHealthData createRelatedPersons = new CreateCDMHealthData();
                        createRelatedPersons.FileName = filepath + "relatedpersons_" + relatedpersoncount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";
                        createRelatedPersons.ProfileType = Profile.ProfileType.RelatedPerson;

                        localrelatedpersons = RelatedPerson.GenerateProfilesByCount(relatedpersoncount, "NA");

                        foreach (Profile relatedperson in localrelatedpersons)
                        {
                            createRelatedPersons.IncomingProfiles.Enqueue(relatedperson);
                        }

                        createRelatedPersons.CreateCount = createRelatedPersons.IncomingProfiles.Count;
                        createRelatedPersons.EmailDomain = ConfigurationManager.AppSettings["cdm:emaildomain"];
                        createRelatedPersons.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]); //;

                        Console.WriteLine("\r\nCreating [" + createRelatedPersons.CreateCount.ToString() + "]  Related Persons\r\n");

                        relatedpersonsFile = createRelatedPersons.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    #region Create Patients

                    if (patientcount > 0)
                    {
                        PatientConfiguration configuration = new PatientConfiguration();
                        configuration.PractionerFileName = practitonerFile;
                        configuration.RelatedPersonsFileName = relatedpersonsFile;
                        configuration.AccountsFileName = accountsFile;
                        configuration.LocationsFileName = locationsFile;

                        configuration.AllergyIntoleranceCount = patientallergycount;
                        configuration.NutritionOrderCount = patientnutritionordercount;
                        configuration.ConditionCount = patientconditioncount;
                        configuration.DeviceCount = patientdevicecount;
                        configuration.ProcedureCount = patientprocedurecount;
                        configuration.MedicationCount = patientmedicationrequestcount;
                        configuration.EpisodesOfCareCount = patientepisodesofcarecount;
                        configuration.EncountersCount = patientencountercount;
                        configuration.AppointmentCount = patientappointmentcount;
                        configuration.CarePlanCount = patientcareplancount;

                        CreateCDMHealthData createPatients = new CreateCDMHealthData();
                        createPatients.FileName = filepath + "patients_" + patientcount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";
                        createPatients.ProfileType = Profile.ProfileType.Patient;

                        localpatients = Patient.GenerateProfilesByCount(patientcount, configuration);

                        foreach (Profile patient in localpatients)
                        {
                            ((Patient)(patient)).Products = localmedications;
                            createPatients.IncomingProfiles.Enqueue(patient);
                        }

                        createPatients.CreateCount = createPatients.IncomingProfiles.Count;
                        createPatients.EmailDomain = ConfigurationManager.AppSettings["cdm:emaildomain"];
                        createPatients.Clients = int.Parse(ConfigurationManager.AppSettings["cdm:clients"]); //;

                        Console.WriteLine("\r\nCreating [" + createPatients.CreateCount.ToString() + "]  Patients\r\n");

                        patientsFile = createPatients.CreateProfiles(_serviceProxy);
                    }

                    #endregion

                    Console.WriteLine("End Time: " + DateTime.Now.ToString());

                    return;

                }
            }

            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        #endregion Create Sample Data
            
        static void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                GenerateData app = new GenerateData();
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
    }
}
