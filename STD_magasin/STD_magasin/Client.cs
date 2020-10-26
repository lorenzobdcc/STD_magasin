///Auteur : Lorenzo Bauduccio
///Classe : T.IS E2B
///Version : 1.0
///Date : 26.10.2020
///description : classe de gestion des clients et de leur déplacement
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
        public Vector2 actualPosition;


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
        public Client(Vector2 startPosition, Size size, Vector2 destinationp, float timeFactor,int type, int height, int width) : base(startPosition, size, destinationp)
        {
            widthMagasin = width;
            heightMagasin = height;
            destination = destinationp;
            isInQueue = false;
            estLibre = false;
            aFiniEnCaisse = false;
            stTimer = new Stopwatch();
            stTimer.Start();

            actualPosition = startPosition;
            //le type de client fait varier le temps d'attente et la couleur du client dans le magasin
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
                return  Move(destination);
            }
        }
        /// <summary>
        /// déplacement d'une position A à la position B
        /// </summary>
        /// <param name="destination"></param>
        private Vector2 Move(Vector2 destination)
        {
            
            double distanceTraveled = 1;
            double xDiff = destination.X - actualPosition.X;
            double yDiff = destination.Y - actualPosition.Y;
            double angleRad;
            if (xDiff > 0 && yDiff > 0)
                angleRad = Math.Atan(yDiff / xDiff);
            else if (xDiff < 0 && yDiff > 0)
                angleRad = Math.PI - Math.Atan(yDiff / -xDiff);
            else if (xDiff < 0 && yDiff < 0)
                angleRad = Math.PI + Math.Atan(-yDiff / -xDiff);
            else if (xDiff > 0 && yDiff < 0)
                angleRad = 2 * Math.PI - Math.Atan(-yDiff / xDiff);
            else
                angleRad = 0;
            double x = Math.Cos(angleRad) * distanceTraveled;
            double y = Math.Sin(angleRad) * distanceTraveled;
            float newX = actualPosition.X + Convert.ToSingle(x);
            float newY = actualPosition.Y + Convert.ToSingle(y);

            if (newX > actualPosition.X && newX > destination.X)
                newX = destination.X;
            if (newX < actualPosition.X && newX < destination.X)
                newX = destination.X;
            if (newY > actualPosition.Y && newY > destination.Y)
                newY = destination.Y;
            if (newY < actualPosition.Y && newY < destination.Y)
                newY = destination.Y;

            actualPosition.X = newX;
            actualPosition.Y = newY;
            return  new Vector2(newX, newY);
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
            }
            //fais rebondir le client si il touche le bord du magasin
            if (Position.Y  > heightMagasin -Size.Height || Position.Y < 0)
            {
                startPosition = Position;
                destination.Y = -destination.Y;
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
