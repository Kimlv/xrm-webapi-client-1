/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi
{
    /// <summary>
    /// Attribute making an entity class retrievable by the <see cref="XrmWebApiClient"/>.
    /// </summary>
    public class EntityLogicalCollectionNameAttribute : Attribute
    {
        /// <summary>
        /// The logical collection name of the entity represented by the class.
        /// </summary>
        public string EntityLogicalCollectionName { get; }

        /// <summary>
        /// Initializes an <see cref="EntityLogicalCollectionNameAttribute"/>.
        /// </summary>
        /// <param name="entityLogicalCollectionName">
        /// The logical collection name of the entity represented by the class
        /// </param>
        public EntityLogicalCollectionNameAttribute(string entityLogicalCollectionName)
        {
            EntityLogicalCollectionName = entityLogicalCollectionName;
        }
    }
}
