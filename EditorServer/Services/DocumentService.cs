using AutoMapper;
using EditorServer.Contexts;
using EditorServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorServer.Services
{
    public interface IDocumentService
    {
        IEnumerable<DocumentPreview> GetDocumentsBySubjectId(int subjectId);
        IEnumerable<DocumentsTree> GetDocumentsTreeBySubjectId(int subjectId);
        DocumentPreview GetFullContent(int documentId);
        IActionResult UpdateDocument(DocumentPreview document);
        IActionResult RemoveDocument(int documentId);
    }
    public class DocumentService : IDocumentService
    {
        private readonly DocumentContext _documentContext;
        private readonly IMapper _mapper;

        public DocumentService(IDbContextFactory contextFactory, IMapper mapper)
        {
            _documentContext = contextFactory.CreateDbContext<DocumentContext>();
            _mapper = mapper;
        }

        public DocumentPreview GetFullContent(int documentId)
        {
            var content = new StringBuilder();

            var document = _documentContext.Documents.Find(documentId);

            if (document == null)
            {
                return _mapper.Map<DocumentPreview>(document);
            }

            ParseData(new List<DocumentDTO>() { document }, ref content);

            void ParseData(IEnumerable<DocumentDTO> documents, ref StringBuilder content)
            {
                foreach (var document in documents)
                {
                    if (!document.Childrens.Any())
                    {
                        content.Append(document.Text);
                    }
                    else
                    {
                        ParseData(document.Childrens, ref content);
                    }
                }
            }

            document.Text = content.ToString();

            return _mapper.Map<DocumentPreview>(document);
        }

        public IEnumerable<DocumentPreview> GetDocumentsBySubjectId(int subjectId) // Надо убрать
        {
            var parentNodes = _documentContext.Documents.Where(d => d.SubjectId == subjectId);

            return _mapper.Map<IEnumerable<DocumentPreview>>(parentNodes);
        }

        public IEnumerable<DocumentsTree> GetDocumentsTreeBySubjectId(int subjectId)
        {
            var parentNodes = _documentContext.Documents.Where(d => d.SubjectId == subjectId && d.ParentId == null);

            return parentNodes.Select(book => new DocumentsTree()
            {
                Id = book.Id,
                Name = book.Name,
                Children = book.Childrens.Select(section => new DocumentsTree()
                {
                    Id = section.Id,
                    Name = section.Name,
                    Children = section.Childrens.Select(subsection => new DocumentsTree()
                    {
                        Id = subsection.Id,
                        Name = subsection.Name,
                        Children = subsection.Childrens.Select(paragraph => new DocumentsTree()
                        {
                            Id = paragraph.Id,
                            Name = paragraph.Name
                        })
                    })
                })
            });
        }

        public IActionResult UpdateDocument(DocumentPreview document)
        {
            var documentDTO = _mapper.Map<DocumentDTO>(document);

            if (documentDTO.Id == 0) //Save new document
            {
                _documentContext.Documents.Add(documentDTO);
            }
            else // Update existing
            {
                var existingDocument = _documentContext.Documents.Find(document.Id);

                // Переделать, вроде можно через маппер
                existingDocument.Name = documentDTO.Name;
                existingDocument.ParentId = documentDTO.ParentId;
                existingDocument.SubjectId = documentDTO.SubjectId;
                existingDocument.AuthorId = documentDTO.AuthorId;
                existingDocument.Description = documentDTO.Description;
                existingDocument.Text = documentDTO.Text;

                _documentContext.Documents.Update(existingDocument);
            }

            _documentContext.SaveChanges();

            return new OkResult();
        }

        public IActionResult RemoveDocument(int documentId)
        {
            var document = _documentContext.Documents.Find(documentId);

            if (document == null)
            {
                return new NotFoundResult();
            }

            RemoveChilds(new List<DocumentDTO>() { document });

            void RemoveChilds(IEnumerable<DocumentDTO> documents)
            {
                foreach (var document in documents)
                {
                    if (document.Childrens.Any())
                    {
                        RemoveChilds(document.Childrens);
                    }
                    _documentContext.Documents.Remove(document);
                }
            }

            _documentContext.SaveChanges();

            return new OkResult();
        }
    }
}
