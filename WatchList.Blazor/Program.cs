using Bogus;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WatchList.DependencyInjection;
using WatchList.Domain.WatchItems;
using WatchList.Domain.WatchItems.Entity;
using WatchList.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
keepAliveConnection.Open();
builder.Services.AddAppServices(dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlite(keepAliveConnection));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    
    var faker = new Faker<WatchList.Domain.WatchItems.Entity.WatchItem>()
        .RuleFor(e => e.Id, f => f.Random.Guid())
        .RuleFor(e => e.Title, f => f.Commerce.Product())
        .RuleFor(e => e.Description, f => f.Lorem.Paragraphs().OrNull(f))
        .RuleFor(e => e.Status, f => f.PickRandom(Status.List.Skip(1)))
        .RuleFor(e => e.Type, f => f.PickRandom(WatchItemType.List.Skip(1)))
        .RuleFor(e => e.Rating, f => new Rating(f.Random.Float(0, 10)))
        .RuleFor(e => e.Genre, f =>
        {
            return Enumerable.Range(0, f.Random.Number(1, 5))
                .Select(_ => f.Music.Genre())
                .ToHashSet();
        });
    
    db.WatchItems.AddRange(faker.GenerateForever().Take(30));
    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();