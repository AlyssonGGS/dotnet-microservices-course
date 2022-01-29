apply-k8s:
	kubectl apply -f K8S/platforms-depl.yaml
	kubectl apply -f K8S/commands-depl.yaml

delete-k8s:
	kubectl delete deployment platform-depl
	kubectl delete deployment command-depl
	kubectl delete service commands-clusterip-srv
	kubectl delete service platforms-clusterip-srv