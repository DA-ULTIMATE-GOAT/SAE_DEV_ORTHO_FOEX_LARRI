﻿using System;
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
    public class SalleBoss : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso;
        private Coeur _coeur;

        //private GraphicsDeviceManager _graphics;
        private SallePrincipale _sallePrincipale;
        private Boss _boss;
        private Entree _entree;
        private readonly ScreenManager _screenManager;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private Vector2 _positionPerso;


        public const int VITESSE_PERSO = 110;


        public Vector2 PositionPerso { get => _positionPerso; set => _positionPerso = value; }
        public Coeur Coeur { get => _coeur; set => _coeur = value; }

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SalleBoss(Game1 game) : base(game)
        {

            _myGame = game;
            _boss = new Boss(_perso);
            _perso = new Perso();
            Coeur = new Coeur();
        }
        public override void Initialize()
        {
            _perso._positionPerso = PositionPerso;
            base.Initialize();
        }
        public override void LoadContent()
        {
            Coeur.Initialize();
            _tiledMap = Content.Load<TiledMap>("map/SalleBoss/SalleBoss");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            Coeur.LoadContent(_myGame);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!_perso._attaque)
                if (!_perso._attaque)
                    _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
            _boss.DeplacementBoss(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            Coeur.AnimationCoeur(gameTime);
            Coeur.CoeurSprite.Play(Coeur.AnimationCoeur(gameTime));
            Coeur.CoeurSprite.Update(deltaTime);


        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            Coeur.Draw(_myGame._spriteBatch);
            _myGame._spriteBatch.End();

        }

    }
}