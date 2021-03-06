//--------------------------------------------------------------------------------
// <copyright file="Categoria.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using Importers;
using Importers.Json;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Instancia de las categorias posibles para <see cref="Residuo"/>.
  /// </summary>
  public class Categoria : JsonConvertibleBase
  {
    /// <summary>
    /// Constructor de categoria.
    /// </summary>
    /// <param name="nombre">.</param>
    public Categoria(string nombre)
    {
      this.Nombre = nombre;
    }

    /// <summary>
    /// Obtiene o establece el nombre de la categoría.
    /// </summary>
    /// <value><see langword="string"/>.</value>
    public string Nombre { get; set; }
    
    /// <summary>
    /// Metodo para guardar en Json.
    /// </summary>
    /// <param name="exporter"></param>
    public override void JsonSave(JsonExporter exporter)
    {
        exporter.Save(this);
    }
  }
}