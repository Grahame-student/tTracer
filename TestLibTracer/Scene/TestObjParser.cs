using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;
using NUnit.Framework;

namespace TestLibTracer.Scene;

internal class TestObjParser
{
    private const String FILE_EMPTY = "";
    private const String FILE_JUNK_SINGLE_LINE = "invalid line";
    private const String FILE_SINGLE_VERTICE = "v -1 1 0";
    private const String FILE_MULTI_VERTICE = "v -1 1 0\n" + 
                                              "v -1.0000 0.5000 0.0000\n" +
                                              "v 1 0 0\n" +
                                              "v 1 1 0";
    private const String FILE_VERTICE_AND_FACE = "v -1 1 0\n" +
                                                 "v -1.0000 0.5000 0.0000\n" +
                                                 "v 1 0 0\n" +
                                                 "v 1 1 0\n" +
                                                 "\n" +
                                                 "f 1 2 3\n" +
                                                 "f 1 3 4";
    private const String FILE_POLYGON = "v -1 1 0\n" +
                                        "v -1 0 0\n" +
                                        "v 1 0 0\n" +
                                        "v 1 1 0\n" +
                                        "v 0 2 0\n" +
                                        "\n" +
                                        "f 1 2 3 4 5";
    private const String FILE_MULTI_GROUP = "v -1 1 0\n" +
                                            "v -1 0 0\n" +
                                            "v 1 0 0\n" +
                                            "v 1 1 0\n" + 
                                            "\n" +
                                            "g FirstGroup\n" + 
                                            "f 1 2 3\n" +
                                            "g SecondGroup\n" +
                                            "f 1 3 4";

    private ObjParser _parser;

