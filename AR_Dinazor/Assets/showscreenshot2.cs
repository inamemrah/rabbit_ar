using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class showscreenshot2 : MonoBehaviour {
	private int screenshotCount = 0;

	string ActiveUrl;

	Texture2D screenCap;
	Texture2D border;
	private bool shot = false;

	public GameObject ssButton,SizeSlider,AddButton;

	string UnityClass = "com.unity3d.player.UnityPlayer";
	private string filePath;
	private string movePath;


	void Start () {
		screenCap = new Texture2D(300, 200, TextureFormat.RGB24, false); // 1
		border = new Texture2D(2, 2, TextureFormat.ARGB32, false); // 2
		border.Apply();

		ssButton.SetActive (true);
        SizeSlider.SetActive (true);
        AddButton.SetActive (true);
		
	}

	public void ScreenShot()
	{
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{

			captureScreenshot ("true");
            ssButton.SetActive(false);
            SizeSlider.SetActive(false);
            AddButton.SetActive(false);

        }
	}

	void Update()
	{
		if (shot == true) 
		{
			if (File.Exists (Application.persistentDataPath + "/" + filePath)) 
			{
				if (Application.platform == RuntimePlatform.Android) {
					File.Move (Application.persistentDataPath + "/" + filePath, movePath);

					UpdateGallery ();
				}
				shot = false;

				if (Application.platform == RuntimePlatform.Android) {
					
				}
			}
		}
	}
 	

	void OnGUI(){
		

		/*if(shot)
		{
			//GUI.DrawTexture(new Rect(10, 10, 30, 30), screenCap, ScaleMode.StretchToFill);
		}*/
	}

	void captureScreenshot(string result){
		if (result == "true") {
			StartCoroutine(Capture());
		} 
	}


 public	IEnumerator Capture(){
		yield return new WaitForEndOfFrame();
		screenCap.ReadPixels(new Rect(198, 98, 298, 198), 0, 0);
		screenCap.Apply();

		// Encode texture into PNG
		byte[] bytes = screenCap.EncodeToPNG();
		//Object.Destroy(screenCap);

		// For testing purposes, also write to a file in the project folder

		string screenshotFilename = "Green_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".JPG";

		ScreenCapture.CaptureScreenshot (screenshotFilename);


        if (Application.platform == RuntimePlatform.Android) {

			string myFolderLocation = "/sdcard/DCIM/ARDinasours/";


			if (!System.IO.Directory.Exists(myFolderLocation))
			{
				System.IO.Directory.CreateDirectory(myFolderLocation);
			}

			movePath = myFolderLocation + screenshotFilename;



		} 


		filePath = screenshotFilename;

		shot = true;

        ssButton.SetActive(true);
        SizeSlider.SetActive(true);
        AddButton.SetActive(true);


        ActiveUrl = movePath;

		// string action = Intent.ACTION_MEDIA_SCANNER_SCAN_FILE 
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		string action = intentClass.GetStatic<string>("ACTION_MEDIA_SCANNER_SCAN_FILE");

		// Intent intentObject = new Intent(action);
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", action);

		// Uri uriObject = Uri.parse("file:" + filePath);
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + movePath);

		// intentObject.setData(uriObject);
		intentObject.Call<AndroidJavaObject>("setData", uriObject);

		// this.sendBroadcast(intentObject);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("sendBroadcast", intentObject);




	}







	/*
	public IEnumerator CaptureScreen()
	{
		
		yield return new WaitForEndOfFrame();



		string screenshotFilename = "Green_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".JPG";
		string DefaultPath = Application.persistentDataPath;

		Application.CaptureScreenshot (screenshotFilename);

		if (Application.platform == RuntimePlatform.Android) {

			string myFolderLocation = "/sdcard/DCIM/Green/";


			if (!System.IO.Directory.Exists(myFolderLocation))
			{
				System.IO.Directory.CreateDirectory(myFolderLocation);
			}

			Path = myFolderLocation + screenshotFilename;

		} 



		shot = true;




		ssButton.SetActive (true);
		backButton.SetActive (true);
		testColor.SetActive (true);
		colorButton.SetActive (true);
		Slider_T.SetActive (true);
		Text_t.SetActive (true);
		Slider_S.SetActive (true);
		Text_S.SetActive (true);



		


	}

*/


	void UpdateGallery()
	{
		ActiveUrl = movePath;

		// string action = Intent.ACTION_MEDIA_SCANNER_SCAN_FILE 
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		string action = intentClass.GetStatic<string>("ACTION_MEDIA_SCANNER_SCAN_FILE");

		// Intent intentObject = new Intent(action);
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", action);

		// Uri uriObject = Uri.parse("file:" + filePath);
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + movePath);

		// intentObject.setData(uriObject);
		intentObject.Call<AndroidJavaObject>("setData", uriObject);

		// this.sendBroadcast(intentObject);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("sendBroadcast", intentObject);

	}

}