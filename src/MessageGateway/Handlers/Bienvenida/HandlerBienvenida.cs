//--------------------------------------------------------------------------------
// <copyright file="HandlerBienvenida.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Text;
using MessageGateway.Forms;

namespace MessageGateway.Handlers.Bienvenida
{

    /// <summary>
    /// Handler principal de bienvenida al user.
    /// </summary>
    public class HandlerBienvenida : MessageHandlerBase
    {

        /// <summary>
        /// Cosntructor que reacciona al comando de inicialización de bot /start.
        /// </summary>
        /// <param name="next">IHandler siguiente</param>
        public HandlerBienvenida(IMessageHandler next)
        : base(new string[] {"/start"}, next)
        {
        }

        /// <summary>
        /// Internal Handle que devuelve un menu y redirecciona a la opción seleccionada.
        /// </summary>
        /// <param name="message">IMessage traido del Form.</param>
        /// <param name="response">String respuesta al user.</param>
        /// <returns>True: si se pudo manejar.</returns>
        protected override bool InternalHandle(IMessage message, out string response)
        {
            if (this.CanHandle(message) && (CurrentForm as FrmBienvenida).CurrentState == faseWelcome.Inicio)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendJoin('\n',
                $"¡Bienvenido a {AdaptadorTelegram.Instancia.TelegramBot.BotName}, nuestra plataforma de economía circular!",
                "Por favor, selecciona una de las opciones para ingresar: ",
                "\n",
                "1. Iniciar sesión",
                "2. Registrarse",
                "3. Tengo un link de invitación");
                response = sb.ToString();

                (CurrentForm as FrmBienvenida).CurrentState = faseWelcome.Eligiendo;
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// Las fases necesarias de la bienenida.
        /// </summary>
        public enum faseWelcome
        {
            ///Recien inicalizado bot.
            Inicio,

            ///Eligiendo del menu
            Eligiendo,

            ///Eligiendo registrarse como empresa o emprendedor.
            choosingRegister

        }
    }
}