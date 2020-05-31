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
        [Route("get-tree")]
        public IEnumerable<DocumentPreviewTree> GetTreeDocumentsBySubjectId(int subjectId)
        {
            return _documentService.GetTreeDocumentsBySubjectId(subjectId);
        }

        [HttpGet]
        [Route("get-document")]
        public DocumentPreview GetDocument (int documentId)
        {
            return _documentService.GetDocument(documentId);
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
