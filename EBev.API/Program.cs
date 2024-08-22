using EBev.API.Extension;

var builder = WebApplication
                .CreateBuilder(args);

builder
    .Services.ConfigureServices(builder);

var app = builder.Build();

app.Configure(builder);

app.Run();