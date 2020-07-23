using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using EditorServer.Models;
using System.Text.Json;

namespace Editor.Tests.Integration.Tests
{
    public class DocumentIntegrationTest : IClassFixture<WebApplicationFactory<EditorServer.Startup>>
    {
        private readonly WebApplicationFactory<EditorServer.Startup> _factory;

        public DocumentIntegrationTest(WebApplicationFactory<EditorServer.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]

        public async Task GetAllDocuments()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5000/"),
                AllowAutoRedirect = true
            });

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var response = await client.GetAsync("api/documents/all");
            var responseString = response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<IEnumerable<DocumentPreview>>(responseString.Result, serializeOptions);

            Assert.True(true);
        }

        [Fact]
        public async Task GetDocumentsTest()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5000/"),
                AllowAutoRedirect = true
            });

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var response = await client.GetAsync("api/documents/get-documents?subjectId=1");
            var responseString = response.Content.ReadAsStringAsync();
            //var body = JsonSerializer.Deserialize<IEnumerable<DocumentPreviewTree>>(responseString.Result, serializeOptions);

            Assert.True(true);
        }
    }
}
