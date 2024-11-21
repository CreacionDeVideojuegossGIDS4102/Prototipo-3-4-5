using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
//Fernando Arvizu Sotelo
public class MovementController : MonoBehaviour
{
    //Velociad de los personajes
    public float movementSpeed = 3.0f;
    //Representa la ubicacion
    Vector2 movement = new Vector2();
    //Referencia al rigibody
    Rigidbody2D rgb2D;
    Animator animator;
    string animationState = "AnimationState";
    enum CharStates{
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idleSouth = 0

    }

    // Start is called before the first frame update
    void Start(){
        //Establece el enlace
        rgb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        this.UpdateState();
    }

    /*
 * Método que define la animación a ejecutar en base al movimiento realizado por el usuario.
 */
private void UpdateState() {
    if (movement.x > 0) { // ESTE
        animator.SetInteger(animationState, (int)CharStates.walkEast);
    } else if (movement.x < 0) { // OESTEs
        animator.SetInteger(animationState, (int)CharStates.walkWest);
    } else if (movement.y > 0) { // NORTE
        animator.SetInteger(animationState, (int)CharStates.walkNorth);
    } else if (movement.y < 0) { // SUR
        animator.SetInteger(animationState, (int)CharStates.walkSouth);
    } else { // IDLE
        animator.SetInteger(animationState, (int)CharStates.idleSouth);
    }
}

    private void FixedUpdate(){
        MoverPersonaje();
        
    }

    private void MoverPersonaje(){
        //Captura los datos del usuario 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Conserva el rango de velocidad
        movement.Normalize();
        rgb2D.velocity = movement* movementSpeed;

    }
}
