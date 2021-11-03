//--------------------------------------------------------------------------------
// <copyright file="GestorInvitaciones.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System;
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
        private GestorInvitaciones(){}
        private static GestorInvitaciones instancia {get;set;}

        //Lo hago singleton porque solo se precisa una instancia y tiene que guardar un estado (los invites enviados)
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

        public IGateway GatewayMensajes {get; set;}

        /// <summary>
        /// Lista donde se almacenan las invitaciones enviadas para mantener un registro.
        /// </summary>
        public List<Invitacion> InvitacionesEnviadas = new List<Invitacion>();
        public void EnviarInvitacion<T>(string destinatario, string nombreTemp) where T : IUsuario, new()
        {
            if (this.GatewayMensajes == null)
            {
                throw new InvalidOperationException("No se ha establecido el gateway para el envío de mensajes.");
            }

            IUsuario user = new T();
            user.Nombre = nombreTemp;
            Invitacion invite = new Invitacion(user, destinatario);

            GatewayMensajes.EnviarInvitacion(destinatario, invite.ArmarMensajeInvitacion());

            this.InvitacionesEnviadas.Add(invite);
        }

        // Este método es usado externamente por el MessageGateway
        public Invitacion ValidarInvitacion(string usuarioAceptante, string enlace) 
        {
            Invitacion invite = this.InvitacionesEnviadas.Where(
                (Invitacion i) => i.Destinatario == usuarioAceptante && i.Link == enlace && !i.fueAceptada
            ).SingleOrDefault();

            if (invite != null) {
                invite.Aceptar();
                return invite;
            }
            else {
                return null;
            }
        }
    }
}