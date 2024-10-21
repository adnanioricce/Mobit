pipeline {
    agent any

    environment {
        DOTNET_ENVIRONMENT = 'Production'
    }

    stages {
        stage('Checkout') {
            steps {
                script {
                    checkout scm
                }
            }
        }

        stage('Setup Nix Environment') {
            steps {
                sh 'apt update && apt install -y xz-utils'
                sh 'curl -L https://nixos.org/nix/install | sh -s -- --daemon'
                sh 'nix-shell --run "echo Nix environment set up"'
            }
        }

        stage('Build .NET Solution') {
            steps {
                sh 'nix-shell --run "dotnet build"'
            }
        }

        stage('Run Unit Tests') {
            steps {
                sh 'nix-shell --run "dotnet test Mobit.Tests/Mobit.Tests.csproj"'
            }
        }
/*
        stage('Build Docker Images') {
            steps {
                sh 'nix-shell --run "docker-compose build"'
            }
        }

        stage('Run Integration Tests') {
            steps {
                sh 'nix-shell --run "dotnet test path/to/API.IntegrationTests"'
            }
        }

        stage('Run E2E Tests') {
            steps {
                sh 'nix-shell --run "dotnet test path/to/API.E2E.Tests"'
            }
        }
*/
        stage('Push Docker Images') {
            steps {
                sh 'nix-shell --run "docker-compose push"'
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed!'
        }
    }
}

