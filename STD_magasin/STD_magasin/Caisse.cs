///Auteur : Lorenzo Bauduccio
///Classe : T.IS E2B
///Version : 1.0
///Date : 26.10.2020
///description : classe de gestion de la caisse et de ces clients
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STD_magasin
{
    public class Caisse : Sprite
    {

        const int MAX_CLIENTS = 5;
        const int WIDTH_AND_HEIGHT_OF_CAISSE = 30;
        const int DECALAGE_BETWEEN_CLIENT = 40;

        List<Client> lstClientMagasin = new List<Client>();
        public List<Client> lstClientCaisse = new List<Client>();
        public bool isOpen;
        SolidBrush myBrush;
        Client clientEnCaisse;



        public Caisse(Vector2 startPosition, Size size, Vector2 speed, float timeFactor, List<Client> lstClient) : base(startPosition, size, speed)
        {
            destination = new Vector2(5, 5);
            lstClientMagasin = lstClient;
            isOpen = false;
        }

        public override Vector2 Position
        {
            get
            {
                return new Vector2(startPosition.X, startPosition.Y);
            }
        }
        public override void Paint(object sender, PaintEventArgs e)
        {

            if (isOpen)
            {
                myBrush = new SolidBrush(Color.Green);
            }
            else
            {
                myBrush = new SolidBrush(Color.Red);
            }

            

            e.Graphics.FillRectangle(myBrush, startPosition.X, startPosition.Y, WIDTH_AND_HEIGHT_OF_CAISSE, WIDTH_AND_HEIGHT_OF_CAISSE);

            if (isOpen)
            {
                GetClient();
            }
            GetPositionClient();
            PassageEnCaisse();
            DeleteClient();
            //si un client est a la caisse les secondes avant le suivant s'affiche
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            if (clientEnCaisse != null)
            {
            e.Graphics.DrawString((clientEnCaisse.tempsEnCaisse - clientEnCaisse.stTimer.Elapsed.Seconds).ToString(), new Font("Arial", 16), whiteBrush, Position.ToPointF());
            }


        }
        /// <summary>
        /// ajoute un client libre a la caisse
        /// </summary>
        public void GetClient()
        {
            if (lstClientCaisse.Count < MAX_CLIENTS)
            {
                foreach (var client in lstClientMagasin)
                {
                    if (client.isInQueue == false && client.estLibre)
                    {
                        
                        client.isInQueue = true;
                        client.sw.Reset();
                        client.stTimer.Reset();
                        client.startPosition = this.Position;
                        client.destination = new Vector2(0, 0);
                        lstClientCaisse.Add(client);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// aligne les position des client pour etre au dessus de la caisse
        /// </summary>
        public void GetPositionClient()
        {
            int i = 1;
            foreach (var client in lstClientCaisse)
            {
                client.startPosition.Y = this.Position.Y - DECALAGE_BETWEEN_CLIENT * i;
                i++;
            }
        }
        /// <summary>
        /// suprime un client apres avoir fini en caisse
        /// </summary>
        public void DeleteClient()
        {
            foreach (var client in lstClientCaisse)
            {
                if (client.stTimer.Elapsed.Seconds > client.tempsEnCaisse)
                {

                    clientEnCaisse = null;
                    client.aFiniEnCaisse = true;
                    lstClientCaisse.Remove(client);
                    break;
                }
            }
        }
        /// <summary>
        /// fais passer en caisse le prochain client dans la queue
        /// </summary>
        public void PassageEnCaisse()
        {
            if (clientEnCaisse == null)
            {
                foreach (var client in lstClientCaisse)
                {
                    clientEnCaisse = client;
                    client.stTimer.Restart();
                    break;
                }
            }
        }

    }
}

