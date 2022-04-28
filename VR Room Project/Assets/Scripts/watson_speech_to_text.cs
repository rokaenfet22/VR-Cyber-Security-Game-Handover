using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.SpeechToText.V1;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.DataTypes;
using UnityEngine.Audio;
using UnityEngine.UI;

//Make sure to set classname to that of the script's
public class watson_speech_to_text : MonoBehaviour
{
    //inspection section defining
    #region SET THESE VARIABLES IN THE INSPECTOR
    [Tooltip("The services URL https://api.eu-gb.speech-to-text.watson.cloud.ibm.com/instances/f3436999-0f0a-4e96-8f61-96a5ba8fee3d")]
    [SerializeField]
    private string _serviceUrl;
    [Header("IAM Authentication")]
    [Tooltip("The IAM apikey")]
    [SerializeField]
    private string _iamApikey;

    [Header("Parameters")]
    [Tooltip("The Model to use,. This defaults to en-GB_BroadbandModel")]
    [SerializeField]
    private string _recognizeModel;
    #endregion

    //defining variables to be used in this script
    private int _recordingRoutine = 0;
    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingBufferSize = 1;
    private int _recordingHZ = 22050;
    //_service to access Watson Speech-To-Text variables
    private SpeechToTextService _service;

    //for colour changing
    
    private string _finalTranscript;

    // Start is called before the first frame update
    void Start()
    {
        //allows using Watson log's in Unity, useful for bug tracking
        LogSystem.InstallDefaultReactors();
        Runnable.Run(CreateService());
    }

    private IEnumerator CreateService()
    {
        if (string.IsNullOrEmpty(_iamApikey))
        {
            throw new IBMException("Please provide IAM apikey for this service");
        }

        IamAuthenticator authenticator = new IamAuthenticator(apikey: _iamApikey);

        while (!authenticator.CanAuthenticate())
            yield return null;

        _service = new SpeechToTextService(authenticator);

        if (!string.IsNullOrEmpty(_serviceUrl))
        {
            _service.SetServiceUrl(_serviceUrl);
        }

        Active = true;
        StartRecording();
    }

    public bool Active
    {
        get { return _service.IsListening; }
        set
        {
            if (value && !_service.IsListening)
            {
                _service.RecognizeModel = (string.IsNullOrEmpty(_recognizeModel) ? "en-GB_BroadbandModel" : _recognizeModel);
                _service.DetectSilence = true;
                _service.EnableWordConfidence = true;
                _service.SilenceThreshold = 0.01f;
                _service.MaxAlternatives = 1;
                _service.EnableInterimResults = true;
                _service.OnError = OnError;
                _service.InactivityTimeout = -1;
                _service.WordAlternativesThreshold = null;
                _service.EndOfPhraseSilenceTime = null;
                _service.StartListening(OnRecognize, OnRecognizeSpeaker);
            }
            else if (!value && _service.IsListening)
            {
                _service.StopListening();
            } 
        }
    }

    private void StartRecording()
    {
        if (_recordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            _recordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    private void StopRecording()
    {
        if (_recordingRoutine != 0)
        {
            Microphone.End(_microphoneID);
            Runnable.Stop(_recordingRoutine);
            _recordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;
        //Log.Debug("SpeechToTextScript.OnError()","Error!{0}",error);
    }

    private IEnumerator RecordingHandler()
    {
        // print used microphone, if any
        //Log.Debug("SpeechToTextScript.RecordingHandler()","devices: {0}",Microphone.devices);
        _recording = Microphone.Start(_microphoneID,true,_recordingBufferSize,_recordingHZ);
        yield return null;

        if (_recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = _recording.samples / 2;
        float[] samples = null;

        while (_recordingRoutine != 0 && _recording != null)
        {
            int writePos = Microphone.GetPosition(_microphoneID);
            if (writePos > _recording.samples || !Microphone.IsRecording(_microphoneID))
            {
                Log.Error("SpeechToTextScript.RecordingHandler()","Microphone Disconnected");
                StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint) || (!bFirstBlock && writePos < midPoint))
            {
                samples = new float[midPoint];
                _recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)),Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                record.Clip.SetData(samples, 0);

                _service.OnListen(record);
                bFirstBlock = !bFirstBlock;
            }
            else
            {
                int remaining = bFirstBlock ? (midPoint - writePos) : (_recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)_recordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }
        }
        yield break;
    }

    private void OnRecognize(SpeechRecognitionEvent result)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    string text = string.Format("{0}",alt.transcript);
                    _finalTranscript = text;
                }
            }
        }
    }

    private void OnRecognizeSpeaker(SpeakerRecognitionEvent result)
    {

    }

    public string GetFinalTranscript()
    {
        return _finalTranscript;
    }
    
}
