using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)/*kiểm tra va chạm của blade với vật thể*/
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            GameManager.Instance.Explode();
        }
    }
}
