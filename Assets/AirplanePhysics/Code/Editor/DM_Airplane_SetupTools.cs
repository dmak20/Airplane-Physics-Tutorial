using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DM
{
	public static class DM_Airplane_SetupTools
	{
		public static void BuildDefaultAirplane(string aName)
		{
            GameObject rootGO = new GameObject(aName, typeof(DM_Airplane_Controller), typeof(DM_BaseAirplane_Input));
            GameObject cogGO = new GameObject("COG");
            cogGO.transform.SetParent(rootGO.transform, false);

			// create the base components or find them
			DM_BaseAirplane_Input input = rootGO.GetComponent<DM_BaseAirplane_Input>();
			DM_Airplane_Controller controller = rootGO.GetComponent<DM_Airplane_Controller>();
			DM_Airplane_Characteristics characteristics = rootGO.GetComponent<DM_Airplane_Characteristics>();

			if (controller)
			{
				controller.input = input;
				controller.characteristics = characteristics;
				controller.centerOfGravity = cogGO.transform;

				GameObject graphicsGrp = new GameObject("Graphics_GRP");
				GameObject collisionGrp = new GameObject("Collision_GRP");
				GameObject controllerGrp = new GameObject("ControlSurfaces_GRP");
				graphicsGrp.transform.SetParent(rootGO.transform, false);
				collisionGrp.transform.SetParent(rootGO.transform, false);
				controllerGrp.transform.SetParent(rootGO.transform, false);

				GameObject engineGO = new GameObject("Engine", typeof(DM_Airplane_Engine));
				DM_Airplane_Engine engine = engineGO.GetComponent<DM_Airplane_Engine>();
				controller.engines.Add(engine);
				engineGO.transform.SetParent(rootGO.transform, false);

				// Create the base airplane
				GameObject defaultAirplane = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AirplanePhysics/Art/Objects/Airplanes/Indie-Pixel_Airplane/IndiePixel_Airplane.fbx", typeof(GameObject));

				if (defaultAirplane)
				{
					GameObject.Instantiate(defaultAirplane, graphicsGrp.transform);
				}

				Selection.activeObject = rootGO;
			}
        }
	}
}