using System;
using FrogDataLib.DataManagement;

namespace StarterKit;

/*
  ALWAYS include [Serializable] on your derived class(es) if
  they will be added to the save. Make sure to follow all the
  rules for unity serialization, IE properties WILL NOT serialize
  only fields and fields should be public. Dictionaries are also
  not valid with unity serialization so you'll have to decompose
  those into arrays or another intermediary
*/
[Serializable]
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
  */
  public string Name = "Test";
  public int Value = 100;
}
