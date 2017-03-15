using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIPersonRole : MonoBehaviour {
    //TODO: have list of roleInfo, automaticly iterate throut enum and add role info, outomaticly assigne getroles ect. 
    public RoleInfo employee, turist, child, homeless, none, freeEmployee;

    void Start()
    {
        employee.role = Role.Employee;
        turist.role = Role.Turist;
        child.role = Role.Child;
        homeless.role = Role.Homeless;
        none.role = Role.None;
        freeEmployee.role = Role.FreeEmplyee;


    }

    public RoleInfo GetRoleInfo(Role _role)
    {
        switch (_role)
        {
            case Role.Employee:
                return employee;
               
            case Role.Homeless:
                return homeless;
               
            case Role.Turist:
                return turist;
                
            case Role.Child:
                return child;

            case Role.FreeEmplyee:
                return freeEmployee;

            default:
                return none;    
        }
    }


    public void AddSheldule(Role _role, ShelduleInfo _shelduleInfo)
    {
        switch (_role)
        {
            case Role.Employee:
                employee.sheldule.Add(_shelduleInfo);
                break;
            case Role.Homeless:
                homeless.sheldule.Add(_shelduleInfo);
                break;
            case Role.Turist:
                turist.sheldule.Add(_shelduleInfo);
                break;
            case Role.Child:
                child.sheldule.Add(_shelduleInfo);
                break;
            case Role.FreeEmplyee:
                freeEmployee.sheldule.Add(_shelduleInfo);
                break;
            default:
                none.sheldule.Add(_shelduleInfo);
                break;
        }
    }

    public void AddSheldule(Role _role, int _time, GameObject _destination)
    {
        ShelduleInfo shelduleInfo;

        shelduleInfo.time = _time;
        shelduleInfo.destination = _destination;

        switch (_role)
        {
            case Role.Employee:
                employee.sheldule.Add(shelduleInfo);
                break;
            case Role.Homeless:
                homeless.sheldule.Add(shelduleInfo);
                break;
            case Role.Turist:
                turist.sheldule.Add(shelduleInfo);
                break;
            case Role.Child:
                child.sheldule.Add(shelduleInfo);
                break;
            case Role.FreeEmplyee:
                freeEmployee.sheldule.Add(shelduleInfo);
                break;
            default:
                none.sheldule.Add(shelduleInfo);
                break;
        }
    }

}
