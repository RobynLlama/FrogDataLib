using System;
using UnityEngine;

namespace FrogDataLib.DataManagement;

/// <summary>
/// The base data model from which all save data must derive. This
/// is in order to have the sentinel value for integrity checking
/// </summary>
[Serializable]
public abstract class FrogDataModel
{
  /// <summary>
  /// This sentinel value allows me to detect if unity
  /// decided to silently emit an empty object when
  /// deserializing. Changing it serves no purpose but
  /// if it is ever set to 0 your data will become invalid
  /// and be discarded
  /// </summary>
  [SerializeField]
  public int _frogSentinel = 8675309;
}
