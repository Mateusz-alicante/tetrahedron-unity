using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active_vertex : MonoBehaviour
{
    public char vertex;
    public Manager manager;

    Manager managerObj;

    // Start is called before the first frame update
    void Start()
    {
        managerObj = manager.GetComponent<Manager>();
        var material = this.GetComponent<Renderer>().material;
        switch (vertex)
        {
            
            case 'A':
                material.SetColor("_Color", Color.red);
                break;
            case 'B':
                material.SetColor("_Color", Color.blue);
                break;
            case 'C':
                material.SetColor("_Color", Color.green);
                break;
            case 'D':
                material.SetColor("_Color", Color.yellow);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        managerObj.AddToSeq(vertex);
    }
}
