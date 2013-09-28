using UnityEngine;
using System.Collections;

public class FuckBombs : FContainer {
	//Start off with only 1 FBomb slot, increase slots as they are purchased
	
	//Constants
	public const int MAX_LENGTH = 32;
	public const float LEFT_EDGE = -235.0f;
	public const float RIGHT_EDGE = 235.0f;
	
	private FSprite[] _fbombs;
	
	
	// Use this for initialization
	public void Start () {
		_fbombs = new FSprite[FuckTokyo.MAX_BOMBS];
		
		//Display up to 5 bombs
		for(int i = 0; i < FuckTokyo.MAX_BOMBS; i++){
			_fbombs[i] = new FSprite("CloseButton_normal");
			_fbombs[i].anchorX = 0.0f;
			_fbombs[i].scale = 0.5f;
			_fbombs[i].x = LEFT_EDGE + (i * 30);
			_fbombs[i].y = 140.0f;
			AddChild(_fbombs[i]);
		}
	}
	
	// Update is called once per frame
	public void Update(int[] fArray) {
		//Check the array
		for(int i = 0; i < FuckTokyo.MAX_BOMBS; i++){
			if(fArray[i] >= 0){
				_fbombs[i].alpha = 1.0f;
			}
			else{
				_fbombs[i].alpha = 0.25f;
			}
		}
	}
}
