// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>


namespace QDocNetLib.Persistence {
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;

    /// <summary>
    /// Html exporter. Exports the documentation info to HTML.
    /// </summary>
    public class HtmlExporter {
        private const char KeyDelimiter = '$';

        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Persistence.HtmlExporter"/> class.
        /// </summary>
        /// <param name="unit">The root entity in which to start.</param>
        /// <param name="dirPath">The path to the directory.</param>
        public HtmlExporter(Unit unit, string dirPath)
        {
            this.Unit = unit;
            this.DirPath = dirPath;
            this.BuildArgumentsDictionary();

            this.rootTemplate = this.FetchMainTemplate();
            this.classTemplate = this.FetchClassTemplate();
            this.namespaceTemplate = this.FetchNamespaceTemplate();
        }

        void BuildArgumentsDictionary()
        {
            this.templateArguments = new Dictionary<string, Func<Entity, string>>
            {
                { "version", (x) => LibInfo.BuildCopyrightMessage() },
                { "roots", (x) => this.GatherSubEntitiesList( this.Unit.Classes ) },
                { "classes", (x) => this.GatherSubEntitiesList( x.Entries ) },
                { "project_name", (x) => this.Unit.Name },
                { "name", (x) => x.Id.Name },
                { "class_summary", (x) => x.Docs[Documentation.Attribute.Summary].ToString() },
                { "methods", (x) => this.GatherClassMethods(x) },
                { "properties", (x) => this.GatherClassProperties(x) }
            };
        }

        /// <summary>Gathers the sub entities list.</summary>
        /// <returns>The sub entities list, as an HTML list of li's.</returns>
        /// <param name="entities">A collection of <see cref="Entity"/>'s.</param>
        string GatherSubEntitiesList(IEnumerable<Entity> entities)
        {
            StringBuilder toret = new StringBuilder();

            foreach(Entity entity in entities) {
                Documentation.DocPiece summary =
                                 entity.Docs[ Documentation.Attribute.Summary ];

                toret.Append("<li>");
                toret.Append( "<b><a href='");
                toret.Append( BuildFileNameFor( entity ) );
                toret.Append( "'>");
                toret.Append( entity.Id.Name );
                toret.Append("</a></b><br/><i>");
                toret.Append( summary );
                toret.Append( "</i></li>" );
            }

            return toret.ToString();
        }

        string GatherClassMethods(Entity x)
        {
            StringBuilder toret = new StringBuilder();

            foreach(Entity entry in x.Entries) {
                if ( entry.Type == Id.AType.Method ) {
                    toret.Append( "<li><b>");
                    toret.Append( entry.Id.Name );

                    if ( entry.Id.Parameters.Length > 0 ) {
                        string delim = "";
                        toret.Append( "(" );

                        foreach(string param in entry.Id.Parameters) {
                            toret.Append( delim );
                            toret.Append( "<br/>" );
                            toret.Append( "&nbsp;&nbsp;&nbsp;&nbsp;");
                            toret.Append( param );
                            delim = ",";
                        }

                        toret.Append( ")" );
                    } else {
                        toret.Append( "()<br/>" );
                    }

                    toret.Append( "</b><br/><p>" );

                    if ( entry.Docs.Parameters.Length > 0 ) {
                        toret.Append( "<ul>" );
                        foreach(Documentation.DocPiece docParam in entry.Docs.Parameters) {
                            toret.Append( "<li>" );
                            toret.Append( HtmlFromText( docParam.ToString() ) );
                            toret.Append( "</li>" );
                        }
                        toret.Append( "</ul>" );
                    }

                    toret.Append( "</p><p><i>" );
                    toret.Append( HtmlFromText( entry.Docs.ToString() ) );
                    toret.Append( "</i></p></li>");

                }
            }

            return toret.ToString();
        }

        string GatherClassProperties(Entity x)
        {
            StringBuilder toret = new StringBuilder();

            foreach(Entity entry in x.Entries) {
                if ( entry.Type == Id.AType.Property ) {
                    toret.Append( "<li><b>");
                    toret.Append( entry.Id.Name );
                    toret.Append( "</b><br/>" );
                    toret.Append( "<p><i>" );
                    toret.Append( HtmlFromText( entry.Docs.ToString() ) );
                    toret.Append( "</i></p></li>");
                }
            }

            return toret.ToString();
        }

