using ProcessMining.Core.Domain.BaseModels;

namespace ProcessMining.Core.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public override string ToString()
        {
            return Username;
        }
    }
}
