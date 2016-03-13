using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK{
	
	public class UIUtilities : MonoBehaviour {

		public static bool IsCursorOnUI(){
			EventSystem eventSystem = EventSystem.current;
			return ( eventSystem.IsPointerOverGameObject(-1) );
		}
		
	}

}