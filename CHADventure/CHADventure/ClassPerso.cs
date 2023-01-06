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
    public class ClassPerso
    {
        public const int HAUTEUR_SPRITE = 72;
        public const int LARGEUR_SPRITE = 92;
        public const int VITESSE_PERSO = 110;

        private Vector2 _positionPerso = new Vector2(400, 672);
        private AnimatedSprite _perso;
        private int vitesse = 100;
        private readonly ScreenManager _screenManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private String _animation;
        


        public void InitPosition(Vector2 _positionPerso)
        {
            this._positionPerso = _positionPerso;
        }

        public void DeplacementPerso(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            if ((_positionPerso.X > ClassPerso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);

                _animation = "walkWest";
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= VITESSE_PERSO * deltaTime;

            }
            else if ((_positionPerso.X < 800 - ClassPerso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);

                _animation = "walkEast";
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                    _positionPerso.X += VITESSE_PERSO * deltaTime;

            }
            else if ((_positionPerso.Y > ClassPerso.HAUTEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);

                _animation = "walkNorth";
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= VITESSE_PERSO * deltaTime;
                Console.WriteLine(_mapLayer2.GetTile(tx, ty).GlobalIdentifier);

            }
            else if ((_positionPerso.Y < 800 - ClassPerso.HAUTEUR_SPRITE / 2) && keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);

                _animation = "walkSouth";
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += VITESSE_PERSO * deltaTime;
                Console.WriteLine("DOWN");
            }
            else
            {
                _animation = "idle";
                _perso.Update(deltaTime);
              
            }
            _perso.Play(_animation);
        }
        private bool IsCollision(ushort x, ushort y)
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
