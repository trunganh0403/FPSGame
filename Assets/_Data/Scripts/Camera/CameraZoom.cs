using Cinemachine;
using UnityEngine;

public class CameraZoom : GameMonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomFieldOfView = 20f;
    public float normalFieldOfView = 60f;
    public float zoomSpeed = 5f;
    [SerializeField] protected Transform scope;

    void Update()
    {
        HandleZoom();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVirtualCamera();
    }

    protected virtual void LoadVirtualCamera()
    {
        if (this.virtualCamera != null) return;
        this.virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        Debug.LogWarning(transform.name + ": LoadVirtualCamera", gameObject);
    }

    private void HandleZoom()
    {

        if (IsShooting())
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomFieldOfView, zoomSpeed * Time.deltaTime);
            this.SetScope(true);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, normalFieldOfView, zoomSpeed * Time.deltaTime);
            this.SetScope(false);
        }
    }

    protected virtual bool IsShooting()
    {
        return InputManager.Instance.Fire2Input ==1;
    }

    protected virtual void SetScope(bool isZoom)
    {
        if (scope == null) return;
        scope.gameObject.SetActive(isZoom);
    }    

}