        /// <summary>
        /// Does the exporting work,
        /// saving the doc info in HTML format to a given path.
        /// </summary>
        public void SaveTo()
        {
            this.CreateFileForUnit( "index.html", this.rootTemplate );

            foreach(Entity entity in this.Unit.Classes) {
                this.CreateFileFor( entity );
            }

            return;
        }

        /// <summary>
        /// Creates the doc file, as HTML.
        /// </summary>
        /// <param name="name">The name of the future file.</param>
        /// <param name="template">The template for substitutions.</param>
        void CreateFileForUnit(string name, string template)
        {
            this.SubstituteTemplate( null, name, template );
        }

        void CreateFileFor(Entity entity)
        {
            if ( entity.Type == Id.AType.Namespace ) {
                this.SubstituteTemplate( entity, BuildFileNameFor( entity ), this.namespaceTemplate );

                foreach(Entity subEntity in entity.Entries) {
                    this.CreateFileFor( subEntity );
                }
            } else {
                this.SubstituteTemplate( entity, BuildFileNameFor( entity ), this.classTemplate );
            }

            return;
        }

        void SubstituteTemplate(Entity entity, string fileName, string template)
        {
            string filePath = Path.Combine( this.DirPath, fileName );

            using (var fo = new StreamWriter( filePath ) ) {
                int i = 0;

                while( i < template.Length ) {
                    char ch = template[ i ];
                    char chNext = '\0';

                    if ( i < ( template.Length - 1 ) ) {
                        chNext = template[ i + 1 ];
                    }

                    if ( ch == KeyDelimiter
                      && chNext != KeyDelimiter )
                    {
                        string key = ReadKey( template, ref i );
                        this.templateArguments.TryGetValue( key, out Func<Entity, string> subst);
                        string contents = "#KEY_ERROR#";

                        if ( subst != null ) {
                            contents = subst( entity );
                        }

                        fo.Write( contents );
                    } else {
                        fo.Write( ch );
                    }

                    ++i;
                }
            }

            return;
        }

        /// <summary>
        /// Reads a key from the template.
        /// It is surrounded by '$'.
        /// </summary>
        /// <returns>The read key.</returns>
        /// <param name="template">The string in which to read.</param>
        /// <param name="i">The current index in the template.</param>
        string ReadKey(string template, ref int i)
        {
            StringBuilder toret = new StringBuilder();

            if ( template[ i ] == KeyDelimiter ) {
                ++i;

                while( template[ i ] != KeyDelimiter ) {
                    toret.Append( template[i] );
                    ++i;
                }
            }

            return toret.ToString();
        }

        /// <summary>
        /// Gets or sets the root entity for HTML exporting.
        /// </summary>
        /// <value>The entity.</value>
        public Unit Unit {
            get; set;
        }

        string FetchMainTemplate()
        {
            return this.FetchTemplate( "QDocNetLib.Res.index.html" );
        }

        string FetchClassTemplate()
        {
            return this.FetchTemplate( "QDocNetLib.Res.entity.html" );
        }

        string FetchNamespaceTemplate()
        {
            return this.FetchTemplate( "QDocNetLib.Res.namespace.html" );
        }

        /// <summary>
        /// Fetchs a given template.
        /// </summary>
        /// <returns>The template, as a string.</returns>
        /// <param name="name">The name of the template to retrieve.</param>
        string FetchTemplate(string name)
        {
            string toret = null;

            using( var stream = this.GetType().Assembly.
                                    GetManifestResourceStream( name ) )
            {
                using (var reader = new StreamReader( stream ) ) {
                    toret = reader.ReadToEnd();
                }
            }

            return toret;
        }

        /// <summary>
        /// Builds the file name for the given entity.
        /// </summary>
        /// <returns>The file name, as a string.</returns>
        /// <param name="entity">An <see cref="Entity"/>.</param>
        static string BuildFileNameFor(Entity entity)
        {
            return "entity_" + entity.Id.Name + ".html";
        }

        static string HtmlFromText(string text)
        {
            string delims = "\n\t";
            string[] html = new string[]{ "<br/>", "" };

            foreach(char ch in delims) {
                int pos = delims.IndexOf( ch );

                if ( pos >= 0 ) {
                    text = text.Replace( ch.ToString(), html[ pos ] );
                }
            }

            return text;
        }

        /// <summary>
        /// Gets the dir path to which the doc files will be saved.
        /// </summary>
        /// <value>The dir path, not set until SaveTo() is called.</value>
        public string DirPath {
            get; private set;
        }

        string rootTemplate;
        string classTemplate;
        string namespaceTemplate;
        Dictionary<string, Func<Entity, string>> templateArguments;
    }
}
