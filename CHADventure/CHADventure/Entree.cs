using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace CHADventure
{
    public class Entree : GameScreen
    {
        private Game1 _myGame;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private Vector2 position;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Entree(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            
            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("Entree");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            _tiledMapRenderer.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(); // on utilise la reference vers
                                      // Game1 pour chnager le graphisme
        }
        public void PositionPerso(ClassPerso perso, Vector2 position)
        {
            perso.InitPosition(position);
        }
    }
}
