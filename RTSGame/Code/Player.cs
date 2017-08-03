using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player 
{
    private Texture2D _playerTexture;
    private Rectangle _playerRectangle;
    private Color _playerColor;

    private Point targetLocation;

    private int playerSpeed;
	public Player(Texture2D _playerTexture, Rectangle _playerRectangle, Color _playerColor)
	{
        this._playerTexture = _playerTexture;
        this._playerRectangle = _playerRectangle;
        this._playerColor = _playerColor;

        playerSpeed = 4;
        targetLocation = _playerRectangle.Location;
	}

    public void move()
    {
        //move in X axis first then move Y

        if (playerPosition.X != targetLocation.X)
        {
            if(playerPosition.X > targetLocation.X)
                playerPosition = new Point(playerPosition.X - playerSpeed, playerPosition.Y);
            if(playerPosition.X < targetLocation.X)
                playerPosition = new Point(playerPosition.X + playerSpeed, playerPosition.Y);
        }else
            if(playerPosition.X == targetLocation.X)
        {
            if(playerPosition.Y != targetLocation.Y)
            {
                if (playerPosition.Y > targetLocation.Y)
                    playerPosition = new Point(playerPosition.X, playerPosition.Y - playerSpeed);
                if (playerPosition.Y < targetLocation.Y)
                    playerPosition = new Point(playerPosition.X, playerPosition.Y + playerSpeed);
            }
        }
    }

    public void setTargetLocation(Point targetLocation)
    {
        this.targetLocation = targetLocation;
    }
    
    public Texture2D playerTexture
    {
        get { return _playerTexture; }
    }

    public Rectangle playerRectangle
    {
        get { return _playerRectangle; }
    }

    public Color playerColor
    {
        get { return _playerColor; }
    }

    public Point playerPosition
    {
        set { _playerRectangle.Location = value; }
        get { return _playerRectangle.Location; }
    }
}
