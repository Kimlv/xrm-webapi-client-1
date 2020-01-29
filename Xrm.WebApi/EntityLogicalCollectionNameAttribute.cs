/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi
{
    public class EntityLogicalCollectionNameAttribute : Attribute
    {
        public string EntityLogicalCollectionName { get; }

        public EntityLogicalCollectionNameAttribute(string entityLogicalCollectionName)
        {
            EntityLogicalCollectionName = entityLogicalCollectionName;
        }
    }
}
