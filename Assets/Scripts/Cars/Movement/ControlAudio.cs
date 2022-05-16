using UnityEngine;
using System.Collections;

namespace Cars.Movement
{
    [RequireComponent(typeof(AudioSource))]
    public class ControlAudio : MonoBehaviour
    {
        private enum AudioEnum
        {
            CanTurnOn,
            CanTurnOff
        };
        
        private bool[] _audioStates = new bool[2];

        private const float MIN_VOL = 0.3f;

        private const float MIN_PITCH = 1f;
        private const float MAX_PITCH = 2.5f;
        
        private ParentControl _pC;
        private AudioSource[] _as;
        private string _path = "Audio/Cars/FirstCar";

        private void Awake()
        {
            _as = GetComponents<AudioSource>();
            _pC = GetComponent<ParentControl>();

            FillStartingAudioStates();
            SetAudioComponentsStates();
        }

        private void FillStartingAudioStates()
        {
            _audioStates[(int)AudioEnum.CanTurnOn] = true;
            _audioStates[(int)AudioEnum.CanTurnOff] = false;
        }

        private void SetAudioComponentsStates()
        {
            _as[0].loop = false;
            _as[1].loop = _as[2].loop = true;
            _as[0].volume = _as[1].volume = _as[2].volume = MIN_VOL;
        }

        private void Update()
        {
            if (_pC.IsPaused || _pC.IsCarDead)
            {
                foreach (var audioSource in _as)
                    audioSource.enabled = false;

                return;
            }
            
            foreach (var audioSource in _as)
                audioSource.enabled = true;
            
            TurningOn();
            TurningOff();
            PlayVariants();
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

        private void PlayTurningOnAndOffAudio(in string clipName)
        {
            _as[1].Stop();
            _as[2].Stop();
            
            _as[0].clip = Resources.Load<AudioClip>($"{_path}/{clipName}");
            _as[0].Play();
        }

        private void PlayVariants()
        {
            if (!_pC.IsTurnedOn) return;
            if (_as[0].isPlaying) return;
            if (_as[1].isPlaying && _as[2].isPlaying) return;

            var clip = Resources.Load<AudioClip>($"{_path}/{FindOutClipName()}");

            if (_as[1].isPlaying)
            {
                if (_as[1].clip == clip)
                {
                    _as[1].volume = CalcVolume();
                    _as[1].pitch = CalcPitch();
                    return;
                }
                
                _as[2].clip = clip;
                
                StartCoroutine(CrossFade(false, 0.5f, CalcVolume()));
            }
            else
            {
                if (_as[2].clip == clip)
                {
                    _as[2].volume = CalcVolume();
                    _as[2].pitch = CalcPitch();
                    return;
                }
                
                _as[1].clip = clip;
                
                StartCoroutine(CrossFade(true, 0.5f, CalcVolume()));
            }
        }

        private float CalcVolume()
        {
            return Mathf.Lerp(FindOutMinVolForGear(), FindOutMaxVolForGear(), _pC.RPMNeedle01Position);
        }

        private float FindOutMinVolForGear()
        {
            return MIN_VOL;
        }

        private float FindOutMaxVolForGear()
        {
            if (_pC.CurrentGear == ParentControl.GearsEnum.Neutral)
                return 0.5f;
            return 1f;
        }

        private float CalcPitch()
        {
            if (_pC.CurrentGear == ParentControl.GearsEnum.Neutral)
                return Mathf.Lerp(MIN_PITCH, MAX_PITCH, _pC.RPMNeedle01Position);
            
            var gear = _pC.CurrentGear == ParentControl.GearsEnum.Reverse 
                ? _pC.Car.GearReverse 
                : _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
            
            var delta = Mathf.Abs(MAX_PITCH - MIN_PITCH);
            var offset = MIN_PITCH + delta * gear.SoundPitchOffset;
            var endpoint = offset + delta * gear.SoundPitchAmount;
            
            return Mathf.Lerp(offset, endpoint, _pC.RPMNeedle01Position);
        }

        private string FindOutClipName()
        {
            string soundName;

            if (_pC.CurrentGear == ParentControl.GearsEnum.Neutral)
            {
                soundName = $"{_pC.Car.SoundFilename}_off";
            }
            else
            {
                var throttleExt = _pC.throttle ? "on" : "off";

                soundName = $"{_pC.Car.SoundFilename}_{throttleExt}";
            }

            return soundName;
        }

        private IEnumerator CrossFade(bool increaseIndexOneTrack, float duration, float toVolume)
        {
            toVolume = Mathf.Clamp01(toVolume);
            
            var currentTime = 0f;

            if (increaseIndexOneTrack)
            {
                _as[1].pitch = _as[2].pitch;
                _as[1].Play();
            }
            else
            {
                _as[2].pitch = _as[1].pitch;
                _as[2].Play();
            }

            var as1CurrVolume = _as[1].volume;
            var as2CurrVolume = _as[2].volume;
                
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                
                if (increaseIndexOneTrack)
                {
                    _as[1].volume = Mathf.Lerp(as1CurrVolume, toVolume, currentTime / duration);
                    _as[2].volume = Mathf.Lerp(as2CurrVolume, 0f, currentTime / duration);
                }
                else
                {
                    _as[2].volume = Mathf.Lerp(as2CurrVolume, toVolume, currentTime / duration);
                    _as[1].volume = Mathf.Lerp(as1CurrVolume, 0f, currentTime / duration);
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