using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BookshelfToggleFilter : MonoBehaviour
{
    public Genre genre;
    private Toggle _toggle;
    // Start is called before the first frame update
    void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(bool value)
    {
        if (value)
        {
            BookshelfFilter.Instance.FilterAdd(genre);
        }
        else
        {
            BookshelfFilter.Instance.FilterRemove(genre);
        }
    }
}
