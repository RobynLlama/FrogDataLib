using System.Security.Cryptography;
using System.Text;
using Mirror;
using UnityEngine;

namespace FrogDataLib.DataManagement;

/// <summary>
/// A MostlyUniqueID that is only valid during the single
/// frame the game is saving or loading
/// </summary>
public readonly record struct AssetMUD
{
  /// <summary>
  /// The generated Identifier for this MUD
  /// </summary>
  public readonly string Identifier;

  /// <summary>
  /// Create a new MUD from a network behavior
  /// </summary>
  /// <param name="source"></param>
  public AssetMUD(NetworkBehaviour source)
  {
    source.gameObject.transform.GetPositionAndRotation(out var position, out var rotation);
    Identifier = $"{source.netIdentity.assetId}::{Vector3Approximate(position)}::{Vector3Approximate(rotation.eulerAngles)}";
  }

  /// <summary>
  /// Create a new MUD from a saved string
  /// </summary>
  /// <remarks>This is intended for creating MUDs from a serialized data source and is not checked for completeness</remarks>
  /// <param name="id"></param>
  public AssetMUD(string id) =>
    Identifier = id;

  /// <summary>
  /// Creates a SHA256 digest from this MUD
  /// </summary>
  /// <remarks>This is usually shorter than the MUD itself and much nicer for display</remarks>
  /// <returns></returns>
  public string GetDigest()
  {
    using var sha256 = SHA256.Create();
    byte[] buffer = Encoding.UTF8.GetBytes(Identifier);
    byte[] hash = sha256.ComputeHash(buffer);

    var sb = new StringBuilder();
    foreach (byte b in hash)
      sb.Append(b.ToString("X2"));

    return sb.ToString();
  }

  /// <summary>
  /// Creates an approximate string representation of a Vector3
  /// </summary>
  /// <param name="input"></param>
  /// <returns></returns>
  public static string Vector3Approximate(Vector3 input) =>
    $"{input.x:F3},{input.y:F3},{input.z:F3}";
}
