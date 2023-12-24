using Microsoft.AspNetCore.Authorization;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.EndPoint.API.Controllers
{
    [Authorize]
    public class DocumentController : ProcessMiningControllerBase<Document>
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service) : base(service)
        {
            _service = service;
        }
    }
}
