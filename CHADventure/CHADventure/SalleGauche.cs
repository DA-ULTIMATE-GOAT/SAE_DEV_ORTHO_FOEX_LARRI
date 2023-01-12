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
using Microsoft.Xna.Framework.Media;

namespace CHADventure
{
    public class SalleGauche : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso;
        private Coeur _coeur;
        private RedBlob _redBlob;

        private SallePrincipale _sallePrincipale;
        private Entree _entree;
        private readonly ScreenManager _screenManager;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private RedBlob[] _tabBlob;
        private Vector2 _positionPerso;
        Random rndm = new Random();
        private Song _sound;



        public const int VITESSE_PERSO = 110;
        public const int TAILLE_TUILE = 16;

        //changement de scene :
        public bool _peutSallePrincipaleG = false;

        public Vector2 PositionPerso { get => _positionPerso; set => _positionPerso = value; }
        public Coeur Coeur { get => _coeur; set => _coeur = value; }

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SalleGauche(Game1 game) : base(game)
        {
            _myGame = game;
            _perso = new Perso();
            _redBlob = new RedBlob(_perso);
            Coeur = new Coeur();
        }
        public override void Initialize()
        {
            _perso._positionPerso = PositionPerso;
            _tabBlob = new RedBlob[3];
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i] = new RedBlob(_perso);
            }
            base.Initialize();
        }
        public override void LoadContent()
        {
            Coeur.Initialize();
            _tiledMap = Content.Load<TiledMap>("map/Principale/CombatGauche");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("Obstacles2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i] = new RedBlob(_perso);
                _tabBlob[i].Initialize();
                _tabBlob[i].LoadContent(_myGame);
            }
            _sound = Content.Load<Song>("Sound/SalleDG/CombatouBoss");
            MediaPlayer.Play(_sound);
            Coeur.LoadContent(_myGame);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _perso.Attaque(gameTime, _tabBlob[i]);
                if (_tabBlob[i].Attaque(gameTime, _perso))
                {
                    Coeur.Pv -= 1;
                }

            }
            if (!_perso._attaque)
                if (!_perso._attaque)
                _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i].DeplacementBlob(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            }
            Coeur.AnimationCoeur(gameTime);
            Coeur.CoeurSprite.Play(Coeur.AnimationCoeur(gameTime));
            Coeur.CoeurSprite.Update(deltaTime);
            SallesPrincipale(_myGame.tx, _myGame.ty);
            _redBlob.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            for (int i = 0; i < _tabBlob.Length; i++)
            {
                _tabBlob[i].Draw(_myGame._spriteBatch);
            }
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            Coeur.Draw(_myGame._spriteBatch);
            _myGame._spriteBatch.End();

        }
        public void SallesPrincipale(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth + 1);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight + 1);
            _peutSallePrincipaleG = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 31)
            {
                _peutSallePrincipaleG = true;
            }
        }
    }
}