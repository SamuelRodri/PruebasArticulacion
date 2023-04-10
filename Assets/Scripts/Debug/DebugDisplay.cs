using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFG.DebugManagement
{
    public class DebugDisplay : MonoBehaviour
    {
        Dictionary<string, string> debugLogs = new();

        public Text display;

        public GameObject vertebra;
        public static string message;

        private void Update()
        {
            debugLogs.Clear();
            //StartCoroutine(PrintMessage());
        }

        public static void SetMessage(string m)
        {
            message = m;
        }

        IEnumerator PrintMessage()
        {

            yield return new WaitForSeconds(1f);
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Log)
            {
                string[] splitString = logString.Split(char.Parse(":"));
                string debugKey = splitString[0];
                string debugValue = splitString.Length > 1 ? splitString[1] : "";

                if (debugLogs.ContainsKey(debugKey))
                    debugLogs[debugKey] = debugValue;
                else
                    debugLogs.Add(debugKey, debugValue);
            }

            string displayText = "";
            foreach (KeyValuePair<string, string> log in debugLogs)
            {
                if (log.Value == "")
                    displayText += log.Key + "\n";
                else
                    displayText += log.Key + ": " + log.Value + "\n";
            }

            display.text = displayText;
        }
    }
}