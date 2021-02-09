using SelectionSystem.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExecuteFormationButton : MonoBehaviour
{
    public List<Group> selectedGroups;
    public List<GroupSelector> groupSelectors;
    public SelectionHandler selectionHandler;
    void Start()
    {
        selectedGroups = new List<Group>();
    }
    void Update()
    {
        groupSelectors = selectionHandler.currentSelection.OfType<GroupSelector>().ToList();
        if(groupSelectors.Count > 0)
        {
            selectedGroups = new List<Group>();
            for (int i = 0; i < groupSelectors.Count; i++) //TODO: very sloppy
            {
               selectedGroups.Add(groupSelectors[i].gameObject.GetComponent<Group>());
            }


        }
    }

    public void SetFormation(string formation)
    {
        for (int i = 0; i < selectedGroups.Count; i++)
        {
            selectedGroups[i].CurrentFormation = formation;
        }
    }
}
