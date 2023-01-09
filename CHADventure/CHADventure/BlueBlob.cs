using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;

namespace CHADventure
{
    public class BlueBlob
    {
        private Game1 _myGame;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_BLOB = 90;
        Random spawn = new Random();
        BlueBlob[] lesmobs = new BlueBlob[3];

        private Vector2 _positionBlob;
        private AnimatedSprite _blueBlob;
        private String _animationBlob = "idle";

        public override void Initialize()
        {
            for (int i = 0; i < lesmobs.Length; i++)
            {
                lesmobs[i] = new Vector2(spawn.Next(288,496),spawn.Next(256,464));
            }
        }
        public override void LoadContent()
        {


        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw();
            _myGame._spriteBatch.End(); // Game1 pour changer le graphisme

        }
    }
}
