{ lib, buildDotnetModule, dotnetCorePackages, ffmpeg, yt-dlp }:

buildDotnetModule rec {
  pname = "SteamByName";
  version = "0.1";

  src = ./.;

  projectFile = "./SteamByName.sln";
  dotnet-sdk = dotnetCorePackages.sdk_10_0;
  dotnet-runtime = dotnetCorePackages.sdk_10_0;
  dotnetFlags = [ "" ];
  executables = [ "SteamByName" ];
  runtimeDeps = [ ];
}