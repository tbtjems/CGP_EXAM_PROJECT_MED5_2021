using UnityEngine;
 
public class AudioSourceLoudnessTester : MonoBehaviour {
 
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
 
    private float currentUpdateTime = 0f;
 
    private float clipLoudness;
    public float[] clipSampleData;
 
    // Use this for initialization
    
    Renderer rend;
    public Material material;
    
    void Awake () {
     
        if (!audioSource) {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];
        
    }
     
    // Update is called once per frame
    void Update () {
     
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep) {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach (var sample in clipSampleData) {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; 
            
            //Set parameter
            print(clipLoudness);
            
            // Colour
            Color ranCol = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f)
            );
            print("Color: " + ranCol);
            material.SetColor("tint", ranCol);
            
            // Speed
            float value1 = Random.Range(3.0f,20.0f);
            print("Speed: " + value1);
            float rangeSpeed = value1;
            material.SetFloat("speed", rangeSpeed);
            
            
            // Scale 1
            clipLoudness = Mathf.Lerp (clipLoudness, 10.0f, 0.5f  * Mathf.Pow(value1, 1.8f)/1000);
            print("Scale 1: " + clipLoudness);
            float rangeScale1 = clipLoudness;
            material.SetFloat("scale1", rangeScale1);
            
            // Scale 2
            clipLoudness = Mathf.Lerp (clipLoudness, 10.0f, 0.3f  * value1/100);
            print("Scale 2: " + clipLoudness);
            float rangeScale2 = clipLoudness;
            material.SetFloat("scale2", rangeScale2);
            
            // Scale 3
            clipLoudness = Mathf.Lerp (clipLoudness, 10.0f, 0.7f  * value1/1000);
            print("Scale 3: " + clipLoudness);
            float rangeScale3 = clipLoudness;
            material.SetFloat("scale3", rangeScale3);
            
            // Scale 4
            clipLoudness = Mathf.Lerp (clipLoudness, 10.0f, 0.2f  * value1/90);
            print("Scale 4: " + clipLoudness);
            float rangeScale4 = clipLoudness;
            material.SetFloat("scale4", rangeScale4);
            
            // Update step
            updateStep = Mathf.SmoothStep(1.0f,10.0f, clipLoudness);
            print("Update:" + updateStep);
        }
 
    }
 
}