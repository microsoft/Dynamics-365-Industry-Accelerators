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
    using System.Text;
    using System.Threading.Tasks;

    
    [Serializable]
    /// <summary>
    /// Class the is the actual entity definition instance
    /// </summary>
    public class Entity : EntityDefinition
    {
        /// <summary>
        /// the entityID
        /// </summary>
        private string entityId;

        private string tableName;

        private string organizationId;

        private Dictionary<string, Field> fields;

        /// <summary>
        /// the entity table
        /// </summary>
        public override string TableName
        {
            get
            {
                return this.tableName;
            }

            set
            {
                this.tableName = value;
            }
        }

        /// <summary>
        /// the organization it belongs too
        /// </summary>
        public override string OrganizationId
        {
            get
            {
                return this.organizationId;
            }

            set
            {
                this.organizationId = value;
            }
        }

        /// <summary>
        /// the fields it has
        /// </summary>
        public override Dictionary<string, Field> Fields
        {
            get
            {
                return this.fields;
            }

            set
            {
                this.fields = value;
            }
        }



        /// <summary>
        /// the entityID
        /// </summary>
        public override string EntityId
        {
            get
            {
                return this.entityId;
            }

            set
            {
                this.entityId = value;
            }
        }
    }
}
