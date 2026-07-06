var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var database = sqlServer.AddDatabase("gamecatalog");

var web = builder.AddProject<Projects.Web>("web")
    .WithReference(database)
    .WaitFor(database);

builder.AddNextJsApp("frontend", "../frontend")
    .WithReference(web)
    .WaitFor(web);

builder.Build().Run();
