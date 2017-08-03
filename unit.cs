using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class unit
{
    private Texture2D _unitTexture;
    private Rectangle _unitRectangle;
    private Color _unitColor;

    private Point targetLocation;

    private int unitSpeed;
    public unit(Texture2D _unitTexture, Rectangle _unitRectangle, Color _unitColor)
    {
        this._unitTexture = _unitTexture;
        this._unitRectangle = _unitRectangle;
        this._unitColor = _unitColor;

        unitSpeed = 4;
        targetLocation = _unitRectangle.Location;
    }

    public void move()
    {
        //move in X axis first then move Y

        if (unitPosition.X != targetLocation.X)
        {
            if (unitPosition.X > targetLocation.X)
                unitPosition = new Point(unitPosition.X - unitSpeed, unitPosition.Y);
            if (unitPosition.X < targetLocation.X)
                unitPosition = new Point(unitPosition.X + unitSpeed, unitPosition.Y);
        }
        else
            if (unitPosition.X == targetLocation.X)
        {
            if (unitPosition.Y != targetLocation.Y)
            {
                if (unitPosition.Y > targetLocation.Y)
                    unitPosition = new Point(unitPosition.X, unitPosition.Y - unitSpeed);
                if (unitPosition.Y < targetLocation.Y)
                    unitPosition = new Point(unitPosition.X, unitPosition.Y + unitSpeed);
            }
        }
    }

    public void setTargetLocation(Point targetLocation)
    {
        this.targetLocation = targetLocation;
    }

    public Texture2D unitTexture
    {
        get { return _unitTexture; }
    }

    public Rectangle unitRectangle
    {
        get { return _unitRectangle; }
    }

    public Color unitColor
    {
        get { return _unitColor; }
    }

    public Point unitPosition
    {
        set { _unitRectangle.Location = value; }
        get { return _unitRectangle.Location; }
    }
}
