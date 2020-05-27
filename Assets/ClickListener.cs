using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickListener : MonoBehaviour
{
	public GraphicRaycaster ra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(!Input.GetMouseButtonDown(0))
		{
			return;
		}
		List<RaycastResult> list = new List<RaycastResult>();

		//场景中的EventSystem

		PointerEventData eventData = new PointerEventData(EventSystem.current);

		//鼠标位置

		//eventData.pressPosition = Input.mousePosition;

		eventData.position = Input.mousePosition;

		//Canvas的GraphicRaycaster组件，只能获取当前GraphicRaycaster下的所有UI

		//并且子级中的UI组件RaycastTarget设置为true

		//获取Canvas下的所有UI反馈

		ra.Raycast(eventData, list);

		var raycast = FindFirstRaycast(list);

		var go = ExecuteEvents.GetEventHandler<IEventSystemHandler>(raycast.gameObject);
		if(go == null)
		{
			go = raycast.gameObject;
		}

		Debug.Log(go, go);
	}

	/// <summary>
	/// Return the first valid RaycastResult.
	/// </summary>
	protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
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
