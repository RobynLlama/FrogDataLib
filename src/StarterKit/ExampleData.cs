using System;
using FrogDataLib.DataManagement;

namespace StarterKit;

public class ExampleData : FrogDataModel
{
  /*
    All classes used as FrogDataModels must be capable of meeting the
    new() constraint which means they can be created without a
    constructor. Do not define a constructor, it will not be called

    Make certain to assign reasonable defaults to all data you will
    use here because it will be populated just like this when returned
    from a save with no previous data. Consider this your blank slate
    or default state!

    If you must modify or hydrate data you can use the Callbacks
    provided in FrogDataModel such as OnAfterSerialize, etc
  */
  public string Name = "Test";
  public int Value = 100;
}
