using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public struct Hex
{
    
	public const float outerRadius = 2f; //dependent on size defined in MapGenerator; hardcoding for PoC for now
	public const float innerRadius = outerRadius * 0.866025404f;
    public static Vector3[] corners = {
		new Vector3(0f, outerRadius, 0f),
		new Vector3(innerRadius, 0.5f * outerRadius, 0f),
		new Vector3(innerRadius, -0.5f * outerRadius, 0f),
		new Vector3(0f, -outerRadius, 0f),
		new Vector3(-innerRadius, -0.5f * outerRadius, 0f),
		new Vector3(-innerRadius, 0.5f * outerRadius, 0f),
        new Vector3(0f, outerRadius, 0f)
	};
    #nullable enable
    public Tile? tile;
    #nullable disable
    public int height;

    public Hex(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
        if (q + r + s != 0) throw new ArgumentException("q + r + s must be 0");
        tile = null;
        height = -4; //default to deep Ocean
    }
    public readonly int q;
    public readonly int r;
    public readonly int s;

    public bool Equals(Hex b)
    {
        return q == b.q && r == b.r && s == b.s;
    }

    public Hex Add(Hex b)
    {
        return new Hex(q + b.q, r + b.r, s + b.s);
    }


    public Hex Subtract(Hex b)
    {
        return new Hex(q - b.q, r - b.r, s - b.s);
    }


    public Hex Scale(int k)
    {
        return new Hex(q * k, r * k, s * k);
    }


    public Hex RotateLeft()
    {
        return new Hex(-s, -q, -r);
    }

    public Hex RotateRight()
    {
        return new Hex(-r, -s, -q);
    }

    static public List<Hex> directions = new List<Hex>{new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, 1), new Hex(-1, 1, 0), new Hex(0, 1, -1)};

    static public Hex Direction(int direction)
    {
        return Hex.directions[direction];
    }


    public Hex Neighbor(int direction)
    {
        return Add(Hex.Direction(direction));
    }

    static public List<Hex> diagonals = new List<Hex>{new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2)};

    public Hex DiagonalNeighbor(int direction)
    {
        return Add(Hex.diagonals[direction]);
    }


    public int Length()
    {
        return (int)((Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2);
    }


    public int Distance(Hex b)
    {
        return Subtract(b).Length();
    }
}

