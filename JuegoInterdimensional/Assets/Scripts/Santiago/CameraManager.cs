using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float suavidadCamara = 0.3f;
    [HideInInspector] public bool jugadorCombatiendo;

    private Transform transformJugador;
    private Vector3 diferencia;
    private Vector3 velocidadActual = Vector3.zero;
    private Vector3 destino;

    private void Awake()
    {
        transformJugador = GameObject.FindWithTag("Jugador").GetComponent<Transform>();
    }

    private void Start()
    {
        diferencia = transform.position - transformJugador.position;
    }

    private void LateUpdate()
    {
        if (!jugadorCombatiendo)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transformJugador.position + diferencia, ref velocidadActual, suavidadCamara);
        }
        else
        {
            if (transform.position != destino)
                transform.position = Vector3.SmoothDamp(transform.position, destino, ref velocidadActual, suavidadCamara);
        }
    }

    public void EstablecerPosiciones(Vector3 destino)
    {
        this.destino = destino;
    }
}