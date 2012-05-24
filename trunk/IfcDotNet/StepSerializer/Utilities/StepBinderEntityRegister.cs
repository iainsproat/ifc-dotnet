/*
 * Created by Iain Sproat
 * Date: 23/05/2012
 * Time: 20:54
 * 
 */
using System;
using System.Collections.Generic;

using IfcDotNet.Schema;

namespace IfcDotNet.StepSerializer.Utilities
{
	/// <summary>
	/// A Register for all entities encountered in a STEP file
	/// </summary>
	public class StepBinderEntityRegister
	{
		private int _entityCounter = 0;
		private IDictionary<Entity, int> _entityRegister = new Dictionary<Entity, int>();
		
		/// <summary>
		/// 
		/// </summary>
		public StepBinderEntityRegister()
		{
		}
		
		/// <summary>
		/// Determines whether an entity has already been registered
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public bool isAlreadyRegistered( Entity e ){
			int entityId = -1;
			return this.isAlreadyRegistered( e, out entityId );
		}
		
		/// <summary>
		/// Determines whether an entity has already been registered,
		/// and also outputs the id of a registered entity
		/// </summary>
		/// <param name="e"></param>
		/// <param name="entityId"></param>
		/// <returns></returns>
		public bool isAlreadyRegistered(Entity e, out int entityId ){
			return this._entityRegister.TryGetValue( e, out entityId );
		}
		
		/// <summary>
		/// Returns the id of a registered entity
		/// </summary>
		/// <param name="e"></param>
		/// <returns>-1 if the entity is not registered</returns>
		public int getEntityId( Entity e ){
			int entityId = -1;
			this.isAlreadyRegistered( e , out entityId );
			return entityId;
		}
		
		/// <summary>
		/// Registers an entity if it is not already registered
		/// </summary>
		/// <param name="e"></param>
		/// <returns>The registration id of the entity.  A new id if it was not already registered, or its existing Id if it was.</returns>
		public int RegisterEntity( Entity e ){
			int entityId = -1;
			if(isAlreadyRegistered(e, out entityId)){
				return entityId;
			}
			
			//else it's not yet registered, so register and queue it			
			this._entityCounter++;
			this._entityRegister.Add(e, _entityCounter);
			return _entityCounter; //the number we registered the entity with
		}
	}
}
