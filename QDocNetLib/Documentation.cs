namespace QDocLib {
	public class Documentation {
		public Documentation(string contents = "")
		{
			this.Raw = contents;
		}

		public string Raw {
			get; set;
		}
	}
}
