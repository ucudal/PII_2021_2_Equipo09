using System.Collections.Generic;
using MessageGateway.Handlers;
using MessageGateway;

namespace MessageGateway.Forms
{
    public abstract class FormularioBase : IFormulario
    {
        private IGateway gateway = AdaptadorTelegram.Instancia;

        private Dictionary<string, string> referenciaComandos;

        private IMessageHandler _messageHandler;
        protected IMessageHandler messageHandler {
            get
            {
                return _messageHandler;
            }
            set
            {
                this._messageHandler = value;
                IMessageHandler singleHandler = value;
                do
                {
                    singleHandler.ContainingForm = this;
                    singleHandler = singleHandler.Next;
                }
                while (singleHandler != null);
            }
        }

        private IFormulario next;
        public IFormulario Next
        {
            get { return next; }
            set {
                this.next = value;
                gateway.CurrentForm = value;
            }
        }

        public PalabrasClaveHandlers NextMessageKeyword { private get; set; }

        protected FormularioBase(Dictionary<string, string> referenciaComandos)
        {
            this.referenciaComandos = referenciaComandos;
            this.NextMessageKeyword = PalabrasClaveHandlers.Inicio;
        }


        public string ReceiveMessage(IMessage message)
        {
            IMessage mensajeTraducido = new MessageBase(
                message.ChatID,
                this.TraducirCodigo(message.TxtMensaje),
                this.NextMessageKeyword
            );

            string respuesta;
            PalabrasClaveHandlers palabraClaveSiguienteManejador;
            IMessageHandler manejadorUtilizado = this.messageHandler.Handle(
                mensajeTraducido,
                out respuesta,
                out palabraClaveSiguienteManejador
            );

            if (manejadorUtilizado != null)
            {
                this.NextMessageKeyword = palabraClaveSiguienteManejador;
                return respuesta;
            }
            else
            {
                return "El mensaje no pudo ser procesado.";
            }
        }

        private string TraducirCodigo(string codigo)
        {
            if (this.referenciaComandos.ContainsKey(codigo))
            {
                return this.referenciaComandos[codigo];
            }
            else
            {
                return codigo;
            }
        }
    }
}