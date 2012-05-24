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


The majority of the below code originate from the Json.NET project, for which the following additional license applies:

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
 */
 
using System;

namespace StepParser
{
    /// <summary>
    /// StepToken.
    /// </summary>
    public enum StepToken
    {
        /// <summary>
        /// This is returned by the <see cref="StepReader"/> if a <see cref="StepReader.Read"/> method has not been called.
        /// </summary>
        None,
        /// <summary>
        /// Start of the express definition
        /// </summary>
        StartSTEP,
        /// <summary>
        /// A section start token
        /// </summary>
        StartSection,
        /// <summary>
        /// A data line number
        /// </summary>
        LineIdentifier,
        /// <summary>
        /// An entity start token.
        /// </summary>
        StartEntity,
        /// <summary>
        /// An array start token.
        /// </summary>
        StartArray,
        /// <summary>
        /// A comment.
        /// </summary>
        Comment,
        /// <summary>
        /// A reference to a data line
        /// </summary>
        LineReference,
        /// <summary>
        /// An entity name.
        /// </summary>
        EntityName,
        /// <summary>
        /// An interger.
        /// </summary>
        Integer,
        /// <summary>
        /// A float.
        /// </summary>
        Float,
        /// <summary>
        /// A string.
        /// </summary>
        String,
        /// <summary>
        /// A boolean.
        /// </summary>
        Boolean,
        /// <summary>
        /// An enumeration
        /// </summary>
        Enumeration,
        /// <summary>
        /// A Date.
        /// </summary>
        Date,
        /// <summary>
        /// An operator symbol
        /// </summary>
        Operator,
        /// <summary>
        /// A symbol indicating that this parameter is 
        /// overridden by a parameter of the subtype
        /// </summary>
        Overridden,
        /// <summary>
        /// A null token.
        /// </summary>
        Null,
        /// <summary>
        /// An undefined token.
        /// </summary>
        Undefined,
        /// <summary>
        /// An entity end token.
        /// </summary>
        EndEntity,
        /// <summary>
        /// An array end token.
        /// </summary>
        EndArray,
        /// <summary>
        /// End of a line
        /// </summary>
        EndLine,
        /// <summary>
        /// An end section token
        /// </summary>
        EndSection,
        /// <summary>
        /// End of the step physical file
        /// </summary>
        EndSTEP
    }
}
