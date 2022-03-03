# Released

## [3.5.0] - 26-03-2020

### Added

- Neutral value in Calibration proto

## [3.4.0] - 19-03-2020

### Added

- GameData proto 

## [3.3.0] - 19-03-2020

### Changed

- Moved LevelEventType to a separate file 

## [3.2.0] - 17-03-2020

### Changed

- Add START and STOP to ActivityEventType and LevelEventType

## [3.1.1] - 11-03-2020

### Changed

- Fix the CI of the Unity build

## [3.1.0] - 10-03-2020

### Added

- LevelEvent proto

## [3.0.1] - 27-02-2020

### Added

 - Copy Protobuf Lite Jar file into UPM package on build

## [3.0.0] - 19-02-2020

### Added

- `id`, `patientUid` and `updatedAt` in DailyProgress
- `patientUid` in Calibration

### Changed

- `strengthCollected` changed from float to double in DailyProgress
- renamed `timestamp` to `sensorTimestamp` in WristRpyData
- renamed `userId` to `patientUid` in ActivityConfig
- renamed `userName` to `username` in ActivityConfig
- renamed `userId` to `patientUid` in ActivityEvent

### Removed

- neutral gestures (i.e. `NEUTRAL_GRIP`, `NEUTRAL_ROLL`, `NEUTRAL_PITCH` and `NEUTRAL_YAW` from GestureType
- StringData proto
- SubscriptionConfig proto

## [2.0.3] - 06-02-2020

### Added 

- `strengthCollected` to DailyProgress and MovementProgress proto

## [2.0.2] - 06-02-2020

### Added 

- `DailyProgress` and `MovementProgress` proto
- Timestamp to `rawMotionData`

## [2.0.1] - 21-01-2020

### Fixed

- Added Protos namespace to Calibration.proto

## [2.0.0] - 21-01-2020

### Added 

- Gender.proto
- scaleFactor, calibration, gender and hand to ActivityConfig.proto

### Changed

- renamed MovementType.proto Enum values 

### Removed

- neutral from Calibration.proto

## [1.2.1] - 17-01-2020

### Added 

- launchedFromPackageName to ActivityConfig.proto

### Changed

- IntentData to ActivityConfig

## [1.2.0] - 17-01-2020

### Added 
 
 - Added IntentData.proto

## [1.1.9] - 15-01-2020

### Added 
 
 - `ProtosDependencies.xml` for Unity Jar Resolver  

## [1.1.7] - 14-01-2020

### Added
 
 - Java native `protos.jar` file  

## [1.1.6] - 13-01-2020

### Added 
 
 - Added missing Asmdef file removed by accident in the CI

## [1.1.2] - 08-01-2020

### Removed 

 - Removed `userId` from `Gesture` proto

## [1.1.1] - 02-01-2020

### Changed 

 - Changed `GestureConfig` to `GesturesConfig`
 - Removed `QuternionData` proto

## [1.1.0] - 02-01-2020

### Changed

- Documentation for the release process
- Sync versions on 1.1.0 for future

## [1.0.9] - 02-01-2020

### Changed

- Renamed ActivityEvent types to bare verbs

## [1.0.3]-[1.0.8] - 21-11-2019

- same as 1.0.2
- Moved Bitbucket Pipelines to CircleCI
- CI/CD tests

## [1.0.2] - 06-11-2019

### Added

- Bitbucket pipelines

## [1.0.1] - 31-10-2019

### Added

- missing meta file.

## [1.0.0] - 31-10-2019

### Added

- `ActivityEvent` and `Gesture` protobuf

### Changed

- Java's namespace of the protos
