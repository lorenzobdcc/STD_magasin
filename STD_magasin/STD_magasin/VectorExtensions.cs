///Auteur : Lorenzo Bauduccio
///Classe : T.IS E2B
///Version : 1.0
///Date : 26.10.2020
///description : permet de créer des vecteur qui peuvent etre convertie en Pointf
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace STD_magasin
{
    public static class VectorExtensions
    {
        public static PointF ToPointF(this Vector2 vector2)
        {
            return new PointF(vector2.X, vector2.Y);
        }
    }
}
