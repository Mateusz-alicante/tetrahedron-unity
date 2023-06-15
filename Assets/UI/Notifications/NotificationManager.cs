using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class NotificationManager : MonoBehaviour
{

    public TMP_Text field; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushNotification(string text, Color color)
    {
        field.text = text;
        field.color = color;
        Invoke("ClearField", 2f);
        
    }

    void ClearField()
    {
        field.text = "";
    }

}
