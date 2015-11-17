﻿using Ballz.Menu;
using Ballz.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ballz.Renderer
{
    public class MenuRenderer : DrawableGameComponent
    {
        private GameMenu menu;
        private GameMenu parentMenu;
        private SpriteFont menuFont;
        private SpriteBatch spriteBatch;
        private Texture2D textureSplashScreen;

        public MenuRenderer(Game game) : base(game)
        {
            menu = GameMenu.Default;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
            // Load a texture for the background.
            textureSplashScreen = Game.Content.Load<Texture2D>("Textures/Balls");
            
            // Load fonts for the menu.
            menuFont = Game.Content.Load<SpriteFont>("Fonts/Menufont");

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public void HandleMessage(object sender, Message message)
        {
            if (message.Kind == Message.MessageType.MenuMessage)
            {
                var msg = (MenuMessage)message;
                parentMenu = menu;
                menu = msg.Value;
            }
            if (message.Kind == Message.MessageType.LogicMessage)
            {
                //TODO: Check content of logicmessage as soon as it is implemented.
                Enabled = !Enabled;
                Visible = !Visible;
            }

            //TODO: handle Messages
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            // Draw a background screen.
            spriteBatch.Draw(textureSplashScreen, Game.Window.ClientBounds, Color.White);

            if (menu.Items.Count > 0)
            {
                renderMenu(menu,false);
            }
            else
            {
                renderMenu(parentMenu,true);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void renderMenu(GameMenu menu, bool showUnderscore)
        {
            // Draw the MenuTitle.
            spriteBatch.DrawString(
                menuFont,
                menu.DisplayName,
                new Vector2(Game.Window.ClientBounds.Width / 2f - menuFont.MeasureString(menu.DisplayName).X / 2, 0),
                Color.Black);

            // Draw subMenu Items.
            var itemOffset = menuFont.MeasureString(menu.DisplayName).Y + 30;
            string renderString;
            foreach (var item in menu.Items)
            {
                if (showUnderscore && item == menu.SelectedItem.Value && item.SelectionType == GameMenu.ItemType.INPUTFIELD)
                    renderString = item.DisplayName + "_";
                else
                    renderString = item.DisplayName;
                spriteBatch.DrawString(
                    menuFont,
                    renderString,
                    new Vector2(
                        Game.Window.ClientBounds.Width/8f,
                        itemOffset),
                    (menu.SelectedItem != null && menu.SelectedItem.Value == item) ? Color.Red : Color.Black);

                itemOffset += menuFont.MeasureString(renderString).Y + 30;
            }
        }
    }
}