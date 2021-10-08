using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
    class InputHandler
    {
        // The current and previous mouse/keyboard states.
        MouseState currentMouseState, previousMouseState;
        KeyboardState currentKeyboardState, previousKeyboardState;
        

        public void Update()
        {
            // update the mouse and keyboard states
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        // Gets the current position of the mouse cursor.
        public Vector2 MousePosition
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        // Returns whether or not the left mouse button has just been pressed.
        public bool MouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }

        // Returns whether or not the right mouse button has just been pressed.
        public bool MouseRightButtonPressed()
        {
            return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
        }

        // Returns whether or not a given keyboard key has just been pressed.
        public bool KeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
        }

        // Returns whether or not a given keyboard key is currently being held down.
        public bool KeyDown(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }
    }
}
