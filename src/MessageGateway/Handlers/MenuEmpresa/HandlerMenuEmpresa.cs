//--------------------------------------------------------------------------------
// <copyright file="HandlerMenuEmpresa.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.MenuEmpresa
{

  /// <summary>
  /// Handler principal del menu de empresa.
  /// </summary>
  public class HandlerMenuEmpresa : MessageHandlerBase
  {

    /// <summary>
    /// El constructor, reacciona a la palabra clave MenuEmpresa.
    /// </summary>
    /// <param name="next">IHandler siguiente</param>
    public HandlerMenuEmpresa(IMessageHandler next = null)
    : base(new string[] {"menuempresa", "menu"}, next)
    {
    }

    /// <summary>
    /// Método InternalHandle que devuelve un menú y deriva a la opción seleccionada.
    /// </summary>
    /// <param name="message">IMessage traido del form.</param>
    /// <param name="response">String de la respuesta al usuario.</param>
    /// <returns>True: si se pudo manejar.</returns>
    protected override bool InternalHandle(IMessage message, out string response)
    {
      if (this.CanHandle(message) || (CurrentForm as FrmMenuEmpresa).CurrentState == faseMenuEmpresa.Inicio)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendJoin('\n',
        "Estas son las diferentes acciones que puedes realizar:",
        "Escribe \"Menu\" si desea ver este mensaje de nuevo luego",
        "\n",
        "1. Crear publicación",
        "2. Modificar publicaciones",
        "3. Generar reportes",
        "4. Cerrar Sesión",
        "Si quiere cancelar un proceso escriba: /abortar");

        (CurrentForm as FrmMenuEmpresa).CurrentState = faseMenuEmpresa.Eligiendo;
        response = sb.ToString();
        return true;
      }
      else
      {
        response = string.Empty;
        return false;
      }
    }

    /// <summary>
    /// Las fases del menú de empresa.
    /// </summary>
    public enum faseMenuEmpresa
    {
      ///Iniciado el menu.
      Inicio,

      ///Se esta eligiendo una opción.
      Eligiendo

    }
  }
}