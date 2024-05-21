using EzySlice;
using System.Collections.Generic;
using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
	public GameObject[] SliceObject(GameObject obj, Transform targetPlatform, Material crossSectionMaterial = null)
	{
		List<Transform> outsidePart = new List<Transform>();
		List<Transform> insidePart = new List<Transform>();
		int count = obj.transform.childCount;
		var lastSnappedPlatform = PlatformSnap.Instance.GetLastSnappedPlatform().GetComponent<SliceableObject>();
		var sliceEdge = targetPlatform.position.x >= lastSnappedPlatform.transform.position.x ? 
			lastSnappedPlatform.GetRightEdgeCenter() : lastSnappedPlatform.GetLeftEdgeCenter();
		var slicePointX = targetPlatform.position.x >= lastSnappedPlatform.transform.position.x ? 
			lastSnappedPlatform.GetRightEdgePosX() : lastSnappedPlatform.GetLeftEdgePosX();
		
		for (int i = 0; i < count; i++)
        {
	        if(obj.transform.GetChild(0).position.x < Mathf.Abs(slicePointX))
            {
				insidePart.Add(obj.transform.GetChild(0));
				obj.transform.GetChild(0).SetParent(null, false);
			}
			else
            {
				outsidePart.Add(obj.transform.GetChild(0));
				obj.transform.GetChild(0).SetParent(null, false);
			}
        }
		GameObject[] parts = new GameObject[2];
		SlicedHull hull = obj.Slice(sliceEdge, Vector3.right, crossSectionMaterial);
		parts[0] = hull.CreateUpperHull(obj, crossSectionMaterial);
		parts[1] = hull.CreateLowerHull(obj, crossSectionMaterial);
        for (int i = 0; i < outsidePart.Count; i++)
        {
			outsidePart[i].SetParent(parts[0].transform, false);
		}
		for (int i = 0; i < insidePart.Count; i++)
		{
			insidePart[i].SetParent(parts[1].transform, false);
		}
		return parts;
	}
}
