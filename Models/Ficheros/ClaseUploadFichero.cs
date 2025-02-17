namespace SubirFicheros_ClienteServidor.Models.Ficheros
{
    public class ClaseUploadFichero
    {
        public string Nombre { get; set; } = string.Empty;
        public IFormFile? Fichero { get; set; } = null;
    }

    public class ClaseUploadFicheros
    {
        public string Nombre { get; set; } = string.Empty;
        public IFormFile[] Ficheros { get; set; } = [];
    }
}
