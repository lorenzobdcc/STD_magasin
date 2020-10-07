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


        public Client(Vector2 startPosition, Size size, Vector2 speed, float timeFactor,int type) : base(startPosition, size, speed)
        {
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
            // Create pen.
            SolidBrush myBrush = new SolidBrush(Color.Red);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // Draw ellipse to screen.
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(Point.Round(Position.ToPointF()), Size));
            

        }
    }
}
