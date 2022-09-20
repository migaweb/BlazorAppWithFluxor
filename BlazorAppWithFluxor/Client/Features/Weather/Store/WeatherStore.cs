using BlazorAppWithFluxor.Client.Features.Counter.Store;
using BlazorAppWithFluxor.Shared;
using Fluxor;
using System.Net.Http.Json;

namespace BlazorAppWithFluxor.Client.Features.Weather.Store
{
  // State
  public record WeatherState
  {
    public bool Initialized { get; init; }
    public bool Loading { get; init; }
    public WeatherForecast[]? Forecasts { get; init; }
  }

  // Feature
  public class WeatherFeature : Feature<WeatherState>
  {
    public override string GetName() => "Weather";

    protected override WeatherState GetInitialState()
    {
      return new WeatherState
      {
        Initialized = false,
        Loading = false,
        Forecasts = Array.Empty<WeatherForecast>()
      };
    }
  }

  // Actions
  public class WeatherSetInitializedAction { }
  public class WeatherLoadForecastsAction { }

  public class WeatherSetForecastsAction
  {
    public WeatherForecast[] Forecasts { get; }

    public WeatherSetForecastsAction(WeatherForecast[] forecasts)
    {
      Forecasts = forecasts;
    }
  }

  public class WeatherSetLoadingAction
  {
    public bool Loading { get; }

    public WeatherSetLoadingAction(bool loading)
    {
      Loading = loading;
    }
  }

  // Reducers
  public static class WeatherReducers
  {
    [ReducerMethod]
    public static WeatherState OnSetForecasts(WeatherState state, WeatherSetForecastsAction action)
    {
      return state with
      {
        Forecasts = action.Forecasts
      };
    }

    [ReducerMethod]
    public static WeatherState OnSetLoading(WeatherState state, WeatherSetLoadingAction action)
    {
      return state with
      {
        Loading = action.Loading
      };
    }

    [ReducerMethod(typeof(WeatherSetInitializedAction))]
    public static WeatherState OnSetInitialized(WeatherState state)
    {
      return state with
      {
        Initialized = true
      };
    }
  }

  //Effects
  public class WeatherEffects
  {
    private readonly HttpClient _http;
    private readonly IState<CounterState> _counterState;

    public WeatherEffects(HttpClient http, IState<CounterState> counterState)
    {
      _http = http;
      _counterState = counterState;
    }

    [EffectMethod(typeof(CounterIncrementAction))]
    public async Task LoadForecastsOnIncrement(IDispatcher dispatcher)
    {
      // every tenth increment
      if (_counterState.Value.CurrentCount % 10 == 0)
      {
        dispatcher.Dispatch(new WeatherLoadForecastsAction());
      }

      await Task.CompletedTask;
    }

    [EffectMethod(typeof(WeatherLoadForecastsAction))]
    public async Task LoadForecasts(IDispatcher dispatcher)
    {
      dispatcher.Dispatch(new WeatherSetLoadingAction(true));
      await Task.Delay(1000);
      var forecasts = await _http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast") 
        ?? Array.Empty<WeatherForecast>();
      dispatcher.Dispatch(new WeatherSetForecastsAction(forecasts));
      dispatcher.Dispatch(new WeatherSetLoadingAction(false));
    }
  }

}
