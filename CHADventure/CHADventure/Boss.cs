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
    public class Boss
    {
        private Game1 _myGame;
        private Perso _perso;
        public const int TAILLE_FENETRE = 800;
        public const int LARGEUR_BLOB = 25;
        public const int HAUTEUR_BLOB = 19;
        public const int VITESSE_MAX_BLOB = 40;
        public const int VITESSE_MIN_BLOB = 25;
        Random rndm = new Random();
        private Vector2 _positionBoss;
        private AnimatedSprite _spriteBoss;
        private String _animationBoss = "idle";
        private int _vitesse;
        private int pv = 10;
        private float reloadAttack;
        private float _timer;
        private bool isDead = false;

        public Boss(Perso cible)
        {
            this.Perso = cible;
        }

        public void Initialize()
        {

            PositionBoss = new Vector2(400,400);
            _vitesse = rndm.Next(VITESSE_MIN_BLOB, VITESSE_MAX_BLOB);
        }
        public Perso Perso { get => _perso; set => _perso = value; }
        public Vector2 PositionBoss { get => _positionBoss; set => _positionBoss = value; }
        public int Pv { get => pv; set => pv = value; }

        public void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheetBlob = game.Content.Load<SpriteSheet>("mob/Landi/bossAnimation", new MonoGame.Extended.Serialization.JsonContentLoader());
            _spriteBoss = new AnimatedSprite(spriteSheetBlob);
        }

        public void DeplacementBoss(GameTime gameTime, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            if (isDead == false)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _spriteBoss.Play(_animationBoss);
                _spriteBoss.Update(gameTime);
                if (Pv >=1 || Pv <= 10)
                {
                    if (PositionBoss.X > Perso._positionPerso.X)
                    {
                        ushort tx = (ushort)(PositionBoss.X / _tiledMap.TileWidth + 1);
                        ushort ty = (ushort)(PositionBoss.Y / _tiledMap.TileHeight + 1);
                        _animationBoss = "walkEast";

                        if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                            _positionBoss.X -= _vitesse * deltaTime;
                    }
                    if (PositionBoss.X < Perso._positionPerso.X)
                    {
                        ushort tx = (ushort)(PositionBoss.X / _tiledMap.TileWidth - 1);
                        ushort ty = (ushort)(PositionBoss.Y / _tiledMap.TileHeight + 1);
                        _animationBoss = "walkWest";

                        if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                            _positionBoss.X += _vitesse * deltaTime;
                    }
                    if (PositionBoss.Y > Perso._positionPerso.Y)
                    {
                        ushort tx = (ushort)(PositionBoss.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(PositionBoss.Y / _tiledMap.TileHeight);
                        _animationBoss = "walkNorth";

                        if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                            _positionBoss.Y -= _vitesse * deltaTime;
                    }
                    if (PositionBoss.Y < Perso._positionPerso.Y)
                    {
                        ushort tx = (ushort)(PositionBoss.X / _tiledMap.TileWidth);
                        ushort ty = (ushort)(PositionBoss.Y / _tiledMap.TileHeight + 2);
                        _animationBoss = "walkSouth";

                        if (!IsCollision(tx, ty, _mapLayer, _mapLayer2))
                            _positionBoss.Y += _vitesse * deltaTime;
                    }
                    APorter(_positionBoss);
                }

            }

        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (isDead == false)
                spritebatch.Draw(this._spriteBoss, this.PositionBoss);

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
            Vector2 emplacement;
            bool touche = false;
            emplacement.X = Math.Abs(position.X - _positionBoss.X);
            emplacement.Y = Math.Abs(position.Y - _positionBoss.Y);

            if (emplacement.X <= 81 && emplacement.Y <= 62)
            {
                touche = true;
            }
            return touche;

        }


        public bool Attaque(GameTime gameTime, Perso perso)
        {
            bool attaque = false;
            if (isDead == false)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                reloadAttack += elapsed;
                if (APorter(perso._positionPerso) && reloadAttack >= 1500)
                {
                    attaque = true;
                    reloadAttack = 0;
                }
            }
            return attaque;
        }

        public string Mort(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _timer += elapsed;
            if (Pv == 0)
            {
                _animationBoss = "death";
                if (_timer >= 600)
                {
                    isDead = true;
                }
            }
            return _animationBoss;

        }

    }
}
