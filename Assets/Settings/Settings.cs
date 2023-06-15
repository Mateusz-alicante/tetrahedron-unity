using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public static readonly List<string> MODE_NAMES = new List<string>()
    {
        "B3",
        "C3",
        "A3",
    };
            

    public static readonly List<Tetrahedron> MODE_TETRAS = new List<Tetrahedron>
    {
        new Tetrahedron(
            new Vector3(Mathf.Sqrt(2), 0, -1),
            new Vector3(0, Mathf.Sqrt(2), -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, 0)
        ),
        new Tetrahedron(
            new Vector3(Mathf.Sqrt(2), 0, -1),
            new Vector3(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, 0)
        ),
        new Tetrahedron(
            new Vector3(0, -1, 1),
            new Vector3(0,1,1),
            new Vector3(-1,0, 0),
            new Vector3(-1,0,2)
        ),
    };
}
