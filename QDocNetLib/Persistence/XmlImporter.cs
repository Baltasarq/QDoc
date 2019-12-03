// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>

/*
    XML Tags
    ========
    
    see                 <see cref="REF"/> 
    seealso             <seealso cref="REF"/> 
    c                   Word or words as performatted.
    code                Same as above, but multiline.
    para                Paragraph.
    summary             The main documentation.
    example             An example of use for a method. Involves using c/code.
    paramref            <paramref name="x"/>
    exception           <exception cref="REF">Thrown when...</exception>/
    value               The value in a property.
    returns             That that the method returns.
    list                Creates lists.
    remarks             Something important complementing summary.

    Documentation for ID's first character reference
    ================================================


    N                   Namespace

                        You cannot add documentation comments to a namespace,
                        but you can make cref references to them.
                    
    T                   Type: class, interface, struct, enum, delegate

    F                   Field

    P                   Property (including indexers)

    M                   method (including constructors, operators...)

    E                   event

    !                   error string
                        The rest of the string provides information
                        about the error, for links that cannot be resolved.
                        
    The second part of the string is the fully qualified name of the item, starting at the root of the namespace. The name of the item, its enclosing type(s), and namespace are separated by periods. If the name of the item itself has periods, they are replaced by the hash-sign ('#'). It is assumed that no item has a hash-sign directly in its name. For example, the fully qualified name of the String constructor would be "System.String.#ctor".
    
    ===
    
    Methods
    -------
    
    For properties and methods, if there are arguments to the method,
    the argument list enclosed in parentheses follows.
    If there are no arguments, no parentheses are present.
    The arguments are separated by commas.
    The encoding of each argument follows directly
    how it is encoded in a .NET Framework signature:
        Base types. Regular types (ELEMENT_TYPE_CLASS or ELEMENT_TYPE_VALUETYPE)
        are represented as the fully qualified name of the type.
        
        Intrinsic types (for example, ELEMENT_TYPE_I4,
        ELEMENT_TYPE_OBJECT, ELEMENT_TYPE_STRING, ELEMENT_TYPE_TYPEDBYREF.
        and ELEMENT_TYPE_VOID) are represented as the fully qualified name
        of the corresponding full type.
        For example, System.Int32 or System.TypedReference.
        
        ELEMENT_TYPE_PTR is represented as a '*' following the modified type.
        ELEMENT_TYPE_BYREF is represented as a '@' following the modified type.
        ELEMENT_TYPE_CMOD_OPT is represented as a '!'
            and the fully qualified name of the modifier class,
            following the modified type.
        ELEMENT_TYPE_SZARRAY is represented as "[]" 
            following the element type of the array.
        ELEMENT_TYPE_ARRAY is represented as [lowerbound:size,lowerbound:size]
            where the number of commas is the rank - 1,
            and the lower bounds and size of each dimension,
            if known, are represented in decimal.
            If a lower bound or size is not specified,
            it is simply omitted. If the lower bound and size for a
            particular dimension are omitted, the ':' is omitted as well.
            For example, a 2-dimensional array with 1 as the lower bounds and
            unspecified sizes is [1:,1:].
        ELEMENT_TYPE_FNPTR is represented as "=FUNC:type(signature)",
            where type is the return type, and signature is
            the arguments of the method. If there are no arguments,
            the parentheses are omitted. The C# compiler never generates this.
*/


namespace QDocNetLib.Persistence {
	using System.Xml;
	using System.Xml.Linq;
	using System.Collections.Generic;
	
    /// <summary>
    /// Xml importer. Reads the XML Docs and imports its info.
    /// </summary>
    public class XmlImporter {
        /// <summary>
        /// Inits a new <see cref="T:QDocNetLib.Persistence.XmlImporter"/>.
        /// </summary>
        /// <param name="path">The path of the XML file.</param>
        public XmlImporter(string path)
        {
            this.FilePath = path;
        }

        /// <summary>
        /// Does the importing work.
        /// </summary>
        /// <returns>The import.</returns>
        public Unit Import()
        {
            Unit unit = null;
            ISet<Entity> rootEntities = new HashSet<Entity>();
            IDictionary<Id, Entity> allEntities = new Dictionary<Id, Entity>();

            // Read XML file
            try {
	            var xmlRoot = XElement.Load( this.FilePath );
                string name = xmlRoot.Element( "assembly" ).Element( "name" ).Value;
                unit = new Unit( name );

                this.LoadXmlElementsFrom( unit, xmlRoot, rootEntities, allEntities );

                unit.Load( rootEntities, allEntities );
            } catch(XmlException exc)
            {
                throw new System.ApplicationException( exc.Message );
            }
            
            return unit;
        }

