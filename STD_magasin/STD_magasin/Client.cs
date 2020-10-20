using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace STD_magasin
{
    public class Client : Sprite
    {

        readonly int widthMagasin;
        readonly int heightMagasin;
        public bool isInQueue;
        readonly int timer;
        public bool estLibre;
        public SolidBrush myBrush;
        public Stopwatch stTimer;
        public int tempsEnCaisse;
        public bool aFiniEnCaisse;
        SolidBrush defaultBrush;


        /// <summary>
        /// ctor complett
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="size"></param>
        /// <param name="speed"></param>
        /// <param name="timeFactor"></param>
        /// <param name="type"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public Client(Vector2 startPosition, Size size, Vector2 speed, float timeFactor,int type, int height, int width) : base(startPosition, size, speed)
        {
            widthMagasin = width;
            heightMagasin = height;
            destination =speed;
            isInQueue = false;
            estLibre = false;
            aFiniEnCaisse = false;
            stTimer = new Stopwatch();
            stTimer.Start();
            //le type de client fais varier le temps d'attente et la couleur du client dans le magasin
            switch (type)
            {
                case 1:

                    myBrush = new SolidBrush(Color.Red);
                    timer = 15;
                    tempsEnCaisse = 10;
                    break;
                case 2:
                    
                    myBrush = new SolidBrush(Color.Orange);
                    timer = 10;
                    tempsEnCaisse = 7;
                    break;
                case 3:

                    myBrush = new SolidBrush(Color.Yellow);
                    timer = 5;
                    tempsEnCaisse = 5;
                    break;

                default:
                    break;
            }
            defaultBrush = myBrush;
        }

        /// <summary>
        /// postion calculé d'apres le temps le départ et la direction que doit prendre le client
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                float elapsedTime = sw.ElapsedMilliseconds / 1000f;               
                return startPosition + (elapsedTime * destination);
            }
        }
        /// <summary>
        /// dessine le client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Paint(object sender, PaintEventArgs e)
        {
            //fais rebondir le client si il touche le bord du magasin
            if (Position.X > widthMagasin -Size.Width || Position.X < 0)
            {
                startPosition = Position;
                destination.X = -destination.X;
                
                sw.Restart();
            }
            //fais rebondir le client si il touche le bord du magasin
            if (Position.Y  > heightMagasin -Size.Height || Position.Y < 0)
            {
                startPosition = Position;
                destination.Y = -destination.Y;

                sw.Restart();
                
            }
            //si le temps définit pour faire ces course le client passe en état de recherche de caisse
            if (stTimer.Elapsed.TotalSeconds >= timer && isInQueue == false)
            {
                estLibre = true;
                myBrush = new SolidBrush(Color.Green);
            }
            else
            {
                myBrush = defaultBrush;
            }
            //quand il a fini de passer en caisse il se dessine en transparent pour éviter de rester afficher
            if (aFiniEnCaisse)
            {
                myBrush = new SolidBrush(Color.Empty);
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Draw ellipse to screen.
            e.Graphics.FillEllipse(myBrush, new Rectangle(Point.Round(Position.ToPointF()), Size));
            

        }


    }
}
