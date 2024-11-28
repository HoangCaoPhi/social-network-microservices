var builder = DistributedApplication.CreateBuilder(args);
 
var keycloak = builder.AddKeycloak("keycloak",
                                    8080,
                                    adminPassword: builder.AddParameter("AdminPasswordKeycloak"),
                                    adminUsername: builder.AddParameter("AdminUserNameKeyclock"))
                      .WithDataVolume();
 
var sql = builder
            .AddSqlServer("sql", port: 1434)
            .WithImageTag("latest")
            .WithDataVolume("social-network");

var launchProfileName = "https";

builder.AddProject<Projects.ApiGateway>("apigateway", launchProfileName)
       .WithReference(keycloak);

var categorydb = sql.AddDatabase("categorydb");
builder.AddProject<Projects.Category_API>("category-api", launchProfileName)
       .WithReference(categorydb)
       .WithReference(keycloak)
       .WaitFor(categorydb);

builder.AddProject<Projects.Post_API>("post-api", launchProfileName);

builder.AddProject<Projects.Comment_API>("comment-api", launchProfileName);

builder.AddProject<Projects.Like_API>("like-api", launchProfileName);

builder.AddProject<Projects.Notification_API>("notification-api", launchProfileName);

builder.AddNpmApp("web-ui", "../../webapp.client", "dev")
       .WithHttpsEndpoint(env: "FE_PORT")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile();

builder.Build().Run();
