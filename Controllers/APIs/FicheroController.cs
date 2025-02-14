using Microsoft.AspNetCore.Mvc;
using SubirFicheros_ClienteServidor.Models.Ficheros;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SubirFicheros_ClienteServidor.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class FicheroController : ControllerBase
    {
        private readonly IConfiguration _configuracion;

        public FicheroController(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        // GET: api/<FicheroController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FicheroController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("DescargarImagen/{imagen}")]
        public IActionResult DescargarImagen(string imagen)
        {
            var pathrutaficheros = _configuracion.GetValue<string>("RutaFicheros");

            var path = Path.Combine(pathrutaficheros, imagen);
            if (System.IO.File.Exists(path))
            {
                var bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/octet-stream", imagen);
            }
            else
            {
                return NotFound();
            }
        }

        public string ObtenerExtensionFichero(string nombrefichero)
        {
            if (string.IsNullOrEmpty(nombrefichero))
            {
                throw new ArgumentException("El nombre del fichero no puede ser nulo o vacío", nameof(nombrefichero));
            }

            return Path.GetExtension(nombrefichero);
        }

        // POST api/<FicheroController>
        [HttpPost]
        public IActionResult Post(ClaseUploadFichero parametros)
        {
            if (parametros.Fichero != null)
            {
                var pathrutaficheros = _configuracion.GetValue<string>("RutaFicheros");

                var path = Path.Combine(pathrutaficheros, parametros.Nombre + ObtenerExtensionFichero(parametros.Fichero.FileName));

                // Borramos el fichero si existe
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    parametros.Fichero.CopyTo(stream);
                }

                return Ok("/api/fichero/DescargarImagen/" + parametros.Nombre + ObtenerExtensionFichero(parametros.Fichero.FileName));
            }

            return NoContent();
        }

        // PUT api/<FicheroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FicheroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
