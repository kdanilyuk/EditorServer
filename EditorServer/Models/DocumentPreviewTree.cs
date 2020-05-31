using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Models
{
    public class DocumentPreviewTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DocumentPreviewTree> Children { get; set; }
    }
}
