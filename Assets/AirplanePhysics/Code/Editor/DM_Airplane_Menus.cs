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
            GameObject currSelected = Selection.activeGameObject;

            if (currSelected)
            {
                // assign our airplane scripts
                DM_Airplane_Controller curController = currSelected.AddComponent<DM_Airplane_Controller>();
                GameObject curCOG = new GameObject("COG");
                curCOG.transform.SetParent(currSelected.transform);
                curController.centerOfGravity = curCOG.transform;
            }
        }
    }
}