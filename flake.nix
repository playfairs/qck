{
  description = "QCK; Quantum Cryptography Toolkit";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
        dotnet-sdk = pkgs.dotnet-sdk_8;
        dotnet-runtime = pkgs.dotnet-runtime_8;
      in
      {
        devShells.default = pkgs.mkShell {
          buildInputs = [
            dotnet-sdk
            dotnet-runtime
            pkgs.makeWrapper
          ];

          shellHook = ''
            export DOTNET_ROOT=${dotnet-sdk}
            export DOTNET_CLI_TELEMETRY_OPTOUT=1
          '';
        };
      }
    );
}
