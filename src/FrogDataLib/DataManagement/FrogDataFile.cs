using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace FrogDataLib.DataManagement;

[Serializable]
public class FrogDataFile(Dictionary<string, string> input) : FrogDataModel
{

  [SerializeField]
  public string[] Keys = [.. input.Keys];

  [SerializeField]
  public string[] Values = [.. input.Values];

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
