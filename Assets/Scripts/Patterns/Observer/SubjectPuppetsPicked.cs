using System.Collections;
using System.Collections.Generic;
//using Patterns.Observer.Interfaces;
using UnityEngine;

namespace Components.UI
{
    public class SubjectPuppetsPicked : MonoBehaviour, ISubject<int>
    {
        [SerializeField] public int Puppets = 0;
        Collected textoCollected;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Puppet"))
            {
                Puppets++;
                NotifyObservers();
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Puppet"))
            {
                Puppets--;
                NotifyObservers();
            }
        }

        private List<IObserver<int>> _observers = new List<IObserver<int>>();

        public void AddObserver(IObserver<int> observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(IObserver<int> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver<int> observer in _observers)
            {
                observer?.UpdateObserver(Puppets);
                textoCollected.AddCollected(Puppets);
            }
        }
    }
}