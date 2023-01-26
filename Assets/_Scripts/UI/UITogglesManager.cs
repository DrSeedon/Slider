using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITogglesManager : MonoBehaviour
{
    public List<Toggle> Toggles;

    public void ToggleAll(bool value)
    {
        foreach (var toggle in Toggles)
        {
            toggle.isOn = value;
        }
    }
}
