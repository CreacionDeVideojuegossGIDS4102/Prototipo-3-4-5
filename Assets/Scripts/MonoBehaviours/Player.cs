using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fernando Arvizu Sotelo 
//Hereda de clase character

public class Player : Character
{
    public HealthBar healthBarPrefab; // Referencia HealthBar Prefab
    private HealthBar healthBar; // Copia de referencia de HealthBar Prefab

    void Start()
    {
        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab); // Instanciar HealthBar
            healthBar.character = this; // Referencia del Player en HealthBar
        }
        else
        {
            Debug.LogError("HealthBarPrefab no está asignado en el Inspector.");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Consumable consumable = collision.gameObject.GetComponent<Consumable>();
            if (consumable != null)
            {
                Item hitObject = consumable.item;
                if (hitObject != null)
                {
                    Debug.Log("Nombre: " + hitObject.objectName);
                    bool shouldDisappear = false;

                    switch (hitObject.itemType)
                    {
                        case Item.ItemType.COIN: // Moneda
                            shouldDisappear = true;
                            break;

                        case Item.ItemType.HEALTH: // Barra de Salud
                            Debug.Log("Cantidad a Incrementar: " + hitObject.quantity);
                            shouldDisappear = AdjustHitPoints(hitObject.quantity);
                            break;
                    }

                    if (shouldDisappear)
                    {
                        collision.gameObject.SetActive(false); // Desaparecer
                    }
                }
            }
            else
            {
                Debug.LogWarning("El objeto no tiene el componente Consumable.");
            }
        }
    }

    private bool AdjustHitPoints(int amount)
    {
        if (hitPoints != null && hitPoints.value < maxHitPoints) // No se puede exceder el máximo de puntos
        {
            hitPoints.value = hitPoints.value + amount;
            print("Ajustando Puntos: " + amount + ". Nuevo Valor: " + hitPoints.value);
            return true; // Fue modificado
        }

        return false; // No se modifica, entonces el Heart no desaparece
    }
}
