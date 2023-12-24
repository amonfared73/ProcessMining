using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Infra.EntityFramework.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Queries
{
    public class DocumentService : BaseService<Document>, IDocumentService
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;
        public DocumentService(IDbContextFactory<ProcessMiningDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
