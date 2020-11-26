# Gripable BLE Unity

## Current version 

 - Current version: 4.1.0
 - BLE-Android: 1.3.1
 - Protos: 3.5.0
 
## Release and build process

 - Work on a feature/develop branch
 - Once you are done, bump `version` and `gitTagMsg` in `package.json`
 - Commit/create a PR into Develop 
 - When merge is done, create a PR into master. 
 - Once PR is merged into Master, the CI will build Unity and deploy into UPM branch, tagging will occur in CI. 

 - CircleCI should publish an npm package to our verdaccio server once completed

## Activating/Updating Unity License for CircleCI
 - Spin up the docker image from the ci-tools/unity repo with this command
 ```docker run -it --rm -v "$(pwd):/root/project" -w "/root/project" gripable/unity-environment:0.3.6-2018.3.11f1 bash```
 (change the tag ```0.3.6-2018.3.11f1``` part to the latest tag)
 - Run ```./active_license.sh``` inside docker image
 - Login with credentials
 - Copy the whole returned xml (from the ```<?xml...``` tag onwards) into a Unity_vX.alf file
 - Go to https://license.unity3d.com/manual to manually activate the license by uploading the above file
 - Download the resulting Unity_v2018.ulf file and encode to base64 ```base64 -i Unity_v2018.ulf```
 - Copy and paste the result into circleci lastpass Unity License
 - Copy and paste result into UNITY_LICENCE (spelt wrong) environment variable on circleci.com

