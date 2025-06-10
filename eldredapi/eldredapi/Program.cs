using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRewriter(new Microsoft.AspNetCore.Rewrite.RewriteOptions().AddRedirect("tasks", "/exercises"));
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.Now} - Started]");
    await next();
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.Now} - Finished]");
});

var exercises = new List<Exercise>();

app.MapGet("/", () => "Welcome to eldred api");
app.MapGet("/current-date", () => DateTime.Now);

app.MapPost("/exercise", (Exercise exercise) =>
{
    exercises.Add(exercise);

});

app.MapGet("/exercises", () => exercises.ToList());


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();

public record Exercise
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
    public TimeSpan CurrentTime { get; set; } = DateTime.Now.TimeOfDay;
}
