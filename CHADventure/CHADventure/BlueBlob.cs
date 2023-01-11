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
        private Perso _perso;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_MAX_BLOB = 70;
        public const int VITESSE_MIN_BLOB = 50;
        Random rndm = new Random();
        public Vector2 _positionBlob;
        public AnimatedSprite _spriteBlob;
        public String _animationBlob = "idle";
        private int _vitesse;

        
        public void Initialize()
        {
            _perso = new Perso();
            _positionBlob = new Vector2(rndm.Next(288,496),rndm.Next(256,464));
            _vitesse = rndm.Next(VITESSE_MIN_BLOB, VITESSE_MAX_BLOB);
        }
        public void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheetBlob = game.Content.Load<SpriteSheet>("mob/BlueBlob/blueBlobAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _spriteBlob = new AnimatedSprite(spriteSheetBlob);
        }

        public void DeplacementBlob(GameTime gameTime, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _spriteBlob.Play(_animationBlob);
            _spriteBlob.Update(gameTime);

            Console.WriteLine($"{_positionBlob.X} + {_positionBlob.Y}");
        }
        public void Draw(SpriteBatch spritebatch)
         {
            spritebatch.Draw(this._spriteBlob, this._positionBlob);
         }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if ((_mapLayer.TryGetTile(x, y, out tile) == false) && (_mapLayer2.TryGetTile(x, y, out tile) == false))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    }
}