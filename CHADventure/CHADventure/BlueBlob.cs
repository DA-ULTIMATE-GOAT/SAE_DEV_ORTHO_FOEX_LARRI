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
        public const int VITESSE_BLOB = 80;
        Random rndm = new Random();
        public Vector2 _positionBlob;
        public AnimatedSprite _spriteBlob;
        public String _animationBlob = "idle";
        private int _vitesse;

        
        public void Initialize()
        {
            _positionBlob = new Vector2(400,400);
            _vitesse = VITESSE_BLOB;
        }
        public void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheetBlob = game.Content.Load<SpriteSheet>("mob/BlueBlob/blueBlobAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _spriteBlob = new AnimatedSprite(spriteSheetBlob);
        }

        public void DeplacementBlob(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _animationBlob = "idle";
            //_positionBlob = new Vector2(400, 400);
        }
        public void Draw(SpriteBatch spritebatch)
         {
            spritebatch.Draw(this._spriteBlob, this._positionBlob);
         }

    }
}