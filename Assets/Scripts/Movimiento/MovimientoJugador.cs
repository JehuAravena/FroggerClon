using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    private bool puedeSaltar = false;
    private PlayerInput input;
    public bool enPared;

    public GameObject visual;
    public GameObject checkPared;
    public Animator anim;
    public Muerte muerte;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        muerte.muerto = false;
    }

    private void Update()
    {
        Vector2 mover = input.actions["Mover"].ReadValue<Vector2>();
        enPared = Physics2D.OverlapCircle(checkPared.transform.position, 0.1f, LayerMask.GetMask("pared"));
        

        if (mover != Vector2.zero && puedeSaltar && !enPared)
        {
            StartCoroutine(Salto(mover, 0.1f));
            puedeSaltar = false;

            //rotar visual
            if (mover.x > 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (mover.x < 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (mover.y > 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (mover.y < 0)
            {
                visual.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

        }
        else if (mover == Vector2.zero)
        {
            puedeSaltar = true;
        }
    }

    private IEnumerator Salto(Vector2 posicionSalto, float duracion)
    {
        Vector2 posicionInicial = transform.position;
        float tiempo = 0;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector2.Lerp(posicionInicial, posicionInicial + posicionSalto*2, tiempo / duracion);
            anim.SetBool("Salta", true);
            yield return null;
        }
        anim.SetBool("Salta", false);
        yield return null;
    }
}
