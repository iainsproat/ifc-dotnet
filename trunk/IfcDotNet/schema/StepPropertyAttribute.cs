#region License
/*

Copyright 2010, Iain Sproat
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

 * Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
copyright notice, this list of conditions and the following disclaimer
in the documentation and/or other materials provided with the
distribution.
 * The names of the contributors may not be used to endorse or promote products derived from
this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 */
#endregion
using System;

namespace IfcDotNet.Schema
{
    /// <summary>
    /// StepProperty allows the serialization behaviour of a property to be controlled when serialized by the STEP serializer
    /// </summary>
    public class StepPropertyAttribute : Attribute //TODO only one attribute on property (but attribute can be duplicated on overriding properties)
    {
        int _order = -1;
        bool _overridden = false;
        
        /// <summary>
        /// Order is 0 indexed
        /// </summary>
        public int Order{
            get{ return this._order; }
        }
        
        /// <summary>
        /// Indicates whether a property is overridden
        /// </summary>
        public bool Overridden{
            get{ return this._overridden; }
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public StepPropertyAttribute(): this(-1, false){}
        public StepPropertyAttribute(bool overridden):this(-1, overridden){ }
        public StepPropertyAttribute(int order):this(order, false){ }
        public StepPropertyAttribute(int order, bool overridden){
            if(order < 0) throw new ArgumentException("Order cannot be less than 1");
            this._order = order;
            this._overridden = overridden;
        }
    }
}
