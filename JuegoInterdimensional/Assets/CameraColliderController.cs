using UnityEngine;

public class CameraColliderController : MonoBehaviour
{
    [HideInInspector] public int enemyCount;

    private CameraManager cameraManager;
    private Transform playerTransform;
    private bool alreadyMoved;

    private void Awake()
    {
        cameraManager = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        playerTransform = GameObject.FindWithTag("Jugador").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
            enemyCount++;
        if (collision.gameObject.CompareTag("Enemigo") && !alreadyMoved)
        {
            alreadyMoved = true;
            cameraManager.jugadorCombatiendo = true;
            Vector3 posicionDestino = new Vector3(playerTransform.position.x + 11f, 0f, -10f);
            cameraManager.EstablecerPosiciones(posicionDestino);
        }
    }

    private void LateUpdate()
    {
        if (cameraManager.jugadorCombatiendo && enemyCount == 0)
            cameraManager.jugadorCombatiendo = false;
    }
}