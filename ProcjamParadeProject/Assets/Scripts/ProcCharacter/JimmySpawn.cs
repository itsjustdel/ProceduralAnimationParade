using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JimmySpawn : MonoBehaviour {

    int total =0;
    
    public int amountX = 1;
    public int amountY = 1;

    public bool autoWalk = false;
    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        StartCoroutine("Spawn");
	}

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    IEnumerator Spawn()
    {
       // for (int j = 0; j < amountY; j++)
        { 
            for (int i = 0; i < amountX; i++)
            {   
                //make character
                GameObject g = new GameObject();
                
                g.name = "Character " + i.ToString();
                //removes from game after walks far enough
                g.AddComponent<Killme>();

                ColourPicker cP = g.AddComponent<ColourPicker>();
                cP.enabled = false;
                cP.random = false;
                cP.RandomHue();
                cP.strictColours = false;
                cP.tintsAndShades = 6;//this is how many mats a character has

                cP.userControlled = true;


                

                cP.Start();

                
                BindPoseExample bpe = g.AddComponent<BindPoseExample>();


                ProceduralAnimator pA = g.AddComponent<ProceduralAnimator>();

                CharacterControllerProc ccp = g.AddComponent<CharacterControllerProc>();
                //ccp.enabled = false;
                ccp.pA = pA;
                ccp.bPE = bpe;

                if (autoWalk)
                {
                    //need tank on
                    ccp.tankControls = true;
                    ccp.autoWalk = true;
                }

                bpe.spawnPoint = Vector3.right * i * 3 - Vector3.forward *35;//*10 to get off cam

                total++;

                if (i >= amountX - 1)
                    i = 0;


                if (total < amountX)
                    yield return new WaitForEndOfFrame();
                else
                    yield return new WaitForSeconds(1.5f);

            }
        }

        yield break;
    }

}
