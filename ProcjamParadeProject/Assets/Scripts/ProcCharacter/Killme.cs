using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killme : MonoBehaviour
{
    List<Material> toDestroy = new List<Material>();
    public bool destroyNow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).transform.position.z > 80 )
        {
            Kill();
        }

        if (destroyNow)
        {
           // DestroyMateraisl();
           // destroyNow = false;

            Kill();


            ColourPicker cP = GetComponent<ColourPicker>();

            for (int i = 0; i < 6; i++)
            {
                Destroy(cP.matsAndShades[i].material);

                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Destroy(cP.matsAndShades[j].tints[k]);
                        Destroy(cP.matsAndShades[j].shades[k]);
                    }

                }

            }

            Resources.UnloadUnusedAssets();//wow!! Removes unused assets - the ones that were on the character. Seems like we still need to manually kill the other though

       
        }
        

    }


    void Kill()
    {
        //we have selected the materials, we can get rid of the other created materials
        

        Destroy(gameObject);

    }

}
