# Microservice - Demo
## Docker
### Build
    docker build -t jesumarquez/platformservice .
    docker build -t jesumarquez/commandsservice .
### Run
    docker run -p 8080:80 -d jesumarquez/platformservice
### PS
    docker ps

---

## Kubernetes
### Apply
    kubectl apply -f platforms-depl.yaml
    kubectl apply -f commands-depl.yaml
    kubectl apply -f platforms-np-depl.yaml
    kubectl apply -f ingress-srv
    kubectl apply -f local-pvc.yaml
    kubectl apply -f mssql-plat-depl.yaml
### Secret
    kubectl create secret generic mssql --from-literal=SA_PASSWORD="YourPassword.01"