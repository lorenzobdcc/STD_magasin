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
    public class Client : Sprite
    {

        private readonly float timeFactor;

        int typeClient;
        int widthMagasin;
        int heightMagasin;


        public Client(Vector2 startPosition, Size size, Vector2 speed, float timeFactor,int type, int height, int width) : base(startPosition, size, speed)
        {
            widthMagasin = width;
            heightMagasin = height;
            typeClient = type;
            this.timeFactor = timeFactor;
            destination =speed;
        }

        public override Vector2 Position
        {
            get
            {

                float elapsedTime = sw.ElapsedMilliseconds / 1000f;               
                return startPosition + (elapsedTime * destination);

            }
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            if (Position.X > widthMagasin -Size.Width || Position.X < 0)
            {
                startPosition = Position;
                destination.X = -destination.X;
                
                sw.Restart();
            }

            if (Position.Y  > heightMagasin -Size.Height || Position.Y < 0)
            {
                startPosition = Position;
                destination.Y = -destination.Y;

                sw.Restart();
                
            }

            // Create pen.
            SolidBrush myBrush = new SolidBrush(Color.Red);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Draw ellipse to screen.
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(Point.Round(Position.ToPointF()), Size));
            

        }
    }
}
