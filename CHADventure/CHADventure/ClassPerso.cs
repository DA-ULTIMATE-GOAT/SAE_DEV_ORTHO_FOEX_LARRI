using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace CHADventure
{
    public class ClassPerso
    {
        public const int HAUTEUR_SPRITE = 72;
        public const int LARGEUR_SPRITE = 92;
        private Vector2 _positionPerso = new Vector2(400, 672);
        public AnimatedSprite _perso;
        private int vitesse = 100;



        public void InitPosition(Vector2 _positionPerso)
        {
            this._positionPerso = _positionPerso;
        }
    }
}
