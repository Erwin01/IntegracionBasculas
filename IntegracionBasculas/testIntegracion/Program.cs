using Integracion.Adapter;
using Integracion.CustomEventArgs;
using Integracion.Factory;
using System;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
//using static DibalImage.ImagePalletized;

namespace testIntegracion
{
    class Program
    {
        static void Main(string[] Args)
        {

            #region PruebaLecturaArchivoPlano
            var _fileSystemWatcher = new FileSystemWatcher();

            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;

            _fileSystemWatcher.Path = @"C:\Users\pcc\Desktop\MISTIQUETS\";
            //C:\Users\pcc\Desktop\MISTIQUETS
            _fileSystemWatcher.EnableRaisingEvents = true;
            Console.WriteLine("Buscando archivo...");
            //Console.WriteLine("Presione cualquier tecla para continuar");//opcional

            Console.ReadLine();
            #endregion


            #region Prueba Adaptador Serial Avery
            //BasculasSerialesFactory factory = new BasculasSerialesAveryE1010();

            //BasculasSerialesAdapter adapter = factory.GetAdapter();

            //adapter.AbrirPuerto("COM3", 9600, Parity.None, 8);

            //adapter.ObtenerPeso += Adapter_ObtenerPeso;

            //Thread.Sleep(10000);

            //adapter.CerrarPuerto();

            //adapter.ObtenerPeso -= Adapter_ObtenerPeso;
            #endregion


            #region Prueba Adaptador IP Avery
            BasculasIPFactory factory = new BasculasIPAveryZM201();

            BasculasIPAdapter adapter = factory.GetAdapter();

            adapter.ObtenerPeso += AdapterIP_ObtenerPeso;

            adapter.AbrirEndPoint("192.168.1.253", 3000);

            //string peso = adapter.LeerPeso();

            Thread.Sleep(10000);

            //peso = adapter.LeerPeso();

            adapter.CerrarEndPoint();

            adapter.ObtenerPeso -= AdapterIP_ObtenerPeso;
            #endregion
        }

        private static void AdapterIP_ObtenerPeso(object sender, ObtenerPesoIPEventArgs e)
        {
            Console.WriteLine(string.Format("El peso obtenido es: {0}", e.Peso));
        }

        private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Se han actualizado los datos - {e.Name}");
        }

        /// <summary>
        /// Este método es el manejador de eventos de ObtenerPeso para el Adaptador Serial Avery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Adapter_ObtenerPeso(object sender, ObtenerPesoSerialEventArgs e)
        {
            Console.WriteLine(string.Format("El peso obtenido es: {0}", e.Peso));
        }

        private static void PuntoConexionRedAveryZM201()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("172.50.0.146"), 3000);

            if (iPEndPoint == null)
            {
                throw new Exception(string.Format("No posee una conexión. Se recibió: {0}\n", iPEndPoint));
            }

            TcpListener tcpListener = new TcpListener(iPEndPoint);

            tcpListener.Start();

            TcpClient tcpClient = tcpListener.AcceptTcpClient();

            if (!tcpClient.Connected)
            {
                throw new Exception(string.Format("NO ESTA CONECTADO A LA RED: {0}\n", iPEndPoint));
            }

            NetworkStream stream = tcpClient.GetStream();

            byte[] canalEnvioByte = Encoding.ASCII.GetBytes("\nW\r");
            //byte[] canalEnvioByte = Encoding.ASCII.GetBytes("\nP\r");

            stream.Write(canalEnvioByte, 0, canalEnvioByte.Length);

            Thread.Sleep(1);

            byte[] canalRecepcionBytes = new byte[tcpClient.Available];

            tcpClient.GetStream().Read(canalRecepcionBytes, 0, tcpClient.Available);

            stream.Close();

            stream.Dispose();

            tcpClient.Close();

            tcpClient.Dispose();

            tcpListener.Stop();

            StringBuilder mensajeObtenido = new StringBuilder();

            mensajeObtenido.Append(Encoding.Default.GetString(canalRecepcionBytes));

            tcpListener.Stop();

            Console.WriteLine(mensajeObtenido);

            return;

        }


    }
}