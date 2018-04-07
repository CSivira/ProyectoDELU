using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private CameraColliderController cameraColliderController;

    private void Awake()
    {
        cameraColliderController = GameObject.FindWithTag("Jugador").GetComponentInChildren<CameraColliderController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo") && Input.GetKeyDown(KeyCode.K))
        {
            Destroy(collision.gameObject);
            cameraColliderController.enemyCount--;
        }
    }
}