//--------------------------------------------------------------------------------
// <copyright file="Location.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

namespace ClassLibrary.LocationAPI
{
    /// <summary>
    /// Representa las coordenadas y otros datos de la ubicación de una dirección retornada en el método.
    /// <see cref="LocationApiClient.GetLocationAsync(string, string, string, string)"/>.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Obtiene o establece un valor que indica si se encontró o no la dirección. En ese caso son válidos los demás
        /// valores. En caso contrario los demás valores son indeterminados.
        /// </summary>
        /// <value>true si se encontró la dirección; false en caso contrario.</value>
        public bool Found { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección: calle y número, ruta y kilómetro, etc.
        /// </summary>
        /// <value>Por ejemplo, Avenida 8 de Octubre 2738.</value>
        public string AddresLine { get; set; }

        /// <summary>
        /// Obtiene o establece el país.
        /// </summary>
        /// <value>Por ejemplo, Uruguay.</value>
        public string CountryRegion { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección completa, incluyendo ciudad, código postal, etc.
        /// </summary>
        /// <value>Por ejemplo, Avenida 8 de Octubre 2738, Montevideo, 11200, Uruguay.</value>
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Obtiene o establece la localidad o ciudad.
        /// </summary>
        /// <value>Por ejemplo, Montevideo.</value>
        public string Locality { get; set; }

        /// <summary>
        /// Obtiene o establece el código postal.
        /// </summary>
        /// <value>Por ejemplo, 11200.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Obtiene o establece la latitud de la dirección.
        /// </summary>
        /// <value>El valor de la latitud en formato decimal.</value>
        public float Latitude { get; set; }

        /// <summary>
        /// Obtiene o establece la longitud de la dirección.
        /// </summary>
        /// <value>El valor de la longitud en formato decimal.</value>
        public float Longitude { get; set; }

        /// <summary>
        /// Definicion de operador entre Locations, permite compararlos de manera significativa
        /// (es decir, a partir de sus coordenadas).
        /// </summary>
        /// <param name="a">Location.</param>
        /// <param name="b">Location.</param>
        /// <returns>True: si son iguales las coordenadas.</returns>
        public static bool operator == (Location a, Location b)
        {
            if ((object) a == null && (object) b == null)
            {
                return true;
            }

            if ((object) a == null ^ (object) b == null)
            {
                return false;
            }

            return a.Latitude == b.Latitude && a.Longitude == b.Longitude;
        }

        /// <summary>
        /// Operador de diferencia entre Locations (a partir de sus coordenadas).
        /// </summary>
        /// <param name="a">Location.</param>
        /// <param name="b">Location.</param>
        /// <returns>True: si son distintas las coordenadas.</returns>
        public static bool operator != (Location a, Location b)
        {
            return !(a == b);
        }
    }
}