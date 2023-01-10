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
    public class ParcoursDroit : GameScreen
    {
        private Perso _perso = new Perso();
        private Game1 _myGame;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public ParcoursDroit(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("map/Parcours/parcoursdroit");
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("plateforme");
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("acide");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader());
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            base.LoadContent();
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers
                                                       // Game1 pour chnager le graphisme
        }
    }
}
