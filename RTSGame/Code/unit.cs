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
    private int _targetIndex;

    private bool _isSelected;
    private bool _isColliding;

    public bool moving;

    private int unitSpeed;

    private unit tempCollidingUnitIndex;
    public unit(Texture2D _unitTexture, Rectangle _unitRectangle, Color _unitColor)
    {
        this._unitTexture = _unitTexture;
        this._unitRectangle = _unitRectangle;
        this._unitColor = _unitColor;

        unitSpeed = 4;
        targetLocation = _unitRectangle.Location;

        tempCollidingUnitIndex = null;
    }

    public void move()
    {
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

        if (unitPosition.X == targetLocation.X && unitPosition.Y == targetLocation.Y)
        {
            moving = false;
        }
        else
            moving = true;
    }
    public void checkUnitCollision(Rectangle unitRect, unit unitIndex)
    {
        if (_unitRectangle.Intersects(unitRect))
        {
            isColliding = true;
            tempCollidingUnitIndex = unitIndex;
        }
        else
        if (!_unitRectangle.Intersects(unitRect))
        {
            if (unitIndex != tempCollidingUnitIndex)
            {
                isColliding = false;
                tempCollidingUnitIndex = null;
            }
        }
    }

    public void checkEnvironmentCollision()
    {

    }

    public void setTargetLocation(Point targetLocation, int _targetIndex)
    {
        this.targetLocation = targetLocation;
        this._targetIndex = _targetIndex;
    }

    public void setTargetIndex(int _targetIndex)
    {
        this._targetIndex = _targetIndex;
    }

    public Texture2D unitTexture
    {
        get { return _unitTexture; }
    }

    public Rectangle unitRectangle
    {
        get { return _unitRectangle; }
    }

    public bool isSelected
    {
        set { _isSelected = value; }
        get { return _isSelected; }
    }

    public bool isColliding
    {
        set { _isColliding = value; }
        get { return _isColliding; }
    }
    public Color unitColor
    {
        set { _unitColor = value; }
        get { return _unitColor; }
    }

    public Point unitPosition
    {
        set { _unitRectangle.Location = value; }
        get { return _unitRectangle.Location; }
    }

    public int targetIndex
    {
        get { return _targetIndex; }
    }
}
