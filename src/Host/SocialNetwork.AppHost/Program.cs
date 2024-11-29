var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak",
                                    8080,
                                    adminPassword: builder.AddParameter("keycloak-password"),
                                    adminUsername: builder.AddParameter("keycloak-username"))
                      .WithDataVolume();


var sqlPassword = builder.AddParameter("sqlserver-password", true);
var sql = builder.AddSqlServer("sql",
                                password: sqlPassword)
                 .WithImageTag("latest")
                 .WithDataVolume("social-network")
                 .WithLifetime(ContainerLifetime.Persistent);

var mongoDb = builder.AddMongoDB("mongo-db")
                     .WithDataVolume()
                     .WithLifetime(ContainerLifetime.Persistent);


var launchProfileName = "https";

builder.AddProject<Projects.ApiGateway>("apigateway", launchProfileName)
       .WithReference(keycloak);

var categorydb = sql.AddDatabase("CategoryDb");
builder.AddProject<Projects.Category_API>("category-api", launchProfileName)
       .WithReference(categorydb)
       .WithReference(keycloak)
       .WaitFor(categorydb);

builder.AddProject<Projects.Post_API>("post-api", launchProfileName);

var commentDb = mongoDb.AddDatabase("CommentDb");
builder.AddProject<Projects.Comment_API>("comment-api", launchProfileName)
        .WithReference(commentDb)
        .WaitFor(mongoDb);

builder.AddProject<Projects.Like_API>("like-api", launchProfileName);

builder.AddProject<Projects.Notification_API>("notification-api", launchProfileName);

builder.AddNpmApp("web-ui", "../../webapp.client", "dev")
       .WithHttpsEndpoint(env: "FE_PORT")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile();

builder.Build().Run();
