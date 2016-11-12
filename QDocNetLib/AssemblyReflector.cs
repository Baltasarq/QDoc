namespace QDocLib {
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using QDocNetLib;

	/// <summary>
	/// Assembly reflector.
	/// Loads all entities from members.
	/// </summary>
	public class AssemblyReflector {
		public AssemblyReflector(string path)
		{
			this.Assembly = Assembly.Load( path );
			this.entities = new List<Entity>();
		}

		/// <summary>
		/// Gets the entities in the assembly.
		/// </summary>
		/// <value>The entities, as a primitive array.</value>
		public Entity[] Entities {
			get {
				if ( this.entities.Count == 0 ) {
					this.ExtractEntities();
				}

				return this.entities.ToArray();
			}
		}

		/// <summary>
		/// Extracts the entities from the assembly.
		/// </summary>
		private void ExtractEntities()
		{
			Type[] types = this.Assembly.GetTypes();

			foreach(Type type in types) {
				
			}

			return;
		}

		/// <summary>
		/// Gets the assembly.
		/// </summary>
		/// <value>The assembly which is being explored.</value>
		public Assembly Assembly {
			get; private set;
		}

		private List<Entity> entities;
	}
}
