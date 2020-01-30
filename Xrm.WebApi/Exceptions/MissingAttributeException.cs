/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi.Exceptions
{
    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string type, string attribute) :
            base($"{type} is missing expected attribute {attribute}")
        {
        }
    }
}
