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
    public class Entree : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso = new Perso();
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer; 
        private TiledMapTileLayer _mapLayer2;
        private Vector2 _positionPerso;

        public const int VITESSE_PERSO = 110;
        public const int TAILLE_TUILE = 16;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Entree(Game1 game) : base(game)
        {
            _myGame = game;
            
        }
        public override void Initialize()
        {
            _positionPerso = new Vector2(400,672);
            _perso.InitPosition(_positionPerso);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map/Entree/ExterieurMap");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheet);
            base.LoadContent();
            
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            _perso.DeplacementPerso(gameTime,_tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(); // on utilise la reference vers
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            _myGame._spriteBatch.End(); // Game1 pour changer le graphisme
            
        }

        public bool OuverturePorte(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight);
            bool reponse = false;
            if (_mapLayer2.GetTile(tx, ty).GlobalIdentifier == 224 || _mapLayer2.GetTile(tx, ty).GlobalIdentifier == 223)
            {
                reponse = true;
            }
            return reponse;
            
        }
        public override void UnloadContent()
        {
            ushort tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth);
            ushort ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.E) && (_mapLayer2.GetTile(tx, ty).GlobalIdentifier == 224 || _mapLayer2.GetTile(tx, ty).GlobalIdentifier == 223))
            {
                base.UnloadContent();
            }

        }
    }
}
