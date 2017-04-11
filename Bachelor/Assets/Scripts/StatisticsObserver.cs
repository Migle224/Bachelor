using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsObserver : MonoBehaviour, Observer {

    SortedList<DestinationType, int> destiantions = new SortedList<DestinationType, int>();
    SortedList<bool, int> aiEnabled = new SortedList<bool, int>();
    SortedList<Role, int> aiRoles = new SortedList<Role, int>();


    public void UpdateDestination(DestinationType _destinaitonFrom, DestinationType _destinationTo)
    {
        if (_destinaitonFrom != DestinationType.None && destiantions.ContainsKey(_destinaitonFrom))
            destiantions[_destinaitonFrom] = destiantions[_destinaitonFrom]--;

        if (!destiantions.ContainsKey(_destinationTo))
            destiantions.Add(_destinationTo, 0);

        if (_destinationTo != DestinationType.None && destiantions.ContainsKey(_destinationTo))
            destiantions[_destinationTo] = destiantions[_destinationTo]++;
    }

    public void UpdateEnabledState(bool _enabled)
    {
        if (!aiEnabled.ContainsKey(_enabled))
            aiEnabled.Add(_enabled, 0);

        aiEnabled[_enabled] += _enabled ? 1 : -1;
    }

    public void UpdateCount(Role _role, bool _created)
    {
        if (!aiRoles.ContainsKey(_role))
            aiRoles.Add(_role, 0);

        aiRoles[_role] += _created ? 1 : -1;
    }


}
