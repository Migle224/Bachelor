using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
//using UnityEngine.Profiling;
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
    public int testID = 1;
    int savedFrame = 1;
    PerformanceCounter cpuCounter;
    System.TimeSpan PrevCPUPc, CurrCPUPc;
    int amountOfAi;
    string[] rowDataTempCpu = new string[6];
    int simulationSpeed;
    

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        cameraClosePosition = new Vector3(-10f, 16.36f, 0.5f);
        cameraWidePosition = new Vector3(135f, 138f, -43f);
       // cameraClosePosition = cameraWidePosition;
        testID = 1;
      //  Profiler.logFile = Application.persistentDataPath + "/profilerLog.txt";
      //  Profiler.enableBinaryLog = true;
     //   Profiler.enabled = true;
        simulationSpeed = 4;


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
            // rowDataTempCpu[savedFrame] = this.ReadCPUPC().ToString();
            rowDataTempCpu[savedFrame] = Time.deltaTime.ToString();
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
                this.WriteToCSV();
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

  /*  private int ReadCPUPC()
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
    }*/

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
    /*    cpuCounter = new PerformanceCounter();

        cpuCounter.CategoryName = "Process";
        cpuCounter.CounterName = "% Processor Time";
        cpuCounter.InstanceName = "_Total";
        cpuCounter.NextValue();*/
    }

    public void RunTest()
    {
       
       // Profiler.enabled = true;

        testIsRunning = true;
       


        this.InitObjects();
        switch (testID)
        {
            /*  case 1:
                  this.SaveHeader("TEST1", 12, cameraClosePosition, 100);
                  this.RunTest(20, 10, 20, 20, cameraClosePosition, 100);

                  break;
              case 2:
                  this.SaveHeader("TEST2", 12, cameraClosePosition, 100);
                  this.RunTest(20, 20, 20, 20, cameraClosePosition, 100);
                  break;*/
            case 1:
                amountOfAi = 12;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 2:
                amountOfAi = 48;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 3:
                amountOfAi = 100;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 4:
                amountOfAi = 200;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 5:
                amountOfAi = 500;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 6:
                amountOfAi = 1000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 7:
                amountOfAi = 2000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 8:
                amountOfAi = 5000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 9:
                amountOfAi = 8000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;
            case 10:
                amountOfAi = 10000;
                this.SaveHeader("TEST" + testID, amountOfAi, cameraClosePosition, simulationSpeed);
                this.RunTest(amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, amountOfAi / 4, cameraClosePosition, simulationSpeed);
                break;           
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


        string filePath = Application.dataPath + "/CSV/" + (testID - 1) + "Saved_data.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
       // dataToSave.Clear();
    }
        
}
