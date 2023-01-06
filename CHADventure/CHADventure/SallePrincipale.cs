using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace CHADventure
{
    public class SallePrincipale : GameScreen
    {
        private Game1 _myGame;
        private Perso _perso = new Perso();
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private AnimatedSprite _sprite;
        private Vector2 _positionPerso;
        private String _animation;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public SallePrincipale(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            _positionPerso = new Vector2(400, 500);
            _animation = "idle";
            _perso.InitPosition(_positionPerso);
            base.Initialize();
        }

        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map/Principale/mapcentral");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesSalle1");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles2Salle1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._perso = new AnimatedSprite(spriteSheet);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._perso.Play(_perso._animation);
            _perso._perso.Update(deltaTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers
            _tiledMapRenderer.Draw(); // on utilise la reference vers
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._perso, _perso._positionPerso);
            _myGame._spriteBatch.End(); // Game1 pour changer le graphisme                                          // Game1 pour chnager le graphisme
        }

        public bool Dehors(ushort tx, ushort ty)
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight);
            bool reponse = false;
            if (_mapLayer.GetTile(tx, ty).GlobalIdentifier == 31)
            {
                reponse = true;
            }
            return reponse;
        }

    }
}
