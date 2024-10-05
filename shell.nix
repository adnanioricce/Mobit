{ pkgs ? import <nixpkgs> {} }:

pkgs.mkShell {
  name = "ci-cd-env";

  buildInputs = [
    pkgs.dotnet-sdk_6

    pkgs.nodejs_22_x

    pkgs.docker
    pkgs.docker-compose
  ];

  shellHook = ''
    echo "Development environment is ready for CI/CD pipeline!"

    # Start Docker daemon (required if running in a container or VM)
    if ! pgrep -x "dockerd" > /dev/null
    then
      sudo systemctl start docker
    fi
  '';
}

