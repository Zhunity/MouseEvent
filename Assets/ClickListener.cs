using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class ClickListener : MonoBehaviour
{
	List<RaycastResult> list = new List<RaycastResult>();

	// Update is called once per frame
	void Update()
    {
		if(!Input.GetMouseButtonDown(0))
		{
			return;
		}
		

		//场景中的EventSystem

		PointerEventData eventData = new PointerEventData(EventSystem.current);

		//鼠标位置
		eventData.position = Input.mousePosition;


		EventSystem.current.RaycastAll(eventData, list);

		var raycast = FindFirstRaycast(list);

		var go = ExecuteEvents.GetEventHandler<IEventSystemHandler>(raycast.gameObject);
		if(go == null)
		{
			go = raycast.gameObject;
		}

		if(go == null)
		{
			Debug.Log("Click Nothing");
		}
		else
		{
			EditorGUIUtility.PingObject(go);
			Selection.activeObject = go;
			Debug.Log(go, go);
		}
		
	}


	/// <summary>
	/// Return the first valid RaycastResult.
	/// </summary>
	private RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
	{
		for (var i = 0; i < candidates.Count; ++i)
		{
			if (candidates[i].gameObject == null)
				continue;

			return candidates[i];
		}
		return new RaycastResult();
	}
}