        /// <summary>
        /// Loads the given XML documentation.
        /// </summary>
        /// <param name="unit">The <see cref="Unit"/> target of the loading.</param>
        /// <param name="xmlRoot">The XElement root of the info.</param>
        /// <param name="rootEntities">The classes in the <see cref="Unit"/>.</param>
        /// <param name="allEntities">All entities in the <see cref="Unit"/>.</param>
        public void LoadXmlElementsFrom(Unit unit,
                                        XElement xmlRoot,
                                        ISet<Entity> rootEntities,
                                        IDictionary<Id, Entity> allEntities)
        {
            IEnumerable<XElement> members =
                xmlRoot.Element( "members" ).Elements( "member");

            // Load entities with their associated documentation
            foreach(XElement member in members) {
                string name = (string) member.Attribute( "name" );

                // Load the documentation from the (flat) XML
                var xmlDoc = Documentation.Create( name, member );

                // Create the associated entity and store it
                var entity = new Entity( unit ) { Docs = xmlDoc };

                try {
                    allEntities.Add( entity.Id, entity );
                } catch(System.ArgumentException) {
                    string errorMsg = "Entity: " + entity
                        + "\n\n\tis duplicated in the docs:\n"
                        + "\n\t" + allEntities[ entity.Id ];
                    throw new System.ArgumentException( errorMsg );
                }
            }

            // Organize entities in the unit's structure
            this.FindRootClasses( unit, rootEntities, allEntities );
            this.Matrioska( allEntities );
        }

        /// <summary>
        /// Adds a new root class.
        /// </summary>
        /// <param name="cls">The entity, known to be a class.</param>
        /// <param name="unit">The <see cref="Unit"/> target of the loading.</param>
        /// <param name="rootEntities">The classes in the <see cref="Unit"/>.</param>
        /// <param name="allEntities">All entities in the <see cref="Unit"/>.</param>
        void AddRootClass(Entity cls,
                          Unit unit,
                          ISet<Entity> rootEntities,
                          IDictionary<Id, Entity> allEntities)
        {
            if ( cls.Id.Path.Length == 1 ) {
                    rootEntities.Add( cls );
            } else {
                // Is the class in a namespace ?
                Id parentId = cls.Id;

                do {
                    parentId = parentId.Parent;

                    if ( !allEntities.TryGetValue( parentId, out Entity entity ) )
                    {
                        parentId.Type = Id.AType.Namespace;

                        var doc = Documentation.Create( parentId );
                        var nsEntity = new Entity( unit ) { Docs = doc };

                        rootEntities.Add( nsEntity );
                    }
                } while( parentId.Path.Length > 1 );
            }

            return;
        }

        /// <summary>Finds the root classes.</summary>
        /// <param name="unit">The <see cref="Unit"/> target of the loading.</param>
        /// <param name="rootEntities">The classes in the <see cref="Unit"/>.</param>
        /// <param name="allEntities">All entities in the <see cref="Unit"/>.</param>
        void FindRootClasses(Unit unit,
                             ISet<Entity> rootEntities,
                             IDictionary<Id, Entity> allEntities)
        {
            foreach(Entity entity in allEntities.Values) {                
                if ( entity.Id.Type == Id.AType.Method ) {
                    Entity cls = allEntities[ entity.Id.Parent ];
                    cls.Id.SetTypeIfUnknown( entity.Id );


                    this.AddRootClass( cls, unit, rootEntities, allEntities );
                }
                else
                if ( entity.Id.Type == Id.AType.EnumValue ) {
                    Entity enm = allEntities[ entity.Id.Parent ];
                    enm.Id.SetTypeIfUnknown( entity.Id );

                    this.AddRootClass( enm, unit, rootEntities, allEntities );
                }
            }

            // Add the, for example, newly created namespaces
            foreach(Entity entity in rootEntities) {
                Id id = entity.Id;

                if ( !allEntities.ContainsKey( id ) ) {
                    allEntities.Add( id, entity );
                }
            }

            return;
        }
        
        /// <summary>
        /// Puts all entities one inside the other, as corresponding.
        /// </summary>
        /// <param name="allEntities">All entities.</param>
        void Matrioska(IDictionary<Id, Entity> allEntities)
        {
            foreach(Entity entity in allEntities.Values) {
                try {
                    if ( allEntities.TryGetValue( entity.Id.Parent, out Entity parent ) )
                    {
                        parent.Add( entity );
                    }
                } catch(System.ArgumentException) {
                    continue;
                }
            }
            
            return;
        }
        
        /// <summary>
        /// The path of the XML file.
        /// </summary>
        /// <value>The path.</value>
        public string FilePath {
            get; private set;
        }
    }
}
