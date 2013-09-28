using UnityEngine;
using System.Collections;

//Turn this into FContainer
//Move update calls to new MonoBehaviour "FuckGame"
//Build Menu

public class FuckTokyo : MonoBehaviour {
	
	
	public static FuckTokyo instance;
	
	//Constants
	public const int MAX_LENGTH = 32;
	public const float LEFT_EDGE = -235.0f;
	public const float RIGHT_EDGE = 235.0f;
	public const int MAX_BOMBS = 5;
	
	private FLabel _breakLabel;
	private FLabel _fuckstreamLabel;
	
	//FuckMenu
	private FuckMenu _fm;
	
	//FuckGame
	private FuckGame _fg;
	
	//FuckBombs
	private FuckBombs _fb;
	private int[] _fArray;
	
	
	//Multipliers
	private float _dispMultiplier;
	private float _comboMultiplier; //comboMultiplier
	private float _fBombMultiplier;
	private float _conductorMultiplier;
	private float _partsMultiplier;
	private float _railsMultiplier;
	
	private float _timer;
	private float _killTime;
	private float _startTime;
	
	private int _numCars;
	private int _fucksGiven;
	private int _chain;
	private int _fuckBank;
	private int _charOffset;
	private int _mode;	//Determines which string to use:
						// 0 - "fuck"
						// 1 - reverse "kcuf"
						// 2 - dvorak
	
	private string _FUCK;
	private string _DVORAK;
	private string _REVERSE;
	private string _OKAY;
	private bool _chainBroken;
	private bool _started;
	
