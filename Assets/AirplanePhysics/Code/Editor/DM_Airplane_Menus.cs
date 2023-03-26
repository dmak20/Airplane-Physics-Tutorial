using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DM
{
    public static class DM_Airplane_Menus
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
            DM_Airplane_SetupTools.BuildDefaultAirplane("New Airplane");
        }
    }
}