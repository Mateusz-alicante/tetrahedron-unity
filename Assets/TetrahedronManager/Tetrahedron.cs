using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tetrahedron
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;
    public bool Last;
    public bool Clockwise; // should be rendered clockwise or counterclockwise
    public bool Display;
    public char ReflectedOn;

    public Tetrahedron(Vector3 a, Vector3 b, Vector3 c, Vector3 d, bool clockwise = true, bool last = false, bool display = true)
    {
        A = a;
        B = b;
        C = c;
        D = d;
        Clockwise = clockwise;
        Last = last;
        Display = display;
    }

    public Vector3 center()
    {
        return (A + B + C + D) / 4;
    }

    private Vector3 get_new_vertex(Vector3 a, Vector3 b, Vector3 c, Vector3 reflect)
    {
        Vector3 normal = Vector3.Cross(b - a, c - a);
        Plane plane = new Plane(normal, a);
        Vector3 onPlane = plane.ClosestPointOnPlane(reflect);
        return reflect + 2 * (onPlane - reflect);
    }


    public Tetrahedron reflect(char transformation)
    {
        switch (transformation)
        {
            case 'A':
                return new Tetrahedron(get_new_vertex(B, C, D, A), B, C, D, !Clockwise);
            case 'B':
                return new Tetrahedron(A, get_new_vertex(A, C, D, B), C, D, !Clockwise);
            case 'C':
                return new Tetrahedron(A, B, get_new_vertex(A, B, D, C), D, !Clockwise);
            case 'D':
                return new Tetrahedron(A, B, C, get_new_vertex(A, B, C, D), !Clockwise);
            default:
                return new Tetrahedron(A, B, C, D);
        }

    }

    public (List<Tetrahedron>, bool, bool) reflectSeq(string seq)
    // Return list of reflected tetrahedrons, error if invalid palcement, and sequence finished
    {
        int seq_len = seq.Length;
        int i = 0;
        int i2 = 0;
        bool seqFinished = false;
        bool invalidPlacement = false;
        List<Tetrahedron> result = new List<Tetrahedron>();
        result.Add(this.clone(last: false));
        for (i = 0; i < seq_len; i++)
        {
            Tetrahedron newTetra = result[i].reflect(seq[i]);
            for (i2 = 0; i2 < i; i2++)
            {
                if (newTetra.is_same_location(result[i2]))
                {
                    if (i2 == 0) {
                        seqFinished = true;
                        Tetrahedron clone = result[0].clone();
                        clone.Display = false;
                        result.Add(clone);
                    }
                    else invalidPlacement = true;
                    goto Return;
                }
                
            }
            result.Add(newTetra);
            result[i].ReflectedOn = seq[i];
        }


    Return:
        if (!seqFinished) result[i].Last = true;
        return (result, invalidPlacement, seqFinished);
    }

    public Vector3[] get_plot_vertices()
    {
        Vector3 O = this.center();
        return new Vector3[4] { A + (float)0.2 * (O - A), B + (float)0.2 * (O - B), C + (float)0.2 * (O - C), D + (float)0.2 * (O - D) };
    }

    public bool is_equal(Tetrahedron other)
    {
        // Vector3[] otherVertices = new Vector3[] { other.A, other.B, other.C, other.D};
        return (
           Vector3.Distance(A, other.A) < 0.1 &&
            Vector3.Distance(B, other.B) < 0.1 &&
            Vector3.Distance(C, other.C) < 0.1 &&
            Vector3.Distance(D, other.D) < 0.1 &&
            Last == other.Last && ReflectedOn == other.ReflectedOn);
    }

    public bool is_same_location(Tetrahedron other)
    {
        return (
            Vector3.Distance(A, other.A) < 0.1 &&
            Vector3.Distance(B, other.B) < 0.1 &&
            Vector3.Distance(C, other.C) < 0.1 &&
            Vector3.Distance(D, other.D) < 0.1);
    }

    public Tetrahedron clone(bool last = false)
    {
        return new Tetrahedron(A, B, C, D, Clockwise, last: last);
    }

    public static Tetrahedron B3()
    {
        return new Tetrahedron(
            new Vector3(Mathf.Sqrt(2), 0, -1),
            new Vector3(0, Mathf.Sqrt(2), -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, 0)
        ); 
    }

    public static Tetrahedron C3()
    {
        return new Tetrahedron(
            new Vector3(Mathf.Sqrt(2), 0, -1),
            new Vector3(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2, -1),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, 0)
        ); 
    }
    
    public static Tetrahedron A3()
    {
        return new Tetrahedron(
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(0,0,1),
            new Vector3(0,0,-1)
        ); 
    }

    public static Vector3 average(List<Tetrahedron> tls)
    {
        Vector3 sum = new Vector3();
        int n = 0;
        foreach (Tetrahedron t in tls)
        {
            n++;
            sum += t.center();
        }

        return sum / n;
    }
}