	// Use this for initialization
	void Start () {
		
		instance = this;
		
		//Setup to use Futile
		FutileParams fparams = new FutileParams(true, true, false, false);
		fparams.AddResolutionLevel(480.0f, 1.0f, 1.0f, "");
		fparams.origin = new Vector2(0.5f,0.5f);
		Futile.instance.Init(fparams);
		Futile.atlasManager.LoadAtlas("Atlases/BananaGameAtlas_Scale1");
		Futile.atlasManager.LoadFont("Franchise","FranchiseFont_Scale1", "Atlases/FranchiseFont_Scale1", 0.0f,-4.0f);
		
		
		//Display CHAIN BREAK
		_breakLabel = new FLabel("Franchise", "CHAIN BROKEN");
		_breakLabel.alpha = 0.0f;
		Futile.stage.AddChild(_breakLabel);
		
		//Display input stream
		_fuckstreamLabel = new FLabel("Franchise", "");
		_fuckstreamLabel.anchorX = 0.0f;
		_fuckstreamLabel.x = LEFT_EDGE;
		_fuckstreamLabel.y = -90.0f;
		Futile.stage.AddChild(_fuckstreamLabel);
		
		//Initialize private variables
		_timer = 0.0f;
		_startTime = 0.0f;
		_chain = 0;
		_charOffset = 0;
		_chainBroken = false;
		_started = false;
		
		//Do not reset these
		_fucksGiven = 0;
		_fuckBank = 0;
		_numCars = 1;
		_killTime = 1.0f;
		_FUCK = "fuck";
		_OKAY = "okay";
		_REVERSE = "kcuf";
		_DVORAK ="yfiv";
		
		//Set Multipliers
		_dispMultiplier = 1.0f;
		_comboMultiplier = 1.0f;
		_fBombMultiplier = 1.0f;
		_conductorMultiplier = 1.0f;
		_partsMultiplier = 1.0f;
		_railsMultiplier = 1.0f;
		
		//Initialize FuckGame
		_fg = new FuckGame();
		_fg.Start();
		Futile.stage.AddChild(_fg);
		
		//Initialize FuckMenu
		_fm = new FuckMenu();
		_fm.Start();
		Futile.stage.AddChild(_fm);
		
		//Initialize FuckBombs
		_fb = new FuckBombs();
		_fb.Start();
		Futile.stage.AddChild(_fb);
		
		//Initiailize fArray
		_fArray = new int[MAX_BOMBS];
		for(int i = 0; i < MAX_BOMBS; i++){
			_fArray[i] = -1;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
		_fm.Update();
		_fg.Update(_fucksGiven, _chain, _fuckBank, _startTime, _dispMultiplier, _timer);
		_fb.Update(_fArray);
		
		//If the chain is broken, then don't take any input
		if(_chainBroken && _started){
			//Reset if 'R' is pressed
			if(Input.GetKeyDown(KeyCode.R)){
				_fuckstreamLabel.text = "";
				_chainBroken = false;
				_started = false;
				_charOffset = 0;
				_breakLabel.alpha = 0.0f;
			}
		}
			else{
			//Check Timer (If it's greater than 1.0s then the chain is broken)
			if(_started){
				_timer += Time.deltaTime;
				_startTime += Time.deltaTime;
				
				//Combo multiplier: every 5 seconds the timer is running, multiply the timer by 1.04
				float numTiers = Mathf.Floor(_startTime/5.0f);
				
				_comboMultiplier = Mathf.Pow(2.0f, numTiers);
				
				
				//Debug.Log("Timer: " + _timer);
				if(_timer > _killTime){
					chainBroke("Timed out!");
				}
			}
			
			//Get the input
			string checkString = _FUCK;
			
			
			
			string nextKey = checkString[_charOffset % 4] + "";
			
			if(!_started && Input.GetKeyDown("m")){
				Debug.Log("enable menu");
				_fm.isEnabled = !_fm.isEnabled;
			}
			//goodpress
			else if(!_fm.isEnabled && Input.GetKeyDown(nextKey)){
				
				_timer = 0.0f;
				if(!_started){
					_started = true;
				}
				
				//Handle string concatenation
				string oldFucks = _fuckstreamLabel.text;
				string newFucks = "";
				
				if(oldFucks.Length > MAX_LENGTH){
					newFucks = oldFucks.Substring(1, oldFucks.Length-1);
				}
				else{
					newFucks = oldFucks + "";
				}
				
				newFucks = newFucks + _OKAY[_charOffset % 4];
				//newFucks = newFucks + _FUCK[_charOffset % 4];
				
				_fuckstreamLabel.text = newFucks;
				
				Debug.Log("Offset: " + (_charOffset % _FUCK.Length));
				_charOffset++;
				if(_charOffset % 4 == 0){
					//Add a point for every completed fuck
					_fucksGiven++;
					
					_dispMultiplier = _comboMultiplier * _conductorMultiplier * _partsMultiplier * 
						_railsMultiplier * _fBombMultiplier;
					
					_chain += Mathf.FloorToInt(_numCars * _dispMultiplier);
				}
				Debug.Log ("Next char: " + _FUCK[_charOffset % 4]);
			}
			//badpress
			//If a key is pressed and it isn't the right one, then the chain is broken
			else if(_started && Input.anyKeyDown && !Input.GetKeyDown(nextKey) && !Input.GetMouseButtonDown(0)){
				chainBroke("Wrong key!");
			}
		}
	}
	//Chain broke, so reset variables for the next try
	void chainBroke(string bMsg){
		Debug.Log("Chain broken: " + bMsg);
		_chainBroken = true;
		_breakLabel.alpha = 1.0f;
		_fuckstreamLabel.text = "";
		_fuckBank += _chain;
		_charOffset = 0;
		_chain = 0;
		_startTime = 0.0f;
		_timer = 0.0f;
		_dispMultiplier = 1.0f;
		_comboMultiplier = 1.0f;
		_fBombMultiplier = 1.0f;
	}
	
	//Add a bomb to the queue
	public bool addBomb(int bombType){
		Debug.Log("Adding bomb: " + bombType);
		bool added = false;
		for(int i = 0; i < MAX_BOMBS && !added; i++){
			if(_fArray[i] < 0){
				_fArray[i] = bombType;
				added = true;	
			}
		}
		
		if(!added){
			return false;
		}
		
		return true;
	}
	
	public void addUpgrade(int upType){
		Debug.Log("Adding upgrade: " + upType);
	}
	
	void OnGUI() {
	}
}
