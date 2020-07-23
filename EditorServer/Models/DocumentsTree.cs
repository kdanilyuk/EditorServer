using System.Collections.Generic;

namespace EditorServer.Models
{
    public class DocumentsTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DocumentsTree> Children { get; set; }
    }
}
