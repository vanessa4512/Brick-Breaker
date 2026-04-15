using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[]       states;
    public int            health         { get; private set; }
    public bool           unbreakable;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if (!this.unbreakable)
        {
            this.health = this.states.Length;
            this.spriteRenderer.sprite = this.states[this.health-1];
        }
    }

    private void Hit() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
