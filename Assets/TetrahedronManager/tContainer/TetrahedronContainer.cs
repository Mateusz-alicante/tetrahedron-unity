using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrahedronContainer : MonoBehaviour
{
    public Manager manager;
    public Tetrahedron t;
    public GameObject ActiveVertex;
    public Color LastColor;
    public GameObject TetrahedromMesh;
    public Color tColor;
    Vector3[] vertices;
    private char[] vertices_ls = new char[] { 'A', 'B', 'C', 'D' };
    private Color[] vertex_color = new Color[] { Color.red , Color.blue, Color.green, Color.yellow };
    

    // Start is called before the first frame update
    void Start()
    {
        if (t.Last)
        {
            tColor = LastColor;
        } else
        {
            tColor = Random.ColorHSV();
            tColor.a = 0.60f;
        }
        GameObject current_vertex;

        vertices = t.get_plot_vertices();
        for (int i = 0; i < vertices.Length; i++)
        {
            if (t.Last)
            {
                current_vertex = Instantiate(ActiveVertex);
                active_vertex current_vertex_obj = current_vertex.GetComponent<active_vertex> ();
                current_vertex_obj.vertex = vertices_ls[i];
                current_vertex_obj.manager = manager;
                current_vertex.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            } else {
                current_vertex = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                if (vertices_ls[i] == t.ReflectedOn)
                {
                    current_vertex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    current_vertex.GetComponent<Renderer>().material.color = vertex_color[i];
                } else
                {
                    current_vertex.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    current_vertex.GetComponent<Renderer>().material.color = tColor;
                }
            }

            current_vertex.transform.parent = transform;
            
            current_vertex.transform.position = vertices[i];

            for (int i2 = i + 1; i2 < vertices.Length; i2++)
            {
                Vector3 start = vertices[i];
                Vector3 end = vertices[i2];

                GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cylinder.transform.parent = this.transform;
                cylinder.transform.position = start + (end - start) / 2;
                cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, end-start); ;
                cylinder.transform.localScale = new Vector3((float)0.05, Vector3.Distance(start, end) / 2, (float)0.05);
                cylinder.GetComponent<Renderer>().material.color = tColor;
            }

        };

        GameObject meshObject = Instantiate(TetrahedromMesh, transform);
        TetrahedronMesh mesh = meshObject.GetComponent<TetrahedronMesh>();
        mesh.GenerateVertices(vertices[0], vertices[1], vertices[2], vertices[3]);
        mesh.color = tColor;
        mesh.Clockwise = t.Clockwise;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
