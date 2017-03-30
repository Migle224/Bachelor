using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    AIPersonRole aiPersonRoles;

    public Role role;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateAIRoleSheldule(Role _role, int _time, GameObject _destination)
    {
        aiPersonRoles.AddSheldule(_role, _time, _destination);
    }

    public RoleInfo GetRoleInfo(Role _role)
    {
        return aiPersonRoles.GetRoleInfo(_role);
    }

    public void InitSheldule(List<GameObject> _busStops, List<GameObject> _workPlaces, List<GameObject> _turistPlaces)
    {

        switch (this.role)
        {
            case Role.Child:
                break;
            case Role.Employee:
                break;
            case Role.FreeEmployee:
                break;
            case Role.Homeless:
                break;
            case Role.Turist:
                break;

        }

    }

    void InitEmployeeSheldule(List<GameObject> _busStops, List<GameObject> _workPlaces, List<GameObject> _turistPlaces)
    {
        aiPersonRoles.AddSheldule(this.role, 7 * 60, _workPlaces[Random.Range(0, _workPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 17 * 60, _workPlaces[Random.Range(0, _busStops.Count)]);      
    }

    void InitFreeEmployeeSheldule(List<GameObject> _busStops, List<GameObject> _workPlaces, List<GameObject> _turistPlaces)
    {
        int workDayStart = Random.Range(7, 15);

        aiPersonRoles.AddSheldule(this.role, workDayStart * 60, _workPlaces[Random.Range(0, _workPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, (workDayStart+9) * 60, _workPlaces[Random.Range(0, _busStops.Count)]);
    }

    void InitTuristSheldule(List<GameObject> _busStops, List<GameObject> _workPlaces, List<GameObject> _turistPlaces)
    {
        int dayStart = Random.Range(7, 11);

        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
    }

    void InitHomelessSheldule(List<GameObject> _busStops, List<GameObject> _workPlaces, List<GameObject> _turistPlaces)
    {
        int dayStart = Random.Range(7, 11);

        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _busStops.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _turistPlaces.Count)]);
        aiPersonRoles.AddSheldule(this.role, 0, _turistPlaces[Random.Range(0, _busStops.Count)]);
    }

}
