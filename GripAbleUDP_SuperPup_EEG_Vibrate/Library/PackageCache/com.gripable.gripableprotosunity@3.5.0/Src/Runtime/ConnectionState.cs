// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ConnectionState.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protos {

  /// <summary>Holder for reflection information generated from ConnectionState.proto</summary>
  public static partial class ConnectionStateReflection {

    #region Descriptor
    /// <summary>File descriptor for ConnectionState.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ConnectionStateReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVDb25uZWN0aW9uU3RhdGUucHJvdG8SE2NvbS5ncmlwYWJsZS5wcm90b3Mq",
            "VQoPQ29ubmVjdGlvblN0YXRlEg4KCkNPTk5FQ1RJTkcQABINCglDT05ORUNU",
            "RUQQARIQCgxESVNDT05ORUNURUQQAhIRCg1ESVNDT05ORUNUSU5HEANCIAoT",
            "Y29tLmdyaXBhYmxlLnByb3Rvc1ABqgIGUHJvdG9zYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Protos.ConnectionState), }, null));
    }
    #endregion

  }
  #region Enums
  public enum ConnectionState {
    [pbr::OriginalName("CONNECTING")] Connecting = 0,
    [pbr::OriginalName("CONNECTED")] Connected = 1,
    [pbr::OriginalName("DISCONNECTED")] Disconnected = 2,
    [pbr::OriginalName("DISCONNECTING")] Disconnecting = 3,
  }

  #endregion

}

#endregion Designer generated code