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
using System.Reflection;

/// <summary>
/// Holds information about the references between entities
/// e.g. #20, when found within an entity declaration, is a reference to the entity in line 20.
/// </summary>
internal struct StepEntityReference{
    int referencingObject;
    PropertyInfo referencingProperty;
    int referencedObject;
    int index;
    bool isIndexed;
    
    /// <summary>
    /// The object which makes reference to another line
    /// </summary>
    public int ReferencingObject{
        get{ return this.referencingObject; }
    }
    
    /// <summary>
    /// The property which is to be filled by the referenced object
    /// </summary>
    public PropertyInfo Property{
        get{ return this.referencingProperty; }
    }
    
    /// <summary>
    /// The external object which is referenced
    /// </summary>
    public int ReferencedObject{
        get{ return this.referencedObject; }
    }
    
    /// <summary>
    /// If the property is an array, then an index is required for each individual value
    /// </summary>
    public int Index{
        get{
            if(!this.isIndexed)
                throw new ApplicationException("Do not call Index if IsIndexed is false");
            return this.index;
        }
    }
    
    public bool IsIndexed{
        get{ return this.isIndexed; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="referencingId"></param>
    /// <param name="prop"></param>
    /// <param name="referencedId"></param>
    public StepEntityReference(int referencingId, PropertyInfo prop, int referencedId){
        this.referencingObject = referencingId;
        this.referencedObject = referencedId;
        if(prop == null)
            throw new ArgumentNullException("prop");
        this.referencingProperty = prop;
        this.index = -1;
        this.isIndexed = false;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="referencingId"></param>
    /// <param name="prop"></param>
    /// <param name="referencedId"></param>
    /// <param name="indx"></param>
    public StepEntityReference(int referencingId, PropertyInfo prop, int referencedId, int indx){
        this.referencingObject = referencingId;
        this.referencedObject = referencedId;
        if(prop == null)
            throw new ArgumentNullException("prop");
        this.referencingProperty = prop;
        this.index = indx;
        this.isIndexed = true;
    }
}
