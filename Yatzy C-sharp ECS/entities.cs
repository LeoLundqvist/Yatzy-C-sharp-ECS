using System;
using System.Collections.Generic;

namespace Entities
{ 
    // Represents an entity in the ECS (Entity-Component-System) architecture.
    public class Entity
    {
        // Unique identifier for the entity.
        public int Id { get; }

        // Dictionary to store components, with component type as key.
        private Dictionary<Type, object> components = new Dictionary<Type, object>();

        // Constructor to initialize the entity with an ID.
        public Entity(int id)
        {
            Id = id;
        }

        // Adds a component of type T to the entity.
        public void AddComponent<T>(T component)
        {
            components[typeof(T)] = component;
        }

        // Retrieves a component of type T from the entity.
        public T GetComponent<T>() where T : class
        {
            // Try to get the component from the dictionary.
            if (components.TryGetValue(typeof(T), out var component))
            {
                // If found, return the component casted to type T.
                return component as T;
            }
            // If not found, return null.
            return null;
        }
    }
}