using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadWebCam : MonoBehaviour{
	
public RawImage webcam_image;
private string screenShotURL= "http://200.5.235.51:8090/"; //change for your own public address

private WebCamTexture WCTexture;
private int tx_width = 320;
private int tx_height = 240;

    // Use this for initialization
    IEnumerator Start () {
    //WCTexture = new WebCamTexture(160, 120);
    WCTexture = new WebCamTexture(tx_width, tx_height);
    webcam_image.texture = WCTexture;
	webcam_image.material.mainTexture = WCTexture;
    WCTexture.Play();
	
		while (true) {			
			yield return UploadPNG ();
            yield return new WaitForSeconds (0.1f);
            //yield return new WaitForSeconds(1f);
        }
	}

	IEnumerator UploadPNG()
	{     

		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        //Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        Texture2D tex = new Texture2D(tx_width, tx_height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.ReadPixels(new Rect(width/2 - tx_width/2, height/2 - tx_height/2, tx_width - 2, tx_height - 2), 0, 0);
        tex.Apply();

		// Encode texture into PNG
		//byte[] bytes = tex.EncodeToPNG();
		byte[] bytes = tex.EncodeToJPG();
		Object.Destroy(tex);

		// For testing purposes, also write to a file in the project folder
		// File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);


		// Create a Web Form
		WWWForm form = new WWWForm();
		//form.AddField("frameCount", Time.frameCount.ToString());
		form.AddBinaryData("fileUpload", bytes);

		// Upload to a cgi script
		WWW w = new WWW(screenShotURL, form);
		yield return w;

		if (w.error != null)
		{
			Debug.Log(w.error);
		}
		/*else
		{
			Debug.Log("Finished Uploading Screenshot");
		}*/
	}
}