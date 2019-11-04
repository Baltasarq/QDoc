// QDocNet - (c) 2017 Baltasar MIT License <baltasarq@gmail.com>


namespace QDocNetLib.Persistence {
    /// <summary>
    /// Html exporter. Exports the documentation info to HTML.
    /// </summary>
    public class HtmlExporter {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Persistence.HtmlExporter"/> class.
        /// </summary>
        /// <param name="unit">The root entity in which to start.</param>
        public HtmlExporter(Unit unit)
        {
            this.Unit = unit;
        }

        /// <summary>
        /// Does the exporting work, saving the doc info in HTML format to a given path.
        /// </summary>
        /// <param name="path">Path.</param>
        public void SaveTo(string path)
        {
        }

        /// <summary>
        /// Gets or sets the root entity for HTML exporting.
        /// </summary>
        /// <value>The entity.</value>
        public Unit Unit {
            get; set;
        }
    }
}
