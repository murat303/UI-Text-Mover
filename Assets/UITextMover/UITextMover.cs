using UnityEngine;
using UnityEngine.UI;

public class UITextMover : MonoBehaviour
{
    public GameObject text;
    public float speed = 10;
    public float spacing = 25;

    float startX, endX;

    void Start()
    {
        var rt = GetComponent<RectTransform>();
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        startX = v[2].x;
        endX = v[1].x;

        Spawn();
    }

    public void Spawn()
    {
        var temp = Instantiate(text, transform).transform;
        temp.position = new Vector3(startX, transform.position.y, transform.position.z);
        temp.gameObject.SetActive(true);
        var textWidth = temp.GetComponent<Text>().preferredWidth;

        var move = temp.gameObject.AddComponent<TextMover>();
        move.mover = this;
        move.spawnX = startX - textWidth - spacing;
        move.endX = endX - textWidth - spacing;
        move.speed = speed;
    }

    public class TextMover : MonoBehaviour
    {
        public float spawnX;
        public float endX;
        public float speed;
        public UITextMover mover;
        bool spawned;

        private void Update()
        {
            if (transform.position.x > endX)
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            else
                Destroy(gameObject);
            
            if(transform.position.x < spawnX && !spawned)
            {
                spawned = true;
                mover.Spawn();
            }
        }
    }

}
