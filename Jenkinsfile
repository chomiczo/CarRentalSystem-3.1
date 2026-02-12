pipeline {
    agent any
    environment {
        COMPOSE_PROJECT_NAME = "carrental"
    }
    stages {
        stage('Czyszczenie Środowiska') {
            steps {
                script {
                    sh 'docker-compose down || true' 
                    sh 'docker rm -f car-rental-test || true'
                }
            }
        }
        
        stage('Uruchomienie (Compose)') {
            steps {
                echo 'Budowanie obrazów i start kontenerów...'
                sh 'docker-compose up -d --build'
            }
        }

        stage('Oczekiwanie na Start') {
            steps {
                echo 'Czekam 30 sekund, aż aplikacja w C# sama wykona migracje...'
                sleep 30 
            }
        }
        
        stage('Weryfikacja') {
            steps {
                sh 'docker ps'
            }
        }
    }
}