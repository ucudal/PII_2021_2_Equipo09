//--------------------------------------------------------------------------------
// <copyright file="Publicador.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
//Patrones utilizados: Creator, SRP y singleton
//Se le delega la tarea de Crear publicaciones para mejorar el SRP de EMpresa, ademas de darle
//la capacidad de crear instancis de publicación (y recurrente).
//Es singleton porque solo se necesita una instancia y almacena un estado.
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using ClassLibrary.LocationAPI;
using ClassLibrary.Publication;
using ClassLibrary.User;
using Importers;

namespace BotCore.Publication
{
    /// <summary>
    /// Clase creadora de instancias y persistidora de publicación.
    /// </summary>
    public class Publicador
    {
        private DataAccess da; 

        private static Publicador instancia;

        /// <summary>
        /// Obtiene la instancia del Singleton.
        /// </summary>
        /// <value><see cref = "Publicador"/>.</value>
        public static Publicador Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Publicador();
                }
                return instancia;
            }
        }
        private Publicador()
        {
            this.da = DataAccess.Instancia;
        }

        /// <summary>
        /// Crea y persiste en memoria la publicación.
        /// </summary>
        /// <param name="residuo"><see cref = "Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword = "double"/>.</param>
        /// <param name="moneda"><see langword = "string"/>.</param>
        /// <param name="cantidad"><see langword = "int"/>.</param>
        /// <param name="lugarRetiro"><see langword = "string"/>.</param>
        /// <param name="vendedor"><see cref = "Empresa"/>.</param>
        /// <param name="descripcion"><see langword = "string"/>.</param>
        /// <param name="categoria"><see langword = "string"/>.</param>
        public void PublicarOferta(Residuo residuo, double precioUnitario, string moneda, int cantidad, Location lugarRetiro, Empresa vendedor, string descripcion, Categoria categoria)
        {
            List<Publicacion> activeOffers = da.Obtener<Publicacion>();
            Publicacion offer = new Publicacion(residuo,precioUnitario,moneda,cantidad,lugarRetiro,vendedor,descripcion,categoria);
            if (!(activeOffers.Contains(offer)))
            {    
                da.Insertar(vendedor.CrearOferta(
                    residuo,
                    precioUnitario,
                    moneda,
                    cantidad,
                    lugarRetiro,
                    descripcion,
                    categoria
                ));
            }
            else
            {
                throw new System.Exception("Esta publicacion ya existe!");
            }
        }

        /// <summary>
        /// Metodo que toma dos publicaciones y sustituye una por otra en la base de datos.
        /// </summary>
        /// <param name="ofertaOld">La oferta vieja</param>
        /// <param name="ofertaNew">la oferta nueva.</param>
        public void ActualizarOferta(Publicacion ofertaOld, Publicacion ofertaNew)
        {
            da.Actualizar(ofertaOld,ofertaNew);
        }

        /// <summary>
        /// Metodo que toma dos publicaciones recurrentes y sustituye una por otra en la base de datos.
        /// </summary>
        /// <param name="ofertaOld">La oferta recurrente vieja</param>
        /// <param name="ofertaNew">la oferta recurrente nueva.</param>
        public void ActualizarOfertaRecurrente(PublicacionRecurrente ofertaOld, PublicacionRecurrente ofertaNew)
        {
            da.Actualizar(ofertaOld,ofertaNew);
        }

        /// <summary>
        /// Crea y persiste en memoria una nueva publicación recurrente.
        /// </summary>
        /// <param name="residuo"><see cref = "Residuo"/>.</param>
        /// <param name="precioUnitario"><see langword = "double"/>.</param>
        /// <param name="moneda"><see langword = "string"/>.</param>
        /// <param name="cantidad"><see langword = "int"/>.</param>
        /// <param name="lugarRetiro"><see langword = "string"/>.</param>
        /// <param name="vendedor"><see cref = "Empresa"/>.</param>
        /// <param name="descripcion"><see langword = "string"/>.</param>
        /// <param name="categoria"><see langword = "Categoria"/>.</param>
        /// <param name="frecuenciaAnualRestock"><see langword = "int"/>.</param>
        public void PublicarOfertaRecurrente(Residuo residuo, double precioUnitario, string moneda, int cantidad, Location lugarRetiro, Empresa vendedor, string descripcion, Categoria categoria, int frecuenciaAnualRestock)
        {
            da.Insertar(vendedor.CrearOfertaRecurrente(
                residuo,
                precioUnitario,
                moneda,
                cantidad,
                lugarRetiro,
                descripcion,
                categoria,
                frecuenciaAnualRestock
            ));
        }
    }
}