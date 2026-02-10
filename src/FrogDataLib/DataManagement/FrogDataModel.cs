using System;
using UnityEngine;

namespace FrogDataLib.DataManagement;

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
