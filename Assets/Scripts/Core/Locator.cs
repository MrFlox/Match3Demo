using System;
using System.Collections.Generic;

namespace Core
{
  public static class Locator
  {
    public static Dictionary<string, IService> _services = new();

    public static void Register<T>(T service) where T : IService
    {
      string key = typeof(T).Name;

      if (_services.ContainsKey(key))
      {
        throw new Exception($"Already has this {key}");
      }
      _services.Add(key, service);
    }
    public static void Unregister<T>()
    {
        _services.Remove(typeof(T).Name);
    }

    public static T Get<T>() where T: class, IService
    {
      return _services[typeof(T).Name] as T;
    }
  }
}