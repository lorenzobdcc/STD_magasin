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

        List<Client> lstClient = new List<Client>();



        public Caisse(Vector2 startPosition, Size size, Vector2 speed, float timeFactor) : base(startPosition, size, speed)
        {
            
            this.timeFactor = timeFactor;

            destination = new Vector2(5, 5);
        }

        public override Vector2 Position
        {
            get
            {

                float elapsedTime = sw.ElapsedMilliseconds / timeFactor;
                float x = destination.X   + elapsedTime;
                float y = destination.Y   + elapsedTime;
                startPosition.X += x;
                startPosition.Y += y;
                return new Vector2(x, y);

            }
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            SolidBrush myBrush = new SolidBrush(Color.Blue);

            // Draw ellipse to screen.
            e.Graphics.FillRectangle(myBrush, startPosition.X, startPosition.Y, 30, 30);

        }
    }
}
