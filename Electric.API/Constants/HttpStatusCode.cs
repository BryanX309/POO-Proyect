namespace Electric.API.Constants
{
    public class HttpStatusCode
    {
        /// <summary>
        /// La solicitud se realizó con éxito.
        /// </summary>
        public const int OK = 200;

        /// <summary>
        /// La solicitud se completó y se creó un nuevo recurso.
        /// </summary>
        public const int CREATED = 201;

        /// <summary>
        /// La solicitud tuvo éxito, pero no hay contenido para devolver.
        /// </summary>
        public const int NO_CONTENT = 204;

        /// <summary>
        /// El servidor no puede procesar la solicitud debido a un error del cliente.
        /// </summary>
        public const int BAD_REQUEST = 400;

        /// <summary>
        /// La solicitud requiere autenticación del usuario.
        /// </summary>
        public const int UNAUTHORIZED = 401;

        /// <summary>
        /// El cliente no tiene permisos para acceder al recurso solicitado.
        /// </summary>
        public const int FORBIDDEN = 403;

        /// <summary>
        /// El recurso solicitado no se pudo encontrar en el servidor.
        /// </summary>
        public const int NOT_FOUND = 404;

        /// <summary>
        /// La solicitud no se pudo completar debido a un conflicto con el estado actual del recurso.
        /// </summary>
        public const int CONFLICT = 409;

        /// <summary>
        /// La solicitud estaba bien formada, pero contiene errores semánticos o de validación.
        /// </summary>
        public const int UNPROCESSABLE_ENTITY = 422;

        /// <summary>
        /// El servidor se encontró con una condición inesperada que le impidió completar la solicitud.
        /// </summary>
        public const int INTERNAL_SERVER_ERROR = 500;

        /// <summary>
        /// El servidor no soporta la funcionalidad requerida para completar la solicitud.
        /// </summary>
        public const int NOT_IMPLEMENTED = 501;

        /// <summary>
        /// El servidor no está listo para manejar la solicitud por mantenimiento o sobrecarga.
        /// </summary>
        public const int SERVER_UNAVAILABLE = 503;
    }
}