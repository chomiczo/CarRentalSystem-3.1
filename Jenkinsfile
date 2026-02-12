pipeline {
    agent any
    environment {
        // Ustawiamy zmienną, żeby Compose wiedział gdzie szukać projektu
        COMPOSE_PROJECT_NAME = "carrental"
    }
    stages {
        stage('Czyszczenie') {
            steps {
                // Usuwamy stare kontenery, żeby nie było konfliktu nazw
                sh 'docker-compose down || true' 
                // Na wszelki wypadek usuwamy stary kontener testowy jeśli został
                sh 'docker rm -f car-rental-test || true'
            }
        }
        
        stage('Uruchomienie (Compose)') {
            steps {
                // To magiczna komenda: Buduje apkę I stawia bazę danych
                sh 'docker-compose up -d --build'
            }
        }

        stage('Migracja Bazy Danych') {
            steps {
                echo 'Czekam 20 sekund aż SQL Server wstanie...'
                sleep 20 
                // Wykonujemy komendę wewnątrz działającego kontenera "app"
                // Używamy "docker-compose exec", bo to bezpieczniejsze niż zgadywanie nazwy kontenera
                sh 'docker-compose exec -T app dotnet ef database update'
            }
        }
    }
}