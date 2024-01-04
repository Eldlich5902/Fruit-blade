using UnityEngine;

public class Blade : MonoBehaviour
{
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private Camera mainCamera;
    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    private Vector3 direction;
    public Vector3 Direction => direction;

    private bool slicing;
    public bool Slicing => slicing;

    private void Awake()/*Gán collider*/
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();/*hiệu ứng đường chém*/
    }

    private void OnEnable()/*Bật blade*/
    {
        StopSlice();
    }

    private void OnDisable()/*Tắt blade*/
    {
        StopSlice();
    }

    private void Update()
    {
        /*Nhấp chuột bật lưỡi đao*/
        if (Input.GetMouseButtonDown(0)) {
            StartSlice();
        } else if (Input.GetMouseButtonUp(0)) {
            StopSlice();
        } else if (slicing) {
            ContinueSlice();
        }
    }

    private void StartSlice()/*bắt đầu chém*/
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;

        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlice()/*ngừng chém*/
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()/*Tiếp tục chém*/
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);/*chuyển đổi từ không gian màn hình sang khong gian 3d*/
        newPosition.z = 0f;
        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;/*Tốc độ di chuyển = khoảng cách/thời gian*/
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

}
