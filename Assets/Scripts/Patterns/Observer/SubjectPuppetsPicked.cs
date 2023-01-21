using System.Collections;
using System.Collections.Generic;
//using Patterns.Observer.Interfaces;
using Components.GameManagement.Scores;
using UnityEngine;

namespace Patterns.Observer
{
    public class SubjectPuppetsPicked : MonoBehaviour, ISubject<int>
    {
        //[SerializeField] public int Puppets = 0;
        //PuppetCounter textPuppets;


        private List<IObserver<int>> _observers = new List<IObserver<int>>();

        public void AddObserver(IObserver<int> observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(IObserver<int> observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(int newPuppet)
        {
            foreach (IObserver<int> observer in _observers)
            {
                Debug.Log($"Notificando observador {observer}");
                observer?.UpdateObserver(newPuppet);
                //textPuppets.AddCollected(Puppets);
            }
        }
    }
}