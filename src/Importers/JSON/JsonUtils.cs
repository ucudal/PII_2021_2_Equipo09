using ClassLibrary.User;
using ClassLibrary.Publication;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;

namespace Importers.Json
{
    /// <summary>
    /// Clase que exporta objetos de tipo IJsonConvertible a strings en formato JSON.
    /// </summary>
    public class JsonExporter
    {
        private static JsonSerializerOptions config = new JsonSerializerOptions()
        {
            ReferenceHandler = MyReferenceHandler.Instance,
            WriteIndented = true
        };
        private static string filePath = @"data.json";

        /// <summary>
        /// Guarda un objeto que implemente IJsonConvertible en formato JSON.
        /// </summary>
        /// <param name="obj">el objeto a exportar.</param>
        /// <typeparam name="T">tipo del objeto que implementa IJsonConvertible.</typeparam>
        /// <returns>El string JSON que representa este objeto.</returns>
        public void Save<T>(T obj) where T : IJsonConvertible
        {
            string json = JsonSerializer.Serialize(obj, config);
            //string typeName = obj.GetType().ToString();
            //string folderName = typeName.Substring(typeName.LastIndexOf('.') + 1);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Obtiene un objeto del tipo especificado de un archivo en formato JSON.
        /// </summary>
        /// <typeparam name="T">el tipo del objeto obtenido.</typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IJsonConvertible
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }

        /*
        public void Visit(Categoria c)
        {
            this.ExportResult = JsonSerializer.Serialize(c);
        }

        public void Visit(Publicacion p)
        {
            this.ExportResult = JsonSerializer.Serialize(p);
        }

        public void Visit(PublicacionRecurrente pr)
        {
            this.ExportResult = JsonSerializer.Serialize(pr);
        }

        public void Visit(Residuo r)
        {
            this.ExportResult = JsonSerializer.Serialize(r);
        }

        public void Visit(Venta v)
        {
            this.ExportResult = JsonSerializer.Serialize(v);
        }

        public void Visit(DatosLogin dl)
        {
            this.ExportResult = JsonSerializer.Serialize(dl);
        }

        public void Visit(Empresa e)
        {
            this.ExportResult = JsonSerializer.Serialize(e);
        }

        public void Visit(Emprendedor e)
        {
            this.ExportResult = JsonSerializer.Serialize(e);
        }

        public void Visit(Habilitacion h)
        {
            this.ExportResult = JsonSerializer.Serialize(h);
        }

        public void Visit(Invitacion i)
        {
            this.ExportResult = JsonSerializer.Serialize(i);
        }
        */
    }
}