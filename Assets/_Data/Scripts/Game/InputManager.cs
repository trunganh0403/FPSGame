using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }

    [SerializeField] protected float fire1Input;
    public float Fire1Input { get => fire1Input; }

    [SerializeField] protected float fire2Input;
    public float Fire2Input { get => fire2Input; }  

    [SerializeField] protected float mouseScrollWheel;
    public float MouseScrollWheel { get => mouseScrollWheel; }


    void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager.instance = this;
    }

    void Update()
    {
        this.GetMouseDown();
    }

    void FixedUpdate()
    {
        this.GetMousePos();
    }

    protected virtual void GetMouseDown()
    {
        this.fire1Input = Input.GetAxis("Fire1");
        this.fire2Input = Input.GetAxis("Fire2");
        this.mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    protected virtual void GetMousePos()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
