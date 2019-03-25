Author: Michael Gernaey
Details: Utilities that can be leveraged to load data or export data depending on the utility tool we are providing

This file will take the Picklist (optionset) value file you created with ExportCodeableConcepts and also take the instance values
mapping file. It will take them and union them, so that it can fine the actual "value" of the codeableconcept picklist, versus
the string value that is in the file

It will then create all the actual instance records.

Note: that there are a few limitations and a few records may fail due to being duplicates, or because their name is > 500 chars
We will fix this in the files, but if you create your own thats the limitation on name 500.

Copyright 
This document is provided "as-is". Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. 

Some examples depicted herein are provided for illustration only and are fictitious. No real association or connection is intended or should be inferred. 

This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft, Active Directory, ActiveX, BizTalk, Excel, Great Plains, Internet Explorer, JScript, Microsoft Dynamics, MSN, Outlook, PivotTable, PivotChart, Visual Basic, Visual Studio, Windows, Windows Live, Windows Server, and Windows Vista are trademarks of the Microsoft group of companies. 

All other trademarks are property of their respective owners.
