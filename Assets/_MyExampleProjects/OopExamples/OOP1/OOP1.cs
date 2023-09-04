using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOP1 : MonoBehaviour
{
    private Transform camTransform;

    public GameObject directionalLight;
    private Transform lightTransform;

    private void Awake()
    {
        camTransform = this.GetComponent<Transform>();
        Debug.Log(camTransform.localPosition);

        
        Debug.Log(GameObject.Find("Directional Light").transform.position);

    }

    // Start is called before the first frame update
    void Start()
    {
        Character hero = new Character();
        Debug.LogFormat("Hero {0} - {1} Experience",hero.name, hero.experience);
        

        Character woman_car = new Character("Rihanna",100);
        woman_car.WriteInfo();


        Weapon arrow = new Weapon("Arrow", 105);
        arrow.WriteWeaponInfo();


        Paladin knight = new Paladin("Super Paladin", 250, arrow);
        knight.WriteInfo();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
