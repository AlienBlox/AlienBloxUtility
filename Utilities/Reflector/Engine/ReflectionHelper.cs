using System;

namespace AlienBloxUtility.Utilities.Reflector.Engine
{
    /// <summary>
    /// A centralized location for AlienBlox's Reflection Engine
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Get the Type of this object with caching
        /// </summary>
        /// <param name="obj">The object to get</param>
        /// <returns>The Type</returns>
        public static Type GetTypeWithCache(this object obj)
        {
            ReflectionCache.Cache ??= [];

            if (!ReflectionCache.Cache.Contains(obj.GetType()))
                ReflectionCache.Cache.Add(obj.GetType());

            return obj.GetType();
        }

        /// <summary>
        /// Searches for a type with the specified name in the reflection cache.
        /// </summary>
        /// <remarks>This method searches only the types currently loaded in the reflection cache. It does
        /// not perform a full assembly scan.</remarks>
        /// <param name="typeName">The name of the type to locate. The search is case-sensitive and matches the type's simple name.</param>
        /// <returns>A <see cref="Type"/> object representing the type with the specified name, or <see langword="null"/> if no
        /// matching type is found.</returns>
        public static Type FindType(string typeName)
        {
            return ReflectionCache.Cache.Find(x => x.Name == typeName);
        }
    }
}
