//--------------------------------------------------------------------------------
// <copyright file="GestorInvitaciones.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using ClassLibrary.User;
using MessageGateway;

namespace BotCore.User
{
    /// <summary>
    /// Clase que se encarga de generar usuarios temporales y
    /// enviarselo a personas para facilitar su registro
    /// de manera personal.
    /// </summary>
    public class GestorInvitaciones
    {
        private GestorInvitaciones()
        {
        }
        
        /// <summary>
        /// Lista donde se almacenan las invitaciones enviadas para mantener un registro.
        /// </summary>
        private List<Invitacion> invitacionesEnviadas = new List<Invitacion>();

        /// <summary>
        /// Metodo de acceso al singleton.
        /// </summary>
        /// <value></value>
        public static GestorInvitaciones Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorInvitaciones();
                }

                return instancia;
            }
        }

        private static GestorInvitaciones instancia { get; set; }

        // Lo hago singleton porque solo se precisa una instancia y tiene que guardar un estado (los invites enviados)

        /// <summary>
        /// Metodo que crea la <see cref = "Invitacion"/> y la envia al destinatario especificado.
        /// </summary>
        /// <param name="destinatario">El contacto objetivo (username).</param>
        /// <param name="nombreTemp">Nombre placeholder para el IUsuario, el destinatario lo sobreescribirá luego.</param>
        /// <typeparam name="T"></typeparam>
        public void EnviarInvitacion<T>(string destinatario, string nombreTemp)
        where T : IUsuario, new()
        {
            IUsuario user = new T();
            user.Nombre = nombreTemp;
            Invitacion invite = new Invitacion(user, destinatario);

            TelegramService.Instancia.EnviarMensaje(destinatario, invite.ArmarMensajeInvitacion());

            this.invitacionesEnviadas.Add(invite);
        }

        // Este método es usado externamente por el MessageGateway
        private bool ValidarInvitacion(string usuarioAceptante, string enlace)
        {
            Invitacion invite = this.invitacionesEnviadas.Where((Invitacion i) => i.Destinatario == usuarioAceptante && i.Link == enlace && !i.FueAceptada).SingleOrDefault();

            if (invite != null)
            {
                invite.Aceptar();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}