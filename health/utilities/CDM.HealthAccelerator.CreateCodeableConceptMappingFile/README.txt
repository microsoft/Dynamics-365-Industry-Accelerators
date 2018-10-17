Author: Michael Gernaey
Details: Utilities that can be leveraged to load data or export data depending on the utility tool we are providing

when you have an instance of the health care acelerator you need to have a few things. 
1) the codeable concepts picklist
2) the values of the codeableconcept instances in the table. Each value will be assigned a specific picklist value. For instance you can have a
picklist value of "Language", however the instance (row) of the codeable concept entity will be en-US. 

when you first create an instance, it has the codeable concept picklist populated, but NOT the instance values.
you are provided a file that has the actual instance values. However when you go to insert them, since there is a 
picklist (optionset) you need to have the proper mapping of string (Language) to optionset value (950001) so that
your instances actually make sense

this program, will create a list of String to Value mappings from the optionset in the codeable concept table
then you create either leverage the file of values to be imported via the ImportCodeableConcepts program
or you could create your own values file, based on the mapping this created.

Note: that you are provided both files to start in a fresh instance, but this enables you to customize as you see fit

Copyright 
This document is provided "as-is". Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. 

Some examples depicted herein are provided for illustration only and are fictitious. No real association or connection is intended or should be inferred. 

This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft, Active Directory, ActiveX, BizTalk, Excel, Great Plains, Internet Explorer, JScript, Microsoft Dynamics, MSN, Outlook, PivotTable, PivotChart, Visual Basic, Visual Studio, Windows, Windows Live, Windows Server, and Windows Vista are trademarks of the Microsoft group of companies. 

All other trademarks are property of their respective owners.
