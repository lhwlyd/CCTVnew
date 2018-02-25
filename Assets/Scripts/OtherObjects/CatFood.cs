using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFood : MonoBehaviour {

    public int foodPoints;

    public int getFoodPoints() {
        return this.foodPoints;
    }

    public void setFoodPoints( int foodPts ) {
        this.foodPoints = foodPts;
    }

    public void getEaten(Cat cat)
    {
        this.foodPoints--;
        cat.Fed(3);
        if (foodPoints <= 0) {
            Destroy(this.gameObject);
        }
    }
}
