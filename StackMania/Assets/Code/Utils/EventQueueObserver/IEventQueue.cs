﻿using System;

public interface IEventQueue
{
    void Subscribe(EventIds eventId, IEventObserver eventObserver);
    void Unsubscribe(EventIds eventId, IEventObserver eventObserver);
    void EnqueueEvent(EventData eventData);
}

