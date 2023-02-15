using System;
namespace CSharpDemos.ClassLibrary.DesignPatterns.StatePattern
{
    /*
     * The State design pattern is a behavioral design pattern that allows an object to change its behavior based on its internal state. 
     * In this pattern, the object's behavior is determined by its current state, which can be changed dynamically by the object or its collaborators. 
     * The pattern separates the responsibilities of an object into separate classes, each representing a different state, 
     * and delegates the behavior of the object to the appropriate state class.
     * 
     * Suppose we have a traffic light system that can be in one of three states: "Green", "Yellow", and "Red". Depending on the state of the traffic light, 
     * different actions should be taken by the cars and pedestrians.
     * 
     * We can define an interface ITrafficLightState that represents the different states of the traffic light, and implement it with concrete classes for each state.
     * 
     * The TrafficLight class is responsible for managing the state of the traffic light and delegating behavior to the appropriate state class.
     * 
     * In this example, when the traffic light is first created, it starts in the "Green" state. When the Handle() method is called on the traffic light, 
     * it delegates the behavior to the current state. Depending on the state, the traffic light may transition to a different state by calling SetState(). 
     * The behavior of the traffic light changes dynamically based on its internal state.
     */

    public class InvokeStatePattern : IInvokeMethod
    {
        public void InvokeMethod()
        {
            TrafficLight light = new TrafficLight();    // state is set to GreenState
            light.Handle();                             // state is set to YellowState
            light.Handle();                             // state is set to RedState
        }
    }

    public interface ITrafficLightState
    {
        void Handle(TrafficLight trafficLight);
    }

    public class GreenState : ITrafficLightState
    {
        public void Handle(TrafficLight trafficLight) => trafficLight.SetState(new YellowState());  // Allow cars to pass and pedestrians to cross
    }

    public class YellowState : ITrafficLightState
    {
        public void Handle(TrafficLight trafficLight) => trafficLight.SetState(new RedState());     // Warn cars to slow down and prepare to stop
    }

    public class RedState : ITrafficLightState
    {
        public void Handle(TrafficLight trafficLight) => trafficLight.SetState(new GreenState());   // Allow pedestrians to cross and wait for next cycle
    }

    public class TrafficLight
    {
        private ITrafficLightState _state;

        public TrafficLight() => _state = new GreenState();
        public void SetState(ITrafficLightState state) => _state = state;
        public void Handle() => _state.Handle(this);
    }
}

