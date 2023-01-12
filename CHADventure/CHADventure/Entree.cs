using Microsoft.Xna.Framework;
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
        private Game1 _myGame;    // Initialization des variables
        private Perso _perso;
        private Coeur _coeur;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private TiledMapTileLayer _mapLayer;
        private TiledMapTileLayer _mapLayer2;
        private Vector2 _positionPerso;

        //changement de scene :
        private bool _peutentrer = false;

        public Vector2 PositionPerso { get => _positionPerso; set => _positionPerso = value; }
        public Coeur Coeur { get => _coeur; set => _coeur = value; }
        public bool Peutentrer { get => _peutentrer; set => _peutentrer = value; }


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui esthg
        // défini dans Game1
        public Entree(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize() 
        {
            _perso = new Perso();
            Coeur = new Coeur();

            _perso._positionPerso = PositionPerso;

            base.Initialize();
        }
        public override void LoadContent()
        {
            Coeur.Initialize(); // Initialize un nouveau Coeur
            _tiledMap = Content.Load<TiledMap>("map/Entree/ExterieurMap");              //Défini la map
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree");       //Défini les couches de collisions
            _mapLayer2 = _tiledMap.GetLayer<TiledMapTileLayer>("obstaclesEntree2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            SpriteSheet spriteSheetPerso = Content.Load<SpriteSheet>("ezio/ezioAnimation.sf", new MonoGame.Extended.Serialization.JsonContentLoader()); //Load le personnage
            _perso._ezioSprite = new AnimatedSprite(spriteSheetPerso);
            Coeur.LoadContent(_myGame);
            base.LoadContent();
            

        }

        public override void Update(GameTime gameTime)  // Update les animations du perso, la map, et les déplacements du perso
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Coeur.AnimationCoeur(gameTime);
            Coeur.CoeurSprite.Play(Coeur.AnimationCoeur(gameTime));
            Coeur.CoeurSprite.Update(deltaTime);
            if (!_perso._attaque)
                _perso.DeplacementPerso(gameTime, _tiledMap, _mapLayer, _mapLayer2);
            _tiledMapRenderer.Update(gameTime);
            _perso._ezioSprite.Play(_perso._animation);
            _perso._ezioSprite.Update(deltaTime);
            OuverturePorte(_myGame.tx, _myGame.ty);
        }
        public override void Draw(GameTime gameTime) // Dessine la map, le personnage et, ainsi que le coeur
        {
            _tiledMapRenderer.Draw();
            _myGame._spriteBatch.Begin();
            _myGame._spriteBatch.Draw(_perso._ezioSprite, _perso._positionPerso);
            Coeur.Draw(_myGame._spriteBatch);
            _myGame._spriteBatch.End();

        }

        public void OuverturePorte(ushort tx, ushort ty) // permet de détecter l'endroit qui nous sert a changé de salle
        {
            tx = (ushort)(_perso._positionPerso.X / _tiledMap.TileWidth);
            ty = (ushort)(_perso._positionPerso.Y / _tiledMap.TileHeight);
            Peutentrer = false;
            if (_mapLayer2.GetTile(tx, ty).GlobalIdentifier == 224 || _mapLayer2.GetTile(tx, ty).GlobalIdentifier == 223)
            {
                Peutentrer = true;
            }
        }
    }
}