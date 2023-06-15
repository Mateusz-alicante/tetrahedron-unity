using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public TMP_InputField input;

    List<GameObject> containers = new List<GameObject>();
    
    public GameObject TetrahedronContainer;
    List<Tetrahedron> prevSeq = new List<Tetrahedron> ();
    List<Tetrahedron> TSequence;

    public bool invalidSeq;
    public bool isFinished;

    public GameObject NotificationField;
    private NotificationManager notificationManager;

    public GameObject PathManagerObject;
    private PathManager pathManager;

    int i;
    int i2;
    private string valid; // string containing sequence
    
    public int tetraMode = 0; // initial mode to be used


    // Start is called before the first frame update
    void Start()
    {
        
        notificationManager = NotificationField.GetComponent<NotificationManager>();
        pathManager = PathManagerObject.GetComponent<PathManager>();

        SeqUpdated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToSeq(char newLetter)
    {
        input.text += newLetter;
    }

    private GameObject GetTetraContainer(Tetrahedron tetra)
    {
        GameObject tetrahedron_container_object = Instantiate(TetrahedronContainer, this.transform);
        TetrahedronContainer tetrahedron_container = tetrahedron_container_object.GetComponent<TetrahedronContainer>();
        tetrahedron_container.t = tetra;
        tetrahedron_container.manager = this;
        return tetrahedron_container_object;
    }



    public void SeqUpdated(string seq = "")
    {
        valid = "";
        foreach (char c in seq)
        {
            if (c == 'A' || c == 'B' || c == 'C' || c == 'D')
            {
                valid += c;
            }
        }

        if (seq != valid)
        {
            input.text = valid;
            notificationManager.PushNotification("Invalid input", Color.red);
        }
        
        buildSeq();
        
    }

    public void buildSeq()
    {
        Tetrahedron t1 = Settings.MODE_TETRAS[tetraMode];

        (TSequence, invalidSeq, isFinished) = t1.reflectSeq(valid);

        if (invalidSeq)
        {
            notificationManager.PushNotification("Invalid Sequence", Color.red);
        } else if (isFinished)
        {
            notificationManager.PushNotification("Sequence Completed", Color.green);
            pathManager.CreatePath(TSequence, true);
            foreach (GameObject container in containers)
            {
                Destroy(container);
            }
            prevSeq.Clear();
            containers.Clear();
            return;
        }

        for (i =  0; i < TSequence.Count; i++)
        {
            if (i > prevSeq.Count - 1)
            {
                GameObject tetrahedron_container_object = GetTetraContainer(TSequence[i]);
                containers.Add(tetrahedron_container_object);
            } else if (!TSequence[i].is_equal(prevSeq[i]))
            {
                Destroy(containers[i]);
                GameObject tetrahedron_container_object = GetTetraContainer(TSequence[i]);
                containers[i] = tetrahedron_container_object;
            }
        }


        for (i2 = i; i2 < containers.Count(); i2++)
        {
            Destroy(containers[i2]);
        }

        if (containers.Count > 0) containers.RemoveRange(i, i2 - i);

        if (input.text.Length > i - 1)
            // If there are more characters in the input than tetrahedrons
        {
            input.text = input.text.Substring(0, i - 1);

        }

        pathManager.CreatePath(TSequence, false);

        prevSeq = TSequence;
    }
}
