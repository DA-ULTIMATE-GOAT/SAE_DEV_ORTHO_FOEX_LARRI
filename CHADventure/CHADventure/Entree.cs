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
    public class Entree : GameScreen
    {
        private Game1 _myGame;
        private readonly ScreenManager _screenManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer; 
        private TiledMapTileLayer _mapLayer2;
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private const int VITESSE_PERSO = 110;
        private const int TAILLE_TUILE = 16;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Entree(Game1 game) : base(game)
        {
            _myGame = game;
            
        }
        public override void Initialize()
        {
            
            _positionPerso = new Vector2(400,672);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("ExterieurMap");
             _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree");
             _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyboardState = Keyboard.GetState();
            if ((_positionPerso.X > ClassPerso.LARGEUR_SPRITE/4 ) && keyboardState.IsKeyDown(Keys.Q))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth -1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight+1);

                _perso.Play("walkWest");
                _perso.Update(deltaTime);
                if (!IsCollision(tx,ty))
                    _positionPerso.X -= VITESSE_PERSO * deltaTime;
                Console.WriteLine("LEFT");
            }
            else if ((_positionPerso.X < 800 - ClassPerso.LARGEUR_SPRITE/4) && keyboardState.IsKeyDown(Keys.D))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth+ 1);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight+1);

                _perso.Play("walkEast");
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                _positionPerso.X += VITESSE_PERSO * deltaTime;
                Console.WriteLine("RIGHT");
            }
            else if ((_positionPerso.Y > ClassPerso.HAUTEUR_SPRITE/4) && keyboardState.IsKeyDown(Keys.Z))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight -1);

                _perso.Play("walkNorth");
                _perso.Update(deltaTime);
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= VITESSE_PERSO * deltaTime;
                Console.WriteLine("UP");
            }
            else if ((_positionPerso.Y < 800 - ClassPerso.HAUTEUR_SPRITE/2) && keyboardState.IsKeyDown(Keys.S))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight +2);

                _perso.Play("walkSouth");
                _perso.Update(deltaTime);
                if (!IsCollision(tx,ty))
                    _positionPerso.Y += VITESSE_PERSO * deltaTime;
                Console.WriteLine("DOWN");
            }
            else
            {
                _perso.Play("idle");
                _perso.Update(deltaTime);

            }
            _tiledMapRenderer.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(); // on utilise la reference vers
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();                         // Game1 pour chnager le graphisme
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
