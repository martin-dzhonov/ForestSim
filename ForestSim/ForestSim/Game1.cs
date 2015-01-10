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
using ForestSim.Fauna;

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
        List<Tree> trees;
        List<Lumberjack> lumberjacks;
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
            weather = new Weather();
            hud = new Hud();

            map = new Map(Constants.MAPHEIGHT, Constants.MAPWIDTH, 30);
            trees = new List<Tree>();
            lumberjacks = new List<Lumberjack>();
            treeMap = new bool[Constants.MAPHEIGHT / Constants.TREESIZE, Constants.MAPWIDTH / Constants.TREESIZE];


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.GenerateTrees();
            this.GenerateHumans();
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
                if (trees[i].Grow && !trees[i].IsDead)
                {
                    if (trees[i] is Sapling)
                    {
                        trees[i] = new AdultTree(trees[i]);
                        trees[i].Load(Content);
                        trees[i].Subscribe(weather);
                        trees[i].Spawned += new EventHandler(this.SpawnSapling);
                    }
                    if (trees[i] is AdultTree)
                    {
                        trees[i] = new ElderTree(trees[i]);
                        trees[i].Load(Content);
                        trees[i].Subscribe(weather);
                        trees[i].Spawned += new EventHandler(this.SpawnSapling);
                    }
                }
            }

            for (int i = 0; i < lumberjacks.Count; i++)
            {
                lumberjacks[i].Update(gameTime);
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
            for (int i = 0; i < lumberjacks.Count; i++)
            {
                lumberjacks[i].Draw(spriteBatch);
            }

            Loger.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SpawnSapling(object sender, EventArgs e)
        {
            Tree parent = sender as Tree;

            List<int> directionsRow = new List<int>{-1, -1, -1, 0, 0, 1, 1, 1};
            List<int> directionsCol = new List<int>{-1, 0, 1, -1, 1, -1, 0, 1};

            int parentRow = parent.Y / Constants.TREESIZE;
            int parentCol = parent.X / Constants.TREESIZE;

            for (int i = 0; i < directionsRow.Count; i++)
            {
                int newTreeRow = parentRow + directionsRow[i];
                int newTreeCol = parentCol + directionsCol[i];
                if (newTreeRow < 0 || newTreeRow >= treeMap.GetLength(0)
                    || newTreeCol <0 || newTreeCol >= treeMap.GetLength(1))
                {
                    directionsRow.RemoveAt(i);
                    directionsCol.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < directionsRow.Count; i++)
            {
                int newTreeRow = parentRow + directionsRow[i];
                int newTreeCol = parentCol + directionsCol[i];
                if (treeMap[newTreeRow, newTreeCol])
                {
                    directionsRow.RemoveAt(i);
                    directionsCol.RemoveAt(i);
                    i--;
                }
            }

            bool isSpotAvalable = directionsRow.Count > 0;

            if (isSpotAvalable)
            {
                int randomDirIndex = RandomGenerator.GetRandomInt(0, directionsRow.Count - 1);
                int newTreeRow = parentRow + directionsRow[randomDirIndex];
                int newTreeCol = parentCol + directionsCol[randomDirIndex];
                int newTreeY = newTreeRow * Constants.TREESIZE;
                int newTreeX = newTreeCol * Constants.TREESIZE;

                Tree newTree = new Sapling(newTreeX, newTreeY);
                newTree.Load(Content);

                newTree.Subscribe(weather);
                newTree.Spawned += new EventHandler(this.SpawnSapling);

                MarkTreeMap(newTree);
                trees.Add(newTree);
            }
        }

        private void MarkTreeMap(Tree tree)
        {
            treeMap[tree.Y / Constants.TREESIZE, tree.X / Constants.TREESIZE] = true;
        }

        private void GenerateTrees()
        {
            for (int i = 0; i < treeMap.GetLength(1); i++)
            {
                for (int j = 0; j < treeMap.GetLength(0); j++)
                {
                    int treeSpawnChance = 20;
                    if (RandomGenerator.GetRandomInt(1, 100) < treeSpawnChance)
                    {
                        Tree newTree = new AdultTree(i * Constants.TREESIZE, j * Constants.TREESIZE);
                        newTree.Load(Content);

                        newTree.Subscribe(weather);
                        newTree.Spawned += new EventHandler(this.SpawnSapling);

                        MarkTreeMap(newTree);
                        trees.Add(newTree);
                    }
                }
            }
        }

        private void GenerateHumans()
        {
            Lumberjack lj = new Lumberjack(500, 500, 40, 40);
            lj.Load(Content);
            lumberjacks.Add(lj);
        }

    }
}
