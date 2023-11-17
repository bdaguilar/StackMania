﻿using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class ServiceLocator
{
	public static ServiceLocator Instance = _instance ?? (_instance = new ServiceLocator());
	public static ServiceLocator _instance;

	public readonly Dictionary<Type, object> _services;

    public ServiceLocator()
	{
		_services = new Dictionary<Type, object>();
	}

	public void RegisterService<T>(T service)
	{
		var type = typeof(T);
		Assert.IsFalse(_services.ContainsKey(type), $"Service {type} already registered");

		_services.Add(type, service);
	}

    public void UnregisterService<T>()
    {
        var type = typeof(T);
        Assert.IsTrue(_services.ContainsKey(type), $"Service {type} is not registered yet");

        _services.Remove(type);
    }

    public T GetService<T>()
	{
		var type = typeof(T);
		if(!_services.TryGetValue(type, out var service))
		{
			throw new Exception($"Service {type} not found");
		}

		return (T) service;
	}

	public bool Contains<T>()
	{
		var type = typeof(T);
		return _services.ContainsKey(type);
	}
}

