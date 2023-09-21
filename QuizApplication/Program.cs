using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using QuizApplication.Hubs;


void MutateCorsPolicy(CorsOptions options)
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
}


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));
builder.Services.AddCors(MutateCorsPolicy);
var app = builder.Build();
app.UseCors("CorsPolicy");
app.MapHub<ChatHub>("/chat");
app.MapHub<QuizHub>("/quiz");
await app.RunAsync();