    [Test]
    public void Parse_SetsVerticesToEmptyPointList_WhenFileEmpty()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_EMPTY);

        Assert.That(_parser.Vertices, Is.EqualTo(new List<TPoint>()));
    }

    [Test]
    public void Parse_SetsFacesToEmptyShapeList_WhenFileEmpty()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_EMPTY);

        Assert.That(_parser.Faces, Is.EqualTo(new List<Shape>()));
    }

    [Test]
    public void Parse_SetsGroupsToEmptyGroupsDict_WhenFileEmpty()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_EMPTY);

        Assert.That(_parser.Groups, Is.EqualTo(new Dictionary<String, Group>()));
    }

    [Test]
    public void Parse_IgnoresInvalidLines_WhenFilesOnlyContainsInvalidData()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_JUNK_SINGLE_LINE);

        Assert.That(_parser.Vertices, Is.EqualTo(new List<TPoint>()));
        Assert.That(_parser.Faces, Is.EqualTo(new List<Shape>()));
        Assert.That(_parser.Groups, Is.EqualTo(new Dictionary<String, Group>()));
    }

    [Test]
    public void InvalidLines_ReturnsOne_WhenFileContainsOneInvalidLine()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_JUNK_SINGLE_LINE);

        Assert.That(_parser.LinesIgnored, Is.EqualTo(1));
    }

    [Test]
    public void Parse_AddsOneVertice_WhenFileContainsSingleVertice()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_SINGLE_VERTICE);

        Assert.That(_parser.Vertices.Count, Is.EqualTo(1));
    }

    [Test]
    public void Parse_AddsOneVerticeUsingLineData_WhenFileContainsSingleVertice()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_SINGLE_VERTICE);

        Assert.That(_parser.Vertices[0], Is.EqualTo(new TPoint(-1, 1, 0)));
    }

    [Test]
    public void Parse_AddsMultipleVertices_WhenFileContainsMultipleVertices()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_MULTI_VERTICE);

        Assert.That(_parser.Vertices.Count, Is.EqualTo(4));
    }

    [Test]
    public void Parse_AddsMultipleVerticeUsingLineData_WhenFileContainsMultipleVertices()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_MULTI_VERTICE);

        var expectedResult = new List<TPoint>
        {
            new(-1, 1,   0),
            new(-1, 0.5, 0),
            new( 1, 0,   0),
            new( 1, 1,   0)
        };
        Assert.That(_parser.Vertices, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Parse_AddsMultipleVertices_WhenFileContainsMultipleVerticesAndFaces()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_VERTICE_AND_FACE);

        Assert.That(_parser.Vertices.Count, Is.EqualTo(4));
    }

    [Test]
    public void Parse_AddsMultipleVerticeUsingLineData_WhenFileContainsMultipleVerticesAndFaces()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_VERTICE_AND_FACE);

        var expectedResult = new List<TPoint>
        {
            new(-1, 1,   0), // 1
            new(-1, 0.5, 0), // 2
            new( 1, 0,   0), // 3
            new( 1, 1,   0)  // 4
        };
        Assert.That(_parser.Vertices, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Parse_AddsMultipleFaces_WhenFileContainsMultipleVerticesAndFaces()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_VERTICE_AND_FACE);

        Assert.That(_parser.Faces.Count, Is.EqualTo(2));
    }

    [Test]
    public void Parse_AddsMultipleFacesFromLineData_WhenFileContainsMultipleVerticesAndFaces()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_VERTICE_AND_FACE);
        var expectedResult = new List<Shape>
        {
            new Triangle(
                new TPoint(-1, 1, 0),
                new TPoint(-1, 0.5, 0),
                new TPoint(1, 0, 0)),
            new Triangle(
                new TPoint(-1, 1, 0),
                new TPoint(1, 0, 0),
                new TPoint(1, 1, 0))
        };

        Assert.That(_parser.Faces, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Parse_AddsMultipleFacesToDefaultGroup_WhenFileContainsMultipleVerticesAndFacesButNoGroup()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_VERTICE_AND_FACE);
        var expectedResult = new Group();
        expectedResult.Add(new Triangle(
                new TPoint(-1, 1, 0),
                new TPoint(-1, 0.5, 0),
                new TPoint(1, 0, 0))
            );
        expectedResult.Add(new Triangle(
                new TPoint(-1, 1, 0),
                new TPoint(1, 0, 0),
                new TPoint(1, 1, 0))
        );

        Assert.That(_parser.Groups["default"], Is.EqualTo(expectedResult));
    }

    [Test]
    public void Parse_AddsThreeFacesFromLineData_WhenFileContainsSinglePolygon()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_POLYGON);
        var expectedResult = new List<Shape>
        {
            new Triangle(
                new TPoint(-1, 1, 0),    // 1
                new TPoint(-1, 0, 0),    // 2
                new TPoint(1,  0, 0)),   // 3
            new Triangle(
                new TPoint(-1, 1, 0),    // 1
                new TPoint(1,  0, 0),    // 3
                new TPoint(1,  1, 0)),   // 4
            new Triangle(
                new TPoint(-1, 1, 0),    // 1
                new TPoint(1,  1, 0),    // 4
                new TPoint(0,  2, 0))    // 5
        };

        Assert.That(_parser.Faces, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Parse_AddsMultipleGroupsFromData_WhenFileContainsGroups()
    {
        _parser = new ObjParser();

        _parser.Parse(FILE_MULTI_GROUP);
        var expectedResult = new Dictionary<String, Group>();
        var group1 = new Group();
        group1.Add(new Triangle(
            new TPoint(-1, 1, 0),
            new TPoint(-1, 0.5, 0),
            new TPoint(1, 0, 0))
        );
        expectedResult.Add("FirstGroup", group1);
        var group2 = new Group();
        group2.Add(new Triangle(
            new TPoint(-1, 1, 0),
            new TPoint(1, 0, 0),
            new TPoint(1, 1, 0))
        );
        expectedResult.Add("SecondGroup", group2);

        Assert.That(_parser.Groups, Is.EqualTo(expectedResult));
    }
}
