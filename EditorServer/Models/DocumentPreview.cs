using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Models
{
    public class DocumentPreview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int? ParentId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
    }
}
