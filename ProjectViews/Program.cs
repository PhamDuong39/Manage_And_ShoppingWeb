var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Default endpoint
    endpoints.MapAreaControllerRoute(
        name: "user",
        areaName: "User",
        pattern: "User/{controller=Home}/{action=Index}/{id?}"
    );

    // Admin endpoint
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );

    // Catch-all for User area
    endpoints.MapAreaControllerRoute(
        name: "user_area",
        areaName: "User",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
    );

    // Catch-all for other areas
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
