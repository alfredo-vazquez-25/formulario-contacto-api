using FormularioContactoAPI.Data;
using FormularioContactoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FormularioContactoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FormularioContactoController(ApplicationDbContext context)
    {
        _context = context;
    }

    //[HttpPost]
    //public async Task<IActionResult> Submit([FromForm] FormularioContacto formularioContacto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    // Guardar datos en la base de datos
    //    _context.FormularioContactos.Add(formularioContacto);
    //    await _context.SaveChangesAsync();

    //    // Manejo del archivo adjunto
    //    if (formularioContacto.Adjunto != null)
    //    {
    //        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

    //        if (!Directory.Exists(uploadsFolderPath))
    //        {
    //            Directory.CreateDirectory(uploadsFolderPath);
    //        }

    //        var filePath = Path.Combine(uploadsFolderPath, Path.GetFileName(formularioContacto.Adjunto.FileName));

    //        using (var stream = new FileStream(filePath, FileMode.Create))
    //        {
    //            formularioContacto.Adjunto.CopyTo(stream);
    //        }
    //    }

    //    return Ok(new { message = "Formulario enviado correctamente" });
    //}
    [HttpPost]
    public async Task<IActionResult> GuardarFormularioContacto([FromForm] FormularioContacto formularioContacto)
    {
        byte[]? adjunto = null;

        if (formularioContacto.Adjunto != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formularioContacto.Adjunto.CopyToAsync(memoryStream);
                adjunto = memoryStream.ToArray();
            }
        }

        await _context.GuardarFormularioContacto(formularioContacto.Email, formularioContacto.Nombres, formularioContacto.Apellidos, formularioContacto.Comentarios, adjunto);

        return Ok(new { Message = "Formulario guardado con éxito." });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FormularioContacto>> ObtenerFormularioContactoPorId(int id)
    {
        var formulario = await _context.ObtenerFormularioContactoPorId(id);

        if (formulario == null)
        {
            return NotFound();
        }

        return formulario;
    }
}
