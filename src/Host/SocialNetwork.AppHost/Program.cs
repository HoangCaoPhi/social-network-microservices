var builder = DistributedApplication.CreateBuilder(args);

// Identity 
var keycloak = builder.AddKeycloak("keycloak",
                                    8080,
                                    adminPassword: builder.AddParameter("AdminPasswordKeycloak"),
                                    adminUsername: builder.AddParameter("AdminUserNameKeyclock"))
                      .WithDataVolume();

// Database
var sql = builder
            .AddSqlServer("sql", port: 1434)
            .WithImageTag("latest")
            .WithDataVolume();

var categorydb = sql.AddDatabase("categorydb");
 
builder.AddProject<Projects.ApiGateway>("apigateway")
       .WithReference(keycloak);

builder.AddProject<Projects.webapp_client>("webapp-client");

builder.AddProject<Projects.Category_API>("category-api")
       .WithReference(categorydb)
       .WithReference(keycloak);

builder.AddProject<Projects.Post_API>("post-api");

builder.AddProject<Projects.Comment_API>("comment-api");

builder.AddProject<Projects.Like_API>("like-api");

builder.AddProject<Projects.Notification_API>("notification-api");

builder.Build().Run();
