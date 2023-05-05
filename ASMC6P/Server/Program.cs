using ASMC6P.Server.Extensions.DependencyInjection;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddOData(opt =>
{
    opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5000).AddRouteComponents("odata", GetEdmModel());
    opt.TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddRazorPages();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseHealthChecks("/health");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();

    odataBuilder.EntityType<UserDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<UserDto>("Identity");

    odataBuilder.EntityType<ProductEntity>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<ProductEntity>("Product");

    odataBuilder.EntityType<CartItemEntity>().HasKey(entity => new { entity.ProductId });
    odataBuilder.EntitySet<CartItemEntity>("CartItemEntity");


    odataBuilder.EntityType<CategoryEntity>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<CategoryEntity>("Category");

    odataBuilder.EntityType<OrderOverviewDto>().HasKey(entity => new { entity.Id });
    odataBuilder.EntitySet<OrderOverviewDto>("OrderOverviewDto");

    return odataBuilder.GetEdmModel();
}