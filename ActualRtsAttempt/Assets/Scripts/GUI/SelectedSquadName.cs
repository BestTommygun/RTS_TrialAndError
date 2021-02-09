using SelectionSystem.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SelectedSquadName : MonoBehaviour
{
    private TextMeshProUGUI text;
    public SelectionHandler selectionHandler;
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var selectedGroups = selectionHandler.currentSelection.OfType<GroupSelector>();
        if (selectedGroups.Count() > 0)
            text.SetText(selectedGroups.FirstOrDefault().name);
    }
}
