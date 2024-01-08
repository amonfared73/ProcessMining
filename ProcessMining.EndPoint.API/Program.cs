using ProcessMining.EndPoint.API.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.AddProcessMining(out WebApplication app);
app.Run();
