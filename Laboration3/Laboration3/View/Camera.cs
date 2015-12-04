using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.View
{
    class Camera
    {
        private int border;
        private float size;
        int height;
        int width;
        private float scale;
        private Vector2 position = Vector2.Zero;
        Viewport graphics;

        public Camera(Viewport Graphics)
        {
            graphics = Graphics;
            size = 100;
            border = 10;
            height = graphics.Height - border * 2; // har en höjd
            width = graphics.Width - border * 2; //har en bredd

            if (height < width)//om höjden är mindre än bredden så sätter vi bredden till höjden!
            {
                width = height;
            }
            else
            {
                height = width;
            }
            scale = width / size;
        }

        public Vector2 convertToLogicalCoords(float x, float y)
        { 
            float logicalX = x / graphics.Width;
            float logicalY = y / graphics.Height;

            return new Vector2(logicalX, logicalY);
        }
        public Rectangle GetGameWindow()//skapar rectangeln
        {
            return new Rectangle((int)border, (int)border, (int)width, (int)height);
        }
        public Vector2 returnPositionOfField(float x, float y) //tar emot en x och ett y position//den gamla convert to visual cordinates
        {
            float visualX = border + x * scale;
            float visualY = border + y * scale;

            return new Vector2(visualX, visualY);
        }
        public Vector2 convertToVisualCoords(Vector2 coordinates)
        {
            float visualX = coordinates.X * graphics.Width;
            float visualY = coordinates.Y * graphics.Height;

            return new Vector2(visualX, visualY);
        }

        public float scaleSizeTo(float rawsize, float size)
        {
            return size / rawsize * scale;//när man har matrix
        }
        //internal Matrix? GetMatrix()
        //{
        //    //En matris är en beskrivning utav rotation, position och skala. Du kan multiplicera vectorer med matriser och få
        //    //vektorn roterad, skalad och positionerad enligt matrisen. Rotation, position och skalans ordning i multiplication
        //    //har effekt på hur resultatet blir.
        //    return Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(-position.X, -position.Y, 0) * Matrix.CreateScale(scale);
        //}
    }
}
