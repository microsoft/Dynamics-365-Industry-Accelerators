Author: Michael Gernaey
Details: Utilities that can be leveraged to load data or export data depending on the utility tool we are providing

When you first install a fresh instance you need to do a couple of things if you want sample data

1.	Install the healthaccelerator into the instance

2.	Sync healthcare branch
	a.	Create a temp folder c:\temp\cds (it can be anything but this is what I configure already in the app.configs)
	b.	Copy %git%\\health\utilities\resourcefiles folder (copy the entire folder) to the c:\temp\cds folder 
	c.	Load the health\utilities\ CDM.HealthAccelerator.Utilities project
	d.	Set CDM.HealthAccelerator.ImportCodeableConcepts project as the startup
	e.	Run it
	f.	Hit enter (don’t enter a server if you are in the US)
	g.	Hit y and enter for HTTPs question
	h.	Login as your msdyn account (your onmicrosoft.com one or whatever you log in as)
	i.	Find the instance in the generated list and type in that number and hit enter
	j.	It will import all the codeable concepts instance values

3.	To generate the sample data       
	a.	Set CDM.HealthAccelerator.GenerateSampleData as the startup project
	b.	Run it (its got all the settings you need configured, long as you used the folder name I said
	c.	You should have an instance saved from Step 2 (select that,… probably #1) or pick a new instance line # and hit Enter
	d.	It will run through and create your sample data

Note: that the settings in the GenerateSampleData in the appconfig dictates how many instances of each entity type it creates. 
Currently there is no specific way to only create some and not others as it expects it to be a clean or at least a full run
of data, even if you already have some

Copyright 
This document is provided "as-is". Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. 

Some examples depicted herein are provided for illustration only and are fictitious. No real association or connection is intended or should be inferred. 

This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft, Active Directory, ActiveX, BizTalk, Excel, Great Plains, Internet Explorer, JScript, Microsoft Dynamics, MSN, Outlook, PivotTable, PivotChart, Visual Basic, Visual Studio, Windows, Windows Live, Windows Server, and Windows Vista are trademarks of the Microsoft group of companies. 

All other trademarks are property of their respective owners.
