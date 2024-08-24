var builder = WebApplication.CreateBuilder(args);

// Injecting Services
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Configure HTTP req pipeline
if(!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
