using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public GameObject StatusTemplate;
    public Transform StatusPanel;
    public List<GameObject> ActiveStatuses;

    private void Start()
    {
        ActiveStatuses = new List<GameObject> { };
    }

    public void AddStatus(GameObject status)
    {
        StatusReapplyType reapplyType = status.GetComponent<StatusBase>().GetReapplyType();
        switch (reapplyType)
        {
            case StatusReapplyType.RankUp:
                RankUpStatus(status);
                break;
            case StatusReapplyType.Immune:
                ImmuneStatus(status);
                break;
            case StatusReapplyType.RefreshLength:
                RefreshStatus(status);
                break;
            case StatusReapplyType.Multiple:
                InstantiateStatus(status);
                break;
        }

    }

    public void RemoveStatus(GameObject statusObject)
    {
        for (int i = 0; i < ActiveStatuses.Count; ++i)
        {
            if (ActiveStatuses[i].GetInstanceID() == statusObject.GetInstanceID())
            {
                ActiveStatuses.RemoveAt(i);
                Destroy(statusObject);
                break;
            }
        }
    }

    public List<GameObject> GetStatusesByCat(StatusCat Category)
    {
        List<GameObject> selectedStatuses = new List<GameObject> { };
        foreach (GameObject Status in ActiveStatuses)
        {
            StatusBase s = Status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>();
            if (s.Category == Category)
            {
                selectedStatuses.Add(Status);
            }
        }
        return selectedStatuses;
    }

    private GameObject GetStatusByName(string StatusName)
    {
        GameObject foundStatus = null;
        foreach (GameObject status in ActiveStatuses)
        {
            StatusBase s = status.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>();
            if (s.Name == StatusName)
            {
                foundStatus = status;
                break;
            }
        }
        return foundStatus;
    }

    private void InstantiateStatus(GameObject status)
    {
        GameObject s = Instantiate(StatusTemplate, StatusPanel);
        s.GetComponent<StatusSpriteTemplate>().setupStatus(status);
        ActiveStatuses.Add(s);
    }

    private void RankUpStatus(GameObject status)
    {
        string statusName = status.GetComponent<StatusBase>().Name;
        GameObject foundStatus = GetStatusByName(statusName);
        if (foundStatus != null)
        {
            int currentRank = foundStatus.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().rank;
            int newRank = currentRank + status.GetComponent<StatusBase>().rank;
            foundStatus.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().rank = newRank;
            foundStatus.GetComponent<StatusSpriteTemplate>().setRank(newRank.ToString());
        }
        else
        {
            InstantiateStatus(status);
        }
    }

    private void ImmuneStatus(GameObject status)
    {
        string statusName = status.GetComponent<StatusBase>().Name;
        GameObject foundStatus = GetStatusByName(statusName);
        if(foundStatus == null) {
            InstantiateStatus(status);
        }
    }
    
    private void RefreshStatus(GameObject status)
    {
        string statusName = status.GetComponent<StatusBase>().Name;
        GameObject foundStatus = GetStatusByName(statusName);
        if (foundStatus != null)
        {
            int currentLength = foundStatus.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().length;
            int newLength = currentLength + status.GetComponent<StatusBase>().length;
            foundStatus.GetComponent<StatusSpriteTemplate>().status.GetComponent<StatusBase>().length = newLength;
        }
        else
        {
            InstantiateStatus(status);
        }
    }

}
