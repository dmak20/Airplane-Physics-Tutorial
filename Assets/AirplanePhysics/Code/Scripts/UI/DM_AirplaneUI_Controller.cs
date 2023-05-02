using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DM
{
    public class DM_AirplaneUI_Controller : MonoBehaviour
    {
        #region Variables
        public List<IAirplaneUI> instruments = new List<IAirplaneUI>();
        #endregion

        #region BuiltIn Methods
        // Start is called before the first frame update
        void Start()
        {
            instruments = transform.GetComponentsInChildren<IAirplaneUI>().ToList();
        }

        // Update is called once per frame
        void Update()
        {
            if (instruments.Count > 0)
            {
                foreach (IAirplaneUI instrument in instruments)
                {
                    instrument.HandleAirplaneUI();
                }
            }
        } 
        #endregion
    }

}