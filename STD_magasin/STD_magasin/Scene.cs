///Auteur : Lorenzo Bauduccio
///Classe : T.IS E2B
///Version : 1.0
///Date : 26.10.2020
///description : classe qui représente le magasin et permet la gestion des clients et des caisses
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STD_magasin
{
    public class Scene : Control
    {
        const int CLIENT_MIN = 20;
        const int CAISSE_MAX = 10;
        const int SECOND_SPAWN_CLIENT = 2;
        const int NBR_CLIENT_MAX_CAISSE = 5;
        const int NBR_CLIENT_TO_OPEN_CAISSE = 3;
        const int NBR_CLIENT_TO_CLOSE_CAISSE = 2;
        const int DEFAULT_X_FOR_SPAWN_CAISSE = 440;
        const int DEFAULT_Y_FOR_SPAWN_CAISSE = 70;
        const int HEIGHT = 500;
        const int WIDTH = 800;
        private const int FPS = 60;
        

        Random rnd = new Random();
        private Bitmap bitmap = null;
        private Graphics g = null;
        private readonly Timer tmrFrame;
        private bool disposed = false;
        Stopwatch stTimer;


        public List<Client> lstClients = new List<Client>();
        public List<Caisse> lstCaisses = new List<Caisse>();

        public Scene() : base()
        {
            DoubleBuffered = true;
            rnd = new Random();
            AddClients();
            tmrFrame = new Timer()
            {
                Interval = 1000 / FPS,
                Enabled = true
            };
            tmrFrame.Tick += TmrFrame_Tick;
            stTimer = new Stopwatch();
            stTimer.Start();
        }
        /// <summary>
        /// ajoute les clients de base du magasin et les caisses
        /// </summary>
        public void AddClients()
        {
            for (int i = 0; i < CLIENT_MIN; i++)
            {
                //ajoute des client a une position aléatoire en 0 et 480 et x et y avec une direction aléatoire
                Client client = new Client(new Vector2(rnd.Next(HEIGHT), rnd.Next(WIDTH)), new Size(22, 22), new Vector2(rnd.Next(100), rnd.Next(100)), 2000, rnd.Next(1, 4), HEIGHT, WIDTH);
                lstClients.Add(client);
                Paint += client.Paint;
            }
            for (int i = 0; i < CAISSE_MAX; i++)
            {
                //i +1 pour que la caisse ne soit pas contre le bord
                Caisse caisse = new Caisse(new Vector2(DEFAULT_Y_FOR_SPAWN_CAISSE * (i+1), DEFAULT_X_FOR_SPAWN_CAISSE), new Size(2, 2), new Vector2(0, 0), 200, lstClients);
                lstCaisses.Add(caisse);
                Paint += caisse.Paint;
            }
            lstCaisses[0].isOpen = true;
        }
        private void TmrFrame_Tick(object sender, System.EventArgs e) => Invalidate();
        /// <summary>
        /// dessine les diferent ellements de la scene
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Initialize bitmap and g if null
            bitmap ??= new Bitmap(Size.Width, Size.Height);
            g ??= Graphics.FromImage(bitmap);

            PaintEventArgs p = new PaintEventArgs(g, e.ClipRectangle);

            p.Graphics.Clear(BackColor);
            base.OnPaint(p);
            e.Graphics.DrawImage(bitmap, new Point(0, 0));
            AddNewClient();
            CheckNumberOfCaisse();
            DeleteClient();
            AfficheStats(  e);
        }
        /// <summary>
        /// affiche les differentes statistique du magasin
        /// </summary>
        /// <param name="e"></param>
        private void AfficheStats(PaintEventArgs e)
        {

            SolidBrush whiteBrush = new SolidBrush(Color.White);

            int clientLibre = 0;
            int clientFaisLaQueue = 0;
            int caisseOuverte = 0;
            foreach (var caisse in lstCaisses)
            {
                if (caisse.isOpen)
                {
                    caisseOuverte++;
                }
            }
            foreach (var client in lstClients)
            {
                if (client.estLibre)
                {
                    clientLibre++;
                }
                if (client.isInQueue)
                {
                    clientFaisLaQueue++;
                }
            }
            e.Graphics.DrawString("Clients total: " + (lstClients.Count).ToString(), new Font("Arial", 16), whiteBrush, new Point(550, 10));
            e.Graphics.DrawString("Clients en achat: " + (lstClients.Count - clientFaisLaQueue).ToString(), new Font("Arial", 16), whiteBrush, new Point(550, 30));
            e.Graphics.DrawString("Clients à la queue: " + (clientFaisLaQueue).ToString(), new Font("Arial", 16), whiteBrush, new Point(550, 50));
            e.Graphics.DrawString("Caisses ouvertes: " + (caisseOuverte).ToString(), new Font("Arial", 16), whiteBrush, new Point(550, 70));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Dispose managed objects
                bitmap.Dispose();
                g.Dispose();
                tmrFrame.Dispose();
            }

            // Free unmanaged resources
            // Set large fields to null
            disposed = true;
            base.Dispose(disposing);
        }
        /// <summary>
        /// ajoute un client si le temps est dépasé
        /// </summary>
        private void AddNewClient()
        {
            if (stTimer.Elapsed.TotalSeconds > SECOND_SPAWN_CLIENT)
            {
                Client client = new Client(new Vector2(rnd.Next(HEIGHT), rnd.Next(WIDTH)), new Size(22, 22), new Vector2(rnd.Next(100), rnd.Next(100)), 2000, rnd.Next(1, 4), HEIGHT, WIDTH);
                lstClients.Add(client);
                Paint += client.Paint;
                stTimer.Restart();
            }
        }
        /// <summary>
        /// verifie si le nombre de caisse est suffisant ou superieur au nombre de clients dans le magasin
        /// </summary>
        private void CheckNumberOfCaisse()
        {
            int clientLibre = 0;
            int caisseOuverte = 0;
            //compte le nombre de client en attente d'etre pris en charge
            foreach (var client in lstClients)
            {
                if (client.estLibre)
                {
                    clientLibre++;
                }
            }
            //compte le nombres de caisses ouverte
            foreach (var caisse in lstCaisses)
            {
                if (caisse.isOpen)
                {
                    caisseOuverte++;
                }
            }
            //si le nombre de clients en attente de faire la queue est plus grand que 3 passe une caisse de fermé a ouverte
            if (clientLibre - caisseOuverte * NBR_CLIENT_MAX_CAISSE >= NBR_CLIENT_TO_OPEN_CAISSE)
            {
                foreach (var caisse in lstCaisses)
                {
                    if (caisse.isOpen == false)
                    {
                        caisse.isOpen = true;
                        break;
                    }
                }
            }
            if (caisseOuverte > 1)
            {
                //calcul si le nombre de caisse est trop élevé 
                if (clientLibre - caisseOuverte * NBR_CLIENT_MAX_CAISSE <= NBR_CLIENT_TO_CLOSE_CAISSE)
                {
                    foreach (var caisse in lstCaisses)
                    {
                        if (caisse.isOpen == true && caisse.lstClientCaisse.Count == 0)
                        {
                            caisse.isOpen = false;
                            break;
                        }
                    }

                }
            }

        }
        /// <summary>
        /// supprime un client de la liste des clients
        /// </summary>
        private void DeleteClient()
        {
            foreach (var client in lstClients)
            {
                if (client.aFiniEnCaisse)
                {
                    client.myBrush.Color = Color.Empty;
                    lstClients.Remove(client);
                    break;
                }
            }
        }

        ~Scene() => Dispose(false);
    }
}
