using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace CHADventure
{
    public class ClassPerso
    {
        public const int HAUTEUR_SPRITE = 72;
        public const int LARGEUR_SPRITE = 72;

        private AnimatedSprite Sprite;
        private Vector2 position;
        private int vitesse = 100;
        public ClassPerso(Game1 game)
        {
        }



        public void InitPosition(Vector2 position)
        {
            this.position = position;
        }


    }
}
