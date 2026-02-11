using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FrogDataLib.DataManagement;

/// <summary>
/// A decomposed representation of the MasterData dictionary in a way
/// that unity won't scream about. This is not intended for use but
/// it must be public as far as I understand things
/// </summary>
/// <param name="input">the dictionary</param>
[Serializable]
public class FrogDataFile(Dictionary<string, string> input) : FrogDataModel
{

  /// <summary>
  /// Keys from the dict
  /// </summary>
  [SerializeField]
  public string[] Keys = [.. input.Keys];

  /// <summary>
  /// Values from the dict
  /// </summary>
  [SerializeField]
  public string[] Values = [.. input.Values];

  /// <summary>
  /// Attempts to parts the Keys and Values arrays back into a dictionary
  /// </summary>
  /// <param name="value">The completed dictionary (only on success)</param>
  /// <returns>TRUE on success, FALSE otherwise</returns>
  public bool TryParse([NotNullWhen(true)] out Dictionary<string, string>? value)
  {
    value = null;

    if (Keys.Length != Values.Length)
      return false;

    value = [];

    for (int i = 0; i < Keys.Length; i++)
      value[Keys[i]] = Values[i];

    return true;
  }
}
