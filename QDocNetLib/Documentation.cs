// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

namespace QDocNetLib {
    using System.Xml.Linq;
	using System.Collections.Generic;

	/// <summary>
	/// The documentation present in the file.
	/// </summary>
	public sealed class Documentation {
        /// <summary>A modifier in the text, such as see.</summary>
        public abstract class Modifier {
            /// <summary>
            /// Initializes a new instance of the <see cref="T:QDocNetLib.Documentation.Modifier"/> class.
            /// </summary>
            /// <param name="pos">The position in the text.</param>
            /// <param name="contents">The contents.</param>
            public Modifier(int pos, string contents)
            {
                this.Pos = pos;
                this.Contents = contents;
            }
            
            /// <summary>
            /// Gets the command, such as see.
            /// </summary>
            /// <value>The command.</value>
            public abstract string Command {
                get;
            }
            
            /// <summary>
            /// Gets or sets the position.
            /// </summary>
            /// <value>The position.</value>
            public int Pos {
                get; set;
            }
            
            /// <summary>
            /// Gets or sets the contents.
            /// </summary>
            /// <value>The contents.</value>
            public string Contents {
                get {
                    return this.contents;
                }
                set {
                    this.contents = value.Trim();
                }
            }
            
            /// <summary>
            /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation.Modifier"/>.
            /// </summary>
            /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation.Modifier"/>.</returns>
            public override string ToString()
            {
                return "." + this.Command + "{" + this.Contents + "}";
            }
            
            /// <summary>
            /// Creates the appropriate modifier for the given command.
            /// </summary>
            /// <returns>The created <see cref="Modifier"/>.</returns>
            /// <param name="pos">The position in text.</param>
            /// <param name="element">The <see cref="XElement"/> to read from.</param>
            public static Modifier Create(int pos, XElement element)
            {
                Modifier toret = null;
                string cmd = element.Name.LocalName;
                
                if ( cmd == SeeModifier.KeyWord ) {
                    var attrCref = element.Attribute( "cref" );
                    
                    if ( attrCref != null ) {
                        string contents = attrCref.Value;
                        toret = new SeeModifier( pos, contents );
                    } else {
                        throw new System.ArgumentException( "malformed modifier: " + cmd );
                    }
                } else {
                    throw new System.ArgumentException( "unexpected modifier: " + cmd );
                }
                
                return toret;
            }
            
            string contents;
        }
        
        /// <summary>The See modifier.</summary>
        public class SeeModifier: Modifier {
            /// <summary>The command key word for this modifier.</summary>
            public const string KeyWord = "see";
        
            /// <summary>
            /// Initializes a new instance of the <see cref="T:QDocNetLib.Documentation.SeeModifier"/> class.
            /// </summary>
            /// <param name="pos">Position.</param>
            /// <param name="contents">Contents.</param>
            public SeeModifier(int pos, string contents)
                :base( pos, contents )
            {
            }
            
            /// <summary>
            /// Gets the command.
            /// </summary>
            /// <value>The command.</value>
            public override string Command {
                get {
                    return KeyWord;
                }
            }
        }
    
        /// <summary>
        /// A piece of documentation.
        /// </summary>
        public sealed class DocPiece {
            DocPiece()
            {
                this.modifiers = new List<Modifier>();
            }
            
            /// <summary>
            /// Initializes a new instance of the <see cref="T:QDocNetLib.XmlDocumentation.DocPiece"/> class.
            /// </summary>
            /// <param name="contents">The doc piece itself.</param>
            public DocPiece(string contents)
                : this()
            {
                this.Contents = contents;
                this.Name = null;
            }
            
            /// <summary>
            /// Initializes a new instance of the <see cref="T:QDocNetLib.XmlDocumentation.DocPiece"/> class.
            /// </summary>
            /// <param name="name">The associated name, for instance, for a param.</param>
            /// <param name="contents">The doc piece itself.</param>
            public DocPiece(string name, string contents)
                : this()
            {
                this.Contents = contents;
                this.Name = name;
            }
        
            /// <summary>
            /// Gets the name, for example, for 'param' docs.
            /// </summary>
            /// <value>The name, as a string.</value>
            public string Name {
                get; private set;
            }
            
            /// <summary>
            /// Gets the contents, the docs themselves.
            /// </summary>
            /// <value>The contents, as a string.</value>
            public string Contents {
                get {
                    return this.contents;
                }
                set {
                    this.contents = value.Trim();
                }
            }
            
            /// <summary>Removes all modifiers.</summary>
            public void ClearModifiers()
            {
                this.modifiers.Clear();
            }
            
            /// <summary>
            /// Gets the count of modifiers pieces.
            /// </summary>
            /// <value>The count pieces.</value>
            public int CountModifiers {
                get {
                    return this.modifiers.Count;
                }
            }
            
            /// <summary>
            /// Adds the specified <see cref="Modifier"/>.
            /// </summary>
            /// <param name="modifier">The modifier to add.</param>
            public void Add(Modifier modifier)
            {
                this.modifiers.Add( modifier );
            }
            
