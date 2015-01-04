using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ForestSim.Flora;
using ForestSim.Utils;

namespace ForestSim
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Weather weather;
        Hud hud;
        Map map;
        Tree tree;
        List<Tree> trees;
        bool[,] treeMap;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Constants.WINDOWWIDTH;
            graphics.PreferredBackBufferHeight = Constants.WINDOWHEIGHT;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            trees = new List<Tree>();
            weather = new Weather();
            hud = new Hud();
            map = new Map(Constants.MAPWIDTH, Constants.MAPHEIGHT, 30);
            Tile.Content = Content;
            tree = new AdultTree(500, 500, 50, 50);
            tree.Subscribe(weather);
            tree.Spawned += new EventHandler(this.SpawnSapling);
            trees.Add(tree);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tree.Load(Content);
            hud.Load(Content);
            map.Load(Content);
            Loger.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            weather.Update(gameTime);
            hud.Update(weather);

            for (int i = 0; i < trees.Count; i++)
            {
                trees[i].Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            map.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            for (int i = 0; i < trees.Count; i++)
            {
                trees[i].Draw(spriteBatch);
            }
            Loger.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SpawnSapling(object sender, EventArgs e)
        {
            Tree parent = sender as Tree;
            Tree tree = new AdultTree(parent.X - 50, parent.Y, parent.Width, parent.Height);
            tree.Load(Content);
            tree.Subscribe(weather);
            tree.Spawned += new EventHandler(this.SpawnSapling);
            trees.Add(tree);
        }
    }
}
 