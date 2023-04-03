using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GlobalVariables variables;
    private float elixirToAdd;

    public void addElixir() 
    {
        elixirToAdd = Random.Range(30, 50);
        variables.elixir += elixirToAdd;
        Debug.Log(variables.elixir);
    }
    public void onClickRIfle() 
    {
        if(variables.elixir >= 1000 && variables.canUseRifle == false) 
        {
            variables.canUseRifle = true;
            variables.elixir -= 1000;
        }
        else if (variables.elixir < 1000 && variables.canUseRifle == false)
        {
            Debug.Log("not enough");
        }
    }
}
