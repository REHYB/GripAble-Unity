# Gripable Protos

## Introduction 

Gripable Protos use [Google Protobuf](https://developers.google.com/protocol-buffers) library which we use for generating C#, Java and Dart classes.


## Building locally

To build protos locally use 
```
   ./gradlew build
```

This will generate files in `./build/generated/source/proto/release/` folder.

## Releasing 

**Note:** We treat Gripable Protos uniquely, we don't follow the git-flow and we don't use SNAPSHOTs therefore whenever we make a change that needs to be consumed by any other projects we create a new release.

### Create a release 

Before creating a release you must follow these steps:

 - Ensure all documentation (`release-notes.md`) is up to date.
 - Edit `gradle.properties` (bump `ARTIFACT_VERSION`). We do follow [semantic versioning](https://semver.org/)
 - Edit `package.json` (bump `version` and update `gitTagMsg`)
 - Edit `dart/pubspec.yaml` (bump `version` to `x.y.z+dart`)
 - Commit and push to `develop` branch

Open a PR from `develop` to `master` when you feel like you are done. After the merge of the PR into master, the CI will run for ~6 minutes to generate all versions of the package.

The CI will release 3 kinds of Protobuf:

 - It creates an AAR directly into Maven
 - It creates a commit on UPM branch amd tags it the version `X.Y.Z` 
 - It creates a release by tagging a commit on master branch release with `X.Y.Z+dart`

## Setup 

### Setup for Java

First make sure your project is set up with [gripable maven](https://bitbucket.org/gripable/maven/src/releases/)

Include Java package in your project's `build.gradle` file:

```
dependencies {
    implementation "com.gripable.protos:protos:X.X.X"
    ...
}
```

where `X.X.X` represents the latest version (tag) of this repo.

### Setup for Unity

Include Unity package editing `Packages/manifest.json` adding the following line:

```json
"com.gripable.gripableprotosunity": "ssh://git@bitbucket.org/gripable/gripable_protos.git#X.X.X
```

where `X.X.X` represents the latest version of this repo.

### Setup for Dart

Released Dart protobufs are located in `dart` folder. Released tag versions are suffixed with `+dart`. Locate the package as follows

```yaml
dependencies:
  ...
  gripable_protos:
    url: https://bitbucket.org/gripable/gripable_protos.git
    ref: X.X.X+dart
```

## Usage 

### Add a new proto

Protos are located in a `src/main/proto` folder. An example of a proto file, i.e. `User.proto`

```proto

// These options are mandatory.

syntax = "proto3";

package com.gripable.protos;

option java_package = "com.gripable.protos";
option java_multiple_files = true;

option csharp_namespace = "Protos";

enum Handedness {
   RIGHT_HAND = 1;
   LEFT_HAND = 2;
   AMBIDEXTROUS = 3;
}

message User {
   int64 id = 1; 
   string userName = 2;
   int32 age = 3;
   double weight = 4;
   bool admin = 5;
   Handedness dominantHand = 6;
   // ... 
}
```

 - For detailed reference of Protobuf see [reference guide](https://developers.google.com/protocol-buffers/docs/proto3)
 - Use CamelCase for fields. 

## Commands

A list of helpful commands

- Clean `./gradlew clean`
- Build `./gradlew build`
- Assemble Snapshot (Force `-SNAPSHOT`): `./gradlew assembleSnapshot`
- Upload Archive `./gradlew uploadArchives`
- Compares the build version between the latest git tag and version specified in `gradle.properties`: `./gradlew verifyBuildVersion`
- Copy C# generated files into the proper place for Unity build: `./gradlew copyCSharpToUnityProject`
- Copy Dart generated files into the proper place for : `./gradlew copyDartProtosToPackage`
  If something goes wrong, rerun with added`--info`, or `--stacktrace` flag.

```
 ./gradlew clean assembleSnapshot verifyBuildVersion uploadArchives
```

**Note**: If you want to build a SNAPSHOT, please bear in mind that you have to run `assembleSnapshot` command.
Generated files are located in `./build/generated/source/proto/release/`

## CI/CD

### Docker images

There are two Dockerfiles in this repository, named `Dockerfile` and `unity.Dockerfile`, both are using custom Gripable images.

The first one is full Android SDK + Dart environment for Java and Dart, second one is Unity specific.

```
docker build -t gripable_protos -f ./Dockerfile . \
 && docker run --rm -it gripable_protos
```

### Gripable images

You can find Gripable's custom Dockerfiles in [ci-tools repository](https://bitbucket.org/gripable/ci-tools).
