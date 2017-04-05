using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AIInfoWindowController : MonoBehaviour {

    public Text destinationText, timeText, destinationsListText;
    public Material aiMaterial, selectedAIMaterial;
    GameObject selectedAI;


    void Update()
    {

       /* if (Input.GetMouseButton(0))
        { // << use GetMouseButton instead of GetMouseButtonDown
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.gameObject.tag == "AI")
                {
                    this.HideSelectedAI();
                    selectedAI = hit.transform.gameObject;
                    this.ShowSelectedAI();
                    this.ShowAIInfo(selectedAI.GetComponent<Movement>());
                }


            }
        }*/
    }

    void HideSelectedAI()
    {
        if (selectedAI != null)
        {
            selectedAI.GetComponent<Renderer>().material = aiMaterial;
        }
    }
    void ShowSelectedAI()
    {
        selectedAI.GetComponent<Renderer>().material = selectedAIMaterial;
    }

    /*void ShowAIInfo(Movement _aiInfo)
    {
        this.destinationText.text = "Destination: " + _aiInfo.currentDestination.gameObject.GetComponent<DestinationInfo>().destinationName;
        this.timeText.text = "Time for this destination: " + (Time.timeSinceLevelLoad - _aiInfo.timeSinceDestination);
        this.destinationsListText.text = "Destinations list: " + _aiInfo.destinationsList.ToString();
    }*/
}
