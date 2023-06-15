using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UImanager : MonoBehaviour
{
    private Button[] buttons = new Button[4];
    public Manager manager;
    private readonly char[] vertices = new char[] { 'A', 'B', 'C', 'D' };
    Manager managerObj;

    // Start is called before the first frame update
    void Start()
    {
        managerObj = manager.GetComponent<Manager>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        for (int i = 0; i < vertices.Length; i++)
        {
            int curr_i = i;
            buttons[i] = root.Q<Button>(vertices[curr_i].ToString());

            buttons[i].clicked += delegate { manager.AddToSeq(vertices[curr_i]); }; 
        }
    }

}
