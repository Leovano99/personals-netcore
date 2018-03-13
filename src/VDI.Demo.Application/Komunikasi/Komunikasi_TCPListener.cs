using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Visionet_Backend_NetCore.Handler;

namespace Visionet_Backend_NetCore.Komunikasi
{
    public class Komunikasi_TCPListener
    {
        private int PORT;
        private TcpListener tcpListener;

        public Komunikasi_TCPListener(int port)
        {
            this.PORT = port;

            tcpListener = new TcpListener(port);

            Setting_variabel.ConsoleBayangan = new ConsoleBayangan();
            Setting_variabel.ConsoleBayangan.AdaPaketWriteConsoleListener += AdaHandlerPaketWriteConsoleListener;
        }

        public void AdaHandlerPaketWriteConsoleListener(object sender, AdaPaketWriteConsoleArgs e)
        {
            KirimBroadcast(""+e.Message);
        }


        List<Task> tasks = new List<Task>();
        public void StartListener()
        {
            try
            {
                tcpListener.Start();
            }
            catch (Exception e)
            {
                if(Setting_variabel.enable_debug==true) Debug.WriteLine(e.Message + " " + e.StackTrace);
            }


            for (int i = 0; i < 2; i++)
            {
                Task task = new Task(Service, TaskCreationOptions.None);
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            tcpListener.Stop();

            try
            {
                tcpListener.Server.Close();
            }
            catch (Exception e) { }
            StartListener();
        }

        public void StopListener()
        {
            if (tcpListener != null)
            {
                try
                {
                    tcpListener.Stop();
                }
                catch (Exception e) { }

                try
                {
                    tcpListener.Server.Close();
                }
                catch (Exception e) { }
            }
        }

        public void KirimPaket(System.IO.StreamWriter streamwriter, string paket)
        {
            try
            {
                if(Setting_variabel.enable_debug==true) Debug.WriteLine("KIRIM BROADCAST LOKAL:" + paket);
                streamwriter.WriteLine(paket);
                streamwriter.Flush();
            }
            catch (Exception e) { }
        }

        public void KirimBroadcast(string paket)
        {
            if(Setting_variabel.enable_debug==true) Debug.WriteLine("Kirim broadcast : " + paket);
            for (int i = 0; i < list_streamwriter.Count; i++)
            {
                if (list_streamwriter[i] != null)
                {
                    try
                    {
                        list_streamwriter[i].WriteLine(paket);
                        list_streamwriter[i].Flush();
                    }
                    catch (Exception e) { }
                }
            }
        }

        public bool IsRunning()
        {
            return mRun;
        }

        List<System.IO.StreamWriter> list_streamwriter = new List<System.IO.StreamWriter>();
        int count_putus = 0;
        bool mRun = true;
        private void Service()
        {
            string data_masuk = "";
            mRun = true;

            Socket socketForClient = null;
            System.IO.StreamWriter streamWriter = null;
            System.IO.StreamReader streamReader = null;
            NetworkStream networkStream = null;

            socketForClient = tcpListener.AcceptSocket();

            if (socketForClient.Connected)
            {
                try
                {
                    networkStream = new NetworkStream(socketForClient);
                    streamWriter = new System.IO.StreamWriter(networkStream); //iki asline 

                    list_streamwriter.Add(streamWriter);

                    streamReader =  new System.IO.StreamReader(networkStream);

                    KirimBroadcast("[SERVER] Debug protocol by Reza");
                    KirimBroadcast("[SERVER] Client " + socketForClient.RemoteEndPoint + " connected");
                    //                    
                    while (mRun == true)
                    {
                        try
                        {
                            //METODE READ BIASA
                            if (networkStream.CanRead)
                            {
                                byte[] myReadBuffer = new byte[2048];
                                StringBuilder myCompleteMessage = new StringBuilder();
                                int numberOfBytesRead = 0;
                                do
                                {
                                    try
                                    {
                                        numberOfBytesRead = networkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                                        if (myReadBuffer == null)
                                        {
                                            //if(Setting_variabel.enable_debug==true) Debug.WriteLine("Client terputus...!");
                                            mRun = false;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        mRun = false;
                                    }
                                }
                                while (networkStream.DataAvailable);

                                data_masuk = myCompleteMessage.ToString();

                                //hapus buffer
                                myCompleteMessage = null;
                                Array.Clear(myReadBuffer, 0, myReadBuffer.Length);
                            }
                            else
                            {
                                //if(Setting_variabel.enable_debug==true) Debug.WriteLine("Maaf, tidak bisa membaca data.");
                            }
                            //END METODE READ
                            

                            KirimBroadcast("[NEW PACKET] "+data_masuk);

                            if (data_masuk == "exit")
                            {
                                mRun = false;
                            }


                            if (data_masuk == "enter")
                            {
                                string baca = Console.ReadLine();
                                streamWriter.WriteLine(baca);
                                streamWriter.Flush();
                            }


                            if (data_masuk == null)
                            {
                                mRun = false;
                            }


                            if (data_masuk == "")
                            {
                                if (count_putus > 3)
                                {
                                    mRun = false;
                                }
                                else
                                {
                                    count_putus++;
                                }
                            }
                            else
                            {
                                count_putus = 0;
                            }
                            

                        }
                        catch (Exception e)
                        {
                            mRun = false;
                        }

                    }
                }
                catch (Exception e)
                {
                }
            }
            
            try
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
                if (networkStream != null)
                {
                    networkStream.Close();
                    networkStream.Dispose();
                }
                if (streamWriter != null)
                {
                    list_streamwriter.Remove(streamWriter);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch (Exception e)
            {
            }


            try
            {
                socketForClient.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                socketForClient.Dispose();
            }
            catch (Exception ex)
            {
            }
            
            Service();
        }


        #region handler event
        public event AdaPaketReadFromClientEventHandler AdaPaketReadFromClientListener;
        protected virtual void OnAdaPaketWriteConsole(AdaPaketReadFromClientArgs e)
        {
            if (AdaPaketReadFromClientListener != null)
            {
                AdaPaketReadFromClientListener(this, e);
            }
        }
        #endregion


    }
}
