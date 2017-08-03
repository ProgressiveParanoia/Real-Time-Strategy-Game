using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class BoxSelection 
{
    private Rectangle _BoxSelection;
    private Texture2D _BoxTexture;

    private MouseState previousMouseState;

    private bool _canDraw;

    private bool _HorizontalGreater;
    private bool _HorizontalLesser;

    private bool _VerticalGreater;
    private bool _VerticalLesser;

	public BoxSelection(Texture2D _BoxTexture)
	{
        this._BoxTexture = _BoxTexture;

        _BoxSelection = new Rectangle(-1, -1, 0, 0);
        previousMouseState = Mouse.GetState();
	}

    public void generateBox()
    {
        MouseState currentState = Mouse.GetState();

        if (currentState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            _BoxSelection = new Rectangle(currentState.X, currentState.Y, 10, 5);
        }
        //start generation of X and Y axis
        if(currentState.LeftButton == ButtonState.Pressed)
        {
            _BoxSelection = new Rectangle(_BoxSelection.X, _BoxSelection.Y, currentState.X - _BoxSelection.X, currentState.Y - _BoxSelection.Y);
        }

        //reset once released
        if(currentState.LeftButton == ButtonState.Released)
        {
            _BoxSelection = new Rectangle(-1, -1, 0, 0);
        }

        if(_BoxSelection.X != -1 && _BoxSelection.Y != -1)
        {
            _canDraw = true;
        }
        else
        {
            _canDraw = false;
        }

        previousMouseState = currentState;
    }

    public void generateHorizontalLine(int posY, SpriteBatch spriteBatch)
    {

        if (_BoxSelection.Width > 0)
        {

            for(int i = 0; i <= _BoxSelection.Width - 10; i += 10)
            {
                if(_BoxSelection.Width - i >= 0)
                {
                    spriteBatch.Draw(BoxTexture, new Rectangle(_BoxSelection.X + i, posY, 10, 5), Color.White);

                }
            }
            //("Horizontal: Positive. First Point: "+posY);
        }else
            if(_BoxSelection.Width < 0)
        {
            for(int i = -10; i >= _BoxSelection.Width; i -= 10)
            {
                if(_BoxSelection.Width - i <= 0)
                {
                    spriteBatch.Draw(BoxTexture, new Rectangle(_BoxSelection.X + i, posY, 10, 5), Color.White);
                }
            }
            //("Horizontal: Negative. First Point:"+posY);
        }
    }

    public void generateVerticalLine(int posX, SpriteBatch spriteBatch)
    {

        if(_BoxSelection.Height > 0)
        {
            for(int i = -2; i <= _BoxSelection.Height; i += 10)
            {
                if(_BoxSelection.Height - i >= 0)
                {
                    spriteBatch.Draw(BoxTexture, new Rectangle(posX, _BoxSelection.Y + i, 10, 5), new Rectangle(0,0, BoxTexture.Width,BoxTexture.Height
                     ),Color.White, MathHelper.ToRadians(90),new Vector2(0,0),SpriteEffects.None, 0);

                }
            }
            //("Vertical: Positive. First Point:"+posX);
        }else
            if(_BoxSelection.Height < 0)
        {
            for(int i = 0; i >= _BoxSelection.Height; i -= 10)
            {
                if(_BoxSelection.Height - i <= 0)
                {
                    spriteBatch.Draw(BoxTexture, new Rectangle(posX, _BoxSelection.Y + i, 10, 5), new Rectangle(0, 0, BoxTexture.Width, BoxTexture.Height
                     ), Color.White, MathHelper.ToRadians(90), new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }
            //("Vertical: Negative. First Point:" + posX);
        }
    }

    public Rectangle BoxRect
    {
        get { return _BoxSelection; }
    }

    public Texture2D BoxTexture
    {
        get { return _BoxTexture; }
    }

    public bool canDraw
    {
        get { return _canDraw; }
    }

}
