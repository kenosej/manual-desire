using UnityEngine;

namespace Cars.Movement
{
    public class ControlAudio : MonoBehaviour
    {
        private ParentControl _pC;
        private AudioSource _as;
        private string path = "Audio/Cars/FirstCar";
        private bool _playStartupSound = true;
        private bool _playShutOffSound = false;

        private void Awake()
        {
            _pC = GetComponentInParent<ParentControl>();
            _as = GetComponent<AudioSource>();
        }

        private void Update()
        {
            PlayTurningOnOrOff();
            
            //if (_pC.IsTurnedOn && _pC.RPMNeedle01Position < 0.001f && !_as.isPlaying)
            //{
            //    _as.clip = Resources.Load<AudioClip>($"{path}/idle");
            //    //_as.Play();
            //    _as.PlayOneShot(_as.clip);
            //}

            //if (_pC.IsTurnedOn && _pC.RPMNeedle01Position > 0.001f && _pC.RPMNeedle01Position < 0.7f)
            //{
            //    _as.clip = Resources.Load<AudioClip>($"{path}/low_on");
            //    _as.PlayOneShot(_as.clip);
            //}
            //
            //if (_pC.IsTurnedOn && _pC.RPMNeedle01Position > 0.7f)
            //{
            //    _as.clip = Resources.Load<AudioClip>($"{path}/high_on");
            //    _as.PlayOneShot(_as.clip);
            //}
        }

        private void PlayTurningOnOrOff()
        {
            if (_pC.IsTurnedOn && _playStartupSound)
            {
                _as.clip = Resources.Load<AudioClip>($"{path}/startup");
                //_as.Play();
                _as.PlayOneShot(_as.clip);
                _playStartupSound = false;
                _playShutOffSound = true;
            }

            if (!_pC.IsTurnedOn && _playShutOffSound)
            {
                _as.clip = Resources.Load<AudioClip>($"{path}/shutoff");
                //_as.Play();
                _as.PlayOneShot(_as.clip);
                _playShutOffSound = false;
                _playStartupSound = true;
            }
        }
    }
}
