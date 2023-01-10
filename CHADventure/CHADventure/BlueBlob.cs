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
    public class BlueBlob : Game
    {
        private Game1 _myGame;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_BLOB = 90;
        Random rndm = new Random();
        public Vector2 _positionBlob;
        public AnimatedSprite _spriteBlob;
        public String _animationBlob = "idle";


        public BlueBlob()
        {

        }

        protected override void Initialize()
        {
            _positionBlob = new Vector2(rndm.Next(288, 496), rndm.Next(256, 464));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteSheet spriteSheetBlob = Content.Load<SpriteSheet>("mob/BlueBlob/blueBlobAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _spriteBlob = new AnimatedSprite(spriteSheetBlob);
        }

        protected override void Update(GameTime gameTime)
        {
            _spriteBlob.Play("idle");
            _spriteBlob.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _myGame._spriteBatch.Draw(_spriteBlob, _positionBlob);
        }
    }
}