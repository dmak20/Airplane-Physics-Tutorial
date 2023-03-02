using Codice.CM.WorkspaceServer.Tree.GameUI.HeadTree;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace DM
{
    [CustomEditor(typeof(DM_BaseAirplane_Input))]
    public class DM_BaseAirplaneInput_Editor : Editor
    {

        #region Variables
        private DM_BaseAirplane_Input targetInput;
        #endregion

        #region Builtin Methods
        private void OnEnable()
        {
            targetInput = (DM_BaseAirplane_Input)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            string debugInfo = "";
            debugInfo += $"Pitch = {targetInput.Pitch}\n";
            debugInfo += $"Roll = {targetInput.Roll}\n";
            debugInfo += $"Yaw = {targetInput.Yaw}\n";
            debugInfo += $"Throttle = {targetInput.Throttle} \n";
            debugInfo += $"Brake = {targetInput.Brake} \n";
            debugInfo += $"Flaps = {targetInput.Flaps} \n";

            // custom editor code
            GUILayout.Space(20);
            EditorGUILayout.TextArea(debugInfo, GUILayout.Height(100));
            GUILayout.Space(20);
            Repaint();
        }
        #endregion

    }
}