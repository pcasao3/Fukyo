using UnityEngine;
using System.Collections;

//Create button class(?), extension of FButton
//Keeps track of: Cost, Value, which multiplier it contributes to
//

public class FuckMenu : FContainer {
	
	//Constants
	public const int MAX_LENGTH = 32;
	public const float LEFT_EDGE = -235.0f;
	public const float RIGHT_EDGE = 235.0f;
	
	private FSprite _banana;
	private FButton _b0;
	private FButton _b1;
	private FButton _b2;
	
	private FButton _b3;
	private FButton _b4;
	private FButton _b5;
	
	private bool _isEnabled;
	
	public bool isEnabled {
  		get { return _isEnabled; }
  		set { _isEnabled = value; }
	}
	
	// Use this for initialization
	public void Start () {
		
		_isEnabled = false;
		isVisible = false;
		_banana = new FSprite("Banana");
		_banana.y = 75;
		AddChild(_banana);
		
		//FBomb Buttons
		//FBomb types:
			//Standard (can press any key)
			//Reverse (must type 'KCUF')
			//Dvorak (must type 'YFIV')
		
		
		_b0 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		_b0.x = LEFT_EDGE/2;
		_b1 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		
		_b2 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		_b2.x = RIGHT_EDGE/2;
		
		_b0.y = _b1.y = _b2.y = -30;
		
		_b0.SignalPress += HandleMash;
		_b1.SignalPress += HandleReverse;
		_b2.SignalPress += HandleDvorak;
		
		AddChild(_b0);
		AddChild(_b1);
		AddChild(_b2);
		
		//Upgrade Buttons
		_b3 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		_b3.x = LEFT_EDGE/2;
		_b4 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		
		_b5 = new FButton("CloseButton_normal", "CloseButton_down", "CloseButton_over", "ClickSound");
		_b5.x = RIGHT_EDGE/2;
		
		_b3.rotation = _b4.rotation = _b5.rotation = 45;
		_b3.y = _b4.y = _b5.y = -100;
		
		_b3.SignalPress += Conductor;
		_b4.SignalPress += Parts;
		_b5.SignalPress += Rails;
		
		AddChild(_b3);
		AddChild(_b4);
		AddChild(_b5);
		
	}
	
	//Increase the cost of an item every time it is purchased
	//Other Consumables: Extra Life
	
	//Report FBomb	
	private void HandleMash(FButton button){
		//Check if there's enough in the bank to spend, otherwise fail
		Debug.Log("Add Mash");
		if(FuckTokyo.instance.addBomb(0)){
			Debug.Log("Bought Mash.");
		}
		else{
			Debug.Log("Purchase failed!");
		}
	}
	
	private void HandleReverse(FButton button){
		Debug.Log("Add Reverse");
		FuckTokyo.instance.addBomb(1);
	}
	
	private void HandleDvorak(FButton button){
		Debug.Log("Add Dvorak");
		FuckTokyo.instance.addBomb(2);
	}
	
	//Report Upgrades
	//Other Upgrades: Cars
	private void Conductor(FButton button){
		Debug.Log("Conductor UP");
		FuckTokyo.instance.addUpgrade(0);
	}
	
	private void Parts(FButton button){
		Debug.Log("Parts UP");
		FuckTokyo.instance.addUpgrade(1);
	}
	
	private void Rails(FButton button){
		Debug.Log("Rails UP");
		FuckTokyo.instance.addUpgrade(2);
	}

	// Update is called once per frame
	public void Update () {
		if(!_isEnabled){
			isVisible = false;
		}
		else{
			isVisible = true;
		}
		
	}
}
