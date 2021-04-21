using System;
using System.IO;

namespace HoLib.Helpers
{
    public static class Utils
    {
        private const uint DKDRSeed = 131;

        public static T[] CreateArray<T>(int length, params object[] args)
        {
            var array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = (T)Activator.CreateInstance(typeof(T), args);

            return array;
        }

        public static uint DKDRHash(string value)
        {
            value = value.ToUpper();
            
            var hash = 0u;
            for (var i = 0; i < value.Length; i++)
                hash = (hash * DKDRSeed) + value[i];

            return hash;
        }

        public static string CreateDirectory(params string[] paths)
        {
            var di = new DirectoryInfo(Path.Combine(paths));

            if (!di.Exists && di.Parent != null)
                di.Create();

            return di.FullName;
        }

        public static string GenerateUniqueFileName(string path)
        {
            var extindex = path.LastIndexOf('.');
            var newpath = path;

            for (var i = 1; File.Exists(newpath); i++)
                newpath = path.Insert(extindex, $".{i}");

            return newpath;
        }
    }
}
