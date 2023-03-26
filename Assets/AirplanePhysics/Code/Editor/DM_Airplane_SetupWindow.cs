using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DM
{

    public class DM_Airplane_SetupWindow : EditorWindow
    {
        #region Variables
        private string wantedName;
        #endregion

        #region Custom Methods
        public static void LaunchSetupWindow()
        {
            GetWindow(typeof(DM_Airplane_SetupWindow), true, "Airplane Setup").Show();
        }

        private void OnGUI()
        {
            wantedName = EditorGUILayout.TextField("Airplane Name:", wantedName);
            if (GUILayout.Button("Create new Airplane"))
            {
                DM_Airplane_SetupTools.BuildDefaultAirplane(wantedName);
                GetWindow<DM_Airplane_SetupWindow>().Close();
            }
        }
        #endregion
    }

}