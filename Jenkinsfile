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

	stage('Uruchomienie Kontenera') {
    	   steps {
            	script {
            		// Najpierw usuwamy stary kontener, jeśli istnieje, żeby nie było błędu nazwy
            		sh 'docker rm -f car-rental-test || true'
            		// Uruchamiamy aplikację na porcie 8081
            		sh 'docker run -d --name car-rental-test -p 8081:80 car-rental-app:latest'
            		echo 'Aplikacja powinna być dostępna pod http://localhost:8081'
        		}
    		}
	}
    stage('Budowanie i Uruchomienie (Compose)') {
    steps {
        sh 'docker-compose down' // Usuwa stare kontenery
        sh 'docker-compose up -d --build' // Buduje i uruchamia wszystko w tle
    }
}
stages {
        stage('Deploy with Compose') {
            steps {
                sh 'docker-compose down'
                sh 'docker-compose up -d --build'
            }
        }
        stage('Migrate Database') {
            steps {
                echo 'Czekam na start bazy...'
                sleep 20
                // Wykonanie migracji EF Core wewnątrz kontenera
                sh 'docker exec carrentalsystem-app-1 dotnet ef database update'
            }
        }
    }
}