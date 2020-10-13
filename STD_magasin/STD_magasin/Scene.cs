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
        const int CLIENT_MAX = 50;
        const int CLIENT_MIN = 10;
        const int CAISSE_MAX = 10;
        private const int FPS = 60;
        Random rnd = new Random();

        const int HEIGHT = 500;
        const int WIDTH = 800;


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
        public void AddClients()
        {
            int positionCaisse_y = 70;
            int positionCaisse_x = 400;
            for (int i = 0; i < 20; i++)
            {
                Client client = new Client(new Vector2(rnd.Next(400), rnd.Next(400)), new Size(22, 22), new Vector2(rnd.Next(-50, 50), rnd.Next(-50, 50)), 2000, rnd.Next(1, 4), HEIGHT, WIDTH);
                lstClients.Add(client);
                Paint += client.Paint;
            }
            for (int i = 0; i < CAISSE_MAX; i++)
            {
                Caisse caisse = new Caisse(new Vector2(positionCaisse_y * i, positionCaisse_x), new Size(2, 2), new Vector2(50, -50), 200, lstClients);
                lstCaisses.Add(caisse);
                Paint += caisse.Paint;
            }
            lstCaisses[0].isOpen = true;
        }
        private void TmrFrame_Tick(object sender, System.EventArgs e) => Invalidate();

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

        private void AddNewClient()
        {
            if (stTimer.Elapsed.TotalSeconds > 2)
            {
                Client client = new Client(new Vector2(rnd.Next(400), rnd.Next(400)), new Size(22, 22), new Vector2(rnd.Next(-50, 50), rnd.Next(-50, 50)), 2000, rnd.Next(1, 4), HEIGHT, WIDTH);
                lstClients.Add(client);
                Paint += client.Paint;
                stTimer.Restart();
            }
        }
        private void CheckNumberOfCaisse()
        {
            int clientLibre = 0;
            int caisseOuverte = 0;
            foreach (var client in lstClients)
            {
                if (client.estLibre)
                {
                    clientLibre++;
                }
            }
            foreach (var caisse in lstCaisses)
            {
                if (caisse.isOpen)
                {
                    caisseOuverte++;
                }
            }
            if (clientLibre - caisseOuverte * 5 >= 3)
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
                if (clientLibre - caisseOuverte * 5 <= 2)
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
