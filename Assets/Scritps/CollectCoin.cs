using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoin : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject portal;
    public AudioSource collect;
    public List<GameObject> foods;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;

        if (pointsText != null)
            pointsText.text = "Points: 0";
    }

    private void CheckFoods()
    {
        if (points == foods.Count)
        {
            portal.GetComponent<Portal>().PlayAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            points++;

            if (pointsText != null)
                pointsText.text = "Points: " + points;

            if (collect != null)
                collect.Play();

            Destroy(other.gameObject);

            CheckFoods();
        }
    }
}
