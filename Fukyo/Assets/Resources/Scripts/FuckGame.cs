using UnityEngine;
using System.Collections;

public class FuckGame : FContainer {
	
	//Constants
	public const int MAX_LENGTH = 32;
	public const float LEFT_EDGE = -235.0f;
	public const float RIGHT_EDGE = 235.0f;
	
	private FLabel _givenLabel;
	private FLabel _chainLabel;
	private FLabel _bankLabel;
	
	//Debug Labels
	private FLabel _timerLabel;
	private FLabel _multLabel;
	private FLabel _killTimeLabel;
	
	// Use this for initialization
	public void Start () {
		
		//Display "Fucks Given" here
		_givenLabel = new FLabel("Franchise", "Given: 0");
		_givenLabel.scale = 0.75f;
		_givenLabel.anchorX = 0.0f;
		_givenLabel.x = LEFT_EDGE;
		_givenLabel.y = 110.0f;
		AddChild(_givenLabel);	
		
		//Display the current chain here
		_chainLabel = new FLabel("Franchise", "Chain: 0");
		_chainLabel.scale = 0.75f;
		_chainLabel.anchorX = 0.0f;
		_chainLabel.x = LEFT_EDGE;
		_chainLabel.y = 80.0f;
		AddChild(_chainLabel);
		
		
		//Display the current bank here
		_bankLabel = new FLabel("Franchise", "Bank: 0");
		_bankLabel.scale = 0.75f;
		_bankLabel.anchorX = 0.0f;
		_bankLabel.x = LEFT_EDGE;
		_bankLabel.y = 50.0f;
		AddChild(_bankLabel);
		
		//Display the current chain time here
		_timerLabel = new FLabel("Franchise", "Time: 0s");
		_timerLabel.scale = 0.75f;
		_timerLabel.anchorX = 0.0f;
		_timerLabel.x = LEFT_EDGE;
		_timerLabel.y = 20.0f;
		AddChild(_timerLabel);
		
		//Display the current multiplier here
		_multLabel = new FLabel("Franchise", "x1.0");
		_multLabel.scale = 0.75f;
		_multLabel.anchorX = 0.0f;
		_multLabel.x = RIGHT_EDGE-100;
		_multLabel.y = 90.0f;
		AddChild(_multLabel);
		
		//Display the current chain time here
		/*
		_killTimeLabel = new FLabel("Franchise", "Time: 0s");
		_killTimeLabel.anchorX = 0.0f;
		_killTimeLabel.x = RIGHT_EDGE-200;
		_killTimeLabel.y = 60.0f;
		AddChild(_killTimeLabel);*/
		
	}
	
	//Update labels
	public void Update (int fucksGiven, int chain, int fuckBank, float startTime,
		float dispMultiplier, float timer) {
		_givenLabel.text = "Given: " + fucksGiven;
		_chainLabel.text = "Chain: " + chain;
		_bankLabel.text = "Bank: " + fuckBank;
		_timerLabel.text = "Time: " + startTime.ToString("####0.00") + "s" ;
		_multLabel.text = "x" + dispMultiplier;
		//_killTimeLabel.text = "Kill: " + timer;
	}
}
