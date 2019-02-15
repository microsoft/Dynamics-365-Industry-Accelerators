
# Microsoft Dynamics 365 Health Accelerator

This repository represents the artifacts related to the Health Accelerator. The Health Accelerator represents a FHIR HL7 standard CDS data model, sample apps, forms, views and other Customer Engagement collateral. Here you will be able to download and leverage the following:

# Documentation

The documentation covers user guides, how to's, sample data descriptions, entity mapping data, attribute descriptions etc.

# FHIR / HL7

To review the FHIR / HL7 V3.01 spec we are utilizing look here https://www.hl7.org/fhir/
Please see the resource list here https://www.hl7.org/fhir/resourcelist.html

# Samplecode

The sample code represents SDK style examples on how to insert data into the Health Accelerator model for CDS.

# Utilities

There are 2 primary utilities right now
1. ImportCodeableConcepts : there is a data\codeableconcepts.csv file that contains all the codeable concepts that we are including. This project will take this file, and the file generated from (2) below and import all the values into your codeable concepts entity instances

2. CreateCodeableConceptMappingFile : this solution will allow you to connect to your CE / CDS instance and create a mapping file that downloads the OptionSet values from the codeable concept entity. You will need these values for when you want to import or create new codeable concepts, to map the strings to the actual OptionSet values.

# Requirements

You will need a full CDS / Customer Engagement v9.0 or greater Dynamics 365 instance. For some examples you will also need a Power BI / Power BI Pro LICENSE and possibly a Microsoft Azure subscription for some of the AI and AAD examples.

# Contributing

If you want to contribute reach out to us on LinkedIn or sign up here https://aka.ms/cdmengage 

# Legal Notice

Microsoft and any contributors grant you a license to the Microsoft documentation and other content in this repository under the Creative Commons Attribution 4.0 International Public License, see the LICENSE file, and grant you a license to any code in the repository under the MIT License, see the LICENSE-CODE file.

Microsoft, Windows, Microsoft Azure and/or other Microsoft products and services referenced in the documentation may be either trademarks or registered trademarks of Microsoft in the United States and/or other countries. The licenses for this project do not grant you rights to use any Microsoft names, logos, or trademarks. Microsoft's general trademark guidelines can be found at http://go.microsoft.com/fwlink/?LinkID=254653.

Privacy information can be found at https://privacy.microsoft.com/en-us/

Microsoft and any contributors reserve all others rights, whether under their respective copyrights, patents, or trademarks, whether by implication, estoppel or otherwise.
