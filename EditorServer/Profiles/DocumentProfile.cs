using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EditorServer.Models;

namespace EditorServer.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, DocumentPreview>();
            CreateMap<DocumentPreview, DocumentDTO>();
        }
    }
}
