// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Legacy Member", Scope = "member", Target = "~F:FrogDataLib.DataManagement.FrogDataModel.FrogSentinel")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Newtonsoft requires this to be instance scoped", Scope = "member", Target = "~M:FrogDataLib.DataManagement.FrogDataModel.ShouldSerializeFrogSentinel~System.Boolean")]
