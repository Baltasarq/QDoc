using System;

namespace QDocNetLib.Persistence {
    /// <summary>
    /// Html exporter. Exports the documentation info to HTML.
    /// </summary>
    public class HtmlExporter {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:QDocNetLib.Persistence.HtmlExporter"/> class.
        /// </summary>
        /// <param name="entity">The root entity in which to start.</param>
        public HtmlExporter(Entity entity)
        {
            this.Entity = entity;
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
        public Entity Entity {
            get; set;
        }
    }
}
