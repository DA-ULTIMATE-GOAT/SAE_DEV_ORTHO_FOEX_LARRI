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
        public const int VITESSE_MAX_BLOB = 40;
        public const int VITESSE_MIN_BLOB = 25;
        Random rndm = new Random();
        private Vector2 _positionBlob;
        private AnimatedSprite _spriteBlob;
        private String _animationBlob = "idle";
        private int _vitesse;

        public BlueBlob(Perso cible)
        {
            this.Perso = cible;
        }

        public void Initialize()
        {
           
            PositionBlob = new Vector2(rndm.Next(288,496),rndm.Next(256,464));
            _vitesse = rndm.Next(VITESSE_MIN_BLOB, VITESSE_MAX_BLOB);
        }
        public Perso Perso { get => _perso; set => _perso = value; }
        public Vector2 PositionBlob { get => _positionBlob; set => _positionBlob = value; }

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
            if (PositionBlob.X > Perso._positionPerso.X)
            {
                ushort tx = (ushort)(PositionBlob.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(PositionBlob.Y / _tiledMap.TileHeight + 1);
                _animationBlob = "walkEast";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionBlob.X -= _vitesse * deltaTime;
            }
            if (PositionBlob.X < Perso._positionPerso.X)
            {
                ushort tx = (ushort)(PositionBlob.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(PositionBlob.Y / _tiledMap.TileHeight + 1);
                _animationBlob = "walkWest";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionBlob.X += _vitesse * deltaTime;
            }
            if (PositionBlob.Y > Perso._positionPerso.Y)
            {
                ushort tx = (ushort)(PositionBlob.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(PositionBlob.Y / _tiledMap.TileHeight);
                _animationBlob = "walkNorth";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionBlob.Y -= _vitesse * deltaTime;
            }
            if (PositionBlob.Y < Perso._positionPerso.Y)
            {
                ushort tx = (ushort)(PositionBlob.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(PositionBlob.Y / _tiledMap.TileHeight + 2);
                _animationBlob = "walkSouth";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionBlob.Y += _vitesse * deltaTime;
            }
            Console.WriteLine($"{PositionBlob.X} + {PositionBlob.Y}");
        }
        public void Draw(SpriteBatch spritebatch)
         {
            spritebatch.Draw(this._spriteBlob, this.PositionBlob);
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

        private bool APorter(Vector2 position)
        {
            bool touche = false;
            position.X = Math.Abs(Perso._positionPerso.X - PositionBlob.X);
            position.Y = Math.Abs(Perso._positionPerso.Y - PositionBlob.Y);

            if (position.X <= 12 && position.Y <= 12)
                touche = true;
            return touche;

        }


    }
}