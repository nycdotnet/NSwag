using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.NewtonsoftJson.Generation;
using NSwag.Generation.WebApi;
using Xunit;

namespace NSwag.CodeGeneration.CSharp.Tests
{
    public class FileTests
    {
        public class FileDownloadController : Controller
        {
            [Route("DownloadFile")]
            public HttpResponseMessage DownloadFile()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public async Task When_file_is_generated_system_alias_is_there_with_defaults()
        {
            // Arrange
            var swaggerGenerator = new WebApiOpenApiDocumentGenerator(new WebApiOpenApiDocumentGeneratorSettings
            {
                SchemaSettings = new NewtonsoftJsonSchemaGeneratorSettings()
            });

            var document = await swaggerGenerator.GenerateForControllerAsync<FileDownloadController>();

            // Act
            var codeGen = new CSharpClientGenerator(document, new CSharpClientGeneratorSettings
            {
                GenerateClientInterfaces = true
            });

            Assert.Equal("System", codeGen.Settings.GlobalSystemNamespaceAlias);

            var code = codeGen.GenerateFile();

            // Assert
            Assert.Contains("using System = global::System", code);
        }

        [Fact]
        public async Task When_file_is_generated_system_alias_is_there_with_overridden_name()
        {
            // Arrange
            var swaggerGenerator = new WebApiOpenApiDocumentGenerator(new WebApiOpenApiDocumentGeneratorSettings
            {
                SchemaSettings = new NewtonsoftJsonSchemaGeneratorSettings()
            });

            var document = await swaggerGenerator.GenerateForControllerAsync<FileDownloadController>();

            // Act
            var codeGen = new CSharpClientGenerator(document, new CSharpClientGeneratorSettings
            {
                GenerateClientInterfaces = true,
                GlobalSystemNamespaceAlias = "SuperGlobalSystem"
            });

            Assert.Equal("SuperGlobalSystem", codeGen.Settings.GlobalSystemNamespaceAlias);

            var code = codeGen.GenerateFile();

            // Assert
            Assert.Contains("using SuperGlobalSystem = global::System", code);
        }
    }
}
