// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: GestureType.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protos {

  /// <summary>Holder for reflection information generated from GestureType.proto</summary>
  public static partial class GestureTypeReflection {

    #region Descriptor
    /// <summary>File descriptor for GestureType.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GestureTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFHZXN0dXJlVHlwZS5wcm90bxITY29tLmdyaXBhYmxlLnByb3RvcyqhAQoL",
            "R2VzdHVyZVR5cGUSCwoHU1FVRUVaRRAAEgsKB1JFTEVBU0UQARILCgdGTEVY",
            "SU9OEAISDQoJRVhURU5TSU9OEAMSDQoJUFJPTkFUSU9OEAQSDgoKU1VQSU5B",
            "VElPThAFEgkKBVVMTkFSEAYSCgoGUkFESUFMEAcSEgoORE9VQkxFX1NRVUVF",
            "WkUQCBISCg5ET1VCTEVfUkVMRUFTRRAJQiAKE2NvbS5ncmlwYWJsZS5wcm90",
            "b3NQAaoCBlByb3Rvc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Protos.GestureType), }, null));
    }
    #endregion

  }
  #region Enums
  public enum GestureType {
    [pbr::OriginalName("SQUEEZE")] Squeeze = 0,
    [pbr::OriginalName("RELEASE")] Release = 1,
    [pbr::OriginalName("FLEXION")] Flexion = 2,
    [pbr::OriginalName("EXTENSION")] Extension = 3,
    [pbr::OriginalName("PRONATION")] Pronation = 4,
    [pbr::OriginalName("SUPINATION")] Supination = 5,
    [pbr::OriginalName("ULNAR")] Ulnar = 6,
    [pbr::OriginalName("RADIAL")] Radial = 7,
    [pbr::OriginalName("DOUBLE_SQUEEZE")] DoubleSqueeze = 8,
    [pbr::OriginalName("DOUBLE_RELEASE")] DoubleRelease = 9,
  }

  #endregion

}

#endregion Designer generated code
