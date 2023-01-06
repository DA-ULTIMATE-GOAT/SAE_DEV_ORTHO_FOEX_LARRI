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
        private readonly ScreenManager _screenManager;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer; 
        private TiledMapTileLayer _mapLayer2;
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private String _animation;

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
            _animation = "idle";
            base.Initialize();
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("map/Entree/ExterieurMap");
             _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree");
             _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("persoAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            var instancePerso = new ClassPerso();
            instancePerso.DeplacementPerso(gameTime);
            _tiledMapRenderer.Update(gameTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(); // on utilise la reference vers
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();                         // Game1 pour changer le graphisme
        }

        /*private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if ((_mapLayer.TryGetTile(x, y, out tile) == false) && (_mapLayer2.TryGetTile(x, y, out tile) == false))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }*/


        public bool OuverturePorte(ushort tx, ushort ty)
        {
            tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
            ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
            bool reponse = false;
            if (_mapLayer2.GetTile(tx, ty).GlobalIdentifier == 224 || _mapLayer2.GetTile(tx, ty).GlobalIdentifier == 223)
            {
                reponse = true;
            }
            return reponse;
            
        }
    }
}
