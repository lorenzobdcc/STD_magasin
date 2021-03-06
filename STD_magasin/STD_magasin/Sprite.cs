﻿///Auteur : Lorenzo Bauduccio
///Classe : T.IS E2B
///Version : 1.0
///Date : 26.10.2020
///description : classe de qui permet de créer diferent composant pour l'affichage
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace STD_magasin
{
    public class Sprite
    {
        private const float DEFAULT_POSITION_X = 50;
        private const float DEFAULT_POSITION_Y = 50;
        private const int DEFAULT_HEIGHT = 50;
        private const int DEFAULT_WIDTH = 50;
        private const float DEFAULT_SPEED_X = 10f;
        private const float DEFAULT_SPEED_Y = 10f;

        public  Vector2 destination;
        public Vector2 startPosition;

        public Sprite(Vector2 startPosition, Size size, Vector2 destination)
        {
            this.startPosition = startPosition;
            Size = size;
            this.destination = destination;
        }

        public Sprite() :
            this(new Vector2(DEFAULT_POSITION_X, DEFAULT_POSITION_Y),
                new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT),
                new Vector2(DEFAULT_SPEED_X, DEFAULT_SPEED_Y))
        { }


        public virtual Vector2 Position
        {
            get
            {
                return startPosition ;
            }
        }

        public Size Size { get; private set; }

        public virtual void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Azure, new Rectangle(Point.Round(Position.ToPointF()), Size));
        }
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
