using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public static class NavigationManager
{
    public static Dictionary<string, Route> RouteInformation = new Dictionary<string, Route>()
    {
        {"World",new Route {RouteDescription = "The big bad world", CanTravel = true } },
        {"Cave", new Route {RouteDescription = "The deep dark cave", CanTravel = false } },
        {"Home", new Route {RouteDescription = "Home sweet home", CanTravel = true } },
        {"Kirkidw", new Route {RouteDescription = "The grand city of Kirkidw", CanTravel = true } },
        {"Shop", new Route { CanTravel = true } },
    };

    private static string PreviousLocation;
    public struct Route
    {
        public string RouteDescription;
        public bool CanTravel;
    }

    public static string GetRouteInfo(string destination)
    {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
    }

    public static bool CanNavigate(string destination)
    {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].CanTravel : false;
    }

    public static void NavigateTo(string destination)
    {
        PreviousLocation = SceneManager.GetActiveScene().name;
        if (destination == "Home")
        GameState.PlayerReturningHome = false;
        TransitionManager.Instance.LoadScene(destination);
    }

    public static void GoBack()
    {
        string backLocation = PreviousLocation;
        PreviousLocation= SceneManager.GetActiveScene().name;
        TransitionManager.Instance.LoadScene(backLocation);            
    }
}
