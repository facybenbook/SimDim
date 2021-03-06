using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimDim;


public class BHV_Hunger : MonoBehaviour 
{
	public float Hunger = 0.5f;  // ratio. 1 is starving at max, 0 is full.

	void Start () 
	{	
		this.Hunger = UnityEngine.Random.value;
	}	
	
	
	void Update () 
	{
		// As the time flows the stomach empty itself.	
		
		this.Hunger += Time.fixedDeltaTime*0.1f;
		if(this.Hunger>1) // clamping value.
			this.Hunger=1;
	}
	
	public bool isHungry()
	{
		if(this.Hunger>0.8f)  //TODO : add some fuzzy logic to this. (and maybe link it to SightRange, and speed: the hungrier I am, the far I go, the careless I am...
 			return true;
		else
			return false;
	}
	
	public void eatFood(SimDim.Food _targetFood)
	{	
		this.Hunger=0; // End of starvation
		_targetFood.beEaten();   //TODO:  for blog: eat (_victim) or beEaten(_killer)
		Debug.Log (this.gameObject.name+" eat "+_targetFood.root.name);
		
		_targetFood.belonging.destroyMatter(_targetFood);
	}
	

	public SimDim.Food lookForFood( List<SimDim.Food> _TheList )
	{
		SimDim.Food nearestFood = _TheList[0];
		float minDistance = Vector3.Distance(this.transform.position, nearestFood.root.transform.position);
		foreach( SimDim.Food FoodCandidate in _TheList)
		{
			float currentDistance = Vector3.Distance(this.transform.position, FoodCandidate.root.transform.position);
			
			if( currentDistance <  minDistance )
			{
				nearestFood = FoodCandidate;
				minDistance = currentDistance;
			}
		}
		return nearestFood;
	}
}
