using FormularioContactoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FormularioContactoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FormularioContacto> FormularioContactos { get; set; }
        public DbSet<LanguageText> LanguageTexts { get; set; }

        //public async Task GuardarFormularioContacto(string email, string nombres, string apellidos, string comentarios, byte[] adjunto)
        //{
        //    await Database.ExecuteSqlRawAsync("EXEC spGuardarFormularioContacto @p0, @p1, @p2, @p3, @p4", email, nombres, apellidos, comentarios, adjunto);
        //}
        public async Task GuardarFormularioContacto(string email, string nombres, string apellidos, string comentarios, byte[]? adjunto)
        {
            //await Database.ExecuteSqlRawAsync("EXEC spGuardarFormularioContacto @Email, @Nombres, @Apellidos, @Comentarios, @Adjunto",
            //new SqlParameter("@Email", email),
            //new SqlParameter("@Nombres", nombres),
            //new SqlParameter("@Apellidos", apellidos),
            //new SqlParameter("@Comentarios", comentarios),
            //new SqlParameter("@Adjunto", adjunto));
            var parameters = new[]
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Nombres", nombres),
                new SqlParameter("@Apellidos", apellidos),
                new SqlParameter("@Comentarios", comentarios),
                new SqlParameter("@Adjunto", adjunto ?? Array.Empty<byte>())
            };

            // Ejecutar el procedimiento almacenado
            await Database.ExecuteSqlRawAsync("EXEC spGuardarFormularioContacto @Email, @Nombres, @Apellidos, @Comentarios, @Adjunto", parameters);

        }


        // Método para ejecutar el stored procedure de obtener por ID
        public async Task<FormularioContacto> ObtenerFormularioContactoPorId(int id)
        {
            var formulario = await FormularioContactos.FromSqlRaw("EXEC spObtenerFormularioContactoPorId @p0", id).FirstOrDefaultAsync();

            if (formulario == null)
            {
                // Aquí puedes manejar el caso en que no se encuentra el formulario, lanzar una excepción, etc.
                return null; // o lanzar una excepción si prefieres
            }

            return formulario;
        }
    }
}
