//--------------------------------------------------------------------------------
// <copyright file="Venta.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright> 
//
// Patrón utilizado: Low coupling.
//Esta clase ayuda a vincular una Empresa y un Emprendedor con una publicación de por medio.
//--------------------------------------------------------------------------------

using ClassLibrary.User;
using System;
using System.Text;
using Importers.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary.Publication
{
  /// <summary>
  /// Clase que reune las compras de <see cref = "Publicacion"/> y los implicados, y se encarga de hacer la compra en sí.
  /// Implementa <see iref = "IPrintable"/>.
  /// </summary>
  public class Venta : JsonConvertibleBase, IPrintable
  {
    /// <summary>
    /// Se crea la instancia de venta con la fecha del momento.
    /// </summary>
    /// <param name="comprador"><see cref = "Emprendedor"/>.</param>
    /// <param name="publicacion"><see cref = "Publicacion"/>.</param>
    public Venta(Emprendedor comprador, Publicacion publicacion)
    {
      this.Comprador = comprador;
      this.Publicacion = publicacion;
      this.Fecha = DateTime.Now;
    }

    /// <summary>
    /// Cosntructor de Json.
    /// </summary>
    [JsonConstructor]
    public Venta()
    {

    }

    /// <summary>
    /// Obtiene o establece la fecha de venta.
    /// </summary>
    /// <value><see cref = "DateTime"/>.</value>
    
    [JsonInclude]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Obtiene o establece el comprador.
    /// </summary>
    /// <value><see cref = "Emprendedor"/>.</value>
    [JsonInclude]
    public Emprendedor Comprador { get; set; }

    /// <summary>
    /// Obtiene o establece la publicacion que fue comprada.
    /// </summary>
    /// <value><see cref = "Publicacion"/>.</value>
    [JsonInclude]
    public Publicacion Publicacion { get; set; }

    /// <summary>
    /// Implementacion del tipo <see iref = "IPrintable"/>.
    /// </summary>
    /// <returns><see langword="string"/>.</returns>
    public string GetTextToPrint() 
    {
      StringBuilder text = new StringBuilder();
      text.AppendLine($"Material: {this.Publicacion.Residuo.Descripcion} ({this.Publicacion.Cantidad} {this.Publicacion.Residuo.UnidadMedida})");
      text.AppendLine($"Vendedor: {this.Publicacion.Vendedor.Nombre}");
      text.AppendLine($"Comprador: {this.Comprador.Nombre}");
      text.AppendLine($"Precio total: {this.Publicacion.Moneda} {this.Publicacion.PrecioUnitario * this.Publicacion.Cantidad}");
      return text.ToString();
    }

    /// <summary>
    /// Metodo para guardar en json.
    /// </summary>
    /// <param name="exporter"></param>
    public override void JsonSave(JsonExporter exporter)
    {
        exporter.Save(this);
    }
  }
}