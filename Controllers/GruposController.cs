using Microsoft.AspNetCore.Mvc;
using WebFamilyFrontEnd.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;

public class GruposController : Controller
{
    private readonly HttpClient _http;

    public GruposController(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("api");
    }

    // GET: /Grupos
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var grupos = await _http.GetFromJsonAsync<List<Grupo>>("grupos");
        return View(grupos);
    }

    // GET: /Grupos/Create
    [HttpGet]
    public IActionResult Create() => View();

    // POST: /Grupos/Create
    [HttpPost]
    public async Task<IActionResult> Create(Grupo grupo)
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.PostAsJsonAsync("grupos", grupo);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Error = "Erro ao cadastrar grupo";
        return View(grupo);
    }

    // GET: /Grupos/Details/{id}
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var grupo = await _http.GetFromJsonAsync<Grupo>($"grupos/{id}");
        if (grupo == null)
        {
            return NotFound();
        }

        return View(grupo);
    }
}
