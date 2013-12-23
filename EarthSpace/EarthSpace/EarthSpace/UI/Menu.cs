using EarthSpace.Graphics;
using EarthSpace.Graphics.Drawables;
using EarthSpace.Input;
using EarthSpace.Input.InputHandlers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.UI
{
    public class Menu : EarthSpace.Graphics.IDrawable
    {
        #region Fields

        SpriteFont titleFont;
        SpriteFont entryFont;

        Color titleColor = Color.Yellow;
        Color entryColor = Color.White;
        Color entryColorSelected = Color.Yellow;
        Color entryColorDisabled = Color.Gray;

        Label titleLabel;
        List<Label> entryLabels = new List<Label>();

        int selectedIndex;
        Sprite selectionSprite;
        float spriteOffset;

        List<Action> entryActions = new List<Action>();
        List<Int32> disabledEntries = new List<Int32>();

        Vector2 titlePosition = new Vector2(GraphicsManager.Viewport.Width / 2, GraphicsManager.Viewport.Height / 5);
        Vector2 entryPosition = new Vector2(GraphicsManager.Viewport.Width / 2, GraphicsManager.Viewport.Height / 3);

        bool allowCancel = false;

        InputHandler moveUp;
        InputHandler moveDown;
        InputHandler select;
        InputHandler cancel;
        InputHandler clickSelect;
        List<InputHandler> hoverHandlers = new List<InputHandler>();


        #endregion Fields

        #region Initialization

        public Menu(string title, SpriteFont titleFont, SpriteFont entryFont)
        {
            this.titleFont = titleFont;
            this.entryFont = entryFont;

            titleLabel = new Label();
            titleLabel.Font = titleFont;
            titleLabel.Text = title;
            titleLabel.Origin = titleLabel.MeasureText() / 2;
            titleLabel.Position = titlePosition;

            selectionSprite = new Sprite();

            moveUp = new KeyPressHandler(Keys.Up);
            moveDown = new KeyPressHandler(Keys.Down);
            select = new KeyPressHandler(Keys.Space, Keys.Enter);
            cancel = new KeyPressHandler(Keys.Escape);
            clickSelect = new ClickHandler(MouseButton.Left);

            moveUp.OnTrigger += OnMoveUp;
            moveDown.OnTrigger += OnMoveDown;
            select.OnTrigger += OnSelect;
            cancel.OnTrigger += OnCancel;
            clickSelect.OnTrigger += OnSelect;
        }

        #endregion

        #region Properties

        public Color TitleColor
        {
            get { return titleColor; }
            set
            {
                titleColor = value;
                titleLabel.Color = value;
            }
        }

        public Color EntryColor
        {
            get { return entryColor; }
            set
            {
                entryColor = value;

                for (int i = 0; i < entryLabels.Count(); i++)
                {
                    if (i != selectedIndex)
                    {
                        entryLabels[i].Color = value;
                    }
                }
            }
        }

        public Color EntryColorSelected
        {
            get { return entryColorSelected; }
            set
            {
                entryColorSelected = value;

                entryLabels[selectedIndex].Color = value;
            }
        }

        public Color EntryColorDisabled
        {
            get { return entryColorDisabled; }
            set
            {
                entryColorDisabled = value;

                foreach (Int16 index in disabledEntries)
                {
                    entryLabels[index].Color = value;
                }
            }
        }

        public Sprite SelectionSprite
        {
            get { return selectionSprite; }
        }

        public bool AllowCancel
        {
            get { return allowCancel; }
            set { allowCancel = value; }
        }

        #endregion

        #region IDrawable

        public void Show()
        {
            titleLabel.Show();

            PositionSprite();
            selectionSprite.Show();

            foreach (Label entryLabel in entryLabels)
            {
                entryLabel.Show();
            }

            moveUp.Enable();
            moveDown.Enable();
            select.Enable();
            if (allowCancel) cancel.Enable();
            clickSelect.Enable();

            foreach (InputHandler handler in hoverHandlers)
            {
                handler.Enable();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Hide()
        {
            titleLabel.Hide();
            selectionSprite.Hide();

            foreach (Label entryLabel in entryLabels)
            {
                entryLabel.Hide();
            }

            moveUp.Disable();
            moveDown.Disable();
            select.Disable();
            if (allowCancel) cancel.Disable();
            clickSelect.Disable();
            
            foreach (InputHandler handler in hoverHandlers)
            {
                handler.Disable();
            }
        }

        #endregion IDrawable

        #region Entry Management

        private Vector2 NextPosition(Label entry)
        {
            Vector2 position = entryPosition;

            entryPosition.Y += entry.MeasureText().Y;

            return position;
        }

        public void AddEntry(string text, Action onSelected)
        {
            Label entryLabel = new Label();
            entryLabel.Font = entryFont;
            entryLabel.Color = selectedIndex == entryLabels.Count() ? entryColorSelected : entryColor;
            entryLabel.Text = text;
            entryLabel.Origin = entryLabel.MeasureText() / 2;
            entryLabel.Position = NextPosition(entryLabel);

            int index = entryLabels.Count();
            entryLabels.Add(entryLabel);
            entryActions.Add(onSelected);

            if (entryLabel.MeasureText().X / 2 > spriteOffset)
            {
                spriteOffset = entryLabel.MeasureText().X / 2;
            }

            MouseHoverHandler handler = new MouseHoverHandler(entryLabel.ScreenArea());
            handler.OnTrigger += (input) =>
            {
                SelectIndex(index);
            };
            hoverHandlers.Add(handler);
        }

        public void AddCancelEntry(string text)
        {
            AddEntry(text, OnCancel);
        }

        public void DisableEntry(int index)
        {
            if (!disabledEntries.Contains(index))
            {
                disabledEntries.Add(index);
                entryLabels[index].Color = entryColorDisabled;
            }
        }

        public void EnableEntry(int index)
        {
            if (disabledEntries.Contains(index))
            {
                disabledEntries.Remove(index);
                entryLabels[index].Color = EntryColor;
            }
        }

        #endregion Entry Management

        #region Events

        private void SelectIndex(int index)
        {
            if (!disabledEntries.Contains(selectedIndex))
            {
                entryLabels[selectedIndex].Color = entryColor;
            }
            
            selectedIndex = index;
            (clickSelect as ClickHandler).ClickArea = entryLabels[selectedIndex].ScreenArea();

            PositionSprite();

            if (!disabledEntries.Contains(selectedIndex))
            {
                entryLabels[selectedIndex].Color = entryColorSelected;
            }
        }

        private void PositionSprite()
        {
            Vector2 spritePosition = entryLabels[selectedIndex].Position;
            spritePosition.X -= spriteOffset;
            spritePosition.X -= selectionSprite.Width;
            spritePosition.Y -= selectionSprite.Height / 2;

            selectionSprite.CenterOrigin();
            selectionSprite.Position = spritePosition;
        }

        private void OnMoveUp(InputState input)
        {
            int index = selectedIndex - 1;

            if (index < 0)
            {
                index = entryLabels.Count() - 1;
            }

            SelectIndex(index);
        }

        private void OnMoveDown(InputState input)
        {
            int index = selectedIndex + 1;

            if (index >= entryLabels.Count())
            {
                index = 0;
            }

            SelectIndex(index);
        }

        private void OnSelect(InputState input)
        {
            if (entryActions[selectedIndex] != null && !disabledEntries.Contains(selectedIndex))
            {
                entryActions[selectedIndex].Invoke();
            }
        }

        private void OnCancel(InputState input)
        {
            Hide();
        }

        private void OnCancel()
        {
            Hide();
        }

        #endregion
    }
}
