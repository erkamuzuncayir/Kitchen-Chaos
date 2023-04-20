using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Recipe", fileName = "Recipe")]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> KitchenObjectSOList;
    public string recipeName;
}
