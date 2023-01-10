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


namespace CHADventure
{
    public class BlueBlob
    {
        private Game1 _myGame;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_BLOB = 90;
        Random rndm = new Random();
        public Vector2 _positionBlob;
        public String _animationBlob = "idle";


        public void DeplacementBlob(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _animationBlob = "idle";
        }
        /* public void Draw(GameTime gameTime)
         {
             _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
         }*/

    }
}