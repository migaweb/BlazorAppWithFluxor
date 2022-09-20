namespace BlazorAppWithFluxor.Client.Features.TrafficLight.Store;

public class TrafficLightState
{
  public bool RedOn { get; }
  public bool AmberOn { get; }
  public bool GreenOn { get; }

  public TrafficLightStateEnum CurrentState { get; set; }

  public TrafficLightState(bool redOn, bool amberOn, bool greenOn)
  {
    RedOn = redOn;
    AmberOn = amberOn;
    GreenOn = greenOn;
  }

  public TrafficLightState Toggle()
  {
    CurrentState = CurrentState switch
    {
      TrafficLightStateEnum.Stop => TrafficLightStateEnum.GetReadyToGo,
      TrafficLightStateEnum.GetReadyToGo => TrafficLightStateEnum.Go,
      TrafficLightStateEnum.Go => TrafficLightStateEnum.GetReadyToStop,
      TrafficLightStateEnum.GetReadyToStop => TrafficLightStateEnum.Stop,
      _ => CurrentState
    };

    return Resolve(CurrentState);
  }

  private TrafficLightState Resolve(TrafficLightStateEnum state)
  {
    var newState = state switch
    {
      TrafficLightStateEnum.Stop => new TrafficLightState(true, false, false),
      TrafficLightStateEnum.GetReadyToGo => new TrafficLightState(true, true, false),
      TrafficLightStateEnum.Go => new TrafficLightState(false, false, true),
      TrafficLightStateEnum.GetReadyToStop => new TrafficLightState(false, true, true),
      _ => new TrafficLightState(true, false, false),
    };

    newState.CurrentState = state;

    return newState;
  }
}
