using System;
using System.Collections.Generic;
using EditorServer.Models;
using EditorServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace EditorServer.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("get-documents")]
        public IEnumerable<DocumentPreview> GetDocumentsBySubjectId(int subjectId)
        {
            return _documentService.GetDocumentsBySubjectId(subjectId);
        }

        [HttpGet]
        [Route("get-documents-tree")]
        public IEnumerable<DocumentsTree> GetDocumentsTreeBySubjectId(int subjectId)
        {
            return _documentService.GetDocumentsTreeBySubjectId(subjectId);
        }

        [HttpGet]
        [Route("get-content")]
        public DocumentPreview GetFullContent(int documentId)
        {
            return _documentService.GetFullContent(documentId);
        }

        [HttpPost]
        [Route("update-document")]
        public IActionResult UpdateDocument([FromBody] DocumentPreview document)
        { 
            return _documentService.UpdateDocument(document);
        }

        [HttpGet]
        [Route("remove-document")]
        public IActionResult RemoveDocument(int documentId)
        {
            return _documentService.RemoveDocument(documentId);
        }
    }
}
