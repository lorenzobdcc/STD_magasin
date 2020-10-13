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

        private readonly float timeFactor;
        const int MAX_CLIENTS = 5;

        List<Client> lstClientMagasin = new List<Client>();
        public List<Client> lstClientCaisse = new List<Client>();
        public bool isOpen;
        SolidBrush myBrush;
        Client clientEnCaisse;



        public Caisse(Vector2 startPosition, Size size, Vector2 speed, float timeFactor, List<Client> lstClient) : base(startPosition, size, speed)
        {
            this.timeFactor = timeFactor;
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
                myBrush = new SolidBrush(Color.Blue);
            }

            if (lstClientCaisse.Count == 0)
            {
                myBrush = new SolidBrush(Color.Red);
            }
            // Draw ellipse to screen.
            e.Graphics.FillRectangle(myBrush, startPosition.X, startPosition.Y, 30, 30);

            if (isOpen)
            {
                GetClient();
            }
            GetPositionClient();
            PassageEnCaisse();
            DeleteClient();
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            if (clientEnCaisse != null)
            {
            e.Graphics.DrawString((clientEnCaisse.tempsEnCaisse - clientEnCaisse.stTimer.Elapsed.Seconds).ToString(), new Font("Arial", 16), whiteBrush, Position.ToPointF());
            }


        }

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
        public void GetPositionClient()
        {
            int i = 1;
            foreach (var client in lstClientCaisse)
            {
                client.startPosition.Y = this.Position.Y - 40 * i;
                i++;
            }
        }

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

