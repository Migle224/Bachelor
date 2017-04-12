using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsObserver : MonoBehaviour, Observer {

    SortedList<DestinationType, int> destiantions = new SortedList<DestinationType, int>();
    SortedList<bool, int> aiEnabled = new SortedList<bool, int>();
    SortedList<Role, int> aiRoles = new SortedList<Role, int>();

    List<ObserverUI> observerUI = new List<ObserverUI>();


    public void UpdateDestination(DestinationType _destinaitonFrom, DestinationType _destinationTo)
    {
        if (!destiantions.ContainsKey(_destinationTo))
            destiantions.Add(_destinationTo, 0);

        if (!destiantions.ContainsKey(_destinaitonFrom))
            destiantions.Add(_destinaitonFrom, 0);

        if (_destinaitonFrom != DestinationType.None && destiantions.ContainsKey(_destinaitonFrom))
            destiantions[_destinaitonFrom] = destiantions[_destinaitonFrom]--;

        if (_destinationTo != DestinationType.None && destiantions.ContainsKey(_destinationTo))
            destiantions[_destinationTo] = destiantions[_destinationTo]++;

        //Debug.Log("From " + _destinaitonFrom + " amounrt " + destiantions[_destinaitonFrom] + " to " + _destinationTo +" amount "+ destiantions[_destinationTo]);

        this.NotifyUI();
    }

    public void UpdateEnabledState(bool _enabled)
    {
        if (!aiEnabled.ContainsKey(_enabled))
        {
            aiEnabled.Add(_enabled, 0);
            aiEnabled.Add(!_enabled, 0);
        }

        if (_enabled)
        {
            aiEnabled[true] += 1;
            aiEnabled[false] -= aiEnabled[false] <= 0 ? 0 : 1;
        }
        else
        {
            aiEnabled[true] -= aiEnabled[true] <= 0 ? 0 : 1;
            aiEnabled[false] += 1;
        }

        this.NotifyUI();
    }

    public void UpdateCount(Role _role, bool _created)
    {
        if (!aiRoles.ContainsKey(_role))
            aiRoles.Add(_role, 0);

        aiRoles[_role] += _created ? 1 : -1;

        this.NotifyUI();
    }

    public SortedList<DestinationType, int> GetDestianionsStatistics()
    {
        return destiantions;
    }

    public SortedList<bool, int> GetEnabledStatistics()
    {
        return aiEnabled;
    }

    public SortedList<Role, int> GetRolesStatistics()
    {
        return aiRoles;
    }

    public void AddUI(ObserverUI _ui)
    {
        observerUI.Add(_ui);
    }

    public void NotifyUI()
    {
        foreach (ObserverUI ui in observerUI)
            ui.UpdateObserverUI();
    }
}
