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
            if (type == LogType.Error || type == LogType.Exception)
            {
                // Obtener el nombre del script y la l�nea del stack trace
                string[] stackTraceLines = stackTrace.Split('\n');
                string scriptInfo = stackTraceLines[1]; // La segunda l�nea contiene la informaci�n del script
                string[] scriptInfoParts = scriptInfo.Split(':');
                string scriptName = scriptInfoParts[0].Trim();
                //int lineNumber = int.Parse(scriptInfoParts[1]);

                // Construir el mensaje de error con el nombre del script y la l�nea
                //string errorMessage = string.Format("Error en {0}, l�nea {1}: {2}", scriptName, lineNumber, logString);

                // Agregar el mensaje de error al componente de texto
                //display.text += errorMessage + "\n";
            }
        }
    }
}