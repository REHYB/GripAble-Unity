// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: MovementProgress.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protos {

  /// <summary>Holder for reflection information generated from MovementProgress.proto</summary>
  public static partial class MovementProgressReflection {

    #region Descriptor
    /// <summary>File descriptor for MovementProgress.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MovementProgressReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZNb3ZlbWVudFByb2dyZXNzLnByb3RvEhNjb20uZ3JpcGFibGUucHJvdG9z",
            "GhJNb3ZlbWVudFR5cGUucHJvdG8iaQoQTW92ZW1lbnRQcm9ncmVzcxIvCgR0",
            "eXBlGAEgASgOMiEuY29tLmdyaXBhYmxlLnByb3Rvcy5Nb3ZlbWVudFR5cGUS",
            "EwoLcmVwZXRpdGlvbnMYAiABKAUSDwoHc2Vjb25kcxgDIAEoBUIgChNjb20u",
            "Z3JpcGFibGUucHJvdG9zUAGqAgZQcm90b3NiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Protos.MovementTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Protos.MovementProgress), global::Protos.MovementProgress.Parser, new[]{ "Type", "Repetitions", "Seconds" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MovementProgress : pb::IMessage<MovementProgress> {
    private static readonly pb::MessageParser<MovementProgress> _parser = new pb::MessageParser<MovementProgress>(() => new MovementProgress());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MovementProgress> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Protos.MovementProgressReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MovementProgress() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MovementProgress(MovementProgress other) : this() {
      type_ = other.type_;
      repetitions_ = other.repetitions_;
      seconds_ = other.seconds_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MovementProgress Clone() {
      return new MovementProgress(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Protos.MovementType type_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Protos.MovementType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "repetitions" field.</summary>
    public const int RepetitionsFieldNumber = 2;
    private int repetitions_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Repetitions {
      get { return repetitions_; }
      set {
        repetitions_ = value;
      }
    }

    /// <summary>Field number for the "seconds" field.</summary>
    public const int SecondsFieldNumber = 3;
    private int seconds_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Seconds {
      get { return seconds_; }
      set {
        seconds_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MovementProgress);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MovementProgress other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (Repetitions != other.Repetitions) return false;
      if (Seconds != other.Seconds) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != 0) hash ^= Type.GetHashCode();
      if (Repetitions != 0) hash ^= Repetitions.GetHashCode();
      if (Seconds != 0) hash ^= Seconds.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Type != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (Repetitions != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Repetitions);
      }
      if (Seconds != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Seconds);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (Repetitions != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Repetitions);
      }
      if (Seconds != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Seconds);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MovementProgress other) {
      if (other == null) {
        return;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.Repetitions != 0) {
        Repetitions = other.Repetitions;
      }
      if (other.Seconds != 0) {
        Seconds = other.Seconds;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            type_ = (global::Protos.MovementType) input.ReadEnum();
            break;
          }
          case 16: {
            Repetitions = input.ReadInt32();
            break;
          }
          case 24: {
            Seconds = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
