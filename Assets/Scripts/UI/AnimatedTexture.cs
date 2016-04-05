using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatedTexture : MonoBehaviour
{
    public int _columns = 2;                        
    public int _rows = 2;                          
    public Vector2 _scale = new Vector3(1f, 1f);    
    public Vector2 _offset = Vector2.zero;         
    public Vector2 _buffer = Vector2.zero;          
	           
    public bool _playOnce = false;


                 
    public bool _disableUponCompletion = false;              
    public bool _playOnEnable = true;               
    public bool _newMaterialInstance = false;       

    private int _index = 0;                         
    private Vector2 _textureSize = Vector2.zero;    
    private Material _materialInstance = null;      
    private bool _hasMaterialInstance = false;      
    private bool _isPlaying = false;
    private new Renderer renderer;          

	[SerializeField]
	private float _framesPerSecond = 10f; 

	public float FramesPerSecond 
	{
		get 
		{ 
			return _framesPerSecond;
		}
		set 
		{
			_framesPerSecond = value;
		}
	}

    public void Play()
    {
        if (_isPlaying)
        {
            StopCoroutine("updateTiling");
            _isPlaying = false;
        }

        renderer.enabled = true;

        _index = _columns;

        StartCoroutine(updateTiling());
    }

    public void ChangeMaterial(Material newMaterial, bool newInstance = false)
    {
        if (newInstance)
        {
            // First check our material instance, if we already have a material instance
            // and we want to create a new one, we need to clean up the old one
            if (_hasMaterialInstance)
                Object.Destroy(renderer.sharedMaterial);

            // create the new material
            _materialInstance = new Material(newMaterial);

            // Assign it to the renderer
            renderer.sharedMaterial = _materialInstance;

            // Set the flag
            _hasMaterialInstance = true;
        }
        else // if we dont have create a new instance, just assign the texture
            renderer.sharedMaterial = newMaterial;

        // We need to recalc the texture size (since different material = possible different texture)
        CalcTextureSize();

        // Assign the new texture size
        renderer.sharedMaterial.SetTextureScale("_MainTex", _textureSize);
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        ChangeMaterial(renderer.sharedMaterial, _newMaterialInstance);
    }

    private void OnDestroy()
    {
        // If we wanted new material instances, we need to destroy the material
        if (_hasMaterialInstance)
        {
            Object.Destroy(renderer.sharedMaterial);
            _hasMaterialInstance = false;
        }
    }

    private void OnEnable()
    {

        CalcTextureSize();

        if (_playOnEnable)
            Play();
    }

    private void CalcTextureSize()
    {
        //set the tile size of the texture (in UV units), based on the rows and columns
        _textureSize = new Vector2(1f / _columns, 1f / _rows);

        // Add in the scale
        _textureSize.x = _textureSize.x / _scale.x;
        _textureSize.y = _textureSize.y / _scale.y;

        // Buffer some of the image out (removes gridlines and stufF)
        _textureSize -= _buffer;
    }

    // The main update function of this script
    private IEnumerator updateTiling()
    {
        _isPlaying = true;

        // This is the max number of frames
        int checkAgainst = (_rows * _columns);

        while (true)
        {
            // If we are at the last frame, we need to either loop or break out of the loop
            if (_index >= checkAgainst)
            {
                _index = 0;  // Reset the index

                // If we only want to play the texture one time
                if (_playOnce)
                {
                    if (checkAgainst == _columns)
                    {
                        if (_disableUponCompletion)
                            renderer.enabled = false;

                        // turn off the isplaying flag
                        _isPlaying = false;

                        // Break out of the loop, we are finished
                        yield break;
                    }
                    else
                        checkAgainst = _columns;    // We need to loop through one more row
                }

            }

            // Apply the offset in order to move to the next frame
            ApplyOffset();

            //Increment the index
            _index++;

            // Wait a time before we move to the next frame. Note, this gives unexpected results on mobile devices
            yield return new WaitForSeconds(1f / _framesPerSecond);
        }
    }

    private void ApplyOffset()
    {
        //split into x and y indexes. calculate the new offsets
        Vector2 offset = new Vector2((float)_index / _columns - (_index / _columns), //x index
                                      1 - ((_index / _columns) / (float)_rows));    //y index

        // Reset the y offset, if needed
        if (offset.y == 1)
            offset.y = 0.0f;

        // If we have scaled the texture, we need to reposition the texture to the center of the object
        offset.x += ((1f / _columns) - _textureSize.x) / 2.0f;
        offset.y += ((1f / _rows) - _textureSize.y) / 2.0f;

        // Add an additional offset if the user does not want the texture centered
        offset.x += _offset.x;
        offset.y += _offset.y;

        // Update the material
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
