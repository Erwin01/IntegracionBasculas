using System;
using System.Net;
using System.Net.Sockets;

namespace Integracion.CustomEventArgs
{
    /// <summary>
    /// Autor            : Andrés Giraldo
    /// Fecha de Creación: 05-Dic/2019
    /// Descripción      : Esta clase es la implementación de los argumentos personalizados del evento ObtenerPeso. Requerido en las lecturas de peso en las básculas por IP.
    /// </summary>
    public class ObtenerPesoIPEventArgs : EventArgs
    {
        public float Peso { get; set; }
    }
}