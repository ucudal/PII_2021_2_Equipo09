using System;
using BotCore.User;
using ClassLibrary.User;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.AceptarInvitacion
{
    public class HandlerValidarCodigo : MessageHandlerBase
    {
        private GestorInvitaciones gi = GestorInvitaciones.Instancia;

        public HandlerValidarCodigo(IMessageHandler next = null)
        : base(new string[] {"CodigoInvitacion"}, next)
        {
        }

        protected override bool InternalHandle(IMessage message, out string response, out string nextHandlerKeyword)
        {
            if (this.CanHandle(message))
            {
                Invitacion invite;
                if (this.gi.ValidarInvitacion(message.TxtMensaje, out invite))
                {
                    this.ContainingForm.Next = new FrmRegistroDatosLogin(invite.OrganizacionInvitada);
                    response = "¡Gracias por aceptar la invitación a unirte a #Nombre del bot#!";
                    nextHandlerKeyword = "Inicio";
                }
                else
                {
                    response = "No se ha podido verificar el código. Por favor, reingrésalo.";
                    nextHandlerKeyword = "CodigoInvitacion";
                }
                return true;
            }
            else
            {
                response = string.Empty;
                nextHandlerKeyword = "CodigoInvitacion";
                return false;
            }
        }
    }
}