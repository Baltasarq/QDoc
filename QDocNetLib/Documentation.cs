namespace QDocNetLib {
	using System.Collections.Generic;

	/// <summary>
	/// The documentation present in any <see cref="Entity"/>.
	/// </summary>
	public class Documentation {
		/// <summary>
		/// Initializes a new instance of the <see cref="QDocNetLib.Documentation"/> class.
		/// </summary>
		/// <param name="contents">The raw contents of the documentation.</param>
		public Documentation(string contents = "")
		{
			this.Raw = contents;
			this.crossReferences = new List<Entity>();
		}

		/// <summary>
		/// Gets or sets the raw documentation.
		/// </summary>
		/// <value>The raw documentation, as a string</value>
		public string Raw {
			get {
				return this.raw;
			}
			set {
				this.raw = value;
				this.Parse();
			}
		}

		/// <summary>
		/// Parses the documentation to generate all info,
		/// such as cross references.
		/// </summary>
		private void Parse()
		{
		}

		/// <summary>
		/// Gets or sets the cross references.
		/// </summary>
		/// <value>The cross references.</value>
		public Entity[] CrossReferences {
			get {
				return this.crossReferences.ToArray();
			}
			set {
				this.crossReferences.Clear();
				this.crossReferences.AddRange( value );
			}
		}

		private List<Entity> crossReferences;
		private string raw;
	}
}
