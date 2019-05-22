Author: Michael Gernaey
Details: Utilities that can be leveraged to load data or export data depending on the utility tool we are providing

When you look at the source code you will see that as I create records, I use enums to associate the optionset values. This
enables me to select them via a randomization call. However to do this, I need to actually have the enums which don't exist by default.
So this took will go through the health accelerator (or any project) and generate an enum c# file for all optionsets (picklists)

then you can add it into your own solution if you want

NOTE: that there are some hardcoded value replacements to make the healthcare one work. If you use this code for yourself, feel free
to modify as you see fit

Copyright 
This document is provided "as-is". Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. 

Some examples depicted herein are provided for illustration only and are fictitious. No real association or connection is intended or should be inferred. 

This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft, Active Directory, ActiveX, BizTalk, Excel, Great Plains, Internet Explorer, JScript, Microsoft Dynamics, MSN, Outlook, PivotTable, PivotChart, Visual Basic, Visual Studio, Windows, Windows Live, Windows Server, and Windows Vista are trademarks of the Microsoft group of companies. 

All other trademarks are property of their respective owners.
