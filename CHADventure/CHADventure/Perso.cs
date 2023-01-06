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
    public class Perso
    {
        public const int HAUTEUR_SPRITE = 72;
        public const int LARGEUR_SPRITE = 92;
        public const int VITESSE_PERSO = 110;

        public Vector2 _positionPerso = new Vector2(400, 672);
        public AnimatedSprite _perso;
        public String _animation;
        


        public void InitPosition(Vector2 _positionPerso)
        {
            this._positionPerso = _positionPerso;
        }

        public void DeplacementPerso(GameTime gameTime, TiledMap _tiledMap,TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            if ((_positionPerso.X > Perso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                _animation = "walkWest";
                
                if (!IsCollision(tx, ty,_mapLayer,_mapLayer2))
                    _positionPerso.X -= VITESSE_PERSO * deltaTime;
            }
            else if ((_positionPerso.X < 800 - Perso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                _animation = "walkEast";
               
                if (!IsCollision(tx, ty,_mapLayer, _mapLayer2))
                    _positionPerso.X += VITESSE_PERSO * deltaTime;

            }
            else if ((_positionPerso.Y > Perso.HAUTEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                _animation = "walkNorth";
               
                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.Y -= VITESSE_PERSO * deltaTime;

                Console.WriteLine(_mapLayer2.GetTile(tx, ty).GlobalIdentifier);

            }
            else if ((_positionPerso.Y < 800 - Perso.HAUTEUR_SPRITE / 2) && keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);
                _animation = "walkSouth";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.Y += VITESSE_PERSO * deltaTime;

                Console.WriteLine(_mapLayer.GetTile(tx, ty).GlobalIdentifier);
            }
            else
            {
                _animation = "idle";
            }
            //Console.WriteLine($"{_positionPerso.X} + {_positionPerso.Y}");
            
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
