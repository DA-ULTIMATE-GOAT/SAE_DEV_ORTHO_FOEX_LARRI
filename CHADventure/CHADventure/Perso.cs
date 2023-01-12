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
    public class Perso
    {
        public const int HAUTEUR_SPRITE = 72;
        public const int LARGEUR_SPRITE = 92;
        public const int VITESSE_PERSO = 110;
        public const int COOLDOWNEZIO = 1;

        public Vector2 _positionPerso;
        private BlueBlob _blueBlob;
        private RedBlob _redBlob;
        public AnimatedSprite _ezioSprite;
        public String _animation = "idle";
        public String _sensIdle = "S";
        private float _coolDown = 0;
        public bool _isCoolDownEzio = true;
        public bool _attaque = false;
        private Coeur _coeur;
        private float _cd;

        public BlueBlob BlueBlob { get => _blueBlob; set => _blueBlob = value; }
        public RedBlob RedBlob { get => _redBlob; set => _redBlob = value; }
        public Coeur Coeur { get => _coeur; set => _coeur = value; }

        public void InitPosition(Vector2 _positionPerso)
        {
            
            this._positionPerso = _positionPerso;
        }

        public void DeplacementPerso(GameTime gameTime, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _coolDown -= deltaTime;
            KeyboardState keyboardState = Keyboard.GetState();
            if ((_positionPerso.X > Perso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                _animation = "walkWest";
                _sensIdle = "W";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.X -= VITESSE_PERSO * deltaTime;
                //Console.WriteLine("LAYER 1 " + _mapLayer.GetTile(tx, ty).GlobalIdentifier);
                //Console.WriteLine("LAYER 2                   :" + _mapLayer2.GetTile(tx, ty).GlobalIdentifier);
            }
            else if ((_positionPerso.X < 800 - Perso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                _animation = "walkEast";
                _sensIdle = "E";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.X += VITESSE_PERSO * deltaTime;
                //Console.WriteLine("LAYER 1 " + _mapLayer.GetTile(tx, ty).GlobalIdentifier);
                //Console.WriteLine("LAYER 2                   :" + _mapLayer2.GetTile(tx, ty).GlobalIdentifier);
            }
            else if ((_positionPerso.Y > Perso.HAUTEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                _animation = "walkNorth";
                _sensIdle = "N";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.Y -= VITESSE_PERSO * deltaTime;
                //Console.WriteLine("LAYER 1 " + _mapLayer.GetTile(tx, ty).GlobalIdentifier);
                //Console.WriteLine("LAYER 2                   :" + _mapLayer2.GetTile(tx, ty).GlobalIdentifier);

            }
            else if ((_positionPerso.Y < 800 - Perso.HAUTEUR_SPRITE / 2) && keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);
                _animation = "walkSouth";
                _sensIdle = "S";

                if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                    _positionPerso.Y += VITESSE_PERSO * deltaTime;

                //Console.WriteLine("LAYER 1 " + _mapLayer.GetTile(tx, ty).GlobalIdentifier);
                //Console.WriteLine("LAYER 2                   :" + _mapLayer2.GetTile(tx, ty).GlobalIdentifier);
            }
            else
            {
                if (_sensIdle == "N")
                    _animation = "idleNorth";

                else if (_sensIdle == "E")
                    _animation = "idleEast";

                else if (_sensIdle == "W")
                    _animation = "idleWest";
                else
                    _animation = "idle";
            }
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
    
        public bool Degats(Vector2 position, BlueBlob blueBlob, GameTime gameTime)
        {

            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _cd += elapsed;
            KeyboardState keyboardState = Keyboard.GetState();
            bool degats = false;
            if(APorter(blueBlob.PositionBlob, blueBlob) && keyboardState.IsKeyDown(Keys.Space) && _cd >= 2000)
            {
                _cd = 0;
                degats = true;
                Console.WriteLine("Marche");
            }
            return degats;
        }

        public bool Degats(Vector2 position, RedBlob redBlob, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _cd += elapsed;
            KeyboardState keyboardState = Keyboard.GetState();
            bool degats = false;
            if (APorter(redBlob.PositionBlob, redBlob) && keyboardState.IsKeyDown(Keys.Space) && _cd >= 2000)
            {
                _cd = 0;
                degats = true;
                _animation = "attack";
                Console.WriteLine("Marche2");
            }
            return degats;
        }
        public bool APorter(Vector2 position, BlueBlob _blueBlob)
        {
            Vector2 emplacement;
            bool touche = false;
            emplacement.X = Math.Abs(position.X - _positionPerso.X);
            emplacement.Y = Math.Abs(position.Y - _positionPerso.Y);

            if (emplacement.X <=  100 && emplacement.Y <= 100)
            {
                touche = true;
            }
           
            return touche;

        }

        public bool APorter(Vector2 position, RedBlob _redBlob)
        {
            Vector2 emplacement;
            bool touche = false;
            emplacement.X = Math.Abs(position.X - _positionPerso.X);
            emplacement.Y = Math.Abs(position.Y - _positionPerso.Y);

            if (emplacement.X <= 100 && emplacement.Y <= 100)
            { 
                touche = true;
            }

            return touche;

        }



    }
}
