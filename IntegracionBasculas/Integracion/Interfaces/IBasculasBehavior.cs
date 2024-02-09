namespace Integracion.Interfaces
{
    /// <summary>
    /// Autor            : Andrés Giraldo
    /// Fecha de Creación: 28-Nov/2019
    /// Descripción      : Esta interfaz implementa el comportamiento del adaptador de una báscula; las operaciones que se pueden realizar con la báscula
    /// </summary>
    public interface IBasculasBehavior
    {
        /// <summary>
        /// Esta operación permite obtener el peso registrado en la báscula
        /// </summary>
        /// <returns>Peso registrado en la báscula</returns>
        float LeerPeso();
    }
}