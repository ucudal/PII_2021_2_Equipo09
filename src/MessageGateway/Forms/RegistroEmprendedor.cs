//--------------------------------------------------------------------------------
// <copyright file="RegistroEmprendedor.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.Publication;
using ClassLibrary.LocationAPI;
using ClassLibrary.User;
using MessageGateway.Handlers;

namespace MessageGateway.Forms
{
    /// <summary>
    /// Formulario que recopilara la información necesaria para registrar un emprendedor.
    /// </summary>
    public class FrmRegistroEmprendedor : FormularioBase, IFormulario, ILocationForm
    {
        /// <summary>
        /// Nombre del emprendedor.
        /// </summary>
        public string Nombre;

        /// <summary>
        /// Ubicación del emprendedor.
        /// </summary>
        /// <value><see cref = "Location" />.</value>
        public Location Ubicacion
        {
            get
            {
                return LocationApiClient.Instancia.GetLocation(direccion,city,dpto);
            }
        }

        /// <summary>
        /// Rubro del emprendedor.
        /// </summary>
        public string Rubro;

        /// <summary>
        /// Especialización del emprendedor.
        /// </summary>
        public string Especializacion;

        /// <summary>
        /// Habilitaciones del emprendedor.
        /// </summary>
        /// <value>List de Habilitacion</value>
        public List<Habilitacion> Habilitaciones;

        /// <summary>
        /// Constructor del formulario de registro de emprendedores con sus handlers.
        /// </summary>
        public FrmRegistroEmprendedor()
        {
            this.messageHandler =
            new HandlerRegistroEmprendedor(
                new HandlerLocation(
                    new HandlerEscape(null)
                )
            );
        }

        /// <summary>
        /// El estado del formulario, dado por su handler principal.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerRegistroEmprendedor.fases.</value>
        public HandlerRegistroEmprendedor.fases CurrentState {get; set;}

        /// <summary>
        /// El estado del formulario respecto la construccion de Location.
        /// </summary>
        /// <value><see langword = "enum"/> de HandlerLocation.faseLocation.</value>
        public HandlerLocation.faseLocation CurrentStateLocation { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string city { get; set; }

        /// <summary>
        /// Departamento donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string dpto { get; set; }

        /// <summary>
        /// Dirección donde se encuentra el emprendedor.
        /// </summary>
        /// <value>String.</value>
        public string direccion { get; set; }
    }
}