using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TetrahedronMesh : MonoBehaviour
{
    public Vector3[] vertices;
    public bool Clockwise;
    public Color color;

    int[] triangles;
    Mesh mesh;
    
    Vector2 uv3a = new Vector2(0,0);
    Vector2 uv1  = new Vector2(0.5f,0);
    Vector2 uv0  = new Vector2(0.25f,Mathf.Sqrt(0.75f)/2);
    Vector2 uv2  = new Vector2(0.75f,Mathf.Sqrt(0.75f)/2);
    Vector2 uv3b = new Vector2(0.5f,Mathf.Sqrt(0.75f));
    Vector2 uv3c = new Vector2(1,0);
 
    

    public void GenerateVertices(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        vertices = new Vector3[]
        {
            A, B, C,
            A, B, D,
            A, C, D,
            B, C, D
        };
    }

    void MakeMeshData()
    {
        
        

        if (Clockwise)
        {
            triangles = new int[] {
            0, 2, 1,
            3, 4, 5,
            6, 8, 7,
            9, 10, 11
            };
            
            

        } else
        {
            triangles = new int[] {
            0,1, 2,
            3,5,4,
            6,7,8,
            9,11,10
            };
            
            
        }


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = new Vector2[]{
            uv0,uv1,uv2,
            uv0,uv2,uv3b,
            uv0,uv1,uv3a,
            uv1,uv2,uv3c
        };
        mesh.RecalculateNormals();
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -1 * normals[i];
        }

        mesh.normals = normals;
        mesh.RecalculateBounds();
        mesh.Optimize();

        GetComponent<Renderer>().material.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData();
    }

}
