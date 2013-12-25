using EarthSpace.Graphics;
using EarthSpace.Graphics.Drawables;
using EarthSpace.Input;
using EarthSpace.Input.InputHandlers;
using EarthSpace.Processing.Processes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EarthSpace.UI
{
    public class Menu : EarthSpace.Graphics.IDrawable
    {
        #region Fields

        private SpriteFont titleFont;
        private SpriteFont entryFont;

        private Color titleColor = Color.Yellow;
        private Color entryColor = Color.White;
        private Color entryColorSelected = Color.Yellow;
        private Color entryColorDisabled = Color.Gray;

        private Label titleLabel;
        private List<Label> entryLabels = new List<Label>();

        private int selectedIndex;
        private Sprite selectionSprite;
        private float spriteOffset;

        private List<Action> entryActions = new List<Action>();
        private List<Int32> disabledEntries = new List<Int32>();

        private Vector2 titlePosition = new Vector2(GraphicsManager.Viewport.Width / 2, GraphicsManager.Viewport.Height / 5);
        private Vector2 entryPosition = new Vector2(GraphicsManager.Viewport.Width / 2, GraphicsManager.Viewport.Height / 3);

        private bool allowCancel = false;

        private InputHandler moveUp;
        private InputHandler moveDown;
        private InputHandler select;
        private InputHandler cancel;
        private InputHandler clickSelect;
        private List<InputHandler> hoverHandlers = new List<InputHandler>();

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

        #endregion Initialization

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

                if (selectedIndex < entryLabels.Count())
                {
                    entryLabels[selectedIndex].Color = value;
                }
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

        #endregion Properties

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
                TransitionProcess colorChange = new TransitionProcess(
                    TransitionProcess.SmoothStep, 0, 1, 0.5f,
                    (x) =>
                    {
                        //Vector3 color = ((entryColorSelected.ToVector3() - entryColor.ToVector3()) * x) + entryColor.ToVector3();
                        //entryLabels[selectedIndex].Color = new Color(color);
                        entryLabels[selectedIndex].Color = Color.Lerp(entryColorSelected, entryColor, x);
                    });
                colorChange.Begin(); 
            }

            selectedIndex = index;
            (clickSelect as ClickHandler).ClickArea = entryLabels[selectedIndex].ScreenArea();

            PositionSprite();

            if (!disabledEntries.Contains(selectedIndex))
            {
                TransitionProcess colorChange = new TransitionProcess(
                    TransitionProcess.SmoothStep, 0, 1, 0.5f,
                    (x) => 
                    {
                        //Vector3 color = ((entryColorSelected.ToVector3() - entryColor.ToVector3()) * x) + entryColor.ToVector3();
                        //entryLabels[selectedIndex].Color = new Color(color);
                        entryLabels[selectedIndex].Color = Color.Lerp(entryColor, entryColorSelected, x);
                    });
                colorChange.Begin(); 
                
            }
        }

        private void PositionSprite()
        {
            Vector2 spritePosition = entryLabels[selectedIndex].Position;
            spritePosition.X -= spriteOffset;
            spritePosition.X -= selectionSprite.Width / 2;

            //spritePosition.Y -= selectionSprite.Height / 2;

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

        #endregion Events
    }
}