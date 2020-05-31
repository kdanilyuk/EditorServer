using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EditorServer.Models
{
    public class DocumentDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public int SubjectId { get; set; }

        public string Text { get; set; }

        public int? ParentId { get; set; }

        public virtual DocumentDTO Parent { get; set; }

        public virtual HashSet<DocumentDTO> Childrens { get; set; }
    }
}
