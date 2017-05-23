using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
//using UnityEngine.Profiling;
using System.Diagnostics;
using System;

public class WalkTestEndPoint : MonoBehaviour
{

    float testStartTime;
    TestController testController;
    GameManager gameManager;
    int instanceID;
    private List<string[]> dataToSave = new List<string[]>();
    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag(Const.tagTESTCONTOLLER);
        if (go)
            testController = go.GetComponent<TestController>();

        go = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER);
        if (go)
            gameManager = go.GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestStarted(int _instanceID)
    {
        testStartTime = gameManager.timeOfDay;
        instanceID = _instanceID;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetInstanceID() == instanceID)
        {

            string[] rowDataTemp = new string[2];
            rowDataTemp[0] =  testController.testID.ToString();
            rowDataTemp[1] = (gameManager.timeOfDay - testStartTime).ToString();
            dataToSave.Add(rowDataTemp);
            this.WriteToCSV();
              testController.RunNextTest();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void WriteToCSV()
    {
        string[][] output = new string[dataToSave.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = dataToSave[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = Application.dataPath + "/CSV/" + (testController.testID - 1) + "walking_test.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
        // dataToSave.Clear();
    }


}
