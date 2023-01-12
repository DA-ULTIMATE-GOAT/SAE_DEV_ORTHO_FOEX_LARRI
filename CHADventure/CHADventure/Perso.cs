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
        public const int HAUTEUR_SPRITE = 72; //Initialization des constantes du Perso
        public const int LARGEUR_SPRITE = 92;
        public const int VITESSE_PERSO = 110;
        public const int COOLDOWNEZIO = 1;

        public Vector2 _positionPerso;      //Initialization des variables du Perso
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

        public void DeplacementPerso(GameTime gameTime, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)  // Méthode pour les déplacement du perso en fonction des colisions
        {
            if (!Attack(gameTime)) // si le perso n'attaque pas alors il peut lancer une direction
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
                }
                else if ((_positionPerso.X < 800 - Perso.LARGEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.D))
                {
                    ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth + 1);
                    ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 1);
                    _animation = "walkEast";
                    _sensIdle = "E";

                    if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                        _positionPerso.X += VITESSE_PERSO * deltaTime;
                }
                else if ((_positionPerso.Y > Perso.HAUTEUR_SPRITE / 4) && keyboardState.IsKeyDown(Keys.Z))
                {
                    ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    _animation = "walkNorth";
                    _sensIdle = "N";

                    if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                        _positionPerso.Y -= VITESSE_PERSO * deltaTime;

                }
                else if ((_positionPerso.Y < 800 - Perso.HAUTEUR_SPRITE / 2) && keyboardState.IsKeyDown(Keys.S))
                {
                    ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight + 2);
                    _animation = "walkSouth";
                    _sensIdle = "S";

                    if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                        _positionPerso.Y += VITESSE_PERSO * deltaTime;
                }
                else // si le personnage de bouge pas il reste fixe en gardant sa dernière direction
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
            else // le perso attaque
            {
                _animation = "attack"; 
            }
           
        }
        private bool IsCollision(ushort x, ushort y, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2) // méthode pour les collisions en fonction de la couche de tuiles 
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if ((_mapLayer.TryGetTile(x, y, out tile) == false) && (_mapLayer2.TryGetTile(x, y, out tile) == false))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
    
        public bool Degats(Vector2 position, BlueBlob blueBlob, GameTime gameTime) // méthode qui retourne un booléen pour savoir si on applique des dégats au Blob bleu
        {

            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _cd += elapsed;
            KeyboardState keyboardState = Keyboard.GetState();
            bool degats = false;
            if(APorter(blueBlob.PositionBlob, blueBlob) && Attack(gameTime))
            {
                _cd = 0;
                degats = true;
            }
            return degats;
        }

        public bool Degats(Vector2 position, RedBlob redBlob, GameTime gameTime) // méthode qui retourne un booléen pour savoir si on applique des dégats au Blob rouge
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _cd += elapsed;
            KeyboardState keyboardState = Keyboard.GetState();
            bool degats = false;
            if (APorter(redBlob.PositionBlob, redBlob) && Attack(gameTime))
            {
                _cd = 0;
                degats = true;
            }
            return degats;
        }
        public bool APorter(Vector2 position, BlueBlob _blueBlob) // méthode qui retourne un booléen pour savoir si le personnage est a porté du Blob bleu
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

        public bool APorter(Vector2 position, RedBlob _redBlob) // méthode qui retourne un booléen pour savoir si le personnage est a porté du Blob rouge
        {
            Vector2 emplacement;
            bool touche = false;
            emplacement.X = Math.Abs(position.X - _positionPerso.X);
            emplacement.Y = Math.Abs(position.Y - _positionPerso.Y);

            if (emplacement.X <= 60 && emplacement.Y <= 60)
            { 
                touche = true;
            }

            return touche;

        }

        public bool Attack(GameTime gameTime) // méthode qui retourne un booléen qui nous sert a savoir si le perso est entrain d'attaquer ou pas
        {
            bool attack = false;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _cd += elapsed;
            KeyboardState keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Space) && _cd >= 2000)
            {
                attack = true;
            }
            return attack;
        }



    }
}
