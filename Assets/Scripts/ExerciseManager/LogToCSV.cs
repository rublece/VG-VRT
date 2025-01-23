using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.IO;

public class LogToCSV : MonoBehaviour
{
    private StreamWriter writer;
    private string filename = null;
    private string rootPath;
    private string headerStr;

    // Start is called before the first frame update
    void Start()
    {
        rootPath = Application.persistentDataPath + "\\";
        headerStr = "SystemTime,RelativeTime,Event,ObjX,ObjY,RayX,RayY,UserX,UserY,UserZ,UserPitch,UserYaw,UserRoll";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LogGaze(float systemTime, float relativeTime, float objX, float objY, float rayX, float rayY, 
        Vector3 userPos, Vector3 userRotAngles)
    {
        if (filename == null)
        {
            Debug.Log("Trying to log but file doesn't exist yet!");
            return;
        }

        using (writer = new StreamWriter(rootPath + filename, true))
        {
            writer.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                systemTime, relativeTime, "Gaze", objX, objY, rayX, rayY,
                userPos.x, userPos.y, userPos.z, userRotAngles.x, userRotAngles.y, userRotAngles.z));
            // Debug.Log("logging Gaze event at time: " + systemTime.ToString());
        }
    }

    public void LogStartOrEnd(float systemTime, float relativeTime, string eventStr)
    {
        if (filename == null)
        {
            Debug.Log("Trying to log but file doesn't exist yet!");
            return;
        }

        using (writer = new StreamWriter(rootPath + filename, true))
        {
            writer.WriteLine(string.Format("{0},{1},{2}", systemTime, relativeTime, eventStr));
            Debug.Log("logging event: " + eventStr + " at time: " + systemTime.ToString());
        }
    }

    public void StartNewLog(string vstringExercise)
    {
        DateTime dateTime = DateTime.Now;
        string date = dateTime.ToString("yyyy-MM-dd");
        string time = dateTime.ToString("HHmmss");
        filename = string.Format("{0}_{1}_{2}.csv", vstringExercise, date, time);

        writer = new StreamWriter(rootPath + filename);
        writer.WriteLine(headerStr); // Add CSV Headers
        writer.Flush();
        writer.Close();

        Debug.Log("Started new log: " + rootPath + filename);
    }

    public void EndLog()
    {
        if (filename == null)
        {
            Debug.Log("Trying to end log but file doesn't exist yet!");
            return;
        }

        filename = null;
        Debug.Log("Ended log: " + rootPath + filename);
    }
}
