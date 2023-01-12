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
        public const int LARGEUR_BOSS = 201;
        public const int HAUTEUR_BOSS = 201;
        public const int VITESSE_MAX_BOSS = 40;
        public const int VITESSE_MIN_BOSS = 25;
        Random rndm = new Random();
        private Vector2 _positionBoss;
        private AnimatedSprite _spriteBoss;
        private String _animationBoss = "idle";
        private int _vitesse;
        private float reloadAttack;

        public Boss(Perso cible)
        {
            this.Perso = cible;
        }
        public void Initialize()
        {

            PositionBoss = new Vector2(400, 208);
            _vitesse = rndm.Next(VITESSE_MIN_BOSS, VITESSE_MAX_BOSS);
        }

        public Perso Perso { get => _perso; set => _perso = value; }
        public Vector2 PositionBoss { get => _positionBoss; set => _positionBoss = value; }
        public void LoadContent(Game1 game)
        {
            SpriteSheet spriteSheetBoss = game.Content.Load<SpriteSheet>("mob/Sans/bossAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _spriteBoss = new AnimatedSprite(spriteSheetBoss);
        }

        public void DeplacementBoss(GameTime gameTime, TiledMap _tiledMap, TiledMapTileLayer _mapLayer, TiledMapTileLayer _mapLayer2)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _spriteBoss.Play(_animationBoss);
            _spriteBoss.Update(gameTime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(this._spriteBoss, this.PositionBoss);
        }
    }
}
