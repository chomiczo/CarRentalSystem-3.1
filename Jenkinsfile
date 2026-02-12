pipeline {
    agent any

    stages {
        stage('Pobranie Kodu') {
            steps {
                // Zakładam, że masz Gita. Jenkins sam pobierze kod.
                echo 'Pobieranie kodu...'
            }
        }

        stage('Budowanie Obrazu Docker') {
            steps {
                script {
                    // To polecenie zbuduje obraz Twojej apki CarRentalSystem
                    sh 'docker build -t car-rental-app:latest .'
                }
            }
        }
        
        stage('Test Uruchomienia') {
            steps {
                 echo 'Sprawdzam czy obraz istnieje...'
                 sh 'docker image inspect car-rental-app:latest'
            }
        }
    }
}