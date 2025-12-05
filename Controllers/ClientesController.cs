using Microsoft.AspNetCore.Mvc;
using WebFamilyFrontEnd.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;

public class ClientesController : Controller
{
    private readonly HttpClient _http;

    public ClientesController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("api");
    }

    // GET: /Clientes
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var clientes = await _http.GetFromJsonAsync<List<Cliente>>("clientes");
        return View(clientes);
    }

    // GET: /Clientes/Create
    [HttpGet]
    public IActionResult Create() => View();

    // POST: /Clientes/Create
    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.PostAsJsonAsync("clientes", cliente);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Error = "Erro ao cadastrar cliente";
        return View(cliente);
    }

    // GET: /Clientes/Details/{id}
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var cliente = await _http.GetFromJsonAsync<Cliente>($"clientes/{id}");
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }
}
