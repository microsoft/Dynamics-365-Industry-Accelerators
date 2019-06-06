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
namespace CDM.HealthAccelerator.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading.Tasks;
    
    [Serializable]
    /// <summary>
    /// Used to define what an entity looks like and a meta-data model
    /// </summary>
    public abstract class EntityDefinition
    {
        /// <summary>
        /// the id that gets stored as part of the key
        /// </summary>
        public abstract string EntityId { get; set; }

        /// <summary>
        /// the name of the hbase table
        /// </summary>
        public abstract string TableName { get; set; }

        /// <summary>
        /// The Tenant (organization) that this belongs too
        /// </summary>
        public abstract string OrganizationId { get; set; }

        /// <summary>
        /// The list of fields applied to the field columnfamily
        /// </summary>
        public abstract Dictionary<string, Field> Fields { get; set; }

        
    }
}
