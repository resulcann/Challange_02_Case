using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    private MeshRenderer _itemRenderer;
    private Material _itemMaterial;

    private void Start()
    {
        _itemRenderer = GetComponent<MeshRenderer>();
        _itemMaterial = _itemRenderer.material;
    }
    public void Slice(MeshSlicer slicer)
    {
        GameObject[] parts = slicer.SliceObject(_itemRenderer.gameObject, transform, _itemMaterial);
        Vector3 localPos = _itemRenderer.transform.localPosition;

        parts[0].transform.SetParent(_itemRenderer.transform.parent);
        parts[1].transform.SetParent(_itemRenderer.transform.parent);

        parts[0].transform.localScale = _itemRenderer.transform.localScale;
        parts[1].transform.localScale = _itemRenderer.transform.localScale;

        parts[0].transform.localPosition = localPos;
        parts[1].transform.localPosition = localPos;

        parts[0].gameObject.AddComponent<BoxCollider>();
        parts[1].gameObject.AddComponent<BoxCollider>();

        parts[0].gameObject.AddComponent<Rigidbody>();
        parts[1].gameObject.AddComponent<Rigidbody>();

        // Kesme yönüne göre içte ve dışta kalan parçaları belirleniyor
        bool isCutFromLeft = transform.position.x >= PlatformSnap.Instance.GetLastSnappedPlatform().transform.position.x;
        GameObject snapPart = isCutFromLeft ? parts[1] : parts[0];
        GameObject fallPart = isCutFromLeft ? parts[0] : parts[1];

        // Snaplenecek olan yani içerideki parçaa isKinematic false olacak
        snapPart.GetComponent<Rigidbody>().isKinematic = true;
        snapPart.AddComponent<SliceableObject>();
        // Diğer parça ise düşeceği için isKinematic false olcak.
        fallPart.GetComponent<Rigidbody>().isKinematic = false;
    
        var forcePower = fallPart.transform.position.x >= PlatformSnap.Instance.GetLastSnappedPlatform().transform.position.x ? 1 : -1;  
        fallPart.GetComponent<Rigidbody>().AddForce(Vector3.right * forcePower, ForceMode.Impulse);

        // snaplenen içeride kalan platformu son snaplenen platform olarak ayarla.
        PlatformSnap.Instance.SetLastSnappedPlatform(snapPart.transform);

        _itemRenderer.gameObject.SetActive(false);
    }

    public float GetCenterXPoint()
    {
        var col = GetComponent<Collider>();
        var middleX = col.bounds.center.x;

        return middleX;
    }
    public float GetLeftEdgePosX()
    {
        var col = GetComponent<Collider>();
        var leftEdgeX = col.bounds.min.x;

        return leftEdgeX;
    }

    public float GetRightEdgePosX()
    {
        var col = GetComponent<Collider>();
        var rightEdgeX = col.bounds.max.x;

        return rightEdgeX;
    }

    public Vector3 GetLeftEdgeCenter()
    {
        var col = GetComponent<Collider>();
        var leftEdgeCenter = new Vector3(GetLeftEdgePosX(), col.bounds.center.y, col.bounds.center.z);

        return leftEdgeCenter;
    }

    public Vector3 GetRightEdgeCenter()
    {
        var col = GetComponent<Collider>();
        var rightEdgeCenter = new Vector3(GetRightEdgePosX(), col.bounds.center.y, col.bounds.center.z);

        return rightEdgeCenter;
    }


}