namespace SubirFicheros_ClienteServidor.Models.Ficheros
{
    public class ClaseUploadFichero
    {
        public string Nombre { get; set; } = string.Empty;
        public IFormFile? Fichero { get; set; } = null;
    }
}
