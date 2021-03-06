﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer {

    void UpdateDestination(DestinationType _destinaitonFrom, DestinationType _destinationTo);
    void UpdateEnabledState(bool _enabled);
    void UpdateCount(Role _role, bool _created);

    SortedList<DestinationType, int> GetDestianionsStatistics();
    SortedList<bool, int> GetEnabledStatistics();
    SortedList<Role, int> GetRolesStatistics();

    void AddUI(ObserverUI _ui);
    void NotifyUI();
}
