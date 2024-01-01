using ProcessMining.EndPoint.API.Extensions;

namespace ProcessMining.EndPoint.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddProcessMining(out WebApplication app);
            app.Run();
        }
    }
}