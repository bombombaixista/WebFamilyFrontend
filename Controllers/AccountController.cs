using Microsoft.AspNetCore.Mvc;
using WebFamilyFrontEnd.Models;
using System.Net.Http.Json;
using System.Text.Json;

public class AccountController : Controller
{
    private readonly HttpClient _http;

    public AccountController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("api");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _http.PostAsJsonAsync("login", request);
        var raw = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Resposta: {raw}");

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<LoginResponse>(raw);
            if (!string.IsNullOrEmpty(result?.Token))
            {
                HttpContext.Session.SetString("token", result.Token);
                return RedirectToAction("Index", "Clientes");
            }
        }

        ViewBag.Error = "Login inválido";
        return View(request);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("token");
        return RedirectToAction("Login");
    }
}
