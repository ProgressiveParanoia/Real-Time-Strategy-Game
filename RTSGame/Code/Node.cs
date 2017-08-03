using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Node
{
    private Texture2D _nodeTexture;
    private Rectangle _nodeRectangle;
    private Color _nodeColor;

	public Node(Texture2D _nodeTexture, Rectangle _nodeRectangle, Color _nodeColor)
	{
        this._nodeTexture = _nodeTexture;
        this._nodeRectangle = _nodeRectangle;
        this._nodeColor = _nodeColor;
	}

    public Texture2D nodeTexture
    {
        set { _nodeTexture = value; }
        get { return _nodeTexture; }
    }

    public Rectangle nodeRectangle
    {
        set { _nodeRectangle = value; }
        get { return _nodeRectangle; }
    }

    public Point nodePosition
    {
        get { return _nodeRectangle.Location; }
    }

    public Color nodeColor
    {
        set { _nodeColor = value; }
        get { return _nodeColor; }
    }
}
