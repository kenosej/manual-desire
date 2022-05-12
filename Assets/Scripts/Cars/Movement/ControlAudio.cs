using System;
using System.Collections;
using UnityEngine;

namespace Cars.Movement
{
    public class ControlAudio : MonoBehaviour
    {
        private enum AudioEnum
        {
            CanTurnOn,
            CanTurnOff,
            CanIdle,
            CanLowOn,
            CanLowOff,
            CanMedOn,
            CanMedOff,
            CanHighOn,
            CanHighOff,
            CanMaxRPM
        };
        
        private bool[] _audioStates = new bool[10];

        private ParentControl _pC;
        private AudioSource[] _as;
        private string _path = "Audio/Cars/FirstCar";

        private void Awake()
        {
            _as = GetComponents<AudioSource>();
            _pC = GetComponentInParent<ParentControl>();

            FillStartingAudioStates();
            _as[0].loop = false;
            _as[1].loop = _as[2].loop = true;
        }

        private void FillStartingAudioStates()
        {
            Array.Fill(_audioStates, true);
            _audioStates[(int)AudioEnum.CanTurnOff] = false;
        }

        private void Update()
        {
            TurningOn();
            TurningOff();
            PlayVariants();
            Debug.Log($"active is {(_as[1].isPlaying ? "1" : "2")}");
        }
        
        private void TurningOn()
        {
            if (!_pC.IsTurnedOn || !_audioStates[(int)AudioEnum.CanTurnOn]) return;
            
            PlayTurningOnAndOffAudio("startup");
            _audioStates[(int)AudioEnum.CanTurnOn] = false;
            _audioStates[(int)AudioEnum.CanTurnOff] = true;
        }

        private void TurningOff()
        {
            if (_pC.IsTurnedOn || !_audioStates[(int)AudioEnum.CanTurnOff]) return;
            
            PlayTurningOnAndOffAudio("shutoff");
            _audioStates[(int)AudioEnum.CanTurnOff] = false;
            _audioStates[(int)AudioEnum.CanTurnOn] = true;
        }

        private void PlayVariants()
        {
            PlayIdle();
            PlayLows();
            PlayMeds();
            PlayHighs();
            PlayMaxRPMs();
        }

        private void PlayMaxRPMs()
        {
            if (!_pC.IsTurnedOn || _pC.RPMNeedle01Position < 0.9f) return;
            
            PlayVariantsAudio("maxRPM");
            ResetAllStates(AudioEnum.CanMaxRPM);
        }

        private void PlayHighs()
        {
            if (!_pC.IsTurnedOn || _pC.RPMNeedle01Position < 0.8f || _pC.RPMNeedle01Position > 0.9f) return;
            
            if (_pC.RPMsAreIncreasing)
            {
                PlayVariantsAudio("high_on");
                ResetAllStates(AudioEnum.CanHighOn);
                return;
            }
            
            PlayVariantsAudio("high_off");
            ResetAllStates(AudioEnum.CanHighOff);
        }

        private void PlayMeds()
        {
            if (!_pC.IsTurnedOn || _pC.RPMNeedle01Position < 0.4f || _pC.RPMNeedle01Position > 0.8f) return;
            
            if (_pC.RPMsAreIncreasing)
            {
                PlayVariantsAudio("med_on");
                ResetAllStates(AudioEnum.CanMedOn);
                return;
            }
            
            PlayVariantsAudio("med_off");
            ResetAllStates(AudioEnum.CanMedOff);
        }

        private void PlayLows()
        {
            if (!_pC.IsTurnedOn || _pC.RPMNeedle01Position < 0.01f || _pC.RPMNeedle01Position > 0.4f) return;
            
            if (_pC.RPMsAreIncreasing)
            {
                PlayVariantsAudio("low_on");
                ResetAllStates(AudioEnum.CanLowOn);
                return;
            }
            
            PlayVariantsAudio("low_off");
            ResetAllStates(AudioEnum.CanLowOff);
        }

        private void PlayIdle()
        {
            if (!_pC.IsTurnedOn || _pC.RPMNeedle01Position > 0.01f) return;

            PlayVariantsAudio("idle");
            ResetAllStates(AudioEnum.CanIdle);
        }

        private void ResetAllStates(AudioEnum except)
        {
            for (int i = 2; i < _audioStates.Length; i++)
            {
                if ((int)except == i)
                {
                    _audioStates[i] = false;
                    continue;
                }

                _audioStates[i] = true;
            }
        }

        private void PlayTurningOnAndOffAudio(in string clipName)
        {
            _as[1].Stop();
            _as[2].Stop();
            
            _as[0].clip = Resources.Load<AudioClip>($"{_path}/{clipName}");
            _as[0].Play();
        }

        private void PlayVariantsAudio(in string clipName)
        {
            if (_as[0].isPlaying) return;
            
            var clip = Resources.Load<AudioClip>($"{_path}/{clipName}");

            if (_as[1].isPlaying)
            {
                if (_as[1].clip == clip) return;
                
                _as[2].clip = clip;
                
                StartCoroutine(CrossFade(false, 0.5f));
            }
            else
            {
                if (_as[2].clip == clip) return;
                
                _as[1].clip = clip;
                
                StartCoroutine(CrossFade(true, 0.5f));
            }
        }

        private IEnumerator CrossFade(bool increaseIndexOneTrack, float duration)
        {
            var currentTime = 0f;

            if (increaseIndexOneTrack)
                _as[1].Play();
            else
                _as[2].Play();

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;

                if (increaseIndexOneTrack)
                {
                    _as[1].volume = Mathf.Lerp(0, 1, currentTime / duration);
                    _as[2].volume = Mathf.Lerp(1, 0, currentTime / duration);
                }
                else
                {
                    _as[2].volume = Mathf.Lerp(0, 1, currentTime / duration);
                    _as[1].volume = Mathf.Lerp(1, 0, currentTime / duration);
                }
                
                yield return null;
            }
            
            if (increaseIndexOneTrack)
                _as[2].Stop();
            else
                _as[1].Stop();
        }

    }
}