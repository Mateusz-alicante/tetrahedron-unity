using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMode : MonoBehaviour
{
    
    public TMP_Dropdown ModeDropdown;
    public Manager manager;
    
    // Start is called before the first frame update
    private void initDropdown()
    {
        ModeDropdown = GetComponent<TMP_Dropdown>();
        ModeDropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(ModeDropdown);
        });
        ModeDropdown.ClearOptions();
        ModeDropdown.AddOptions(Settings.MODE_NAMES);
        ModeDropdown.value = manager.tetraMode;
    }
    // Start is called before the first frame update
    void Start()
    {
        initDropdown();
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        manager.tetraMode = dropdown.value;
        manager.SeqUpdated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
