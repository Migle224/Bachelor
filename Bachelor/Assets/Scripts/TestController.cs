using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.Profiling;
using System.Diagnostics;
using System;


public class TestController : MonoBehaviour
{
    GameObject camera;
    SimulationManager simulationManager;
    Vector3 cameraClosePosition, cameraWidePosition;
    private List<string[]> dataToSave = new List<string[]>();
    bool savingData = false, testIsRunning = false, lastTest = false;
    GameManager gameManager;
    int nextTestTime = 6*60+5;
    int testID = 1;
    int savedFrame = 1;
    PerformanceCounter cpuCounter;
    System.TimeSpan PrevCPUPc, CurrCPUPc;
    int amountOfAi;
    string[] rowDataTempCpu = new string[6];
    

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        cameraClosePosition = new Vector3(-10f, 16.36f, 0.5f);
        cameraWidePosition = new Vector3(135f, 138f, -43f);
        testID = 1;
        Profiler.logFile = Application.persistentDataPath + "/profilerLog.txt";
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;
        
    }
    // Use this for initialization
    void Start()
    {
       

       

    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager == null)
            this.InitObjects();

        if (!testIsRunning)
            this.RunTest();

        if (gameManager.timeOfDay > nextTestTime && !savingData)
        {
            rowDataTempCpu[0] = nextTestTime.ToString();
            savingData = true;          
        }

        if (savingData)
        {
           // rowDataTempCpu[savedFrame] = cpuCounter.NextValue().ToString();
            rowDataTempCpu[savedFrame] = this.ReadCPUPC().ToString();
            savedFrame++;       
        }

        if(savedFrame == 6)
        {
            savingData = false;
            dataToSave.Add(rowDataTempCpu);
            rowDataTempCpu =  new string[6];
            savedFrame = 1;
            nextTestTime = this.GetNextTestTime();

            if (lastTest)
            {
                lastTest = false;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private int ReadCPUPC()
    {
        int ToReturn = 0;
        PrevCPUPc = CurrCPUPc;
        CurrCPUPc = new TimeSpan(0);
        Process[] AllProcesses = Process.GetProcesses();
        for (int Cnt = 0; Cnt < AllProcesses.Length; Cnt++)
            CurrCPUPc += AllProcesses[Cnt].TotalProcessorTime;

        TimeSpan newCPUTime = CurrCPUPc - PrevCPUPc;
        ToReturn = (int)((100 * newCPUTime.TotalSeconds / (1.0f / Time.deltaTime)) / Environment.ProcessorCount);
        return ToReturn;
    }

    int GetNextTestTime()
    {
        int hour6 = 60 * 6 + 5, hour12 = 60 * 12 + 30, hour18 = 18 * 60 + 10, hour20 = 20*60+30;  

        switch(nextTestTime)
        {
            case 0:
                return hour6;
            case 60 * 6 + 5:
                return hour12;
            case 60 * 12 + 30:
                return hour18;
            case 18 * 60 + 10:
                return hour20;
            case 20 * 60 + 30:
                lastTest = true;
                return hour6;

        } 
        return 0;
    }

    void InitObjects()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        simulationManager = GameObject.FindGameObjectWithTag(Const.tagSIMULATIONMANAGER).GetComponent<SimulationManager>();
        Time.timeScale = 1;
        gameManager = GameObject.FindGameObjectWithTag(Const.tagGAMEMANAGER).GetComponent<GameManager>();
        cpuCounter = new PerformanceCounter();

        cpuCounter.CategoryName = "Process";
        cpuCounter.CounterName = "% Processor Time";
        cpuCounter.InstanceName = "_Total";
        cpuCounter.NextValue();
    }

    public void RunTest()
    {
       
        Profiler.enabled = true;

        testIsRunning = true;
       


        this.InitObjects();
        switch (testID)
        {
            case 1:
                this.SaveHeader("TEST1", 12, cameraClosePosition, 100);
                this.RunTest(20, 20, 20, 20, cameraClosePosition, 100);
                break;
           /* case 2:
                this.SaveHeader("TEST2", 12, cameraClosePosition, 100);
                this.RunTest(20, 20, 20, 20, cameraClosePosition, 100);
                break;*/
            //12
           /* case 1:
                this.SaveHeader("TEST1", 12, cameraClosePosition, 4);
                this.RunTest(3, 3, 3, 3, cameraClosePosition, 4);
                break;
            case 2:
                this.SaveHeader("TEST2", 12, cameraClosePosition, 16);
                this.RunTest(3, 3, 3, 3, cameraClosePosition, 16);
                break;
            case 3:
                this.SaveHeader("TEST3", 12, cameraWidePosition, 4);
                this.RunTest(3, 3, 3, 3, cameraWidePosition, 4);
                break;
            case 4:
                this.SaveHeader("TEST4", 12, cameraWidePosition, 16);
                this.RunTest(3, 3, 3, 3, cameraWidePosition, 16);
                break;
                //48
            case 5:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 4);
                this.RunTest(12, 12, 12, 12, cameraClosePosition, 4);
                break;
            case 6:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(12, 12, 12, 12, cameraClosePosition, 16);
                break;
            case 7:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(12, 12, 12, 12, cameraWidePosition, 4);
                break;
            case 8:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(12, 12, 12, 12, cameraWidePosition, 16);
                break;
                //100
            case 9:
                amountOfAi = 100;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi/4, amountOfAi/4, amountOfAi/4, amountOfAi/4, cameraClosePosition, 4);
                break;
            case 10:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 11:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 12:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //200
            case 13:
                amountOfAi = 200;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 14:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 15:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 16:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //200
            case 17:
                amountOfAi = 200;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 18:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 19:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 20:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //500
            case 21:
                amountOfAi = 500;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 22:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 23:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 24:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //1000
            case 25:
                amountOfAi = 1000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 26:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 27:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 28:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //2000
            case 29:
                amountOfAi = 2000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 30:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 31:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 32:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //5000
            case 33:
                amountOfAi = 5000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 34:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 35:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 36:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //8000
            case 37:
                amountOfAi = 8000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 38:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 39:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 40:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;
            //10000
            case 41:
                amountOfAi = 10000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 4);
                break;
            case 42:
                this.SaveHeader("TEST" + testID, 48, cameraClosePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, 16);
                break;
            case 43:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 4);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 4);
                break;
            case 44:
                this.SaveHeader("TEST" + testID, 48, cameraWidePosition, 16);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraWidePosition, 16);
                break;*/
            default:
                this.WriteToCSV();
                UnityEditor.EditorApplication.isPlaying = false;
                break;
        }
        testID++;

    }
    void RunTest(
        int _amountOfEmployes,
        int _amountOfFreeEmployes,
        int _amountOfTourist,
        int _amountOfHomelles,
        Vector3 _cameraPostion,
        int _timeScale)
    {
        
        camera.gameObject.transform.position = _cameraPostion;
        Time.timeScale = Time.timeScale * _timeScale;
        simulationManager.InstantiateAI(Role.Employee, _amountOfEmployes);
        simulationManager.InstantiateAI(Role.FreeEmployee, _amountOfFreeEmployes);
        simulationManager.InstantiateAI(Role.Homeless, _amountOfHomelles);
        simulationManager.InstantiateAI(Role.Turist, _amountOfTourist);
    }

    void SaveStatistics()
    {

    }

    void SaveHeader(string _testID, int _aiNumber, Vector3 _cameraPosition, float _timeScale)
    {
        string[] rowDataTemp = new string[4];
        rowDataTemp[0] = _testID;
        rowDataTemp[1] = _aiNumber.ToString();
        rowDataTemp[2] = _cameraPosition.ToString();
        rowDataTemp[3] = _timeScale.ToString();
        dataToSave.Add(rowDataTemp);
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


        string filePath = Application.dataPath + "/CSV/" + "Saved_data.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
        
}
