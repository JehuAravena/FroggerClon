using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    public Animator anim;
    public MovimientoJugador movimiento;
    public GameObject hitbox;
    public bool muerto;
    public float tiempo;

    private void Update()
    {
        if (Physics2D.OverlapCircle(hitbox.transform.position, .5f, LayerMask.GetMask("muerte")))
        {
            anim.SetBool("muerte", true);
            movimiento.enabled = false;
            muerto = true;
        }

        if (muerto)
        {
            movimiento.visual.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (movimiento.enabled == false)
        {
            tiempo += Time.deltaTime;
            if (tiempo >= 2)
            {
                //Destroy(gameObject);
                Debug.Log("muerte");
            }
        }
    }
}
