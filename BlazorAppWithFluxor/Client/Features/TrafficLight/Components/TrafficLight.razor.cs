using BlazorAppWithFluxor.Client.Features.TrafficLight.Store;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorAppWithFluxor.Client.Features.TrafficLight.Components;

public partial class TrafficLight : FluxorComponent
{
  [Inject] public IState<TrafficLightState> State { get; set; } = null!;
  [Inject] public IDispatcher Dispatcher { get; set; } = null!;

  protected override void OnInitialized()
  {
    base.OnInitialized();
  }

  public void Toggle()
  {
    Dispatcher.Dispatch(new TrafficLightToggleAction());
  }
}
