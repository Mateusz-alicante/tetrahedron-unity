using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    GameObject center;
    public GameObject cameraObject;
    public CameraManager camera;
    List<GameObject> centers = new List<GameObject>();
    List<GameObject> paths = new List<GameObject>();
    private List<Tetrahedron> previousTls = new List<Tetrahedron>();

    Vector3 tc, sum;
    int i, i2, n;
    private bool prevSame;

    private Tetrahedron current, previous;
    // Start is called before the first frame update
    void Awake()
    {
        camera = cameraObject.GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject CreateCenter(Tetrahedron t)
    {
        tc = t.center();
        center = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        center.transform.parent = this.transform;
        center.transform.position = tc;
        center.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        center.GetComponent<Renderer>().material.color = Color.black;
        return center;
    }

    private GameObject CreatePathSegment(Vector3 start, Vector3 end)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.parent = this.transform;
        cylinder.transform.position = start + (end - start) / 2;
        cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, end - start); ;
        cylinder.transform.localScale = new Vector3((float)0.05, Vector3.Distance(start, end) / 2, (float)0.05);
        cylinder.GetComponent<Renderer>().material.color = Color.black;
        return cylinder;
    }

    public void CreatePath(List<Tetrahedron> tls, bool complete)
    {
        camera.last = tls.Last().center();
        camera.center = Tetrahedron.average(tls);
        
        prevSame = true;
       // centers
       if (previousTls.Count == 0)
       {
           centers.Add(CreateCenter(tls[0]));
           prevSame = false;
       } else if (!tls[0].is_equal(previousTls[0]))
       {
           Destroy(centers[0]);
           centers[0] = CreateCenter(tls[0]);
           prevSame = false;
       }
       
       for (i = 1; i < tls.Count; i++)
       {
           prevSame = true;
           if (i >= previousTls.Count())
           {
               centers.Add(CreateCenter(tls[i]));
               paths.Add(CreatePathSegment(tls[i - 1].center(), tls[i].center()));
           } else if (!tls[i].is_equal(previousTls[i]))
           {
               Destroy(centers[i]);
               centers[i] = CreateCenter(tls[i]);
               Destroy(paths[i - 1]);
               paths[i - 1] = CreatePathSegment(tls[i - 1].center(), tls[i].center());
               prevSame = false;
           } else if (!prevSame)
           {
               Destroy(paths[i - 1]);
               paths[i - 1] = CreatePathSegment(tls[i - 1].center(), tls[i].center());
           }

          
       }
       
       for (i2 = i; i2 < centers.Count; i2++)
       {
           Destroy(centers[i2]);
           Destroy(paths[i2-1]);
       }
       
       if (centers.Count > 0)
       {
           centers.RemoveRange(i, i2 - i);
           paths.RemoveRange(i - 1, i2 - i);
       }

       
       
       
       //  
       //  foreach (GameObject center in centers)
       //  {
       //      Destroy(center);
       //  }
       //  foreach (GameObject path in paths)
       //  {
       //      Destroy(path);
       //  }
       //  centers.Clear();
       //  paths.Clear();
       //  foreach (Tetrahedron t in tls)
       //  {
       //      
       //      centers.Add(CreateCenter(t));
       //  }
       //
       //  for (i = 1; i < tls.Count; i++)
       //  {
       //      Vector3 start = tls[i - 1].center();
       //      Vector3 end = tls[i].center();
       //      paths.Add(CreatePathSegment(start, end));
       //  }

        previousTls = tls;
    }
}
