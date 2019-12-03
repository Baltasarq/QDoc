namespace QDocNetGui.UI {
    using System.Windows.Forms;
    
    using QDocNetLib;

    /// <summary>
    /// A tree view node with an <see cref="Entity"/> attached.
    /// </summary>
    public class DocTreeNode: TreeNode
    {
        public DocTreeNode(Entity entity)
            : base( ComposeNodeName( entity ) )
        {
            this.Entity = entity;
        }
        
        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>The <see cref="Entity"/> object.</value>
        public Entity Entity {
            get; set;
        }

        private static string ComposeNodeName(Entity entity)
        {
            return entity.Id.CharType + ": " + entity.Id.Name;
        }
    }
}