            /// <summary>
            /// Gets the stored modifiers.
            /// </summary>
            /// <value>The modifiers, as an array.</value>
            public Modifier[] Modifiers {
                get {
                    return this.modifiers.ToArray();
                }
            }
            
            /// <summary>
            /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation.DocPiece"/>.
            /// </summary>
            /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation.DocPiece"/>.</returns>
            public override string ToString()
            {
                int pos = 0;
                string toret = "";
                
                if ( !string.IsNullOrWhiteSpace( this.Name ) ) {
                    toret += this.Name + ": ";
                }
                
                foreach(Modifier m in this.Modifiers) {
                    toret += this.Contents.Substring( pos, m.Pos - pos );
                    toret += " " + m;
                    pos = m.Pos;
                }
                
                toret += this.Contents.Substring( pos );
                
                return toret;
            }
            
            readonly List<Modifier> modifiers;
            string contents;
        }
    
        /// <summary>
        /// The possible attributes.
        /// </summary>
        public enum Attribute {
            /// <summary>The most important piece of doc.</summary>
            Summary,
            /// <summary>Return value of a method.</summary>
            Returns,
            /// <summary>The explanation for a parameter.</summary>
            Param
        };
    
		/// <summary>
		/// Initializes a new instance of the <see cref="QDocNetLib.Documentation"/> class.
		/// </summary>
		Documentation(string id)
            : this( new Id( id ) )
		{
			this.Id = new Id( id );
		}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Documentation"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        Documentation(Id id)
        {
            this.Id = id;
            this.attributes = new Dictionary<Attribute, DocPiece>();
        }
        
        /// <summary>
        /// Add the specified attr and contents.
        /// </summary>
        /// <param name="attr">An <see cref="Attribute"/>.</param>
        /// <param name="contents">The contents, as a string.</param>
        public void Add(Attribute attr, DocPiece contents)
        {
            this.attributes.Add( attr, contents );
        }
        
        /// <summary>
        /// Gets the associated <see cref="DocPiece"/> to that <see cref="Attribute"/>.
        /// </summary>
        /// <param name="attr">The <see cref="Attribute"/>.</param>
        public DocPiece this[Attribute attr]
        {
            get {
                this.attributes.TryGetValue( attr, out DocPiece toret );
                return toret;
            }
        }
        
		/// <summary>
		/// Gets or sets the full id of this doc piece.
		/// </summary>
		/// <value>The full id, as a string</value>
		public Id Id {
            get; private set;
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:QDocNetLib.Documentation"/>.</returns>
        public override string ToString()
        {
            var toret = new System.Text.StringBuilder();
            
            foreach(Attribute key in this.attributes.Keys) {
                toret.Append( this.attributes[ key ] );
                toret.Append( "\n\t" );
            }
            
            return toret.ToString().Trim();
        }
        
        /// <summary>
        /// Gets the xml tag for the given attribute.
        /// </summary>
        /// <returns>The xml tag, as a string..</returns>
        /// <param name="attr">An <see cref="Attribute"/> to convert.</param>
        static string GetXmlTagFor(Attribute attr)
        {
            return attr.ToString().ToLower();
        }
        
        /// <summary>
        /// Extracts the xml for the given attribute.
        /// </summary>
        /// <returns>The xml contents, as a <see cref="DocPiece"/>.</returns>
        /// <param name="element">An <see cref="T:System.Xml.Linq.XElement"/>.</param>
        /// <param name="attr">The <see cref="T:Attribute"/> for the doc piece to extract.</param>
        static DocPiece ExtractXmlFor(XElement element, Attribute attr)
        {
            var contentsNode = element.Element( GetXmlTagFor( attr ) );
            var toret = new DocPiece( attr.ToString(), "" );
            XNode child = contentsNode.FirstNode;
            
            while( child != null ) {
	            if ( child is XText text ) {
	                toret.Contents += text.Value;
	            }
                else
                if ( child is XElement subElement) {    
                    toret.Add(
                        Modifier.Create(
                            toret.Contents.Length,
                            subElement ) );
	            }
                
                child = child.NextNode;
            }
            
            return toret;
        }
        
        /// <summary>
        /// Creates a new documentation.
        /// </summary>
        /// <returns>The creates docs.</returns>
        /// <param name="name">The name of this entity.</param>
        /// <param name="element">The XML doc info.</param>
        public static Documentation Create(string name, XElement element)
        {
            var toret = new Documentation( name );
            DocPiece summaryDoc = ExtractXmlFor( element, Attribute.Summary );
            
            // Extract summary, always present
            toret.attributes[ Attribute.Summary ] = summaryDoc;
            
            return toret;
        }
        
        /// <summary>
        /// Creates an empty <see cref="Documentation"/>.
        /// </summary>
        /// <returns>The created documentation.</returns>
        /// <param name="id">An <see cref="Id"/>.</param>
        public static Documentation Create(Id id)
        {
            return new Documentation( id );
        }
        
        readonly Dictionary<Attribute, DocPiece> attributes;
	}
}
