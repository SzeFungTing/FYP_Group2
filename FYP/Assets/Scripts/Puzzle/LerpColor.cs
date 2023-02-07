using UnityEngine;

public class LerpColor : MonoBehaviour
{
    public GameObject hexagon;

    void Start()
    {
        //var hexRenderer = GetComponent<Renderer>().material.color = hexagon.GetComponent<Renderer>().material.GetColor("Orange_light");
        //var hexRenderer = hexagon.GetComponent<Renderer>();
        //hexRenderer.material.SetColor("_Color", Color.red);
        //Debug.Log("o");

        Mesh mesh = (gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;

        Color[] colours = mesh.colors;
        colours[1] = Color.red;

        //for (int i = 0; i < colours.Length; i++)
        //{
        //    colours[i] = Color.red;//the new colour you want to set it to.
        //}
    }

}