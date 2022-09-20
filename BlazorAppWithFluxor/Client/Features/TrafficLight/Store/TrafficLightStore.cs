using BlazorAppWithFluxor.Client.Features.Counter.Store;
using Fluxor;

namespace BlazorAppWithFluxor.Client.Features.TrafficLight.Store;

public class TrafficLightFeature : Feature<TrafficLightState>
{
  public override string GetName() => "TrafficLight";

  protected override TrafficLightState GetInitialState()
  {
    return new TrafficLightState(true, false, false) { CurrentState = TrafficLightStateEnum.Stop };
  }
}

// Actions
public class TrafficLightToggleAction { }

// Reducers
public static class TrafficLightReducers
{
  [ReducerMethod(typeof(TrafficLightToggleAction))]
  public static TrafficLightState OnToggle(TrafficLightState state)
  {
    return state.Toggle();
  }
}
