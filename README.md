# FrogDataLib for YAPYAP

**FrogDataLib** is a lightweight data persistence framework for YAPYAP mods. It allows developers to save and load custom mod data into "sidecar" files, preventing save game pollution and ensuring mod data stays synchronized with the user's active save slot.

## Features

- **Zero Pollution:** Mod data is stored in `%AppData%/LocalLow/maisonbap/YAPYAP/FrogData/`, keeping vanilla saves clean.
- **Slot Synchronization:** Automatically handles saving, loading, and deleting data based on the user's active save slot.
- **Type Safety:** Uses a generic `FrogDataContainer<T>` pattern for easy serialization.
- **Corrupt-Data Protection:** Includes a "Sentinel" check to detect failed Unity JSON deserialization.
- **Two-Tier Deserialization:** Internally stores mod saves as strings and serializes them just in time to avoid TypeLoadExceptions when mods are modified on an active save (not recommended!)

## For Developers: How to Use

See the full [Example Project](https://github.com/RobynLlama/FrogDataLib/blob/main/src/StarterKit/StarterKitPlugin.cs) for more in-depth usage tips!

### Define your Data Model

Your data class must inherit from `FrogDataModel` and be marked with the `[Serializable]` attribute.

```csharp
using FrogDataLib.DataManagement;
using System;

[Serializable]
public class MyModData : FrogDataModel
{
    public int PlayerKills;
    public string FavoriteFrogName;
}

```

### Initialize the Container

Create a container using a unique GUID (usually your mod's ID).

```csharp
private FrogDataContainer<MyModData> _dataContainer;

void Awake()
{
    _dataContainer = new FrogDataContainer<MyModData>("com.yourname.mymod");
    
    // Subscribe to FrogDataLib events
    FrogDataManager.OnBeginSaving += SaveMyData;
    FrogDataManager.OnLoadCompleted += LoadMyData;
}

```

### Use Your Object Like Normal

```csharp
myData.FavoriteFrogName = "Gertrude";
```

### Saving and Loading

Use the provided events to stay in sync with the game session.

```csharp
// Called when the user clicks "Save" in-game
private void SaveMyData() =>
  _dataContainer.SaveModData(myData);

// Called when a save slot is finished loading
private void LoadMyData()
{
    MyModData data = _dataContainer.GetModData();
    Debug.Log($"Loaded: {data.FavoriteFrogName}");
}

```

## Important Notes

- **The Frog Sentinel:** `FrogDataLib` uses an internal integer (`8675309`) to verify that Unity's `JsonUtility` didn't silently fail. If your model isn't marked `[Serializable]`, the sentinel will be `0`, and the frogs will refuse to save/load to prevent data loss.
- **Session Cleanup:** Use the `OnSessionEnded` event to clear your local variables when a user returns to the Main Menu.
