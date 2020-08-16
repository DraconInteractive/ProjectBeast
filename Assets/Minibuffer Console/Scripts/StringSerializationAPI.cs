/* Original code[1] Copyright (c) 2014 Jacob Dufault[2]
   Modified code Copyright (c) 2018 Shane Celis[3]
   Licensed under the MIT License[4]

   This comment generated by code-cite[5].

   [1]: https://github.com/jacobdufault/fullserializer
   [2]: https://github.com/jacobdufault
   [3]: http://twitter.com/shanecelis
   [4]: https://opensource.org/licenses/MIT
   [5]: https://github.com/shanecelis/code-cite
*/
#if ! SH_MINIBUFFER_NOLIBS
#define FULL_SERIALIZER
#endif
using System;
#if FULL_SERIALIZER
using SeawispHunter.FullSerializer;
#endif
using System.Text.RegularExpressions;
namespace SeawispHunter.MinibufferConsole {

#if FULL_SERIALIZER
public static class StringSerializationAPI {
  private static readonly fsSerializer _serializer = new fsSerializer();

  public static string Serialize(Type type, object value) {
    // serialize the data
    fsData data;
    _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

    // emit the data via JSON
    //return fsJsonPrinter.CompressedJson(data);
    return fsJsonPrinter.PrettyJson(data);
  }

  public static object Deserialize(Type type, string serializedState) {
    // step 1: parse the JSON data
    fsData data = fsJsonParser.Parse(serializedState);

    // step 2: deserialize the data
    object deserialized = null;
    _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

    return deserialized;
  }

  public static T Deserialize<T>(string serializedState) {
    return (T) Deserialize(typeof(T), serializedState);
  }

  public static string Serialize<T>(T obj) {
    return Serialize(typeof(T), obj);
  }

  public static void Save<T>(string path, T obj) {
    System.IO.File.WriteAllText(PathName.instance.Expand(path),
                                StringSerializationAPI.Serialize(typeof(T),
                                                                 obj));
  }

  public static T Load<T>(string path) {
    var p = PathName.instance.Expand(path);
    if (System.IO.File.Exists(p)) {
      var content = System.IO.File.ReadAllText(p);
      // Hacking in the ability to add one-line comments to JSON.
      content = Regex.Replace(content, @"//.*$", "", RegexOptions.Multiline);
      return (T) StringSerializationAPI.Deserialize(typeof(T), content);
    }
    return default(T);
  }
}
#else
public static class StringSerializationAPI {

  public static string Serialize(Type type, object value) {
    return null;
  }

  public static object Deserialize(Type type, string serializedState) {
    return null;
  }
  public static T Deserialize<T>(string serializedState) {
    return default(T);
  }

  public static void Save<T>(string path, T obj) {
  }

  public static T Load<T>(string path) {
    return default(T);
  }
}
#endif
}

