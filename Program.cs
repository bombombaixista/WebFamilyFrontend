var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// HttpClient apontando para o backend (lendo do appsettings.json)
builder.Services.AddHttpClient("api", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

    if (string.IsNullOrEmpty(baseUrl))
        throw new InvalidOperationException("ApiSettings:BaseUrl não configurado no appsettings.json");

    client.BaseAddress = new Uri(baseUrl);
});

// Session para guardar token
builder.Services.AddSession();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar Session
app.UseSession();

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clientes}/{action=Index}/{id?}");

app.Run();
