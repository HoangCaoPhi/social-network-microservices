var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Category_API>("category-api");

builder.AddProject<Projects.Post_API>("post-api");

builder.AddProject<Projects.Comment_API>("comment-api");

builder.AddProject<Projects.Like_API>("like-api");

builder.AddProject<Projects.Notification_API>("notification-api");

builder.AddProject<Projects.webapp_client>("webapp-client");
builder.AddProject<Projects.WebApp_Server>("webapp-server");

builder.Build().Run();
