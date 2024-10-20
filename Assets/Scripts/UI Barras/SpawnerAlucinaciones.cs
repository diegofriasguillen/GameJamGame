using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerAlucinaciones : MonoBehaviour
{
    public List<GameObject> alucinacionesAud = new List<GameObject>();
    public List<Sprite> alucinaciones = new List<Sprite>();
    public Canvas canvas;
    public Image alucinacion;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            spawnaluc();
        }
    }

    public void spawnearAud()
    {
        GameObject random = alucinacionesAud[Random.Range(0, alucinacionesAud.Count)];

        Vector2 circle = Random.insideUnitCircle*5;

        Vector3 pos = new Vector3(circle.x, Random.Range(-1, 4), circle.y) + FPMovement.instance.transform.position;

        Instantiate(random,pos,Quaternion.identity);
        
    }

    public void spawnaluc()
    {

        Sprite random = alucinaciones[Random.Range(0, alucinaciones.Count)];

        alucinacion.sprite = random;

        alucinacion.GetComponent<Animator>().SetTrigger("Waaaaaa");


    }


}
