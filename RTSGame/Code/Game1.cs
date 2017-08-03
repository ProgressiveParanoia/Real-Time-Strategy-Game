﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace RTSGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<int> unitSelectionReferencePoints;

        private List<Node> nodes;
        private BoxSelection unitSelection;

        private List<unit> playerUnits;

        private unit testUnit;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            unitSelectionReferencePoints = new List<int>();

            for(int i = 0; i < 4; i++)
            {
                unitSelectionReferencePoints.Add(0);
            }

            unitSelection = new BoxSelection(Content.Load<Texture2D>(@"whiteLine"));

            //level generation. move to level generator cs later on
            nodes = new List<Node>();

            int nodeSize = 48; //equal size for each axis
            int nodePositionX = 0;
            int nodePositionY = 0;

            for (int i = 0; ; i++)
            {
                Node TestNode = new Node(Content.Load<Texture2D>(@"grassTexture"), new Rectangle(nodePositionX, nodePositionY, nodeSize, nodeSize), Color.White);
                nodes.Add(TestNode);

                if (nodePositionX > Window.ClientBounds.Width && nodePositionY > Window.ClientBounds.Height)
                    break;

                if (nodePositionX > Window.ClientBounds.Width)
                {
                    nodePositionX = 0;
                    nodePositionY += nodeSize;
                }
                else
                    nodePositionX += nodeSize;
            }

            int playerSize = 32;

            playerUnits = new List<unit>();

            for (int i = 0; i < 3; i++)
            {
                testUnit = new unit(Content.Load<Texture2D>(@"LIFE"), new Rectangle(nodes[22 + i].nodePosition.X, nodes[22 + i].nodePosition.Y, playerSize, nodeSize), Color.White);
                testUnit.setTargetIndex(22 + i);
                playerUnits.Add(testUnit);
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Rectangle mouseRect = new Rectangle(Mouse.GetState().Position.X - 16,Mouse.GetState().Position.Y - 16,32,32);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //node traversal
            //foreach (Node n in nodes)
            //{
            //    if (n.nodeRectangle.Intersects(mouseRect))
            //    {
            //        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            //        {
            //            if (testUnit.isSelected && !unitSelection.canDraw)
            //            {
            //                testUnit.setTargetLocation(n.nodePosition);
            //                break;
            //            }
            //        }
            //    }
            //}

            //node traversal
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                for (int nodeIndex = 0; nodeIndex < nodes.Count; nodeIndex++)
                {
                    if (nodes[nodeIndex].nodeRectangle.Intersects(mouseRect))
                    {
                        for (int playerUnitIndex = 0; playerUnitIndex < playerUnits.Count; playerUnitIndex++)
                        {
                            if(playerUnits[playerUnitIndex].isSelected && !unitSelection.canDraw)
                            {
                                //if (!nodes[nodeIndex].occupied && nodes[nodeIndex].occupantIndex == -1)
                                //{
                                //    playerUnits[playerUnitIndex].setTargetLocation(nodes[nodeIndex].nodePosition, nodeIndex);

                                nodes[nodeIndex].occupantIndex = playerUnitIndex;
                                //}

                                //if (nodes[nodeIndex].occupied)
                                //{
                                //    playerUnits[playerUnitIndex].setTargetLocation(nodes[nodeIndex - 1].nodePosition, nodeIndex - 1);
                                //    Console.WriteLine("stupidthang:" + nodeIndex + "past index:" + (nodeIndex - 1));
                                //}
                                playerUnits[playerUnitIndex].setTargetLocation(nodes[nodeIndex].nodePosition,nodeIndex);
                            }
                        }
                    }
                }
            }

            //node checker
            for(int nodeIndex = 0; nodeIndex < nodes.Count; nodeIndex++)
            {
                for(int playerUnitIndex = 0; playerUnitIndex < playerUnits.Count; playerUnitIndex++)
                {
                    if (nodes[nodeIndex].occupantIndex == playerUnitIndex)
                    {
                        if (!playerUnits[playerUnitIndex].unitRectangle.Intersects(nodes[nodeIndex].nodeRectangle))
                        {
                            if (nodes[nodeIndex].occupied)
                            {
                                nodes[nodeIndex].occupantIndex = -1;
                                nodes[nodeIndex].occupied = false;
                            }
                        }

                        if (playerUnits[playerUnitIndex].unitRectangle.Intersects(nodes[nodeIndex].nodeRectangle))
                        {
                            nodes[nodeIndex].occupied = true;
                        }

                        if (playerUnits.Where(Unit => Unit.isSelected == true).Count() > 1 && !unitSelection.canDraw)
                        {
                            if (nodes[nodeIndex].occupied)
                            {
                                if (playerUnits[nodes[nodeIndex].occupantIndex].unitRectangle.Intersects(playerUnits[playerUnitIndex].unitRectangle))
                                {
                                    playerUnits[playerUnitIndex].setTargetLocation(nodes[nodeIndex-1].nodePosition, nodeIndex-1);
                                }
                            }
                        }
                    }
                }
            }

            if (unitSelection.canDraw)
            {
                /* Index references: [0] = BoxRectY [1] = BoxRectY + BoxRectHeight [2] = BoxRectX [3] = BoxRectX + BoxRectWidth*/

                foreach (unit selectUnit in playerUnits)
                {
                    if ((unitSelectionReferencePoints[0] <= selectUnit.unitPosition.Y && unitSelectionReferencePoints[1] >= selectUnit.unitPosition.Y) &&
                        (unitSelectionReferencePoints[2] <= selectUnit.unitPosition.X && unitSelectionReferencePoints[3] >= selectUnit.unitPosition.X) || //all positive sides
                        (unitSelectionReferencePoints[0] >= selectUnit.unitPosition.Y && unitSelectionReferencePoints[1] <= selectUnit.unitPosition.Y) &&
                        (unitSelectionReferencePoints[2] >= selectUnit.unitPosition.X && unitSelectionReferencePoints[3] <= selectUnit.unitPosition.X) || //all negative sides
                        (unitSelectionReferencePoints[0] >= selectUnit.unitPosition.Y && unitSelectionReferencePoints[1] <= selectUnit.unitPosition.Y) &&
                        (unitSelectionReferencePoints[2] <= selectUnit.unitPosition.X && unitSelectionReferencePoints[3] >= selectUnit.unitPosition.X) || // posiitive X axis negative Y axis
                        (unitSelectionReferencePoints[0] <= selectUnit.unitPosition.Y && unitSelectionReferencePoints[1] >= selectUnit.unitPosition.Y) &&
                        (unitSelectionReferencePoints[2] >= selectUnit.unitPosition.X && unitSelectionReferencePoints[3] <= selectUnit.unitPosition.X)) // negative X axis positive Y axis
                    {
                        selectUnit.isSelected = true;
                    }
                }
            }

            if(Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                clearSelection();
            }

            foreach (unit selectUnit in playerUnits)
            {
                selectUnit.move();
            }

            unitSelection.generateBox();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
                
            foreach(Node n in nodes)
            {
                spriteBatch.Draw(n.nodeTexture, n.nodeRectangle, n.nodeColor);
            } 

            spriteBatch.Draw(testUnit.unitTexture, testUnit.unitRectangle, testUnit.unitColor);

            foreach(unit u in playerUnits)
            {
                spriteBatch.Draw(u.unitTexture, u.unitRectangle, u.unitColor);
            }

            if (unitSelection.canDraw)
            {
                unitSelection.generateHorizontalLine(unitSelection.BoxRect.Y, spriteBatch);
                unitSelection.generateHorizontalLine(unitSelection.BoxRect.Y + unitSelection.BoxRect.Height, spriteBatch);
                unitSelection.generateVerticalLine(unitSelection.BoxRect.X, spriteBatch);
                unitSelection.generateVerticalLine(unitSelection.BoxRect.X + unitSelection.BoxRect.Width, spriteBatch);

                unitSelectionReferencePoints[0] = unitSelection.BoxRect.Y;
                unitSelectionReferencePoints[1] = unitSelection.BoxRect.Y + unitSelection.BoxRect.Height;
                unitSelectionReferencePoints[2] = unitSelection.BoxRect.X;
                unitSelectionReferencePoints[3] = unitSelection.BoxRect.X + unitSelection.BoxRect.Width;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void clearSelection()
        {
            foreach (unit selectUnit in playerUnits)
            {
                selectUnit.isSelected = false;
            }

            unitSelection.canDraw = false;
        }
    }
}
