// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: GripData.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protos {

  /// <summary>Holder for reflection information generated from GripData.proto</summary>
  public static partial class GripDataReflection {

    #region Descriptor
    /// <summary>File descriptor for GripData.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GripDataReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5HcmlwRGF0YS5wcm90bxITY29tLmdyaXBhYmxlLnByb3RvcyJMCghHcmlw",
            "RGF0YRIXCg9zZW5zb3JUaW1lc3RhbXAYASABKAUSDQoFZm9yY2UYAiABKAIS",
            "GAoQY2VudGVyT2ZQcmVzc3VyZRgDIAEoAkIgChNjb20uZ3JpcGFibGUucHJv",
            "dG9zUAGqAgZQcm90b3NiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Protos.GripData), global::Protos.GripData.Parser, new[]{ "SensorTimestamp", "Force", "CenterOfPressure" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GripData : pb::IMessage<GripData> {
    private static readonly pb::MessageParser<GripData> _parser = new pb::MessageParser<GripData>(() => new GripData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<GripData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Protos.GripDataReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GripData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GripData(GripData other) : this() {
      sensorTimestamp_ = other.sensorTimestamp_;
      force_ = other.force_;
      centerOfPressure_ = other.centerOfPressure_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public GripData Clone() {
      return new GripData(this);
    }

    /// <summary>Field number for the "sensorTimestamp" field.</summary>
    public const int SensorTimestampFieldNumber = 1;
    private int sensorTimestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SensorTimestamp {
      get { return sensorTimestamp_; }
      set {
        sensorTimestamp_ = value;
      }
    }

    /// <summary>Field number for the "force" field.</summary>
    public const int ForceFieldNumber = 2;
    private float force_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Force {
      get { return force_; }
      set {
        force_ = value;
      }
    }

    /// <summary>Field number for the "centerOfPressure" field.</summary>
    public const int CenterOfPressureFieldNumber = 3;
    private float centerOfPressure_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float CenterOfPressure {
      get { return centerOfPressure_; }
      set {
        centerOfPressure_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as GripData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(GripData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SensorTimestamp != other.SensorTimestamp) return false;
      if (Force != other.Force) return false;
      if (CenterOfPressure != other.CenterOfPressure) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SensorTimestamp != 0) hash ^= SensorTimestamp.GetHashCode();
      if (Force != 0F) hash ^= Force.GetHashCode();
      if (CenterOfPressure != 0F) hash ^= CenterOfPressure.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (SensorTimestamp != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(SensorTimestamp);
      }
      if (Force != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Force);
      }
      if (CenterOfPressure != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(CenterOfPressure);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SensorTimestamp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SensorTimestamp);
      }
      if (Force != 0F) {
        size += 1 + 4;
      }
      if (CenterOfPressure != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(GripData other) {
      if (other == null) {
        return;
      }
      if (other.SensorTimestamp != 0) {
        SensorTimestamp = other.SensorTimestamp;
      }
      if (other.Force != 0F) {
        Force = other.Force;
      }
      if (other.CenterOfPressure != 0F) {
        CenterOfPressure = other.CenterOfPressure;
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
            SensorTimestamp = input.ReadInt32();
            break;
          }
          case 21: {
            Force = input.ReadFloat();
            break;
          }
          case 29: {
            CenterOfPressure = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
