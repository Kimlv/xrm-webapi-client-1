/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi.Exceptions
{
    /// <summary>
    /// Exception thrown when an expected attribute is missing.
    /// </summary>
    public class MissingAttributeException : Exception
    {
        /// <summary>
        /// Initializes a new <see cref="MissingAttributeException"/>.
        /// </summary>
        /// <param name="type">The name of the type missing the attribute</param>
        /// <param name="attribute">The name of the missing attribute</param>
        public MissingAttributeException(string type, string attribute) :
            base($"{type} is missing expected attribute {attribute}")
        {
        }
    }
}
