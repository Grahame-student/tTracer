using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene;

public class ObjParser
{
    private const Int32 POS_COMMAND = 0;
    private const Int32 POS_X = 1;
    private const Int32 POS_Y = 2;
    private const Int32 POS_Z = 3;
    private const Int32 POS_GROUP = 1;

    private const String DEFAULT_GROUP = "default";

    private String _currentGroup;

    public IList<TPoint> Vertices { get; }
    public IList<Shape> Faces { get; }
    public IDictionary<String, Group> Groups { get; }
    public UInt32 LinesIgnored { get; private set; }

    public ObjParser()
    {
        Vertices = new List<TPoint>();
        Faces = new List<Shape>();
        Groups = new Dictionary<String, Group>();
    }

    public void Parse(String fileContent)
    {
        _currentGroup = DEFAULT_GROUP;
        foreach (String line in fileContent.SplitToLines())
        {
            ParseLine(line);
        }
    }

    private void ParseLine(String line)
    {
        String[] tokens = line.Split(" ");

        switch (tokens[POS_COMMAND])
        {
            case "g":
                SetGroup(tokens);
                break;  
            case "v":
                AddVertice(tokens);
                break;
            case "f":
                AddFace(tokens);
                break;
            default:
                LinesIgnored++;
                break;
        }
    }

    private void SetGroup(IReadOnlyList<String> tokens)
    {
        _currentGroup = tokens[POS_GROUP];
    }

    private void AddVertice(IReadOnlyList<String> tokens)
    {
        var validTokens = true;
        validTokens &= Double.TryParse(tokens[POS_X], out Double x);
        validTokens &= Double.TryParse(tokens[POS_Y], out Double y);
        validTokens &= Double.TryParse(tokens[POS_Z], out Double z);

        // TODO: Fail fast, but should provide some debugging feedback
        if (!validTokens) return;

        // TODO: additional test cases
        // * insufficient tokens

        Vertices.Add(new TPoint(x, y, z));
    }

    private void AddFace(String[] tokens)
    {
        List<Int32> verts = GetPolygonVerts(tokens, out Boolean validTokens);

        // TODO: Fail fast, but should provide some debugging feedback
        if (!validTokens) return;

        if (!Groups.ContainsKey(_currentGroup))
        {
            Groups.Add(_currentGroup, new Group());
        }

        // TODO: additional test cases
        // * insufficient tokens (3+ required for a poly)
        // * parsed verts out of bounds of call vert list

        for (var i = 1; i < verts.Count - 1; i++)
        {
            // Obj file assumes vertices are 1 aligned,
            // be sure to adjust as our list is 0 aligned
            var triangle = new Triangle(
                Vertices[0],
                Vertices[i],
                Vertices[i + 1]);
            Faces.Add(triangle);
            Groups[_currentGroup].Add(triangle);
        }
    }

    private static List<Int32> GetPolygonVerts(String[] tokens, out Boolean validTokens)
    {
        // Remove the command token, so we can iterate over the remaining vertices
        String[] vertTokens = tokens[1..];
        var verts = new List<Int32>();
        validTokens = true;
        foreach (String vertice in vertTokens)
        {
            validTokens &= Int32.TryParse(vertice, out Int32 vert);
            verts.Add(vert); // Invalid vert will be 0 by default
        }

        return verts;
    }
}
